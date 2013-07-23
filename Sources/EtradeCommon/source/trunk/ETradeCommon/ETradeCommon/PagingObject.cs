// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagingObject.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the PagingObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCommon
{
    public class PagingObject<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingObject&lt;T&gt;"/> class.
        /// </summary>
        public PagingObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public PagingObject(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        public PagingObject(System.Int32 count)
        {
            this.Count = count;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="count">The count.</param>
        public PagingObject(T data, System.Int32 count)
        {
            this.Data = data;
            this.Count = count;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public System.Int32 Count { get; set; }

        /// <summary>
        /// the status of page is new or just updated.
        /// </summary>
        public System.Boolean isNew { get; set; }
    }

    /// <summary>
    /// PagingObject with summary in page
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class PagingObject<T, R, U>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data1.</value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the sum in page.
        /// </summary>
        /// <value>The sum in page.</value>
        public R SumInPage { get; set; }

        /// <summary>
        /// Gets or sets the total sum.
        /// </summary>
        /// <value>The total sum.</value>
        public U TotalSum { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public System.Int32 Count { get; set; }

        /// <summary>
        /// the status of page is new or just updated.
        /// </summary>
        public System.Boolean isNew { get; set; }
    }
}