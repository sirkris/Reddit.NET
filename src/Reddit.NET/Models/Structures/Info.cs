using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class Info
    {
        public List<Post> Posts;
        public List<Comment> Comments;
        public List<Subreddit> Subreddits;

        public Info(List<Post> posts, List<Comment> comments, List<Subreddit> subreddits)
        {
            Posts = posts;
            Comments = comments;
            Subreddits = subreddits;
        }

        public Info() { }
    }
}
