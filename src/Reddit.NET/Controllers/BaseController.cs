using Newtonsoft.Json;
using Reddit.NET.Exceptions;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Internal;
using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Reddit.NET.Controllers
{
    public abstract class BaseController : Validators
    {
        internal Models.Internal.Monitor MonitorNull = null;
        internal MonitoringSnapshot MonitoringSnapshotNull = null;
        
        internal abstract ref Models.Internal.Monitor MonitorModel { get; }
        internal abstract ref MonitoringSnapshot Monitoring { get; }

        public int MonitoringWaitDelayMS = 1500;

        internal Dictionary<string, Thread> Threads;

        protected volatile bool Terminate = false;

        public Listings Listings;

        public BaseController()
        {
            Threads = new Dictionary<string, Thread>();
            Listings = new Listings();
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

        protected void LaunchThreadIfNotNull(string key, Thread thread)
        {
            if (thread != null)
            {
                Threads.Add(key, thread);
                Threads[key].Start();
                while (!Threads[key].IsAlive) { }
            }
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
    }
}
