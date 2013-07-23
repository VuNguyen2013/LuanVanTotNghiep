using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradeWebServices.Entities
{
    public class MNews
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string ArticleModifiedDate{get;set;}
    }
}