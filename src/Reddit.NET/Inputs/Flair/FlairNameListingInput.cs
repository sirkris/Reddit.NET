using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairNameListingInput : SrListingInput
    {
        /// <summary>
        /// a user by name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="name">a user by name</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 1000)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        public FlairNameListingInput(string name = "", string after = "", string before = "", int limit = 25, int count = 0,
            string show = "all", bool srDetail = false)
        {
            this.name = name;
            this.after = after;
            this.before = before;
            this.limit = limit;
            this.count = count;
            this.show = show;
            sr_detail = srDetail;
        }
    }
}
