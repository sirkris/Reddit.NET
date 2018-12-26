using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class CommentsUpdateEventArgs
    {
        public List<Comment> OldComments { get; set; }
        public List<Comment> NewComments { get; set; }
        public List<Comment> Added { get; set; }
        public List<Comment> Removed { get; set; }
    }
}
