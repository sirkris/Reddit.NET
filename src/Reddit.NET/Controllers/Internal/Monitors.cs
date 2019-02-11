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

        public Monitors() : base()
        {
            Threads = new Dictionary<string, Thread>();
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
            if (Monitoring.Get(key).Contains(subKey))
            {
                // Stop monitoring.  --Kris
                MonitorModel.RemoveMonitoringKey(key, subKey, ref Monitoring);
                WaitOrDie(Threads[key]);

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

        // TODO - Preserve custom thread monitoring delays when they're rebuilt (occurs whenever a new monitoring thread starts or an existing one stops).  --Kris
        protected void RebuildThreads(string subKey)
        {
            List<string> oldThreads = new List<string>(Threads.Keys);
            KillThreads(oldThreads);

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

        protected void KillThreads(List<string> oldThreads)
        {
            TerminateThread();

            foreach (string key in oldThreads)
            {
                try
                {
                    Threads[key].Join();
                }
                catch (Exception) { }

                Threads.Remove(key);
            }

            ReviveThread();
        }

        protected abstract Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null);
    }
}
