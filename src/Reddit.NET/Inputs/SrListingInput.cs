using System;

namespace Reddit.Inputs
{
    [Serializable]
    public class SrListingInput : ListingInput
    {
        /// <summary>
        /// (optional) the string all
        /// </summary>
        public string show { get; set; }

        /// <summary>
        /// (optional) expand subreddits
        /// </summary>
        public bool sr_detail { get; set; }

        /// <summary>
        /// Populate a new subreddit listing input.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="show">(optional) the string all</param>
        public SrListingInput(string after = "", string before = "", int limit = 25, int count = 0, bool srDetail = false, string show = "all")
            : base(after, before, limit, count)
        {
            sr_detail = srDetail;
            this.show = show;
        }
    }
}
