using System;

namespace Reddit.Things
{
    [Serializable]
    public class CommentOrPost
    {
        public Comment Comment { get; set; }
        public Post Post { get; set; }

        // Note - Either Comment or Post should always be null.  --Kris
        public CommentOrPost(Comment comment = null, Post post = null)
        {
            Comment = comment;
            Post = post;
        }

        public CommentOrPost() { }
    }
}
