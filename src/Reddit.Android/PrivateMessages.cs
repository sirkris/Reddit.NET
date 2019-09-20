using Android.App;
using Android.Content;
using System;

namespace Reddit.Android
{
    public static class PrivateMessages
    {
        /// <summary>
        /// Monitor inbox messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorInboxAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorInboxAndroid(privateMessages, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor inbox messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorInboxAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorInboxAndroid(privateMessages, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor inbox messages on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorInboxAndroid(this Controllers.PrivateMessages privateMessages, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(privateMessages, "PrivateMessagesInbox", "PrivateMessages", 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor sent messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorSentAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorSentAndroid(privateMessages, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor sent messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorSentAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorSentAndroid(privateMessages, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor sent messages on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorSentAndroid(this Controllers.PrivateMessages privateMessages, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(privateMessages, "PrivateMessagesSent", "PrivateMessages", 0, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor unread messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorUnreadAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs = 15000, string lastRes = null)
        {
            return MonitorUnreadAndroid(privateMessages, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor unread messages on Android.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorUnreadAndroid(this Controllers.PrivateMessages privateMessages, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return MonitorUnreadAndroid(privateMessages, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent, lastRes);
        }

        /// <summary>
        /// Monitor unread messages on Android.
        /// </summary>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <param name="lastRes">Serialized JSON representation of the last query result (default: null)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorUnreadAndroid(this Controllers.PrivateMessages privateMessages, Type receiver, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent, string lastRes = null)
        {
            return Monitors.MonitorAndroid(privateMessages, "PrivateMessagesUnread", "PrivateMessages", 0, out alarmIntent, out pendingIntent, lastRes);
        }
    }
}
