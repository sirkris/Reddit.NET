using System.Collections.Generic;

namespace Reddit.Coordinators.EventArgs
{
    public class WikiPagesUpdateEventArgs
    {
        public List<string> OldPages { get; set; }
        public List<string> NewPages { get; set; }
        public List<string> Added { get; set; }
        public List<string> Removed { get; set; }
    }
}
