namespace Alamut.Data.Structure
{
    /// <summary>
    /// represent base type that dedicate a data structure such as
    /// - Entity
    /// - Dto
    /// - Requeest, Response
    /// should have Id
    /// </summary>
    /// <remarks>
    /// - used in History
    /// - CRUD Repository
    /// - Helper(s)
    /// </remarks>
    public interface IIdBased
    {
        string Id { get; set; }
    }
}
