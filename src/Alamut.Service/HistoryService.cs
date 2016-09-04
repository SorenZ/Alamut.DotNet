using System;
using System.Collections.Generic;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Utilities.Http;
using AutoMapper;

namespace Alamut.Service
{
    public class HistoryService<TDocument> :
        IHistoryService<TDocument> 
        where TDocument : IEntity
    {
        private readonly ICrudService<TDocument> _crudService;
        private readonly UserResolverService _userResolverService;
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository,
            ICrudService<TDocument> crudService,
            UserResolverService userResolverService)
        {
            _crudService = crudService;
            _userResolverService = userResolverService;
            _historyRepository = historyRepository;
        }

        //public HistoryService(IHistoryRepository historyRepository,
        //    IRepository<TDocument> repository = null,
        //    IMapper mapper = null)
        //{
        //    _crudService = new CrudService<TDocument>(repository, mapper);
        //    _historyRepository = historyRepository;
        //}

        public ServiceResult<string> Create<TModel>(TModel model)
        {
            var result = _crudService.Create(model);

            if (!result.Succeed) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Create,
                UserId = _userResolverService.GetUserId(),
                CreateDate = DateTime.Now,
                EntityId = result.Data,
                EntityName = typeof(TDocument).Name,
                ModelName = typeof(TModel).Name,
                ModelValue = model,
                UserIp = _userResolverService.GetUserIpAddress()
            };

            _historyRepository.Push(history);

            return result;
        }

        public ServiceResult Update<TModel>(string id, TModel model)
        {
            var result = _crudService.Update(id, model);

            if (!result.Succeed) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Update,
                UserId = _userResolverService.GetUserId(),
                CreateDate = DateTime.Now,
                EntityId = id,
                EntityName = typeof(TDocument).Name,
                ModelName = typeof(TModel).Name,
                ModelValue = model,
                UserIp = _userResolverService.GetUserIpAddress()
            };

            _historyRepository.Push(history);

            return result;
        }

        public virtual ServiceResult Delete(string id)
        {
            var entity = _crudService.ReadOnly.Get(id);

            var result = _crudService.Delete(id);

            if (!result.Succeed) return result;
            if (_historyRepository == null) return result;

            var history = new BaseHistory
            {
                Action = HistoryActions.Delete,
                UserId = _userResolverService.GetUserId(),
                CreateDate = DateTime.Now,
                EntityId = id,
                EntityName = typeof(TDocument).Name,
                ModelName = typeof(TDocument).Name,
                ModelValue = entity,
                UserIp = _userResolverService.GetUserIpAddress()
            };

            _historyRepository.Push(history);

            return result;

        }

        public TModel GetHistoryValue<TModel>(string historyId) where TModel : class
        {
            return _historyRepository.Pull<TModel>(historyId);
        }

        public dynamic GetHistoryValue(string historyId)
        {
            return _historyRepository.Pull(historyId);
        }

        public List<BaseHistory> GetHistories<TModel>(string entityId)
        {
            var entityName = typeof (TDocument).Name;
            var modelName = typeof (TModel).Name;

            return _historyRepository.GetMany(entityName, modelName, entityId);
        }

        public List<BaseHistory> GetHistories(string entityId)
        {
            var entityName = typeof(TDocument).Name;

            return _historyRepository.GetMany(entityName, entityId);
        }
    }
}
