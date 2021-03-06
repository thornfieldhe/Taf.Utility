// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.DateTime.cs" company="">
//   
// </copyright>
// <summary>
//   时间扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text;

namespace Taf.Utility{
    /// <summary>
    ///     The extensions.
    /// </summary>
    public partial class Extensions{
        /// <summary>
        ///     The one minute.
        /// </summary>
        private static readonly TimeSpan OneMinute = new TimeSpan(0, 1, 0);

        /// <summary>
        ///     The two minutes.
        /// </summary>
        private static readonly TimeSpan TwoMinutes = new TimeSpan(0, 2, 0);

        /// <summary>
        ///     The one hour.
        /// </summary>
        private static readonly TimeSpan OneHour = new TimeSpan(1, 0, 0);

        /// <summary>
        ///     The two hours.
        /// </summary>
        private static readonly TimeSpan TwoHours = new TimeSpan(2, 0, 0);

        /// <summary>
        ///     The one day.
        /// </summary>
        private static readonly TimeSpan OneDay = new TimeSpan(1, 0, 0, 0);

        /// <summary>
        ///     The two days.
        /// </summary>
        private static readonly TimeSpan TwoDays = new TimeSpan(2, 0, 0, 0);

        /// <summary>
        ///     The one week.
        /// </summary>
        private static readonly TimeSpan OneWeek = new TimeSpan(7, 0, 0, 0);

        /// <summary>
        ///     The two weeks.
        /// </summary>
        private static readonly TimeSpan TwoWeeks = new TimeSpan(14, 0, 0, 0);

        /// <summary>
        ///     The one month.
        /// </summary>
        private static readonly TimeSpan OneMonth = new TimeSpan(31, 0, 0, 0);

        /// <summary>
        ///     The two months.
        /// </summary>
        private static readonly TimeSpan TwoMonths = new TimeSpan(62, 0, 0, 0);

        /// <summary>
        ///     The one year.
        /// </summary>
        private static readonly TimeSpan OneYear = new TimeSpan(365, 0, 0, 0);

        /// <summary>
        ///     The two years.
        /// </summary>
        private static readonly TimeSpan TwoYears = new TimeSpan(730, 0, 0, 0);

        /// <summary>
        ///     获取两个时间间隔
        /// </summary>
        /// <param name="startTime">
        /// </param>
        /// <param name="endTime">
        /// </param>
        /// <returns>
        ///     The <see cref="TimeSpan" />.
        /// </returns>
        public static TimeSpan GetTimeSpan(this DateTime startTime, DateTime endTime) => endTime - startTime;

        /// <summary>
        ///     判断日期是否是今日
        /// </summary>
        /// <param name="dt">
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsToday(this DateTime dt) => dt.Date == DateTime.Today;

        /// <summary>
        ///     判断dto日期是否是今日
        /// </summary>
        /// <param name="dto">
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsToday(this DateTimeOffset dto) => dto.Date.IsToday();

        /// <summary>
        ///     计算指定月天数
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int GetCountDaysOfMonth(this DateTime date){
            var nextMonth = date.AddMonths(1);
            return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
        }

        /// <summary>
        ///     获取日期周数
        /// </summary>
        /// <param name="datetime">
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int WeekOfYear(this DateTime datetime){
            var dateinf        = new DateTimeFormatInfo();
            var weekrule       = dateinf.CalendarWeekRule;
            var firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        ///     获取日期周数
        /// </summary>
        /// <param name="datetime">
        /// </param>
        /// <param name="weekrule">
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int WeekOfYear(this DateTime datetime, CalendarWeekRule weekrule){
            var dateinf        = new DateTimeFormatInfo();
            var firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        ///     获取日期周数
        /// </summary>
        /// <param name="datetime">
        /// </param>
        /// <param name="firstDayOfWeek">
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int WeekOfYear(this DateTime datetime, DayOfWeek firstDayOfWeek){
            var dateinf  = new DateTimeFormatInfo();
            var weekrule = dateinf.CalendarWeekRule;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        ///     获取日期周数
        /// </summary>
        /// <param name="datetime">
        /// </param>
        /// <param name="weekrule">
        /// </param>
        /// <param name="firstDayOfWeek">
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int WeekOfYear(this DateTime datetime, CalendarWeekRule weekrule, DayOfWeek firstDayOfWeek){
            var ciCurr = CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        ///     获取季度
        /// </summary>
        /// <param name="datetime">
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int GetQuarter(this DateTime datetime){
            if(datetime.Month <= 3){
                return 1;
            }

            if(datetime.Month <= 6){
                return 2;
            }

            return datetime.Month <= 9 ? 3 : 4;
        }

        /// <summary>
        ///     是否是工作日
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsWeekDay(this DateTime date) => !date.IsWeekend();

        /// <summary>
        ///     是否是周末
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsWeekend(this DateTime value) =>
            value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday;

        /// <summary>
        ///     获取X个工作日后日期
        /// </summary>
        /// <param name="from"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTime AddWeekend(this DateTime from, int days){
            var total = 0;
            var dt    = from;
            if(days > 0){
                for(var i = 1; i <= days; i++){
                    dt = CheckAndAddDays(dt, days > 0, ref total);
                }
            } else{
                for(var i = -1; i >= days; i--){
                    dt = CheckAndAddDays(dt, days > 0, ref total);
                }
            }


            return dt;
        }

        /// <summary>
        ///     检查日期+1后是否是周末,并更新总天数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="add"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private static DateTime CheckAndAddDays(DateTime date, bool add, ref int total){
            var d = add ? date.AddDays(1) : date.AddDays(-1);

            total++;
            if(d.IsWeekend()){
                d = CheckAndAddDays(d, add, ref total);
            }

            return d;
        }

        /// <summary>
        ///     时间是否处于时间范围中
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="startDate">
        /// </param>
        /// <param name="endDate">
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsWithin(this DateTime @this, DateTime startDate, DateTime endDate) =>
            @this > startDate && @this < endDate;

    #region 获取具体时间

        /// <summary>
        ///     返回当日结束时间 23:59:59;
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime EndOfDay(this DateTime @this) => @this.Date.AddDays(1).AddSeconds(-1);

        /// <summary>
        ///     返回当日开始时间 00:00:00
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime StartOfDay(this DateTime @this) => @this.Date.Date;

        /// <summary>
        ///     明天
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime NextDay(this DateTime @this) => @this.StartOfDay().AddDays(1);

        /// <summary>
        ///     昨天
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime Yesterday(this DateTime @this) => @this.StartOfDay().AddDays(-1);

        /// <summary>
        ///     日期所在月第一天
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);

        /// <summary>
        ///     日期所在月第一天
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="dayOfWeek">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date, DayOfWeek dayOfWeek){
            var dt = date.GetFirstDayOfMonth();
            while(dt.DayOfWeek != dayOfWeek){
                dt = dt.AddDays(1);
            }

            return dt;
        }

        /// <summary>
        ///     日期所在月最后一天
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetLastDayOfMonth(this DateTime date) =>
            new DateTime(date.Year, date.Month, GetCountDaysOfMonth(date));

        /// <summary>
        ///     日期所在月最后一天
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="dayOfWeek">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetLastDayOfMonth(this DateTime date, DayOfWeek dayOfWeek){
            var dt = date.GetLastDayOfMonth();
            while(dt.DayOfWeek != dayOfWeek){
                dt = dt.AddDays(-1);
            }

            return dt;
        }

        /// <summary>
        ///     获取日期所在周一日期
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date) => date.GetFirstDayOfWeek(null);

        /// <summary>
        ///     获取日期所在周一日期
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="cultureInfo">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetFirstDayOfWeek(this DateTime date, CultureInfo cultureInfo){
            cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            while(date.DayOfWeek != firstDayOfWeek){
                date = date.AddDays(-1).Date;
            }

            return date;
        }

        /// <summary>
        ///     获取日期所在周末日期
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetLastDayOfWeek(this DateTime date) => date.GetLastDayOfWeek(null);

        /// <summary>
        ///     获取日期所在周末日期
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="cultureInfo">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetLastDayOfWeek(this DateTime date, CultureInfo cultureInfo) =>
            date.GetFirstDayOfWeek(cultureInfo).AddDays(6);

        /// <summary>
        ///     获取工作日日期
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="weekday">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetWeekday(this DateTime date, DayOfWeek weekday) => date.GetWeekday(weekday, null);

        /// <summary>
        ///     获取工作日日期
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="weekday">
        /// </param>
        /// <param name="cultureInfo">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetWeekday(this DateTime date, DayOfWeek weekday, CultureInfo cultureInfo){
            var firstDayOfWeek = date.GetFirstDayOfWeek(cultureInfo);
            return firstDayOfWeek.GetNextWeekday(weekday);
        }

        /// <summary>
        ///     获取下周周末
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="weekday">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetNextWeekday(this DateTime date, DayOfWeek weekday){
            while(date.DayOfWeek != weekday){
                date = date.AddDays(1);
            }

            return date;
        }

        /// <summary>
        ///     获取下周日日期
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <param name="weekday">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime GetPreviousWeekday(this DateTime date, DayOfWeek weekday){
            while(date.DayOfWeek != weekday){
                date = date.AddDays(-1);
            }

            return date;
        }

        /// <summary>
        ///     返回几秒钟前时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime SecondsAgo(this int @this) => DateTime.Now.AddSeconds(-@this);

        /// <summary>
        ///     返回几分钟前时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime MinutesAgo(this int @this) => DateTime.Now.AddMinutes(-@this);

        /// <summary>
        ///     返回几小时前时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime HoursAgo(this int @this) => DateTime.Now.AddHours(-@this);

        /// <summary>
        ///     返回几天前时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime DaysAgo(this int @this) => DateTime.Now.AddDays(-@this);

        /// <summary>
        ///     返回几个月前时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime MonthsAgo(this int @this) => DateTime.Now.AddMonths(-@this);

        /// <summary>
        ///     返回几年前时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime YearsAgo(this int @this) => DateTime.Now.AddYears(-@this);

        /// <summary>
        ///     返回几秒钟后时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime SecondsFromNow(this int @this) => DateTime.Now.AddSeconds(@this);

        /// <summary>
        ///     返回几分钟后时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime MinutesFromNow(this int @this) => DateTime.Now.AddMinutes(@this);

        /// <summary>
        ///     返回几小时后时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime HoursFromNow(this int @this) => DateTime.Now.AddHours(@this);

        /// <summary>
        ///     返回几天后时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime DaysFromNow(this int @this) => DateTime.Now.AddDays(@this);

        /// <summary>
        ///     返回几月后时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime MonthsFromNow(this int @this) => DateTime.Now.AddMonths(@this);

        /// <summary>
        ///     返回几年后时间
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime YearsFromNow(this int @this) => DateTime.Now.AddYears(@this);

        /// <summary>
        ///     增加周
        /// </summary>
        /// <param name="dt">
        /// </param>
        /// <param name="count">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime AddWeeks(this DateTime dt, int count){
            var dateBegin = GetWeekday(dt, DayOfWeek.Monday);
            return dateBegin.AddDays(7 * count);
        }

        /// <summary>
        ///     返回所在年的第几天的具体日期
        /// </summary>
        /// <param name="this">
        /// </param>
        /// <param name="year">
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" />.
        /// </returns>
        public static DateTime DayInYear(this int @this, int? year = null){
            var firstDayOfYear = new DateTime(year ?? DateTime.Now.Year, 1, 1);
            return firstDayOfYear.AddDays(@this - 1);
        }

    #endregion

    #region 格式化时间

        /// <summary>
        ///     简化日期格式：xx分钟前
        /// </summary>
        /// <param name="date">
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToAgo(this DateTime date){
            var timeSpan = date.GetTimeSpan(DateTime.Now);
            if(timeSpan < TimeSpan.Zero){
                return "未来";
            }

            if(timeSpan < OneMinute){
                return "现在";
            }

            if(timeSpan < TwoMinutes){
                return "1 分钟前";
            }

            if(timeSpan < OneHour){
                return string.Format("{0} 分钟前", timeSpan.Minutes);
            }

            if(timeSpan < TwoHours){
                return "1 小时前";
            }

            if(timeSpan < OneDay){
                return string.Format("{0} 小时前", timeSpan.Hours);
            }

            if(timeSpan < TwoDays){
                return "昨天";
            }

            if(timeSpan < OneWeek){
                return string.Format("{0} 天前", timeSpan.Days);
            }

            if(timeSpan < TwoWeeks){
                return "1 周前";
            }

            if(timeSpan < OneMonth){
                return string.Format("{0} 周前", timeSpan.Days / 7);
            }

            if(timeSpan < TwoMonths){
                return "1 月前";
            }

            if(timeSpan < OneYear){
                return string.Format("{0} 月前", timeSpan.Days / 31);
            }

            return timeSpan < TwoYears ? "1 年前" : string.Format("{0} 年前", timeSpan.Days / 365);
        }

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <param name="isRemoveSecond">
        ///     是否移除秒
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToDateTimeString(this DateTime dateTime, bool isRemoveSecond = false) =>
            dateTime.ToString(isRemoveSecond ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd HH:mm:ss");

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyyMMddHHmmss"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime dateTime) => dateTime.ToString("yyyyMMddHHmm");

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <param name="isRemoveSecond">
        ///     是否移除秒
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false) =>
            dateTime == null ? string.Empty : ToDateTimeString(dateTime.Value, isRemoveSecond);

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <param name="withOutDash">是否包含-</param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToDateString(this DateTime dateTime, bool withOutDash = false) =>
            dateTime.ToString(withOutDash ? "yyyyMMdd" : "yyyy-MM-dd");

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToDateString(this DateTime? dateTime) =>
            dateTime == null ? string.Empty : ToDateString(dateTime.Value);

        /// <summary>
        ///     获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToTimeString(this DateTime dateTime) => dateTime.ToString("HH:mm:ss");

        /// <summary>
        ///     获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToTimeString(this DateTime? dateTime) =>
            dateTime == null ? string.Empty : ToTimeString(dateTime.Value);

        /// <summary>
        ///     获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToMillisecondString(this DateTime dateTime) =>
            dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        /// <summary>
        ///     获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToMillisecondString(this DateTime? dateTime) =>
            dateTime == null ? string.Empty : ToMillisecondString(dateTime.Value);

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToChineseDateString(this DateTime dateTime) =>
            string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);

        /// <summary>
        ///     获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToChineseDateString(this DateTime? dateTime) =>
            !dateTime.HasValue ? string.Empty : ToChineseDateString(dateTime.Value);

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <param name="isRemoveSecond">
        ///     是否移除秒
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToChineseDateTimeString(this DateTime dateTime, bool isRemoveSecond = false){
            var result = new StringBuilder();
            result.AppendFormat("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
            result.AppendFormat(" {0}时{1}分", dateTime.Hour, dateTime.Minute);
            if(isRemoveSecond == false){
                result.AppendFormat("{0}秒", dateTime.Second);
            }

            return result.ToString();
        }

        /// <summary>
        ///     获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">
        ///     日期
        /// </param>
        /// <param name="isRemoveSecond">
        ///     是否移除秒
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string ToChineseDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false) =>
            dateTime == null ? string.Empty : ToChineseDateTimeString(dateTime.Value);
    }

#endregion
}
