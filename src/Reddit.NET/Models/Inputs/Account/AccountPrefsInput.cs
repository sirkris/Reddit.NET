using System;

namespace Reddit.Models.Inputs.Account
{
    [Serializable]
    public class AccountPrefsInput : SrListingInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool include_categories;

        /// <summary>
        /// Get users with whom the current user has friended, blocked, or trusted.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        public AccountPrefsInput(string after = null, string before = null, int count = 0, int limit = 25, string show = "all",
            bool srDetail = false, bool includeCategories = false)
        {
            this.after = after;
            this.before = before;
            this.count = count;
            this.limit = limit;
            this.show = show;
            sr_detail = srDetail;
            include_categories = includeCategories;
        }
    }
}
