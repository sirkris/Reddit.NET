using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.Controllers.Internal
{
    public abstract class Monitors : BaseController
    {
        public int MonitoringWaitDelayMS = 1500;

        internal Dictionary<string, Thread> Threads;

        protected volatile bool Terminate = false;

        internal abstract Models.Internal.Monitor MonitorModel { get; }
        internal abstract ref MonitoringSnapshot Monitoring { get; }
        internal abstract bool BreakOnFailure { get; set; }
        internal abstract List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal abstract DateTime? MonitoringExpiration { get; set; }

        public Monitors() : base()
        {
            Threads = new Dictionary<string, Thread>();
            BreakOnFailure = false;
            MonitoringSchedule = new List<MonitoringSchedule> { null };  // Monitor 24/7 by default.  --Kris
        }

        protected bool Monitor(string key, Thread thread, string subKey)
        {
            bool res = Monitor(key, thread, subKey, out Thread newThread);

            RebuildThreads(subKey);
            LaunchThreadIfNotNull(key, newThread);

            return res;
        }

        internal bool Monitor(string key, Thread thread, string subKey, out Thread newThread)
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
            List<string> oldThreads = new List<string>(Threads.Keys);
            ResetThreads(oldThreads);

            int i = 0;
            foreach (string key in oldThreads)
            {
                Threads.Add(key, CreateMonitoringThread(key, subKey, (i * MonitoringWaitDelayMS)));
                Threads[key].Start();
                i++;
            }
        }

        protected void LaunchThreadIfNotNull(string key, Thread thread)
        {
            if (thread != null)
            {
                Threads.Add(key, thread);
                Threads[key].Start();
                while (!Threads[key].IsAlive) { }
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
                Thread thread = Threads[key];

                KillThread(key);
                WaitOrDie(thread, timeout);
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
            KillThread(Threads[key]);

            Threads.Remove(key);
        }

        public void KillAllMonitoringThreads()
        {
            foreach (KeyValuePair<string, Thread> pair in Threads)
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

        protected abstract Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null);
    }
}
