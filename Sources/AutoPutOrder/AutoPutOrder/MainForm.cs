using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace AutoPutOrder
{
    public partial class MainForm : Form
    {
        private bool _isAction = false;
        Thread _drawStatusThread;
        Thread _putOrderThread;
        public MainForm()
        {
            InitializeComponent();
        }
        private void DoAction()
        {
            if (_isAction)
            {
                _isAction = false;
                btOK.Text = "Bắt đầu";
                lbStatus.Visible = false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtBAcc.Text) || string.IsNullOrEmpty(txtDuration.Text) || string.IsNullOrEmpty(txtListSymbol.Text) || string.IsNullOrEmpty(txtSAcc.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _isAction = true;
                _drawStatusThread = new Thread(DrawingStatus);
                _drawStatusThread.Start();
                _putOrderThread = new Thread(PutOrder);
                _putOrderThread.Start();
                lbStatus.Visible = true;
                btOK.Text = "Ngưng";
            }
        }
        private void PutOrder()
        {
            if (!string.IsNullOrEmpty(txtListSymbol.Text))
            {
                try
                {
                    var duration = int.Parse(txtDuration.Text);
                    var arrSymbol = txtListSymbol.Text.Split(';');
                    SqlConnection con = new SqlConnection(Common.GetValueFromConfig("EtradeOrdersTest"));                    
                    Random rand = new Random();
                    List<StockCoreService.StockInfoCache> listStockCache = new List<StockCoreService.StockInfoCache>();
                    StockCoreService.StockCoreServicesSoapClient stockCoreService = new StockCoreService.StockCoreServicesSoapClient();
                    while (true)
                    {
                        if (!_isAction)
                        {
                            _putOrderThread.Abort();
                            return;
                        }
                        //random a symbol 
                        var index = rand.Next(0, arrSymbol.Length-1);
                        var symbol = arrSymbol[index];

                        //get symbol infomation
                        StockCoreService.StockInfoCache sInfo = (from x in listStockCache where x.Symbol == symbol select x).SingleOrDefault();
                        if (sInfo == null)
                        {
                            sInfo = stockCoreService.GetStockInfoCache(symbol);
                            if (sInfo != null)
                            {
                                listStockCache.Add(sInfo);
                            }
                        }
                        if (sInfo != null)//if stock info is not null to be continue
                        {
                            //random price
                            var price = rand.NextDouble()*(sInfo.Ceil-sInfo.Floor)+sInfo.Floor;

                            //insert buy order
                            var com = con.CreateCommand();
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "ExecOrder_Insert";
                            com.Parameters.AddWithValue("@RefOrderId","");
                            com.Parameters.AddWithValue("@MessageType","");
                            com.Parameters.AddWithValue("@FisOrderId",0);
                            com.Parameters.AddWithValue("@SecSymbol",symbol);
                            com.Parameters.AddWithValue("@Side","B");
                            com.Parameters.AddWithValue("@Price",price/1000);
                            com.Parameters.AddWithValue("@AvgPrice",DBNull.Value);
                            com.Parameters.AddWithValue("@ConPrice","");
                            com.Parameters.AddWithValue("@Volume",100);
                            com.Parameters.AddWithValue("@ExecutedVol",0);
                            com.Parameters.AddWithValue("@ExecutedPrice",DBNull.Value);
                            com.Parameters.AddWithValue("@CancelVolume",0);
                            com.Parameters.AddWithValue("@CancelledVolume",0);
                            com.Parameters.AddWithValue("@SubCustAccountId",txtBAcc.Text);
                            com.Parameters.AddWithValue("@ExecTransType",0);
                            com.Parameters.Add("@TradeTime", SqlDbType.DateTime).Value = DateTime.Now;
                            com.Parameters.Add("@MatchedTime", SqlDbType.DateTime).Value = DateTime.Now;
                            com.Parameters.Add("@CancelledTime", SqlDbType.DateTime).Value = DateTime.Now;
                            com.Parameters.AddWithValue("@OrderStatus",1);
                            com.Parameters.AddWithValue("@OrdRejReason",0);
                            com.Parameters.AddWithValue("@ConfirmNo","");
                            com.Parameters.AddWithValue("@CancelledConfirmNo","");
                            com.Parameters.AddWithValue("@SourceId",0);
                            com.Parameters.AddWithValue("@ExecType","");
                            com.Parameters.AddWithValue("@CancelledExecType","");
                            com.Parameters.AddWithValue("@PortOrClient","");
                            com.Parameters.AddWithValue("@Market","");
                            com.Parameters.AddWithValue("@MarketStatus","");
                            com.Parameters.AddWithValue("@OrderSource","");
                            com.Parameters.AddWithValue("@IsNewOrder",1);
                            com.Parameters.AddWithValue("@Sequence",0);
                            com.Parameters.AddWithValue("@NumOfMatch",0);
                            com.Parameters.AddWithValue("@QuickOrderId",1);
                            com.Parameters.AddWithValue("@ConditionOrderId",319);
                            com.Parameters.AddWithValue("@IsNewStatus",DBNull.Value);
                            com.Parameters.AddWithValue("@IsNewVolume",DBNull.Value);
                            com.Parameters.AddWithValue("@NewPrice",DBNull.Value);
                            com.Parameters.AddWithValue("@ChangedOrderStatus",0);
                            com.Parameters.AddWithValue("@Condition","");
                            com.Parameters.AddWithValue("@NewVolume",0);
                            com.Parameters.AddWithValue("@WaitMatchVol",0);
                            SqlParameter outputIdParam = new SqlParameter("@OrderId", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            com.Parameters.Add(outputIdParam);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            com.ExecuteNonQuery();
                            con.Close();

                            //inser sell order
                            com = con.CreateCommand();
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "ExecOrder_Insert";
                            com.Parameters.AddWithValue("@RefOrderId", "");
                            com.Parameters.AddWithValue("@MessageType", "");
                            com.Parameters.AddWithValue("@FisOrderId", 0);
                            com.Parameters.AddWithValue("@SecSymbol", symbol);
                            com.Parameters.AddWithValue("@Side", "S");
                            com.Parameters.AddWithValue("@Price", price / 1000);
                            com.Parameters.AddWithValue("@AvgPrice", DBNull.Value);
                            com.Parameters.AddWithValue("@ConPrice", "");
                            com.Parameters.AddWithValue("@Volume", 100);
                            com.Parameters.AddWithValue("@ExecutedVol", 0);
                            com.Parameters.AddWithValue("@ExecutedPrice", DBNull.Value);
                            com.Parameters.AddWithValue("@CancelVolume", 0);
                            com.Parameters.AddWithValue("@CancelledVolume", 0);
                            com.Parameters.AddWithValue("@SubCustAccountId", txtSAcc.Text);
                            com.Parameters.AddWithValue("@ExecTransType", 0);
                            com.Parameters.Add("@TradeTime",SqlDbType.DateTime).Value = DateTime.Now;
                            com.Parameters.Add("@MatchedTime",SqlDbType.DateTime).Value = DateTime.Now;
                            com.Parameters.Add("@CancelledTime", SqlDbType.DateTime).Value = DateTime.Now;
                            com.Parameters.AddWithValue("@OrderStatus", 1);
                            com.Parameters.AddWithValue("@OrdRejReason", 0);
                            com.Parameters.AddWithValue("@ConfirmNo", "");
                            com.Parameters.AddWithValue("@CancelledConfirmNo", "");
                            com.Parameters.AddWithValue("@SourceId", 0);
                            com.Parameters.AddWithValue("@ExecType", "");
                            com.Parameters.AddWithValue("@CancelledExecType", "");
                            com.Parameters.AddWithValue("@PortOrClient", "");
                            com.Parameters.AddWithValue("@Market", "");
                            com.Parameters.AddWithValue("@MarketStatus", "");
                            com.Parameters.AddWithValue("@OrderSource", "");
                            com.Parameters.AddWithValue("@IsNewOrder", 1);
                            com.Parameters.AddWithValue("@Sequence", 0);
                            com.Parameters.AddWithValue("@NumOfMatch", 0);
                            com.Parameters.AddWithValue("@QuickOrderId", 1);
                            com.Parameters.AddWithValue("@ConditionOrderId", 319);
                            com.Parameters.AddWithValue("@IsNewStatus", DBNull.Value);
                            com.Parameters.AddWithValue("@IsNewVolume", DBNull.Value);
                            com.Parameters.AddWithValue("@NewPrice", DBNull.Value);
                            com.Parameters.AddWithValue("@ChangedOrderStatus", 0);
                            com.Parameters.AddWithValue("@Condition", "");
                            com.Parameters.AddWithValue("@NewVolume", 0);
                            com.Parameters.AddWithValue("@WaitMatchVol", 0);
                            outputIdParam = new SqlParameter("@OrderId", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            com.Parameters.Add(outputIdParam);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            com.ExecuteNonQuery();
                            con.Close();
                        }

                        Thread.Sleep(duration);//sleep a duration
                    }
                }
                catch(Exception ex)
                {
                    _isAction = false;
                    MessageBox.Show("Có lỗi xảy ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void DrawingStatus()
        {
            pnStatus.Invoke(new MethodInvoker(delegate
            {
                pnStatus.Visible = true;
            }));
            Graphics g = pnStatus.CreateGraphics();
            int curV1 = 0;
            int curV2 = 20;
            int curV3 = 40;
            int curV4 = 40;
            int i = 0;
            int j = 1;
            int k = 2;
            int l = 3;
            int width = pnStatus.Width;
            int h = pnStatus.Height / 2;
            while (true)
            {                
                if (!_isAction)
                {
                    pnStatus.Invoke(new MethodInvoker(delegate
                    {
                        g.Clear(pnStatus.BackColor);
                        pnStatus.Visible = false;
                        _drawStatusThread.Abort();
                    }));                    
                }
                //move object

                if (curV1 >= width-10)
                {
                    
                    i = 0;
                    j = 1;  
                    k = 2;
                    l = 4;

                    curV1 = 0;
                    curV2 = 10;
                    curV3 = 20;
                    curV4 = 40;
                                      
                }
                else
                {
                    curV1 += i;
                    if (curV1 < width/2)
                        i++;
                    else
                        i--;
                }
                if (curV2 >= width-10)
                {
                    i = 0;
                    j = 1;
                    k = 2;
                    l = 4;

                    curV1 = 0;
                    curV2 = 10;
                    curV3 = 20;
                    curV4 = 40;
                }
                else
                {
                    curV2 += j;
                    if(curV2<width/2)
                        j++;
                    else
                        j--;
                }
                if (curV3 >= width-10)
                {
                    i = 0;
                    j = 1;
                    k = 2;
                    l = 4;

                    curV1 = 0;
                    curV2 = 10;
                    curV3 = 20;
                    curV4 = 40;
                }
                else
                {
                    curV3 += k;
                    if (curV3 <width/2)
                        k++;
                    else
                        k--;

                }
                if (curV4 >= width - 10)
                {
                    i = 0;
                    j = 1;
                    k = 2;
                    l = 4;

                    curV1 = 0;
                    curV2 = 10;
                    curV3 = 20;
                    curV4 = 40;
                }
                else
                {
                    curV4 += l;
                    if (curV4 < width / 2)
                        l++;
                    else
                        l--;

                }
                
                pnStatus.Invoke(new MethodInvoker(delegate
                {
                    g.Clear(pnStatus.BackColor);
                    Pen pen = new Pen(Color.White, h);
                    g.DrawLine(pen, curV1, h,curV1+ h, h);
                    g.DrawLine(pen, curV2, h, curV2 + h, h);
                    g.DrawLine(pen, curV3, h, curV3 + h, h);
                    g.DrawLine(pen, curV4, h, curV4 + h, h);
                    
                }));               
                Thread.Sleep(80);
            }
           
        }
        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            DoAction();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pnStatus.Visible = false;
            lbStatus.Visible = false;
            pnStatus.Width = this.Width;
            pnStatus.Height = 8;
            pnStatus.Left = 0;
            //load default data for controls
            txtListSymbol.Text = Common.GetValueFromConfig("ListSymbol");
            txtDuration.Text = Common.GetValueFromConfig("Duration");
            txtBAcc.Text = Common.GetValueFromConfig("BuyAcc");
            txtSAcc.Text = Common.GetValueFromConfig("SellAcc");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
