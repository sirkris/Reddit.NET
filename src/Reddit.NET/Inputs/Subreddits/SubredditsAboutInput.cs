using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsAboutInput : CategorizedSrListingInput
    {
        /// <summary>
        /// A valid, existing reddit username
        /// </summary>
        public string user { get; set; }

        /// <summary>
        /// Get about data.
        /// </summary>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        public SubredditsAboutInput(string user = "", string after = null, string before = null, int count = 0, int limit = 25, string show = "all",
            bool srDetail = false, bool includeCategories = false)
            : base(after, before, count, limit, show, srDetail, includeCategories)
        {
            this.user = user;
        }
    }
}
