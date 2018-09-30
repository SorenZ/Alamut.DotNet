//using System;
//using System.Collections.Generic;
//using Alamut.Data.Entity;
//using Alamut.Data.Repository;
//using Alamut.Data.Service;
//using Alamut.Data.Structure;
//using Alamut.Utilities.Http;

//namespace Alamut.Service
//{
//    public class HistoryService<TEntity> : 
//        CrudService<TEntity>,
//        IHistoryService<TEntity> 
//            where TEntity : IEntity
//    {
//        private readonly IUserResolverService _userResolverService;
//        private readonly IHistoryRepository _historyRepository;

//        public HistoryService(IRepository<TEntity> repository,
//            IHistoryRepository historyRepository,
//            IUserResolverService userResolverService) 
//            : base(repository)
//        {
//            _userResolverService = userResolverService;
//            _historyRepository = historyRepository;
//        }

//        public override ServiceResult<string> Create<TModel>(TModel model)
//        {
//            var result = base.Create(model);

//            if (!result.Succeed) return result;

//            var history = new BaseHistory
//            {
//                Action = HistoryActions.Create,
//                UserId = _userResolverService.UserId,
//                CreateDate = DateTime.Now,
//                EntityId = result.Data,
//                EntityName = typeof(TEntity).Name,
//                ModelName = typeof(TModel).Name,
//                ModelValue = model,
//                UserIp = _userResolverService.UserIpAddress
//            };

//            _historyRepository.Push(history);

//            return result;
//        }

//        public override ServiceResult Update<TModel>(string id, TModel model)
//        {
//            var result = base.Update(id, model);

//            if (!result.Succeed) return result;

//            var history = new BaseHistory
//            {
//                Action = HistoryActions.Update,
//                UserId = _userResolverService.UserId,
//                CreateDate = DateTime.Now,
//                EntityId = id,
//                EntityName = typeof(TEntity).Name,
//                ModelName = typeof(TModel).Name,
//                ModelValue = model,
//                UserIp = _userResolverService.UserIpAddress
//            };

//            _historyRepository.Push(history);

//            return result;
//        }

//        public override ServiceResult Delete(string id)
//        {
//            var entity = base.ReadOnly.Get(id);
//            var result = base.Delete(id);

//            if (!result.Succeed) return result;

//            var history = new BaseHistory
//            {
//                Action = HistoryActions.Delete,
//                UserId = _userResolverService.UserId,
//                CreateDate = DateTime.Now,
//                EntityId = id,
//                EntityName = typeof(TEntity).Name,
//                ModelName = typeof(TEntity).Name,
//                ModelValue = entity,
//                UserIp = _userResolverService.UserIpAddress
//            };

//            _historyRepository.Push(history);

//            return result;

//        }

//        public TModel GetHistoryValue<TModel>(string historyId)
//            where TModel : class =>
//                _historyRepository.Pull<TModel>(historyId);


//        public dynamic GetHistoryValue(string historyId) =>
//            _historyRepository.Pull(historyId);


//        public List<BaseHistory> GetHistories<TModel>(string entityId)
//        {
//            var entityName = typeof (TEntity).Name;
//            var modelName = typeof (TModel).Name;

//            return _historyRepository.GetMany(entityName, modelName, entityId);
//        }

//        public List<BaseHistory> GetHistories(string entityId)
//        {
//            var entityName = typeof(TEntity).Name;

//            return _historyRepository.GetMany(entityName, entityId);
//        }
//    }
//}
