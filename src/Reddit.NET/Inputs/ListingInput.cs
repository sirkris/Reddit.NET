using System;

namespace Reddit.Inputs
{
    [Serializable]
    public class ListingInput : BaseInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string after { get; set; }

        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string before { get; set; }

        /// <summary>
        /// the maximum number of items desired
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// a positive integer
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// Populate a new listing input.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        public ListingInput(string after = "", string before = "", int limit = 25, int count = 0)
        {
            this.after = after;
            this.before = before;
            this.limit = limit;
            this.count = count;
        }
    }
}
