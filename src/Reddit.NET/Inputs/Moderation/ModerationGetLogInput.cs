using System;

namespace Reddit.Inputs.Moderation
{
    [Serializable]
    public class ModerationGetLogInput : SrListingInput
    {
        /// <summary>
        /// (optional) a moderator filter
        /// </summary>
        public string mod { get; set; }

        /// <summary>
        /// one of (banuser, unbanuser, spamlink, removelink, approvelink, spamcomment, removecomment, approvecomment, addmoderator, invitemoderator, uninvitemoderator, 
        /// acceptmoderatorinvite, removemoderator, addcontributor, removecontributor, editsettings, editflair, distinguish, marknsfw, wikibanned, wikicontributor, wikiunbanned, wikipagelisted, 
        /// removewikicontributor, wikirevise, wikipermlevel, ignorereports, unignorereports, setpermissions, setsuggestedsort, sticky, unsticky, setcontestmode, unsetcontestmode, lock, unlock, 
        /// muteuser, unmuteuser, createrule, editrule, deleterule, spoiler, unspoiler, modmail_enrollment, community_styling, community_widgets, markoriginalcontent)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Get a list of recent moderation actions.
        /// Moderator actions taken within a subreddit are logged. This listing is a view of that log with various filters to aid in analyzing the information.
        /// The optional mod parameter can be a comma-delimited list of moderator names to restrict the results to, or the string a to restrict the results to admin actions taken within the subreddit.
        /// The type parameter is optional and if sent limits the log entries returned to only those of the type specified.
        /// </summary>
        /// <param name="type">one of (banuser, unbanuser, spamlink, removelink, approvelink, spamcomment, removecomment, approvecomment, addmoderator, invitemoderator, uninvitemoderator, 
        /// acceptmoderatorinvite, removemoderator, addcontributor, removecontributor, editsettings, editflair, distinguish, marknsfw, wikibanned, wikicontributor, wikiunbanned, wikipagelisted, 
        /// removewikicontributor, wikirevise, wikipermlevel, ignorereports, unignorereports, setpermissions, setsuggestedsort, sticky, unsticky, setcontestmode, unsetcontestmode, lock, unlock, 
        /// muteuser, unmuteuser, createrule, editrule, deleterule, spoiler, unspoiler, modmail_enrollment, community_styling, community_widgets, markoriginalcontent)</param>
        /// <param name="mod">(optional) a moderator filter</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 500)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="show">(optional) the string all</param>
        public ModerationGetLogInput(string type = "", string mod = "", string after = "", string before = "", int limit = 25, int count = 0, bool srDetail = false, string show = "all")
            : base(after, before, limit, count, srDetail, show)
        {
            this.mod = mod;
            this.type = type;
        }
    }
}
