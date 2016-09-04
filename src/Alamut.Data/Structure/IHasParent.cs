namespace Alamut.Data.Structure
{
    public interface IHasParent
    {
        string Id { get; set; }
        string ParentId { get; set; }
    }
}