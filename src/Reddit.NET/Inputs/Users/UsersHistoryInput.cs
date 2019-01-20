using System;

namespace Reddit.Inputs.Users
{
    [Serializable]
    public class UsersHistoryInput : CategorizedSrListingInput
    {
        /// <summary>
        /// one of (hour, day, week, month, year, all)
        /// </summary>
        public string t { get; set; }

        /// <summary>
        /// one of (hot, new, top, controversial)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// an integer between 2 and 10
        /// </summary>
        public int context { get; set; }

        /// <summary>
        /// One of (links, comments)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Input data for retrieving a user's post or comment history.
        /// </summary>
        /// <param name="type">One of (links, comments)</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="sort">one of (hot, new, top, controversial)</param>
        /// <param name="context">an integer between 2 and 10</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        public UsersHistoryInput(string type = "links", string t = "all", string sort = "new", int context = 3, string after = null, string before = null, int count = 0, int limit = 25, 
            string show = "all", bool srDetail = false, bool includeCategories = false)
            : base(after, before, count, limit, show, srDetail, includeCategories)
        {
            this.type = type;
            this.t = t;
            this.sort = sort;
            this.context = context;
        }
    }
}
