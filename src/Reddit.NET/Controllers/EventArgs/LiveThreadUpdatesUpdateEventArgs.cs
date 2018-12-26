using Reddit.Things;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class LiveThreadUpdatesUpdateEventArgs
    {
        public List<Things.LiveUpdate> OldUpdates { get; set; }
        public List<Things.LiveUpdate> NewUpdates { get; set; }
        public List<Things.LiveUpdate> Added { get; set; }
        public List<Things.LiveUpdate> Removed { get; set; }
    }
}
