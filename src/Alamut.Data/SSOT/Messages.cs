using System.Globalization;

namespace Alamut.Data.SSOT
{
    public class Messages
    {
        private static bool IsRtl => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;

        public const string ItemCreatedPersian = @"آیتم با موفقیت ایجاد شد";
        public const string ItemCreatedEnglish = @"item successfully created";

        public const string ItemsCreatedPersian = @"آیتم ها با موفقیت ایجاد شدند";
        public const string ItemsCreatedEnglish = @"items successfully created";

        public const string ItemUpdatedPersian = @"تغیرات با موفقیت ثبت شد";
        public const string ItemUpdatedEnglish = @"item(s) successfully updated";

        public const string ItemDeletedPersian = @"آیتم با موفقیت حذف شد";
        public const string ItemDeletedEnglish = @"item successfully deleted";

        public static string ItemCreated => IsRtl
            ? ItemCreatedPersian
            : ItemCreatedEnglish;

        public static string ItemsCreated => IsRtl
            ? ItemsCreatedPersian
            : ItemsCreatedEnglish;

        public static string ItemUpdated => IsRtl
            ? ItemUpdatedPersian
            : ItemUpdatedEnglish;

        public static string ItemDeleted => IsRtl
           ? ItemDeletedPersian
           : ItemDeletedEnglish;
    }
}
