namespace Reddit.Controllers.Structures
{
    public class MonitoringSchedule
    {
        /// <summary>
        /// Which days of the week this schedule applies to; set to null to run 7 days/week
        /// </summary>
        public MonitoringScheduleDays ScheduleDays { get; set; }

        /// <summary>
        /// The hour to start monitoring in 24-hour format (0 = midnight, 23 = 11 PM)
        /// </summary>
        public int StartHour { get; set; }

        /// <summary>
        /// The minute to start monitoring
        /// </summary>
        public int StartMinute { get; set; }

        /// <summary>
        /// The hour to stop monitoring in 24-hour format (0 = midnight, 23 = 11 PM)
        /// </summary>
        public int EndHour { get; set; }

        /// <summary>
        /// The minute to stop monitoring
        /// </summary>
        public int EndMinute { get; set; }

        /// <summary>
        /// Specifies a timeframe for when a thing should be monitored.
        /// If this instance is null, it means that the thing will be monitored 24/7.
        /// </summary>
        /// <param name="startHour">The hour to start monitoring in 24-hour format (0 = midnight, 23 = 11 PM)</param>
        /// <param name="startMinute">The minute to start monitoring</param>
        /// <param name="endHour">The hour to stop monitoring in 24-hour format (0 = midnight, 23 = 11 PM)</param>
        /// <param name="endMinute">The minute to stop monitoring</param>
        /// <param name="scheduleDays">Which days of the week this schedule applies to; leave null to run 7 days/week</param>
        public MonitoringSchedule(int startHour = 0, int startMinute = 0, int endHour = 23, int endMinute = 59, MonitoringScheduleDays scheduleDays = null)
        {
            StartHour = startHour;
            StartMinute = startMinute;
            EndHour = endHour;
            EndMinute = endMinute;

            if (scheduleDays != null)
            {
                ScheduleDays = scheduleDays;
            }
            else
            {
                ScheduleDays = new MonitoringScheduleDays();
            }
        }
    }
}
