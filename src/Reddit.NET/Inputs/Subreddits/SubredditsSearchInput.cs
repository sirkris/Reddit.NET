using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsSearchInput : SubredditsSearchProfilesInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool show_users { get; set; }

        /// <summary>
        /// Search subreddits by title and description.
        /// </summary>
        /// <param name="q">a search query</param>
        /// <param name="showUsers">boolean value</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="sort">one of (relevance, activity)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        public SubredditsSearchInput(string q = "", bool showUsers = false, string after = "", string before = "", string sort = "relevance", int count = 0, int limit = 25, string show = "all",
            bool srDetail = false)
            : base(q, after, before, sort, count, limit, show, srDetail)
        {
            show_users = showUsers;
        }
    }
}
