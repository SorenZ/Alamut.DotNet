using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Utilities.Http;
using AutoMapper;

namespace Alamut.Service
{
    public class FullService<TDocument> : 
        IService<TDocument>,
        ICrudService<TDocument>,
        IHistoryService<TDocument> 
        where TDocument : IEntity
    {
        private readonly CrudService<TDocument> _crudService;
        private readonly HistoryService<TDocument> _historyService;
        private readonly bool _historySupported;


        /// <summary>
        /// create full service with history supported
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userResolverService"></param>
        /// <param name="mapper"></param>
        /// <param name="historyRepository"></param>
        public FullService(IRepository<TDocument> repository,
            IMapper mapper,
            UserResolverService userResolverService = null,
            IHistoryRepository historyRepository = null)
        {
            Repository = repository;
            Mapper = mapper;
            _crudService = new CrudService<TDocument>(repository, mapper);

            if (historyRepository != null)
            {
                _historySupported = true;
                _historyService = new HistoryService<TDocument>(historyRepository, _crudService, userResolverService);
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
            return _historySupported 
                ? _historyService.Create(model) 
                : _crudService.Create(model);
        }

        public ServiceResult Update<TModel>(string id, TModel model)
        {
            return _historySupported 
                ? _historyService.Update(id, model) 
                :_crudService.Update(id, model);
        }

        public ServiceResult UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            return _crudService.UpdateOne(id, memberExpression, value);
        }

        public ServiceResult Delete(string id)
        {
            return _historySupported 
                ? _historyService.Delete(id)
                : _crudService.Delete(id);
        }

        public TResult Get<TResult>(string id)
        {
            return _crudService.Get<TResult>(id);
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids)
        {
            return _crudService.GetMany<TResult>(ids);
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate)
        {
            return _crudService.GetMany<TResult>(predicate);
        }

        #endregion

        #region IHistoryService

        ServiceResult<string> IHistoryService<TDocument>.Create<TModel>(TModel model)
        {
            return _historyService.Create(model);
        }

        ServiceResult IHistoryService<TDocument>.Update<TModel>(string id, TModel model)
        {
            return _historyService.Update(id, model);
        }

        ServiceResult IHistoryService<TDocument>.Delete(string id)
        {
            return _historyService.Delete(id);
        }

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
