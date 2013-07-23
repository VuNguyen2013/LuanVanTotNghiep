// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarketStatus.cs" company="OTS">
//   2011
// </copyright>
// <summary>
//   Defines the Portfolio type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETradeCore.Entities
{
    public class MarketStatusInfo
    {
 
        /// <summary>
        /// Gets or sets MarketID.
        /// O: HOSE, N: HNX, C: Upcom
        /// </summary>
        /// <value>The symbol.</value>
        public System.String  MarketID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// HOSE:
        /// P: session 1, O: session 2, C: session 3, K: PT, J: Close.
        /// </summary>
        /// <value>The type of the sec. P, O, C, K, J, G</value>
        /// 
        public System.String  Status { get; set; }


              /// <summary>
        /// Gets or sets the SET time. 
        /// </summary>
        /// <value>The time 5/17/2011 8:30:00 AM.</value>
        public System.DateTime  MessageTime { get; set; }

        /// <summary>
        /// Gets or sets the receive time. 
        /// </summary>
        /// <value>The time 5/17/2011 8:30:00 AM.</value>
        public System.DateTime ReceiveTime { get; set; }



    }
}
