using System;
using System.Globalization;

namespace EvrenDev.Application.DTOS.Shared
{
    public class DateTimeDto
    {
        public DateTime UtcDateTime { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public MonthNameFormats MonthNames { get; set; }
        public string ShortDate { get; set; }
        public string LongDate { get; set; }
        public string FullShortDate { get; set; }
        public string FullLongDate { get; set; }
        public string DisplayDate { get; set; }
    }

    public class MonthNameFormats {
        public string Short {get; set;}
        public string Long {get; set;}
    }

    public class DateTimeFunctions {
        internal static DateTimeDto GetDetailsDate(DateTime? date) {
            return date != null  
                ?
                    new DateTimeDto() {
                        UtcDateTime = date.Value,
                        Day = date.Value.Day,
                        Month = date.Value.Month,
                        Year = date.Value.Year,
                        Hour = date.Value.Hour,
                        Minute = date.Value.Minute,
                        MonthNames = new MonthNameFormats() {
                            Short = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Value.Month),
                            Long = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Value.Month)
                        },
                        ShortDate = date.Value.ToString("dd MMM yy"),
                        LongDate = date.Value.ToString("dd MMMM yyyy"),
                        FullShortDate = date.Value.ToString("dd MMM yy HH:mm"),
                        FullLongDate = date.Value.ToString("dd MMMM yyyy HH:mm"),
                        DisplayDate = date.Value.ToString("dd.MM.yyyy"),
                    }
                :
                    null;
        }
    }   
}