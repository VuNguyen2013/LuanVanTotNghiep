using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccountManager.DataAccess;
using AccountManager.DataAccess.SqlClient;
using AccountManager.Entities;
using ETradeCommon.Enums;
using ETradeCore.Entities;
using ETradeCore.Services;

namespace MigrateDataTCSC
{
    public partial class frmMain : Form
    {
        ISbaCoreProvider coreProvider = new SqlInformixProvider();
        MigrateDBDataContext migrateDbDataContext=new MigrateDBDataContext();
        InnoDBDataContext innoDbDataContext=new InnoDBDataContext();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           // MessageBox.Show(Utils.Common.Decrypt("yzDct3GqHH24e7glaQaIiQ=="));
          //  MessageBox.Show(Utils.Common.Encryt("16092008"));
        }

        #region Clik
        private void btnMigrateMainCustAccount_Click(object sender, EventArgs e)
        {
            List<AccountInfoFromInno> listAccountInfoUnknowFromInnoDB = new List<AccountInfoFromInno>();
            List<AccountInfoFromInno> listAccountInfoAddedFromInnoDB = new List<AccountInfoFromInno>();
            List<BrokerAccount> listBrokerAccount = migrateDbDataContext.BrokerAccounts.ToList();
           
            try
            {
                List<AccountInfoFromInno> listAccountInfoFromInnoDB = innoDbDataContext.AccountInfoFromInnos.ToList();
                if(listAccountInfoFromInnoDB!=null)
                {
                    foreach (var accountInfo in listAccountInfoFromInnoDB)
                    {                        
                        string accountID = accountInfo.AccountID.ToUpper();
                        //is valid account id
                        if(!string.IsNullOrEmpty(accountID) && (accountID.Contains("085C") || accountID.Contains("085F")))
                        {
                            
                            List<CoreAccountInfo> coreAccountInfos = coreProvider.GetCustInfoFromCore(accountID);
                            //existing in sba
                            if(coreAccountInfos!=null && coreAccountInfos.Count>0)
                            {
                                MainCustAccount mainCustAccount =
                                    migrateDbDataContext.MainCustAccounts.Where(
                                        account => account.MainCustAccountID.Equals(accountID)).
                                        FirstOrDefault();

                                if(mainCustAccount==null)
                                {
                                   
                                    mainCustAccount=new MainCustAccount();
                                    mainCustAccount.MainCustAccountID = accountID;
                                    mainCustAccount.FullName = accountInfo.Name;
                                    mainCustAccount.Email = coreAccountInfos[0].Email.Length<=30?coreAccountInfos[0].Email:string.Empty;
                                    mainCustAccount.Phone =  coreAccountInfos[0].Phone.Length<=20?coreAccountInfos[0].Phone:string.Empty;
                                    mainCustAccount.Actived = accountInfo.Actived.Value;
                                    mainCustAccount.Password = accountInfo.Password;
                                    mainCustAccount.PIN = accountInfo.OrderPin;
                                    mainCustAccount.PassLockReason =(int)ETradeCommon.Enums.CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                                    mainCustAccount.PINLockReason = (int)ETradeCommon.Enums.CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                                    mainCustAccount.LockReason = (int)ETradeCommon.Enums.CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                                    //mainCustAccount.TokenID
                                    //mainCustAccount.TokenName
                                    //mainCustAccount.TokenActived

                                    BrokerAccount brokerAccount =
                                        listBrokerAccount.Where(broker => broker.BrokerID.Equals(accountInfo.BrokerID)).
                                            FirstOrDefault();
                                    if(brokerAccount!=null)
                                    {
                                        mainCustAccount.BrokerID = brokerAccount.BrokerID;
                                    }
                                    else
                                    {
                                        mainCustAccount.BrokerID = "admin";
                                    }

                                    //mainCustAccount.PassIsNew
                                    //mainCustAccount.PINIsNew
                                    //mainCustAccount.PassExpDate
                                    //mainCustAccount.PINExpDate
                                    if(accountID.Contains("085C"))
                                        mainCustAccount.CustomerType=(int)ETradeCommon.Enums.CommonEnums.CUSTOMER_TYPE.INTERNAL;
                                    else
                                    {
                                        if(accountID.Contains("085F"))
                                            mainCustAccount.CustomerType = (int)ETradeCommon.Enums.CommonEnums.CUSTOMER_TYPE.FOREIGN;
                                    }

                                    mainCustAccount.AuthType =(int) ETradeCommon.Enums.CommonEnums.AUTHENTICATION_TYPE.PIN_PASS;
                                    mainCustAccount.PinType = (int)ETradeCommon.Enums.CommonEnums.AUTHENTICATION_TYPE.PIN_PASS;
                                    //mainCustAccount.FailedLoginCount
                                    //mainCustAccount.FailedLoginTime
                                    mainCustAccount.CreatedDate = DateTime.Now;
                                    mainCustAccount.CreatedUser = mainCustAccount.BrokerID;

                                    migrateDbDataContext.MainCustAccounts.InsertOnSubmit(mainCustAccount);

                                    foreach (var coreAccountInfo in coreAccountInfos)
                                    {
                                        SubCustAccount subCustAccount = new SubCustAccount();
                                        subCustAccount.SubCustAccountID = coreAccountInfo.SubAccount;
                                        subCustAccount.Name = mainCustAccount.FullName;
                                        subCustAccount.Actived = true;
                                        subCustAccount.LockAccountReason = (int)ETradeCommon.Enums.CommonEnums.LOCK_ACCOUNT_REASON.NOTHING;
                                        subCustAccount.MainCustAccountID = mainCustAccount.MainCustAccountID;
                                        subCustAccount.CreatedDate = DateTime.Now;
                                        subCustAccount.CreatedUser = mainCustAccount.BrokerID;

                                        migrateDbDataContext.SubCustAccounts.InsertOnSubmit(subCustAccount);

                                        List<SubCustAccountPermission> listSubCustAccountPermission = new List<SubCustAccountPermission>();

                                        if (accountInfo.TradeActived.Value)
                                        {
                                            listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CAN_TRADE });
                                        }

                                        if (coreAccountInfo.CanBuy)
                                        {
                                            listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CAN_BUY });
                                        }

                                        if (coreAccountInfo.CanSell)
                                        {
                                            listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CAN_SELL });
                                        }

                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.ODD_SLOT_EXCHANGE });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.VIEW_RESEARCH_ANALYZE });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.VIEW_ORDER_STATUS });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.VIEW_STATMENT });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.VIEW_BALANCE });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.QUICK_ORDER });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CONDITION_ORDER });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.VIEW_INFORMATION_ACCOUNT });
                                        listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.PRICE_TO_BUY });

                                        BankServices bankServices = new BankServices();
                                        BankAccountInfo bankAccountInfo = bankServices.GetBankAccountInfo(coreAccountInfo.SubAccount);
                                        if (bankAccountInfo != null)
                                        {
                                            if (bankAccountInfo.BankAccountType == (int)ETradeCommon.Enums.CommonEnums.BANK_ACCOUNT_TYPE.BANKACC)
                                            {
                                                if (ETradeCommon.Utils.GetAccountType(coreAccountInfo.SubAccount) ==
                                                    (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                                                {
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CASH_ADVANCE });
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CASH_TRANSFER });
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.STOCK_TRANSFER });
                                                }
                                                else
                                                {
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CASH_ADVANCE });
                                                }
                                            }
                                            else
                                            {
                                                if (ETradeCommon.Utils.GetAccountType(coreAccountInfo.SubAccount) ==
                                                    (int)CommonEnums.ACCOUNT_TYPE.MARGIN)
                                                {
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CASH_TRANSFER });
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.STOCK_TRANSFER });
                                                }
                                                else
                                                {
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CASH_ADVANCE });
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.CASH_TRANSFER });
                                                    listSubCustAccountPermission.Add(new SubCustAccountPermission() { SubCustAccountID = coreAccountInfo.SubAccount, CustServicesPermissionID = (int)ETradeCommon.Enums.CommonEnums.SUB_ACCOUNT_PERMISSIONS.STOCK_TRANSFER });
                                                }
                                            }

                                        }
                                        migrateDbDataContext.SubCustAccountPermissions.InsertAllOnSubmit(listSubCustAccountPermission);
                                    }
                                }
                                
                                

                                migrateDbDataContext.SubmitChanges();       
                                listAccountInfoAddedFromInnoDB.Add(accountInfo);
                            }
                            else
                            {
                                listAccountInfoUnknowFromInnoDB.Add(accountInfo);
                            }
                        }
                        else
                        {
                            listAccountInfoUnknowFromInnoDB.Add(accountInfo);
                        }
                        
                    }
                }
                MessageBox.Show("Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.DataSource = listAccountInfoUnknowFromInnoDB;
            textBox1.Text = listAccountInfoUnknowFromInnoDB.Count.ToString();
            dataGridView2.DataSource = listAccountInfoAddedFromInnoDB;
            textBox2.Text = listAccountInfoAddedFromInnoDB.Count.ToString();
        }
        #endregion
        
    }
}
