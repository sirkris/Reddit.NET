using RedditThings = Reddit.Models.Structures;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class LiveThreadUpdatesUpdateEventArgs
    {
        public List<RedditThings.LiveUpdate> OldUpdates { get; set; }
        public List<RedditThings.LiveUpdate> NewUpdates { get; set; }
        public List<RedditThings.LiveUpdate> Added { get; set; }
        public List<RedditThings.LiveUpdate> Removed { get; set; }
    }
}
