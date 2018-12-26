using RedditThings = Reddit.Models.Structures;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class LiveThreadContributorsUpdateEventArgs
    {
        public List<RedditThings.UserListContainer> OldContributors { get; set; }
        public List<RedditThings.UserListContainer> NewContributors { get; set; }
        public List<RedditThings.UserListContainer> Added { get; set; }
        public List<RedditThings.UserListContainer> Removed { get; set; }
    }
}
