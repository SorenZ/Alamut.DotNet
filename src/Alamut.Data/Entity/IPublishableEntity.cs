namespace Alamut.Data.Entity
{
    /// <summary>
    /// provide publishable entity contract
    /// </summary>
    public interface IPublishableEntity
    {
        bool IsPublished { get; set; }
    }
}