using MongoDB.Bson;

namespace Alamut.Helpers.DomainDriven
{
    /// <summary>
    /// responsible for generate unique Id
    /// </summary>
    public class IdGenerator
    {
        /// <summary>
        /// provides a unique id based on BSON object Id
        /// </summary>
        /// <returns></returns>
        public static string GetNewId()
        {
            return ObjectId.GenerateNewId().ToString();
        }

        /// <summary>
        /// provides a new unique ObjectId 
        /// </summary>
        /// <returns></returns>
        public static ObjectId GetObjectId()
        {
            return ObjectId.GenerateNewId();
        }
    }
}