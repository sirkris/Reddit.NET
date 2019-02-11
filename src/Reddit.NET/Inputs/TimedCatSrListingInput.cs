using System;

namespace Reddit.Inputs
{
    [Serializable]
    public class TimedCatSrListingInput : CategorizedSrListingInput
    {
        /// <summary>
        /// one of (hour, day, week, month, year, all)
        /// </summary>
        public string t { get; set; }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        public TimedCatSrListingInput(string t = "all", string after = null, string before = null, bool includeCategories = false, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
            : base(after, before, count, limit, show, srDetail, includeCategories)
        {
            this.t = t;
        }
    }
}
