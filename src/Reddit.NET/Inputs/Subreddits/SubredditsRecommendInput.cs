using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsRecommendInput : BaseInput
    {
        /// <summary>
        /// comma-delimited list of subreddit names
        /// </summary>
        public string omit { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool over_18 { get; set; }

        /// <summary>
        /// Return subreddits recommended for the given subreddit(s).
        /// Gets a list of subreddits recommended for srnames, filtering out any that appear in the optional omit param.
        /// </summary>
        /// <param name="omit">comma-delimited list of subreddit names</param>
        /// <param name="over18">boolean value</param>
        public SubredditsRecommendInput(string omit = "", bool over18 = false)
        {
            this.omit = omit;
            over_18 = over18;
        }
    }
}
