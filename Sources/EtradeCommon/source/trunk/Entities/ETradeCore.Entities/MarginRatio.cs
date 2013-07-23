// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarginRatioInfo.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the Margin Ratio Info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
namespace ETradeCore.Entities
{
    public class MarginRatioInfo
    {
        /// <summary>
        /// Gets or sets the account no from SBA.
        /// </summary>
        /// <value>The account no from SBA.</value>
        public String accountNo { get; set; }

        /// <summary>
        /// Gets or sets the BUY_CR 50 %
        /// </summary>
        /// <value>The BUY_CR 50 %.</value>
        public Decimal BuyCR50Percent { get; set; }

        /// <summary>
        /// Gets or sets the BUY_CR 60 %
        /// </summary>
        /// <value>The BUY_CR 60 %.</value>
        public Decimal BuyCR60Percent { get; set; }

        /// <summary>
        /// Gets or sets the BUY_CR 70 %
        /// </summary>
        /// <value>The BUY_CR 70 %.</value>
        public Decimal BuyCR70Percent { get; set; }

        /// <summary>
        /// Gets or sets the ASSETS.
        /// </summary>
        /// <value>The ASSETS.</value>
        public Decimal Assets { get; set; }

        /// <summary>
        /// Gets or sets the MR.
        /// </summary>
        /// <value>The MR.</value>
        public Decimal MR { get; set; }

        /// <summary>
        /// Gets or sets the call_ force sell.
        /// </summary>
        /// <value>The call_ force sell.</value>
        public Decimal CallForceSell { get; set; }

        /// <summary>
        /// Gets or sets the liabilities.
        /// </summary>
        /// <value>The liabilities.</value>
        public Decimal Liabilities { get; set; }

        /// <summary>
        /// Gets or sets the buy MR.
        /// </summary>
        /// <value>The buy MR.</value>
        public Decimal Buy_MR { get; set; }

        /// <summary>
        /// Gets or sets the shortage force.
        /// </summary>
        /// <value>The shortage force.</value>
        public Decimal ShortageForce { get; set; }

        /// <summary>
        /// Gets or sets the equity.
        /// </summary>
        /// <value>The equity.</value>
        public Decimal Equity { get; set; }

        /// <summary>
        /// Gets or sets the sell_ MR.
        /// </summary>
        /// <value>The sell_ MR.</value>
        public Decimal Sell_MR { get; set; }

        /// <summary>
        /// Gets or sets the call_ LMV.
        /// </summary>
        /// <value>The call_ LMV.</value>
        public Decimal Call_LMV { get; set; }

        /// <summary>
        /// Gets or sets the cash_ BAL.
        /// </summary>
        /// <value>The cash_ BAL.</value>
        public Decimal Cash_BAL { get; set; }

        /// <summary>
        /// Gets or sets the EE.
        /// </summary>
        /// <value>The EE.</value>
        public Decimal EE { get; set; }

        /// <summary>
        /// Gets or sets the call_ SMV.
        /// </summary>
        /// <value>The call_ SMV.</value>
        public Decimal Call_SMV { get; set; }

        /// <summary>
        /// Gets or sets the LMV.
        /// </summary>
        /// <value>The LMV.</value>
        public Decimal LMV { get; set; }

        /// <summary>
        /// Gets or sets the PP.
        /// </summary>
        /// <value>The PP.</value>
        public Decimal PP { get; set; }

        /// <summary>
        /// Gets or sets the force_ LMV.
        /// </summary>
        /// <value>The force_ LMV.</value>
        public Decimal Force_LMV { get; set; }

        /// <summary>
        /// Gets or sets the collateral.
        /// </summary>
        /// <value>The collateral.</value>
        public Decimal Collateral { get; set; }

        /// <summary>
        /// Gets or sets the call margin.
        /// </summary>
        /// <value>The call margin.</value>
        public Decimal CallMargin { get; set; }

        /// <summary>
        /// Gets or sets the force SMV.
        /// </summary>
        /// <value>The force SMV.</value>
        public Decimal Force_SMV { get; set; }

        /// <summary>
        /// Gets or sets the debt.
        /// </summary>
        /// <value>The debt.</value>
        public Decimal Debt { get; set; }

        /// <summary>
        /// Gets or sets the shortage call.
        /// </summary>
        /// <value>The shortage call.</value>
        public Decimal ShortageCall { get; set; }

        /// <summary>
        /// Gets or sets the margin ratio.
        /// </summary>
        /// <value>The margin ratio.</value>
        public Decimal MarginRatio { get; set; }

        /// <summary>
        /// Gets or sets the SMV.
        /// </summary>
        /// <value>The SMV.</value>
        public Decimal SMV { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public String Action { get; set; }

        /// <summary>
        /// Gets or sets the with drawal.
        /// </summary>
        /// <value>The with drawal.</value>
        public Decimal WithDrawal { get; set; }

        /// <summary>
        /// Gets or sets the AR.
        /// </summary>
        /// <value>The AR.</value>
        public Decimal AR { get; set; }

        /// <summary>
        /// Gets or sets the A r_ t1.
        /// </summary>
        /// <value>The A r_ t1.</value>
        public Decimal AR_T1 { get; set; }

        /// <summary>
        /// Gets or sets the A r_ t2.
        /// </summary>
        /// <value>The A r_ t2.</value>
        public Decimal AR_T2 { get; set; }

        /// <summary>
        /// Gets or sets the AP.
        /// </summary>
        /// <value>The AP.</value>
        public Decimal AP { get; set; }

        /// <summary>
        /// Gets or sets the A p_ t1.
        /// </summary>
        /// <value>The A p_ t1.</value>
        public Decimal AP_T1 { get; set; }

        /// <summary>
        /// Gets or sets the A p_ t2.
        /// </summary>
        /// <value>The A p_ t2.</value>
        public Decimal AP_T2 { get; set; }

        /// <summary>
        /// Gets or sets the buy unmatch.
        /// </summary>
        /// <value>The buy unmatch.</value>
        public Decimal BuyUnmatch { get; set; }

        /// <summary>
        /// Gets or sets the sell unmatch.
        /// </summary>
        /// <value>The sell unmatch.</value>
        public Decimal SellUnmatch { get; set; }

        /// <summary>
        /// Gets or sets the MT m_ EE.
        /// </summary>
        /// <value>The MT m_ EE.</value>
        public Decimal MTM_EE { get; set; }

        /// <summary>
        /// Gets or sets the cal force.
        /// </summary>
        /// <value>The cal force.</value>
        public Decimal CalForce { get; set; }
        
    }
}