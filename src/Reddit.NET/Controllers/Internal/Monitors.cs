﻿using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Reddit.Controllers.Internal
{
    public abstract class Monitors : BaseController
    {
        public int MonitoringWaitDelayMS = 1500;

        internal Dictionary<string, ThreadWrapper> Threads;

        protected volatile bool Terminate = false;

        internal abstract Models.Internal.Monitor MonitorModel { get; }
        internal abstract ref MonitoringSnapshot Monitoring { get; }
        internal abstract bool BreakOnFailure { get; set; }
        internal abstract List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal abstract DateTime? MonitoringExpiration { get; set; }
        internal abstract HashSet<string> UseCache { get; set; }

        /// <summary>
        /// An optional cache for preventing the same post from appearing multiple times during monitoring.
        /// See: https://github.com/sirkris/Reddit.NET/issues/117#issuecomment-759501039
        /// </summary>
        public IDictionary<string, HashSet<string>> MonitoringCache { get; internal set; }

        public Monitors() : base()
        {
            Threads = new Dictionary<string, ThreadWrapper>();
            BreakOnFailure = false;
            MonitoringSchedule = new List<MonitoringSchedule> { null };  // Monitor 24/7 by default.  --Kris
        }

        protected bool Monitor(string key, ThreadWrapper thread, string subKey)
        {
            bool res = Monitor(key, thread, subKey, out ThreadWrapper newThread);

            RebuildThreads(subKey);
            LaunchThreadIfNotNull(key, newThread);

            return res;
        }

        internal bool Monitor(string key, ThreadWrapper thread, string subKey, out ThreadWrapper newThread)
        {
            newThread = null;
            if (IsMonitored(key, subKey))
            {
                // Stop monitoring.  --Kris
                TerminateThread();

                MonitorModel.RemoveMonitoringKey(key, subKey, ref Monitoring);
                WaitOrDie(key);

                Threads.Remove(key);

                return false;
            }
            else
            {
                // Start monitoring.  --Kris
                MonitorModel.AddMonitoringKey(key, subKey, ref Monitoring);

                newThread = thread;

                return true;
            }
        }

        public void Wait(int ms)
        {
            DateTime start = DateTime.Now;
            while (start.AddMilliseconds(ms) > DateTime.Now
                && !Terminate)
            {
                int sleepMs = (int)(start.AddMilliseconds(ms) - DateTime.Now).TotalMilliseconds;
                sleepMs = (sleepMs < 100 ? 100 : sleepMs);
                sleepMs = (sleepMs > 3000 ? 3000 : sleepMs);

                Thread.Sleep(sleepMs);
            }
        }

        public bool IsMonitored(string key, string subKey)
        {
            return Monitoring.Get(key).Contains(subKey);
        }

        public bool IsScheduled()
        {
            DateTime now = DateTime.Now;
            foreach (MonitoringSchedule monitoringSchedule in MonitoringSchedule)
            {
                if (monitoringSchedule == null)
                {
                    return true;
                }

                DateTime start = new DateTime(now.Year, now.Month, now.Day, monitoringSchedule.StartHour, monitoringSchedule.StartMinute, 0);
                DateTime end = new DateTime(now.Year, now.Month, now.Day, monitoringSchedule.EndHour, monitoringSchedule.EndMinute, 0);

                if (monitoringSchedule.ScheduleDays.IsScheduledToday()
                    && now >= start
                    && now <= end)
                {
                    return true;
                }
            }

            return false;
        }

        // TODO - Preserve custom thread monitoring delays when they're rebuilt (occurs whenever a new monitoring thread starts or an existing one stops).  --Kris
        protected void RebuildThreads(string subKey)
        {
            List<(string key, object options)> oldThreads =
                Threads.Select(t => new {t.Key, t.Value.Options})
                    .AsEnumerable()
                    .Select(t => (t.Key, t.Options))
                    .ToList();
            List<string> oldThreadKeys = oldThreads.Select(o => o.key).ToList();
            
            ResetThreads(oldThreadKeys);

            int i = 0;
            foreach ((string key, object options) in oldThreads)
            {
                Threads.Add(key, CreateMonitoringThread(key, subKey, (i * MonitoringWaitDelayMS), options: options));
                Threads[key].Thread.Start();
                i++;
            }
        }

        protected void LaunchThreadIfNotNull(string key, ThreadWrapper thread)
        {
            if (thread != null)
            {
                Threads.Add(key, thread);
                Threads[key].Thread.Start();
                while (!Threads[key].Thread.IsAlive) { }
            }
        }

        internal void TerminateThread()
        {
            Terminate = true;
        }

        internal void ReviveThread()
        {
            Terminate = false;
        }

        public void WaitOrDie(string key, int timeout = 60)
        {
            if (Threads.ContainsKey(key))
            {
                ThreadWrapper thread = Threads[key];

                KillThread(key);
                WaitOrDie(thread.Thread, timeout);
            }
        }

        public void WaitOrDie(Thread thread, int timeout = 60)
        {
            DateTime start = DateTime.Now;
            while (thread.IsAlive)
            {
                if (start.AddSeconds(timeout) <= DateTime.Now)
                {
                    // Thread.Abort was removed from .NET Core.  --Kris
                    throw new RedditControllerException("Unable to terminate monitoring thread (thread not responding).");
                }
            }
        }

        protected void KillThread(Thread thread)
        {
            try
            {
                thread.Join();
            }
            catch (Exception) { }
        }

        protected void KillThread(string key)
        {
            KillThread(Threads[key].Thread);

            Threads.Remove(key);
        }

        public void KillAllMonitoringThreads()
        {
            foreach (KeyValuePair<string, ThreadWrapper> pair in Threads)
            {
                KillThread(pair.Key);
            }
        }

        protected void ResetThreads(List<string> oldThreads)
        {
            TerminateThread();

            foreach (string key in oldThreads)
            {
                KillThread(key);
            }

            ReviveThread();
        }

        /// <summary>
        /// Initializes the monitoring cache properties.
        /// </summary>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires</param>
        /// <param name="type">Which monitoring sort's cache to initialize</param>
        internal void InitMonitoringCache(bool useCache, string type)
        {
            if (useCache)
            {
                MonitoringCache[type] = new HashSet<string>();
                if (!UseCache.Contains(type))
                {
                    UseCache.Add(type);
                }
            }
            else if (UseCache.Contains(type))
            {
                UseCache.Remove(type);
            }
        }

        /// <summary>
        /// Creates a new monitoring thread.
        /// </summary>
        /// <param name="key">Monitoring key</param>
        /// <param name="subKey">Monitoring subKey</param>
        /// <param name="startDelayMs">How long to wait before starting the thread in milliseconds (default: 0)</param>
        /// <param name="monitoringDelayMs">How long to wait between monitoring queries; pass null to leave it auto-managed (default: null)</param>
        /// <param name="options">Implementation-specific options.</param>
        /// <returns>The newly-created monitoring thread.</returns>
        protected abstract ThreadWrapper CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null, object options = null);
    }
}
