namespace Alamut.Data.Entity
{
    /// <summary>
    /// provides project based contract for Entity
    /// </summary>
    public interface IMultiProjectEntity
    {
        string ProjectId { get; set; }     
    }
}