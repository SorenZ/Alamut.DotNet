using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;

namespace Alamut.Service
{
    public class Service<TEntity> : Service<TEntity, int>,
        IService<TEntity>
        where TEntity : IEntity
    {
        public Service(IRepository<TEntity, int> repository) : base(repository)
        { }
        
        //public Service(IRepository<TEntity> repository)
        //{
        //    Repository = repository;
        //}

        //protected IRepository<TEntity> Repository { get; }

        //public IQueryRepository<TEntity, int> ReadOnlyRepository => this.Repository;
        
    }
}