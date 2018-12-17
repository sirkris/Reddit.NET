

namespace Reddit.NET.Controllers.EventArgs
{
    public class WikiPageUpdateEventArgs
    {
        public WikiPage OldPage { get; set; }
        public WikiPage NewPage { get; set; }
    }
}
