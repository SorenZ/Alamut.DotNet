using System.Collections.Generic;
using Alamut.Data.Entity;

namespace Alamut.Data.Service
{
    /// <summary>
    /// provide a service that include history on history base in all commmand methods
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public interface IHistoryService<TDocument> : ICrudService<TDocument> 
        where TDocument : IEntity
    {
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