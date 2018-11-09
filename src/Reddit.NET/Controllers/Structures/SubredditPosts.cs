using System.Collections.Generic;

namespace Reddit.NET.Controllers.Structures
{
    public class SubredditPosts
    {
        public List<Post> Best;
        public List<Post> Hot;
        public List<Post> New;
        public List<Post> Rising;
        public List<Post> Top;
        public List<Post> Controversial;

        public SubredditPosts(List<Post> best = null, List<Post> hot = null, List<Post> newPosts = null,
            List<Post> rising = null, List<Post> top = null, List<Post> controversial = null)
        {
            Best = best ?? new List<Post>();
            Hot = hot ?? new List<Post>();
            New = newPosts ?? new List<Post>();
            Rising = rising ?? new List<Post>();
            Top = top ?? new List<Post>();
            Controversial = controversial ?? new List<Post>();
        }
    }
}
