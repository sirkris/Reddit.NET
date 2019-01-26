using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class PostsUpdateEventArgs
    {
        public List<Post> OldPosts { get; set; }
        public List<Post> NewPosts { get; set; }
        public List<Post> Added { get; set; }
        public List<Post> Removed { get; set; }
    }
}
