using Reddit.NET.Controllers.Structures;

namespace Reddit.NET.Models.EventArgs
{
    public class MonitoringUpdateEventArgs
    {
        public MonitoringSnapshot Added;
        public MonitoringSnapshot Removed;
    }
}
