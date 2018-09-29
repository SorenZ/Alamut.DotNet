//using System.Collections.Generic;
//using Alamut.Data.Entity;

//namespace Alamut.Data.Repository
//{
//    public interface IHistoryRepository<THistoryDocument>
//        where THistoryDocument : IHistoryEntity
//    {
//        /// <summary>
//        /// add an item into history collection
//        /// </summary>
//        /// <param name="entity"></param>
//        void Push(THistoryDocument entity);

//        /// <summary>
//        /// get an item(entity value) from history collection and cast it to TModel
//        /// </summary>
//        /// <typeparam name="TModel">type of expected return value</typeparam>
//        /// <param name="id"></param>
//        /// <returns>typed object based on type parameter</returns>
//        TModel Pull<TModel>(string id) where TModel : class;

//        /// <summary>
//        /// get an item value from history collection by id
//        /// </summary>
//        /// <param name="historyId"></param>
//        /// <returns>untyped object</returns>
//        dynamic Pull(string historyId);

//        /// <summary>
//        /// get a list of item from history collection based on criterias
//        /// </summary>
//        /// <param name="entityName">the name of entity</param>
//        /// <param name="modelName">the name of model</param>
//        /// <param name="entityId"></param>
//        /// <returns></returns>
//        List<THistoryDocument> GetMany(string entityName, string modelName, string entityId);

//        List<THistoryDocument> GetMany(string entityName, string entityId);

        
//    }

//    /// <summary>
//    /// default implementation of IHistoryRepository
//    /// based on BaseHistory
//    /// </summary>
//    public interface IHistoryRepository
//    {
//        /// <summary>
//        /// add an item into history collection
//        /// </summary>
//        /// <param name="entity"></param>
//        void Push(BaseHistory entity);

//        /// <summary>
//        /// get an item(entity value) from history collection and cast it to TModel
//        /// </summary>
//        /// <typeparam name="TModel"></typeparam>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        TModel Pull<TModel>(string id) where TModel : class;

//        dynamic Pull(string historyId);

//        /// <summary>
//        /// get a list of item from history collection based on criteria
//        /// </summary>
//        /// <param name="entityName">the name of entity</param>
//        /// <param name="modelName">the name of model</param>
//        /// <param name="entityId"></param>
//        /// <returns></returns>
//        List<BaseHistory> GetMany(string entityName, string modelName, string entityId);

//        List<BaseHistory> GetMany(string entityName, string entityId);


//    }
//}
