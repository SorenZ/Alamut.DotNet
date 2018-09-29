using Alamut.Data.Entity;

namespace Alamut.Data.Repository
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> 
        where TEntity : IEntity
    { }
}