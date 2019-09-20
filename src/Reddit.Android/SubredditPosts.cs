using Android.App;
using Android.Content;
using System;

namespace Reddit.Android
{
    public static class SubredditPosts
    {
        /// <summary>
        /// Monitor Reddit for new "Best" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorBestAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorBestAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Best" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorBestAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorBestAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Best" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorBestAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "BestPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Hot" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorHotAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorHotAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Hot" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorHotAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorHotAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Hot" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorHotAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "HotPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "New" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorNewAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorNewAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "New" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorNewAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorNewAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "New" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorNewAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "NewPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Rising" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorRisingAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorRisingAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Rising" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorRisingAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorRisingAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Rising" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorRisingAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "RisingPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Top" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorTopAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorTopAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Top" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorTopAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorTopAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Top" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorTopAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "TopPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Controversial" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorControversialAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorControversialAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Controversial" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorControversialAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorControversialAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "Controversial" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorControversialAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "ControversialPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueue" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorModQueueAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueue" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorModQueueAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueue" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "ModQueuePosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueReports" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueReportsAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorModQueueReportsAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueReports" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueReportsAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorModQueueReportsAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueReports" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueReportsAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "ModQueueReportsPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueSpam" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueSpamAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorModQueueSpamAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueSpam" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueSpamAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorModQueueSpamAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueSpam" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueSpamAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "ModQueueSpamPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueUnmoderated" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueUnmoderatedAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorModQueueUnmoderatedAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueUnmoderated" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueUnmoderatedAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorModQueueUnmoderatedAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueUnmoderated" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueUnmoderatedAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "ModQueueUnmoderatedPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueEdited" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueEditedAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorModQueueEditedAndroid(subredditPosts, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueEdited" posts on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueEditedAndroid(this Controllers.SubredditPosts subredditPosts, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorModQueueEditedAndroid(subredditPosts, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor Reddit for new "ModQueueEdited" posts on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorModQueueEditedAndroid(this Controllers.SubredditPosts subredditPosts, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(subredditPosts, "ModQueueEditedPosts", subredditPosts.Subreddit, 0, out alarmIntent, out pendingIntent, lastRes);
        }
    }
}
