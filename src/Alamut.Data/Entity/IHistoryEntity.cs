using System;

namespace Alamut.Data.Entity
{
    /// <summary>
    /// provide a contract of requirements for history
    /// </summary>
    public interface IHistoryEntity : 
        IEntity<string>,
        IUserEntity
        
    {
        string UserIp { get; set; }
        DateTime CreateDate { get; set; }
        string Action { get; set; }
        string EntityId { get; set; }
        string EntityName { get; set; }
        string ModelName { get; set; }
        dynamic ModelValue { get; set; }
    }

    /// <summary>
    /// implement a default document for history collection
    /// </summary>
    public class BaseHistory : IHistoryEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserIp { get; set; }
        public DateTime CreateDate { get; set; }
        public string Action { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string ModelName { get; set; }
        public dynamic ModelValue { get; set; } 
    }
}