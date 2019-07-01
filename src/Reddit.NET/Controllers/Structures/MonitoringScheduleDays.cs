using Reddit.Exceptions;
using System;

namespace Reddit.Controllers.Structures
{
    public class MonitoringScheduleDays
    {
        /// <summary>
        /// Whether to monitor on Sundays.
        /// </summary>
        public bool Sunday { get; set; }

        /// <summary>
        /// Whether to monitor on Mondays.
        /// </summary>
        public bool Monday { get; set; }

        /// <summary>
        /// Whether to monitor on Tuesdays.
        /// </summary>
        public bool Tuesday { get; set; }

        /// <summary>
        /// Whether to monitor on Wednesdays.
        /// </summary>
        public bool Wednesday { get; set; }

        /// <summary>
        /// Whether to monitor on Thursdays.
        /// </summary>
        public bool Thursday { get; set; }

        /// <summary>
        /// Whether to monitor on Fridays.
        /// </summary>
        public bool Friday { get; set; }

        /// <summary>
        /// Whether to monitor on Saturdays.
        /// </summary>
        public bool Saturday { get; set; }

        /// <summary>
        /// Create a new instance that specifies which days of the week a thing should be monitored.
        /// </summary>
        /// <param name="sunday">If true, monitor on Sundays</param>
        /// <param name="monday">If true, monitor on Mondays</param>
        /// <param name="tuesday">If true, monitor on Tuesdays</param>
        /// <param name="wednesday">If true, monitor on Wednesdays</param>
        /// <param name="thursday">If true, monitor on Thursdays</param>
        /// <param name="friday">If true, monitor on Fridays</param>
        /// <param name="saturday">If true, monitor on Saturdays</param>
        public MonitoringScheduleDays(bool sunday = true, bool monday = true, bool tuesday = true, bool wednesday = true,
            bool thursday = true, bool friday = true, bool saturday = true)
        {
            Sunday = sunday;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
        }

        /// <summary>
        /// Check to see if today is part of the schedule.
        /// </summary>
        /// <returns>Returns true if we're scheduled to run today, false otherwise.</returns>
        public bool IsScheduledToday()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                default:
                    throw new RedditControllerException("Has another day of the week been invented or is this just null for some reason?  This should never happen.");
                case DayOfWeek.Sunday:
                    return Sunday;
                case DayOfWeek.Monday:
                    return Monday;
                case DayOfWeek.Tuesday:
                    return Tuesday;
                case DayOfWeek.Wednesday:
                    return Wednesday;
                case DayOfWeek.Thursday:
                    return Thursday;
                case DayOfWeek.Friday:
                    return Friday;
                case DayOfWeek.Saturday:
                    return Saturday;
            }
        }
    }
}
