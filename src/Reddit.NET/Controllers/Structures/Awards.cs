using System;

namespace Reddit.Controllers.Structures
{
    [Serializable]
    public class Awards
    {
        /// <summary>
        /// The number of silver gildings (awards) received.
        /// </summary>
        public int Silver { get; private set; }

        /// <summary>
        /// The number of gold gildings (awards) received.
        /// </summary>
        public int Gold { get; private set; }

        /// <summary>
        /// The number of platinum gildings (awards) received.
        /// </summary>
        public int Platinum { get; private set; }

        /// <summary>
        /// The total number of gildings (awards) received.
        /// </summary>
        public int Count
        {
            get
            {
                return (Silver + Gold + Platinum);
            }
            private set { }
        }

        /// <summary>
        /// Initialize the Awards controller from a post.
        /// </summary>
        /// <param name="post">A valid Things.Post instance</param>
        public Awards(Things.Post post)
        {
            if (post.Gildings != null)
            {
                Silver = (post.Gildings.ContainsKey("gid_1") ? post.Gildings["gid_1"] : 0);
                Gold = (post.Gildings.ContainsKey("gid_2") ? post.Gildings["gid_2"] : 0);
                Platinum = (post.Gildings.ContainsKey("gid_3") ? post.Gildings["gid_3"] : 0);
            }
        }

        /// <summary>
        /// Initialize the Awards controller from a comment.
        /// </summary>
        /// <param name="post">A valid Things.Comment instance</param>
        public Awards(Things.Comment comment)
        {
            if (comment.Gildings != null)
            {
                Silver = (comment.Gildings.ContainsKey("gid_1") ? comment.Gildings["gid_1"] : 0);
                Gold = (comment.Gildings.ContainsKey("gid_2") ? comment.Gildings["gid_2"] : 0);
                Platinum = (comment.Gildings.ContainsKey("gid_3") ? comment.Gildings["gid_3"] : 0);
            }
        }

        /// <summary>
        /// Create an empty Awards controller instance.
        /// </summary>
        public Awards() { }
    }
}
