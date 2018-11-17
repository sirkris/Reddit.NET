using System.Collections.Generic;

namespace Reddit.NET.Controllers.EventArgs
{
    public class MonitoringUpdateEventArgs
    {
        public Dictionary<string, List<string>> Monitoring { get; set; }
    }
}
