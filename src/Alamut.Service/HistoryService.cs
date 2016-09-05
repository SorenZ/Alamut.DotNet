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
    public class HistoryService<TDocument> : CrudService<TDocument>,
    IHistoryService<TDocument> 
        where TDocument : IEntity
    {
        private readonly UserResolverService _userResolverService;
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IRepository<TDocument> repository,
            IHistoryRepository historyRepository,
            IMapper mapper,
            UserResolverService userResolverService) 
            : base(repository, mapper)
        {
            _userResolverService = userResolverService;
            _historyRepository = historyRepository;
        }

        public override ServiceResult<string> Create<TModel>(TModel model)
        {
            var result = base.Create(model);

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

        public override ServiceResult Update<TModel>(string id, TModel model)
        {
            var result = base.Update(id, model);

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

        public override ServiceResult Delete(string id)
        {
            var result = base.Delete(id);
            if (!result.Succeed) return result;
            if (_historyRepository == null) return result;

            var entity = base.ReadOnly.Get(id);

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
