using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class CommentsUpdateEventArgs
    {
        public IList<Comment> OldComments { get; set; }
        public IList<Comment> NewComments { get; set; }
        public IList<Comment> Added { get; set; }
        public IList<Comment> Removed { get; set; }
    }
}
