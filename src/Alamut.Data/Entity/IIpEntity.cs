namespace Alamut.Data.Entity
{
    /// <summary>
    /// determine an entity has Ip (for user)
    /// </summary>
    public interface IIpEntity
    {
        string IpAddress { get; set; }
    }
}