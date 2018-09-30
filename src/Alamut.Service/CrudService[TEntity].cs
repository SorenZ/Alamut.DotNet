using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;

namespace Alamut.Service
{
    public class CrudService<TEntity> : CrudService<TEntity, int>,
        ICrudService<TEntity>
        where TEntity : IEntity
    {
        public CrudService(IRepository<TEntity, int> repository) : base(repository)
        { }
    }
}