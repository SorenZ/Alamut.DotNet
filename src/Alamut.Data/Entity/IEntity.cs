using Alamut.Data.Structure;

namespace Alamut.Data.Entity
{

    /// <summary>
    /// provide base interface of an entity
    /// it's required for work with repository 
    /// Id is mandetory
    /// the type of Id is string
    /// </summary>
    public interface IEntity : IIdBased //, IEntity<string>
    { }

    /// <summary>
    /// provide base interface of an entity
    /// it's required for work with repository 
    /// Id is mandetory
    /// the type of Id will define by type parameter
    /// </summary>
    public interface IEntity<TKey> 
    {
        TKey Id { get; set; }
    }
}
