using Reddit.Controllers.Structures;

namespace Reddit.Models.EventArgs
{
    public class MonitoringUpdateEventArgs
    {
        public MonitoringSnapshot Added;
        public MonitoringSnapshot Removed;
    }
}
