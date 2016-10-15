using System;
using Alamut.Data.Entity;

namespace Alamut.Data.Service
{
    [Obsolete("not recomended. use combination of other interfaces")]
    public interface IFullService<TDocument> : IHistoryService<TDocument>
        where TDocument : IEntity
    {
        
    }
}