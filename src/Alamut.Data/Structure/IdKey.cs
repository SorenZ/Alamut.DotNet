namespace Alamut.Data.Structure
{
    public class IdKey : IIdBased
    {
        public IdKey(string id)
        {
            this.Id = id;
        }

        public IdKey(string id, int key) : this(id)
        {
            this.Key = key;
        }

        public string Id { get; set; }

        public int Key { get; set; }
    }
}