namespace Alamut.Data.Entity
{
    /// <summary>
    /// provides Activity based contract for Entity
    /// </summary>
    public interface IActivateEntity
    {
        bool IsActive { get; set; }
    }
}
