using System;

namespace Alamut.Helpers.DateTime
{
    public static class PersianDateExtensions
    {
        public static string GetPersianMonthName(int month)
        {
            switch (month)
            {
                case 1: { return "فروردین";}
                case 2: { return "اردیبهشت";}
                case 3: { return "خرداد"; }
                case 4: { return "تیر"; }
                case 5: { return "مرداد"; }
                case 6: { return "شهریور"; }
                case 7: { return "مهر"; }
                case 8: { return "آبان"; }
                case 9: { return "آذر"; }
                case 10: { return "دی"; }
                case 11: { return "بهمن"; }
                case 12: { return "اسفند"; }
                default: { return "نا مشخص"; }
            }
        }

        public static string GetPersianDateString(this System.DateTime date)
        {
            var p = new System.Globalization.PersianCalendar();

            var year = p.GetYear(date);
            var month = p.GetMonth(date);
            var day = p.GetDayOfMonth(date);

            var monthName = GetPersianMonthName(month);

            return string.Format("{0} {1} {2}", day, monthName, year);
        }

        public static string GetPersianDateStringSlashed(this System.DateTime date)
        {
            var p = new System.Globalization.PersianCalendar();

            var year = p.GetYear(date);
            var month = p.GetMonth(date).ToString("00");
            var day = p.GetDayOfMonth(date).ToString("00");

            return string.Format("{0}/{1}/{2}", year, month, day);
        }

        public static string GetPersianDateStringSlashedWithTime(this System.DateTime date)
        {
            var time = date.ToString("HH:mm");
            if (date.Date == System.DateTime.Now.Date)
                return string.Format("امروز - {0}", time);
            var persianDateString = GetPersianDateString(date);
            return string.Format("" + persianDateString + " - {0}", time);
        }

        public static string GetFullPersianDateTime(this System.DateTime date)
        {
            var time = date.ToString("HH:mm");
            var persianDateString = GetPersianDateString(date);
            var day = "";
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday: day = "شنبه"; break;
                case DayOfWeek.Sunday: day = "يکشنبه"; break;
                case DayOfWeek.Monday: day = "دوشنبه"; break;
                case DayOfWeek.Tuesday: day = "سه‏ شنبه"; break;
                case DayOfWeek.Wednesday: day = "چهارشنبه"; break;
                case DayOfWeek.Thursday: day = "پنجشنبه"; break;
                case DayOfWeek.Friday: day = "جمعه"; break;
                default: day = ""; break;
            }
            return string.Format("{0} ، {1} ساعت : {2}", day, persianDateString, time);
        }
    }
}
