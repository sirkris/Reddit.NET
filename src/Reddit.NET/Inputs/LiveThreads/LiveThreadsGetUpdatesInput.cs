using System;

namespace Reddit.Inputs.LiveThreads
{
    [Serializable]
    public class LiveThreadsGetUpdatesInput : ListingInput
    {
        /// <summary>
        /// subreddit name
        /// </summary>
        public string stylesr { get; set; }

        /// <summary>
        /// Get a list of updates posted in this thread.
        /// </summary>
        /// <param name="styleSr">subreddit name</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        public LiveThreadsGetUpdatesInput(string styleSr = "", string after = "", string before = "", int limit = 25, int count = 0)
            : base(after, before, limit, count)
        {
            stylesr = styleSr;
        }
    }
}
