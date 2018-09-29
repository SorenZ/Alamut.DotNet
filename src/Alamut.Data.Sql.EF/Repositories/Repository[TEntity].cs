using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF.Repositories
{
    public class Repository<TEntity> : Repository<TEntity,int>,
        IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        public Repository(DbContext context) : base(context)
        { }
    }
}