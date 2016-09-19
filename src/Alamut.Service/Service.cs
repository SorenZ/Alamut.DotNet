using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;

namespace Alamut.Service
{
    /// <summary>
    /// provice base service class 
    /// with access to database as readonly repository
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class Service<TDocument> : IService<TDocument> 
        where TDocument : IEntity
    {
        public Service(IRepository<TDocument> repository)
        {
            Repository = repository;
        }

        protected IRepository<TDocument> Repository { get; }

        public IQueryRepository<TDocument> ReadOnly => this.Repository;
    }
}
