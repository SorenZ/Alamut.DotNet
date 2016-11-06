using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Alamut.Sample.DataDriven.ViewModels
{
    public class ArticleCreateVm
    {
        [Required]
        public string Title { get; set; }

        // AUTO FILL properties
        public ICollection<string> RelatedArticleIds { get; set; } = new Collection<string>();
        public ICollection<string> Tags { get; set; } = new Collection<string>();
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        // fill-in business service
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
