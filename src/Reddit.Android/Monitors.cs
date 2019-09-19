using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace Reddit.Android
{
    public static class Monitors
    {
        /// <summary>
        /// Toggles monitoring of something on Android.
        /// </summary>
        /// <param name="key">A unique string identifying what's being monitored</param>
        /// <param name="subKey">An optional string providing additional information about what's being monitored</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query (default: 15000)</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorAndroid(this Controllers.Internal.Monitors monitors, string key, string subKey, int monitoringDelayMs = 15000)
        {
            return MonitorAndroid(monitors, key, subKey, monitoringDelayMs, out Intent alarmIntent, out PendingIntent pendingIntent);
        }

        /// <summary>
        /// Toggles monitoring of something on Android.
        /// </summary>
        /// <param name="key">A unique string identifying what's being monitored</param>
        /// <param name="subKey">An optional string providing additional information about what's being monitored</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorAndroid(this Controllers.Internal.Monitors monitors, string key, string subKey, int monitoringDelayMs,
            out Intent alarmIntent, out PendingIntent pendingIntent)
        {
            return MonitorAndroid(monitors, key, subKey, typeof(MonitoringReceiver), monitoringDelayMs, out alarmIntent, out pendingIntent);
        }

        /// <summary>
        /// Toggles monitoring of something on Android.
        /// </summary>
        /// <param name="key">A unique string identifying what's being monitored</param>
        /// <param name="subKey">An optional string providing additional information about what's being monitored</param>
        /// <param name="receiver">A valid BroadcastReceiver</param>
        /// <param name="monitoringDelayMs">The number of milliseconds to wait between each monitoring query</param>
        /// <param name="alarmIntent">(out) The resulting alarm Intent</param>
        /// <param name="pendingIntent">(out) The resulting PendingIntent</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public static bool MonitorAndroid(this Controllers.Internal.Monitors monitors, string key, string subKey, Type receiver, int monitoringDelayMs, 
            out Intent alarmIntent, out PendingIntent pendingIntent)
        {
            alarmIntent = new Intent(Application.Context, receiver);
            alarmIntent.PutExtra("key", key);
            alarmIntent.PutExtra("subKey", subKey);
            alarmIntent.PutExtra("monitoringDelayMs", monitoringDelayMs);
            alarmIntent.PutExtra("appId", monitors.Dispatch.OAuthCredentials.AppID);
            alarmIntent.PutExtra("appSecret", monitors.Dispatch.OAuthCredentials.AppSecret);
            alarmIntent.PutExtra("accessToken", monitors.Dispatch.OAuthCredentials.AccessToken);
            alarmIntent.PutExtra("refreshToken", monitors.Dispatch.OAuthCredentials.RefreshToken);

            pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            
            bool alreadyScheduled = IsScheduled(alarmIntent);
            if (!IsScheduled(alarmIntent))
            {
                alarmManager.SetExactAndAllowWhileIdle(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + monitoringDelayMs, pendingIntent);

                return true;
            }
            else
            {
                alarmManager.Cancel(pendingIntent);
                pendingIntent.Cancel();
                
                return false;
            }
        }

        /// <summary>
        /// Checks whether a given intent is currently scheduled.
        /// </summary>
        /// <param name="intent">The intent to check</param>
        /// <returns>Whether the given intent is currently scheduled.</returns>
        public static bool IsScheduled(Intent intent)
        {
            return (PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.NoCreate) != null);
        }
    }
}
