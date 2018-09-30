using Alamut.Data.Entity;

namespace Alamut.Data.Service
{
    public interface ICrudService<TEntity> : ICrudService<TEntity, int>,
        IService<TEntity>
        where TEntity : IEntity
    {

    }
}