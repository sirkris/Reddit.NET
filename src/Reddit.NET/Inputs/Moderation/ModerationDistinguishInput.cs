using System;

namespace Reddit.Inputs.Moderation
{
    [Serializable]
    public class ModerationDistinguishInput : APITypeInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// one of (yes, no, admin, special)
        /// </summary>
        public string how { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? sticky { get; set; }

        /// <summary>
        /// Distinguish a thing's author with a sigil.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a link or comment of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish. depends on user.
        /// The first time a top-level comment is moderator distinguished, the author of the link the comment is in reply to will get a notification in their inbox.
        /// sticky is a boolean flag for comments, which will stick the distingushed comment to the top of all comments threads.
        /// If a comment is marked sticky, it will override any other stickied comment for that link (as only one comment may be stickied at a time). Only top-level comments may be stickied.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="how">one of (yes, no, admin, special)</param>
        /// <param name="sticky">boolean value</param>
        public ModerationDistinguishInput(string id = "", string how = "yes", bool? sticky = null)
            : base()
        {
            this.id = id;
            this.how = how;
            this.sticky = sticky;
        }
    }
}
