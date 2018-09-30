using Alamut.Data.Entity;

namespace Alamut.Data.Service
{
    public interface IService<TEntity> : IService<TEntity, int>
        where TEntity : IEntity
    {
        ///// <summary>
        ///// provide a readonly repository in order to facilitate 
        ///// access to data(readonly) from who that access to the service.
        ///// </summary>
        //IQueryRepository<TEntity> ReadOnlyRepository { get; }
    }
}