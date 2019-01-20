using System;

namespace Reddit.Inputs.Moderation
{
    [Serializable]
    public class ModerationModQueueInput : SrListingInput
    {
        /// <summary>
        /// one of (links, comments)
        /// </summary>
        public string only { get; set; }

        /// <summary>
        /// Return a listing of posts relevant to moderators.
        /// reports: Things that have been reported.
        /// spam: Things that have been marked as spam or otherwise removed.
        /// modqueue: Things requiring moderator review, such as reported things and items caught by the spam filter.
        /// unmoderated: Things that have yet to be approved/removed by a mod.
        /// edited: Things that have been edited recently.
        /// Requires the "posts" moderator permission for the subreddit.
        /// </summary>
        /// <param name="only">one of (links, comments)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 500)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="show">(optional) the string all</param>
        public ModerationModQueueInput(string only = "links", string after = "", string before = "", int limit = 25, int count = 0,
            bool srDetail = false, string show = "all")
            : base(after, before, limit, count, srDetail, show)
        {
            this.only = only;
        }
    }
}
