using System.Collections.Generic;
using System.Linq;
using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Data.Structure;
using Alamut.Helpers.Linq;
using Alamut.Sample.DataDriven.Contracts;
using Alamut.Sample.DataDriven.Dto;
using Alamut.Sample.DataDriven.Models;
using Alamut.Sample.DataDriven.ViewModels;
using Alamut.Service;
using Alamut.Utilities.Http;
using Alamut.Utilities.Security;
using AutoMapper.QueryableExtensions;

namespace Alamut.Sample.DataDriven.Services
{
    public class ArticleService : CrudService<Article>,
        IArticleService
    {
        private readonly IArticleRepo _articleRepo;
        private readonly UserResolverService _userResolverService;

        public ArticleService(IArticleRepo articleRepo,
            UserResolverService userResolverService) : base(articleRepo)
        {
            _articleRepo = articleRepo;
            _userResolverService = userResolverService;
        }

        public ServiceResult<string> Create(ArticleCreateVm model)
        {
            model.Code = UniqueKeyGenerator.GenerateByTime();
            model.UserId = _userResolverService.GetUserId();

            return base.Create(model);
        }

        public List<IdTitle> Search(string text)
        {
            return this.Repository.Queryable
                .WhereIf(!string.IsNullOrWhiteSpace(text), q => q.Title.Contains(text))
                .ProjectTo<IdTitle>()
                .ToList();
        }

        public IPaginated<ArticleSimple> GetArticleSimplePaginated(string text, PaginatedCriteria criteria)
        {
            return this.Repository.Queryable
                .WhereIf(!string.IsNullOrWhiteSpace(text), 
                    q => q.Title.Contains(text) || q.Summary.Contains(text) || q.Body.Contains(text))
                .OrderByDescending(o => o.PublishedDate)
                .ProjectTo<ArticleSimple>()
                .ToPaginated(criteria);
        }

        public List<IdTitle> GetIdTitleByIds(IEnumerable<string> ids)
        {
            return this.Repository
                .GetMany(ids, s => new IdTitle
                {
                    Id = s.Id,
                    Title = s.Title
                });
        }

        
    }
}