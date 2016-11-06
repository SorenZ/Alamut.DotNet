using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alamut.Sample.DataDriven.Dto
{
    public class ArticleSimple
    {
        public string Id { get; set; }
        public bool IsPublished { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedDate { get; set; }
        public string UserId { get; set; }
    }
}
