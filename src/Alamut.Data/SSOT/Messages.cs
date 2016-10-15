using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Alamut.Data.SSOT
{
    public class Messages
    {
        private static bool IsRtl => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;

        public const string ItemCreatedPersian = @"آیتم با موفقیت ایجاد شد";
        public const string ItemCreatedEnglish = @"item successfully created.";

        public const string ItemDeletedPersian = @"آیتم با موفقیت حذف شد";
        public const string ItemDeletedEnglish = @"item successfully deleted.";

        public static string ItemCreated => IsRtl
            ? ItemCreatedPersian
            : ItemCreatedEnglish;

        public static string ItemsCreated => IsRtl
            ? @"آیتم ها با موفقیت ایجاد شدند"
            : "items successfully created.";

        public static string ItemUpdated => IsRtl
            ? @"آیتم با موفقیت ویرایش شد"
            : "item successfully updated.";

        public static string ItemDeleted => IsRtl
           ? ItemDeletedPersian
           : ItemDeletedEnglish;
    }
}
