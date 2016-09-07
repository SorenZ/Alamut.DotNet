using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Utilities.Http;
using Alamut.Utilities.Localization;
using AutoMapper;

namespace Alamut.Service
{
    public class FullService<TDocument> : IFullService<TDocument>
        where TDocument : IEntity
    {
        private readonly CrudService<TDocument> _crudService;
        private readonly HistoryService<TDocument> _historyService;
        private readonly ILocalizationService _localizationService;
        private readonly bool _historySupported;

        /// <summary>
        /// create full service with history supported
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userResolverService"></param>
        /// <param name="mapper"></param>
        /// <param name="historyRepository"></param>
        /// <param name="localizationService"></param>
        public FullService(IRepository<TDocument> repository,
            IMapper mapper,
            IHistoryRepository historyRepository = null,
            UserResolverService userResolverService = null,
            ILocalizationService localizationService = null)
        {
            _localizationService = localizationService;
            Repository = repository;
            Mapper = mapper;

            

            if (historyRepository != null && userResolverService != null)
            {
                _historySupported = true;
                _historyService = new HistoryService<TDocument>(repository, historyRepository, mapper, userResolverService);
            }
            else
            {
                _crudService = new CrudService<TDocument>(repository, mapper);
            }

        }

        #region IService

        public IQueryRepository<TDocument> ReadOnly => this.Repository;

        protected IRepository<TDocument> Repository { get; }

        #endregion

        #region ICrudService

        protected IMapper Mapper { get; }

        public ServiceResult<string> Create<TModel>(TModel model)
        {
            if (_localizationService != null && model is IMultiLanguageEnity)
                (model as IMultiLanguageEnity).Lang = _localizationService.CurrenttLanguage;

            return _historySupported
                ? _historyService.Create(model)
                : _crudService.Create(model);
        }

        public ServiceResult Update<TModel>(string id, TModel model)
        {
            return _historySupported
                ? _historyService.Update(id, model)
                : _crudService.Update(id, model);
        }

        public ServiceResult UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            return _historyService.UpdateOne(id, memberExpression, value);
        }

        public ServiceResult Delete(string id)
        {
            return _historySupported
                ? _historyService.Delete(id)
                : _crudService.Delete(id);
        }

        public TResult Get<TResult>(string id)
        {
            return _historyService.Get<TResult>(id);
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids)
        {
            return _historyService.GetMany<TResult>(ids);
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate)
        {
            return _historyService.GetMany<TResult>(predicate);
        }

        #endregion

        #region IHistoryService

        public TModel GetHistoryValue<TModel>(string historyId) where TModel : class
        {
            return _historyService.GetHistoryValue<TModel>(historyId);
        }

        public dynamic GetHistoryValue(string historyId)
        {
            return _historyService.GetHistoryValue(historyId);
        }

        public List<BaseHistory> GetHistories<TModel>(string entityId)
        {
            return _historyService.GetHistories<TModel>(entityId);
        }

        public List<BaseHistory> GetHistories(string entityId)
        {
            return _historyService.GetHistories(entityId);
        }

        #endregion

    }
}
