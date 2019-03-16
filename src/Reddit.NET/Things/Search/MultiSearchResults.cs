using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class MultiSearchResults
    {
        public List<Post> Posts;
        public List<Subreddit> Subreddits;
        public List<User> Users;

        public MultiSearchResults(List<Post> posts = null, List<Subreddit> subreddits = null, List<User> users = null)
        {
            Posts = (posts != null ? posts : new List<Post>());
            Subreddits = (subreddits != null ? subreddits : new List<Subreddit>());
            Users = (users != null ? users : new List<User>());
        }
    }
}
