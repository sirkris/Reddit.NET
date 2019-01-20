using System;

namespace Reddit.Inputs.Listings
{
    [Serializable]
    public class ListingsGetDuplicatesInput : SrListingInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool crossposts_only { get; set; }

        /// <summary>
        /// one of (num_comments, new)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// subreddit name
        /// </summary>
        public string sr { get; set; }

        /// <summary>
        /// Return a list of other submissions of the same URL
        /// </summary>
        /// <param name="sr">subreddit name</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="crosspostsOnly">boolean value</param>
        /// <param name="sort">one of (num_comments, new)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        public ListingsGetDuplicatesInput(string sr = "", string after = "", string before = "", bool crosspostsOnly = false, string sort = "new",
            int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            this.after = after;
            this.before = before;
            crossposts_only = crosspostsOnly;
            this.sort = sort;
            this.sr = sr;
            this.count = count;
            this.limit = limit;
            this.show = show;
            sr_detail = srDetail;
        }
    }
}
