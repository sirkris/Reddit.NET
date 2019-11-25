using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsQueryInput : BaseInput
    {
        /// <summary>
        /// a string up to 50 characters long, consisting of printable characters
        /// </summary>
        public string query { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool include_over_18 { get; set; }

        /// <summary>
        /// Set data pertaining to a subreddit search query.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="includeOver18">boolean value</param>
        public SubredditsQueryInput(string query = "", bool includeOver18 = true)
        {
            this.query = query;
            include_over_18 = includeOver18;
        }
    }
}
