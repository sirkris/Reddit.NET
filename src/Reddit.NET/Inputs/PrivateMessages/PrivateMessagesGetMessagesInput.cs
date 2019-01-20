using System;

namespace Reddit.Inputs.PrivateMessages
{
    [Serializable]
    public class PrivateMessagesGetMessagesInput : CategorizedSrListingInput
    {
        /// <summary>
        /// one of (true, false)
        /// </summary>
        public bool mark { get; set; }

        /// <summary>
        /// TODO - Purpose unknown.
        /// </summary>
        public string mid { get; set; }

        /// <summary>
        /// Retrieve private messages.
        /// </summary>
        /// <param name="mark">one of (true, false)</param>
        /// <param name="mid"></param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        public PrivateMessagesGetMessagesInput(bool mark = false, string mid = "", string after = "", string before = "", bool includeCategories = false, int count = 0,
            int limit = 25, string show = "all", bool srDetail = false)
            : base(after, before, count, limit, show, srDetail, includeCategories)
        {
            this.mark = mark;
            this.mid = mid;
        }
    }
}
