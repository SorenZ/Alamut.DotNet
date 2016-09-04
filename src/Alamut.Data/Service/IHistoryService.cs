using System.Collections.Generic;
using Alamut.Data.Entity;
using Alamut.Data.Structure;

namespace Alamut.Data.Service
{
    /// <summary>
    /// provide a service that include history on history base in all commmand methods
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public interface IHistoryService<TDocument> //ICrudService<TDocument> 
        where TDocument : IEntity
    {
        /// <summary>
        /// create an item by mapping model into entity and 
        /// add it in database.
        /// - create a history entiry for it.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        //ServiceResult<string> Create<TModel>(TModel model);


        /// <summary>
        /// update an item by id
        /// mapping model into entity (use new properties in model & old properties in entity)
        /// update database
        /// - create a history entiry for it.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        //ServiceResult Update<TModel>(string id, TModel model);

        /// <summary>
        /// delete item by Id
        /// - create a history entiry for it.
        /// snapshot the entire document in order to save it for reverse in future
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //ServiceResult Delete(string id);

        /// <summary>
        /// get typed history value by id
        /// convert ModelValue to requested type
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="historyId"></param>
        /// <returns></returns>
        TModel GetHistoryValue<TModel>(string historyId) where TModel : class;

        /// <summary>
        /// get history value by id
        /// </summary>
        /// <param name="historyId"></param>
        /// <returns></returns>
        dynamic GetHistoryValue(string historyId);

        /// <summary>
        /// get list of base History for current TDocument and TModel
        /// by entity Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        List<BaseHistory> GetHistories<TModel>(string entityId);

        /// <summary>
        /// get list of base History for current TDocument 
        /// by entity Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        List<BaseHistory> GetHistories(string entityId);
    }
}