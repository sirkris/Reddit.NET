using RedditThings = Reddit.Models.Structures;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class MessagesUpdateEventArgs
    {
        public List<RedditThings.Message> OldMessages { get; set; }
        public List<RedditThings.Message> NewMessages { get; set; }
        public List<RedditThings.Message> Added { get; set; }
        public List<RedditThings.Message> Removed { get; set; }
    }
}
