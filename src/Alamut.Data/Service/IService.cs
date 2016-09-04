using Alamut.Data.Entity;
using Alamut.Data.Repository;

namespace Alamut.Data.Service
{
    /// <summary>
    /// provide base service contract
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public interface IService<TDocument>  where TDocument : IEntity
    {
        /// <summary>
        /// provide a readonly repository in order to facilitate 
        /// access to data(readonly) from who that access to the service.
        /// </summary>
        IQueryRepository<TDocument> ReadOnly { get; }
    }
}
