using Reddit.Coordinators.Structures;

namespace Reddit.Models.EventArgs
{
    public class MonitoringUpdateEventArgs
    {
        public MonitoringSnapshot Added;
        public MonitoringSnapshot Removed;
    }
}
