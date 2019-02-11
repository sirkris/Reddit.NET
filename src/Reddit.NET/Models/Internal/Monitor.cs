using ControlStructures = Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Models.EventArgs;
using System;

namespace Reddit.Models.Internal
{
    internal class Monitor
    {
        internal ControlStructures.MonitoringSnapshot Monitoring;
        internal event EventHandler<MonitoringUpdateEventArgs> MonitoringUpdated;

        internal Monitor()
        {
            Monitoring = new ControlStructures.MonitoringSnapshot();
        }

        internal virtual void UpdateMonitoring(MonitoringUpdateEventArgs e)
        {
            Monitoring.Remove(e.Removed);
            Monitoring.Add(e.Added);
        }

        protected virtual void OnMonitoringUpdated(MonitoringUpdateEventArgs e)
        {
            MonitoringUpdated?.Invoke(this, e);
        }

        internal void AddMonitoringKey(string key, string subKey, ref ControlStructures.MonitoringSnapshot monitoring)
        {
            ControlStructures.MonitoringSnapshot added = new ControlStructures.MonitoringSnapshot();
            if (monitoring.Get(key).Contains(subKey))
            {
                throw new RedditMonitoringException("That object is already being monitored.");
            }
            else
            {
                monitoring.Get(key).Add(subKey);
                added.Get(key).Add(subKey);
            }

            UpdateMonitoringArgs(added, null);
        }

        internal void RemoveMonitoringKey(string key, string subKey, ref ControlStructures.MonitoringSnapshot monitoring)
        {
            ControlStructures.MonitoringSnapshot removed = new ControlStructures.MonitoringSnapshot();
            if (monitoring.Get(key).Contains(subKey))
            {
                monitoring.Get(key).Remove(subKey);
                removed.Get(key).Add(subKey);
            }
            else
            {
                throw new RedditMonitoringException("That object is not being monitored.");
            }

            UpdateMonitoringArgs(null, removed);
        }

        private void UpdateMonitoringArgs(ControlStructures.MonitoringSnapshot added, ControlStructures.MonitoringSnapshot removed)
        {
            // Event handler to populate Monitoring across all controllers.  --Kris
            MonitoringUpdateEventArgs args = new MonitoringUpdateEventArgs
            {
                Added = added,
                Removed = removed
            };
            OnMonitoringUpdated(args);
        }
    }
}
