namespace Alamut.Data.Entity
{
    /// <summary>
    /// provides Deletable based contract for Entity
    /// </summary>
    public interface IDeletableEntity 
    {
        bool IsDeleted { get; set; }
    }

    /// <summary>
    /// just for use in mongo query builder
    /// mondo builder can't get serilization information of an interface
    /// </summary>
    public class DeletableEntity : IDeletableEntity
    {
        public bool IsDeleted { get; set; }
    }
}