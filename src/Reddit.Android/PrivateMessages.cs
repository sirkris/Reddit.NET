using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Reddit.Android
{
    public static class PrivateMessages
    {
        /// <summary>
        /// Monitor inbox messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorInboxAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs = 15000)
        {
            return MonitorInboxAndroid(privateMessages, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent);
        }

        /// <summary>
        /// Monitor inbox messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorInboxAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent)
        {
            return MonitorInboxAndroid(privateMessages, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent);
        }

        /// <summary>
        /// Monitor inbox messages on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorInboxAndroid(this Controllers.PrivateMessages privateMessages, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent)
        {
            return Monitors.MonitorAndroid(privateMessages, "PrivateMessagesInbox", "PrivateMessages", 0, out alarmIntent, out pendingIntent);
        }
    }
}
