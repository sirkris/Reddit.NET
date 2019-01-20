

namespace Reddit.Coordinators.EventArgs
{
    public class WikiPageUpdateEventArgs
    {
        public WikiPage OldPage { get; set; }
        public WikiPage NewPage { get; set; }
    }
}
