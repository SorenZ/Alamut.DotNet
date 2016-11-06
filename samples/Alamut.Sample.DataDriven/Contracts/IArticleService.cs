using System;
using System.Collections.Generic;
using Alamut.Data.Paging;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Sample.DataDriven.Dto;
using Alamut.Sample.DataDriven.Models;
using Alamut.Sample.DataDriven.ViewModels;

namespace Alamut.Sample.DataDriven.Contracts
{
    public interface IArticleService : ICrudService<Article>
    {
        ServiceResult<string> Create(ArticleCreateVm model);
        List<IdTitle> Search(string text);

        /// <summary>
        /// - sort by `PublishedDate DESC`, 
        /// - search `Title, Summary, Body` by `q`
        /// </summary>
        IPaginated<ArticleSimple> GetArticleSimplePaginated(string text, PaginatedCriteria criteria);
        
    }
}