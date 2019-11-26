using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class PostsUpdateEventArgs
    {
        public IList<Post> OldPosts { get; set; }
        public IList<Post> NewPosts { get; set; }
        public IList<Post> Added { get; set; }
        public IList<Post> Removed { get; set; }
    }
}
