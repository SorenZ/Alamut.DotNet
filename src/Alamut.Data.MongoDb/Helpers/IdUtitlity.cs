using System.Text.RegularExpressions;

namespace Alamut.Data.MongoDb.Helpers
{
    public static class IdUtitlity
    {
        private static readonly Regex ObjectIdValidator = new Regex("^[0-9a-fA-F]{24}$", RegexOptions.Compiled);

        /// <summary>
        /// is provided string is object-id or not
        /// </summary>
        /// <param name="suggestedId"></param>
        /// <returns></returns>
        public static bool IsObjectId(string suggestedId)
        {
            return ObjectIdValidator.IsMatch(suggestedId);
        }
    }
}
