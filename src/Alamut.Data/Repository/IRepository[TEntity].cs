using Alamut.Data.Entity;

namespace Alamut.Data.Repository
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> ,
        IQueryRepository<TEntity>
        where TEntity : IEntity
    { }
}