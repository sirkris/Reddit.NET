using RedditThings = Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NET.Controllers.EventArgs
{
    public class MessagesUpdateEventArgs
    {
        public List<RedditThings.Message> OldMessages { get; set; }
        public List<RedditThings.Message> NewMessages { get; set; }
        public List<RedditThings.Message> Added { get; set; }
        public List<RedditThings.Message> Removed { get; set; }
    }
}
