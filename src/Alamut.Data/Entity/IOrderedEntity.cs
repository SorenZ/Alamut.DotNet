namespace Alamut.Data.Entity
{
    /// <summary>
    /// provide sortable contract
    /// order field populated in insert or update
    /// </summary>
    public interface IOrderedEntity
    {
        int Order { get; set; }
    }
}