using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alamut.Data.Entity;

namespace Alamut.Sample.DataDriven.Models
{
    public class Article : IEntity
        , IUserEntity
        , ICodeEntity
        , IPublishableEntity

    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public string Code { get; set; }


        public bool IsPublished { get; set; }
        public string Title { get; set; }
        public string InternalCode { get; set; }
        public string Basename { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }

        // Single Relational -> OfficeArticleCategory
        public string CategoryId { get; set; }

        // SingleFile
        public string SmallPicId { get; set; }
        // SingleFile
        public string LargePicId { get; set; }

        // Multi Relational -> OfficeArticle
        public ICollection<string> RelatedArticleIds { get; set; }

        public ICollection<string> Tags { get; set; }

    }
}
