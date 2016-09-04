namespace Alamut.Data.Entity
{
    /// <summary>
    /// determine an entity has userId (creator user)
    /// that can be assignd 
    /// </summary>
    public interface IUserEntity
    {
        string UserId { get; set; } 
    }
}