using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class MultiSearchResults
    {
        public List<Post> Posts { get; set; }
        public List<Subreddit> Subreddits { get; set; }
        public List<User> Users { get; set; }

        public string First { get; set; }
        public string Last { get; set; }

        public MultiSearchResults(List<Post> posts = null, List<Subreddit> subreddits = null, List<User> users = null, 
            string first = null, string last = null)
        {
            Posts = (posts != null ? posts : new List<Post>());
            Subreddits = (subreddits != null ? subreddits : new List<Subreddit>());
            Users = (users != null ? users : new List<User>());

            First = first;
            Last = last;
        }
    }
}
