using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsReportInput : BaseInput
    {
        /// <summary>
        /// a string no longer than 2000 characters
        /// </summary>
        public string additional_info { get; set; }

        /// <summary>
        /// a string no longer than 1000 characters
        /// </summary>
        public string ban_evading_accounts_names { get; set; }

        /// <summary>
        /// a string no longer than 250 characters
        /// </summary>
        public string custom_text { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool from_help_center { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string other_reason { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string rule_reason { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string site_reason { get; set; }

        /// <summary>
        /// a string no longer than 1000 characters
        /// </summary>
        public string sr_name { get; set; }

        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string thing_id { get; set; }

        /// <summary>
        /// A valid Reddit username
        /// </summary>
        public string violator_username { get; set; }

        /// <summary>
        /// Report a link, comment or message.
        /// Reporting a thing brings it to the attention of the subreddit's moderators.
        /// Reporting a message sends it to a system for admin review.
        /// For links and comments, the thing is implicitly hidden as well (see /api/hide for details).
        /// </summary>
        /// <param name="additionalInfo">a string no longer than 2000 characters</param>
        /// <param name="banEvadingAccountsNames">a string no longer than 1000 characters</param>
        /// <param name="customText">a string no longer than 250 characters</param>
        /// <param name="fromHelpCenter">boolean value</param>
        /// <param name="otherReason">a string no longer than 100 characters</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        /// <param name="ruleReason">a string no longer than 100 characters</param>
        /// <param name="siteReason">a string no longer than 100 characters</param>
        /// <param name="srName">a string no longer than 1000 characters</param>
        /// <param name="thingId">fullname of a thing</param>
        /// <param name="violatorUsername">A valid Reddit username</param>
        public LinksAndCommentsReportInput(string additionalInfo = "", string banEvadingAccountsNames = "", string customText = "", bool fromHelpCenter = false,
            string otherReason = "", string reason = "", string ruleReason = "", string siteReason = "", string srName = "", string thingId = "",
            string violatorUsername = "")
        {
            additional_info = additionalInfo;
            ban_evading_accounts_names = banEvadingAccountsNames;
            custom_text = customText;
            from_help_center = fromHelpCenter;
            other_reason = otherReason;
            this.reason = reason;
            rule_reason = ruleReason;
            site_reason = siteReason;
            sr_name = srName;
            thing_id = thingId;
            violator_username = violatorUsername;
        }
    }
}
