using Alamut.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF.Repositories
{
    public class QueryRepository<TEntity> : QueryRepository<TEntity, int>
        where TEntity : class, IEntity, new()
    {
        public QueryRepository(DbContext context) : base(context)
        { }
    }
}