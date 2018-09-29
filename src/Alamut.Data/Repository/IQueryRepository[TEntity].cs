using Alamut.Data.Entity;

namespace Alamut.Data.Repository
{
    public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, int>
        where TEntity : IEntity
    { }
}