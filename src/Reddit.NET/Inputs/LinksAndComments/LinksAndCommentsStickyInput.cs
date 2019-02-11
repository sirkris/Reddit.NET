using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsStickyInput : APITypeInput
    {
        /// <summary>
        /// fullname of a link
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// an integer between 1 and 4
        /// </summary>
        public int num { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool state { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool to_profile { get; set; }

        /// <summary>
        /// Set or unset a Link as the sticky in its subreddit.
        /// state is a boolean that indicates whether to sticky or unsticky this post - true to sticky, false to unsticky.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        /// <param name="num">an integer between 1 and 4</param>
        /// <param name="state">boolean value</param>
        /// <param name="toProfile">boolean value</param>
        public LinksAndCommentsStickyInput(string id = "", int num = 1, bool state = true, bool toProfile = false)
            : base()
        {
            this.id = id;
            this.num = num;
            this.state = state;
            to_profile = toProfile;
        }
    }
}
