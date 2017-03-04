using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Utilities.Http;

namespace Alamut.Service
{
    public class FullService<TDocument> : //IFullService<TDocument>
        IService<TDocument>,
        ICrudService<TDocument>,
        IHistoryService<TDocument> 
        where TDocument : IEntity
    {
        private readonly CrudService<TDocument> _crudService;
        private readonly HistoryService<TDocument> _historyService;

        /// <summary>
        /// create full service with history supported
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userResolverService"></param>
        /// <param name="mapper"></param>
        /// <param name="historyRepository"></param>
        public FullService(IRepository<TDocument> repository,
            //IMapper mapper,
            IHistoryRepository historyRepository = null,
            IUserResolverService userResolverService = null)
        {
            Repository = repository;
            //Mapper = mapper;

            if (historyRepository != null && userResolverService != null)
            {
                _crudService = _historyService = new HistoryService<TDocument>(repository, historyRepository, userResolverService);
            }
            else
            {
                _crudService = new CrudService<TDocument>(repository);
            }

        }

        #region IService

        public IQueryRepository<TDocument> ReadOnly => this.Repository;

        protected IRepository<TDocument> Repository { get; }

        #endregion

        #region ICrudService

        //protected IMapper Mapper { get; }

        public ServiceResult<string> Create<TModel>(TModel model)
        {
            return _crudService.Create(model);
        }

        public ServiceResult Update<TModel>(string id, TModel model)
        {
            return _crudService.Update(id, model);
        }

        public ServiceResult UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            return _crudService.UpdateOne(id, memberExpression, value);
        }

        public ServiceResult Delete(string id)
        {
            return _crudService.Delete(id);
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

        public TModel GetHistoryValue<TModel>(string historyId) where TModel : class
        {
            if (_historyService == null)
                throw new NullReferenceException(nameof(_historyService));

            return _historyService.GetHistoryValue<TModel>(historyId);
        }

        public dynamic GetHistoryValue(string historyId)
        {
            if (_historyService == null)
                throw new NullReferenceException(nameof(_historyService));

            return _historyService.GetHistoryValue(historyId);
        }

        public List<BaseHistory> GetHistories<TModel>(string entityId)
        {
            if (_historyService == null)
                throw new NullReferenceException(nameof(_historyService));

            return _historyService.GetHistories<TModel>(entityId);
        }

        public List<BaseHistory> GetHistories(string entityId)
        {
            if (_historyService == null)
                throw new NullReferenceException(nameof(_historyService));

            return _historyService.GetHistories(entityId);
        }

        #endregion

    }
}
