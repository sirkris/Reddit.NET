using RedditThings = Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NET.Controllers.EventArgs
{
    public class LiveThreadUpdatesUpdateEventArgs
    {
        public List<RedditThings.LiveUpdate> OldUpdates { get; set; }
        public List<RedditThings.LiveUpdate> NewUpdates { get; set; }
        public List<RedditThings.LiveUpdate> Added { get; set; }
        public List<RedditThings.LiveUpdate> Removed { get; set; }
    }
}
