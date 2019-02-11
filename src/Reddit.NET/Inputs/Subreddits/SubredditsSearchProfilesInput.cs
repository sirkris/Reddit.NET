using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsSearchProfilesInput : SrListingInput
    {
        /// <summary>
        /// a search query
        /// </summary>
        public string q { get; set; }

        /// <summary>
        /// one of (relevance, activity)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// Search user profiles by title and description.
        /// </summary>
        /// <param name="q">a search query</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="sort">one of (relevance, activity)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        public SubredditsSearchProfilesInput(string q = "", string after = "", string before = "", string sort = "relevance", int count = 0, int limit = 25, string show = "all",
            bool srDetail = false)
            : base(after, before, limit, count, srDetail, show)
        {
            this.q = q;
            this.sort = sort;
        }
    }
}
