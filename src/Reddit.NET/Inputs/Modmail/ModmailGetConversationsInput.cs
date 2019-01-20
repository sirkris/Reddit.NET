using System;

namespace Reddit.Inputs.Modmail
{
    [Serializable]
    public class ModmailGetConversationsInput : ModmailBulkReadInput
    {
        /// <summary>
        /// base36 modmail conversation id
        /// </summary>
        public string after { get; set; }

        /// <summary>
        /// one of (recent, mod, user, unread)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// an integer (default: 25)
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// Get conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="sort">one of (recent, mod, user, unread)</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        public ModmailGetConversationsInput(string after = "", string entity = "", string sort = "unread", string state = "all", int limit = 25)
            : base(entity, state)
        {
            this.after = after;
            this.sort = sort;
            this.limit = limit;
        }
    }
}
