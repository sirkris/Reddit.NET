using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsAutocompleteInput : SubredditsQueryInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool include_profiles { get; set; }

        /// <summary>
        /// Return a list of subreddits and data for subreddits whose names start with 'query'.
        /// Uses typeahead endpoint to recieve the list of subreddits names. 
        /// Typeahead provides exact matches, typo correction, fuzzy matching and boosts subreddits to the top that the user is subscribed to.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="includeProfiles">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        public SubredditsAutocompleteInput(string query = "", bool includeProfiles = false, bool includeOver18 = true)
            : base(query, includeOver18)
        {
            include_profiles = includeProfiles;
        }
    }
}
