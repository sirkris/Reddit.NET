using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsAutocompleteV2Input : SubredditsAutocompleteInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool include_categories { get; set; }

        /// <summary>
        /// an integer between 1 and 10 (default: 5)
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// Version 2 of SubredditAutocomplete.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeProfiles">boolean value</param>
        /// <param name="limit">an integer between 1 and 10 (default: 5)</param>
        public SubredditsAutocompleteV2Input(string query = "", bool includeCategories = false, bool includeOver18 = true, bool includeProfiles = false, int limit = 5)
            : base(query, includeProfiles, includeOver18)
        {
            include_categories = includeCategories;
            this.limit = limit;
        }
    }
}
