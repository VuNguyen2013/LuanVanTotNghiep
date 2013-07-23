// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewestWorkingDatesInfo.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the NewestWorkingDatesInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RTDataServices.Entities
{
    public class NewestWorkingDatesInfo
    {
        /// <summary>
        /// Gets or sets the market id.
        /// </summary>
        /// <value>The market id.</value>
        public virtual System.Int16 MarketId { get; set; }

        /// <summary>
        /// Gets or sets T.
        /// </summary>
        public virtual System.DateTime T { get; set; }

        /// <summary>
        /// Gets or sets T1.
        /// </summary>
        public virtual System.DateTime T1 { get; set; }

        /// <summary>
        /// Gets or sets T2.
        /// </summary>
        public virtual System.DateTime T2 { get; set; }

        /// <summary>
        /// Gets or sets T3.
        /// </summary>
        public virtual System.DateTime T3 { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="NewestWorkingDatesInfo"/> class.
        /// </summary>
        public NewestWorkingDatesInfo()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="NewestWorkingDatesInfo"/> class.
        /// </summary>
        /// <param name="MarketId">
        /// The market id.
        /// </param>
        /// <param name="T">
        /// The t.
        /// </param>
        /// <param name="T1">
        /// The t 1.
        /// </param>
        /// <param name="T2">
        /// The t 2.
        /// </param>
        /// <param name="T3">
        /// The t 3.
        /// </param>
        public NewestWorkingDatesInfo(System.Int16 MarketId, System.DateTime T, System.DateTime T1, System.DateTime T2,
                          System.DateTime T3)
        {
            this.MarketId   = MarketId;
            this.T       	= T;
            this.T1      	= T1;
            this.T2   		= T2;
            this.T3       	= T3;
        }
    }
}