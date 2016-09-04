namespace Alamut.Data.Structure
{
    public interface IIdTitle : IIdBased
    {
        //string Id { get; set; }
        string Title { get; set; }
    }
    
    public class IdTitle : IIdTitle
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public interface IHierarchy
    {
        
    }
}
