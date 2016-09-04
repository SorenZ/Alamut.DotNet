namespace Alamut.Data.Entity
{
    /// <summary>
    /// single source of truth for Enttiy
    /// </summary>
    public class EntitySsot
    {
        public const string IsDeleted = "IsDeleted";
    }

    public static class HistoryActions
    {
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";
    }
}
