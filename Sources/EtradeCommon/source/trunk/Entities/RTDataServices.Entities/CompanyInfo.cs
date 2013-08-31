namespace RTDataServices.Entities
{
    using System.Collections.Generic;

    public class CompanyInfo
    {
        public virtual System.Int16 MarketId { get; set; }
        public virtual System.String Code { get; set; }
        public virtual System.String FullName { get; set; }
        public virtual System.Int32 CompanyId { get; set; }
        public virtual System.String LanguageId { get; set; }

        public CompanyInfo()
        {}

        public CompanyInfo(System.Int16 MarketId, System.String Code, System.String FullName)
        {
            this.MarketId = MarketId;
            this.Code       = Code;
            this.FullName   = FullName;
        }
    }
}