using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class Info
    {
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Subreddit> Subreddits { get; set; }

        public Info(List<Post> posts, List<Comment> comments, List<Subreddit> subreddits)
        {
            Posts = posts;
            Comments = comments;
            Subreddits = subreddits;
        }

        public Info() { }
    }
}
