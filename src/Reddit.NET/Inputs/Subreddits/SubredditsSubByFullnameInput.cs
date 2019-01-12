using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsSubByFullnameInput : SubredditsSubscribeInput
    {
        /// <summary>
        /// A comma-separated list of subreddit fullnames
        /// </summary>
        public string sr { get; set; }

        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit.
        /// To subscribe, action should be sub.  To unsubscribe, action should be unsub.The user must have access to the subreddit to be able to subscribe to it.
        /// The skip_initial_defaults param can be set to True to prevent automatically subscribing the user to the current set of defaults when they take their first subscription action.
        /// Attempting to set it for an unsubscribe action will result in an error.
        /// </summary>
        /// <param name="sr">A comma-separated list of subreddit fullnames</param>
        /// <param name="action">one of (sub, unsub)</param>
        /// <param name="skipInitialDefaults">boolean value</param>
        public SubredditsSubByFullnameInput(string sr = "", string action = "sub", bool skipInitialDefaults = false)
            : base(action, skipInitialDefaults)
        {
            this.sr = sr;
        }
    }
}
