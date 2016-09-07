using Alamut.Data.Entity;

namespace Alamut.Data.Service
{
    public interface IFullService<TDocument> : IHistoryService<TDocument>
        where TDocument : IEntity
    {
        
    }
}