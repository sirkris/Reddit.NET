using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsSearchNamesInput : SubredditsQueryInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool exact { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool include_unadvertisable { get; set; }

        /// <summary>
        /// List subreddit names that begin with a query string.
        /// Subreddits whose names begin with query will be returned.
        /// If include_over_18 is false, subreddits with over-18 content restrictions will be filtered from the results.
        /// If include_unadvertisable is False, subreddits that have hide_ads set to True or are on the anti_ads_subreddits list will be filtered.
        /// If exact is true, only an exact match will be returned. Exact matches are inclusive of over_18 subreddits, but not hide_ad subreddits when include_unadvertisable is False.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="exact">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeUnadvertisable">boolean value</param>
        public SubredditsSearchNamesInput(string query = "", bool exact = false, bool includeOver18 = true, bool includeUnadvertisable = true)
            : base(query, includeOver18)
        {
            this.exact = exact;
            include_unadvertisable = includeUnadvertisable;
        }
    }
}
