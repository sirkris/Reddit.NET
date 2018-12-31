using System;

namespace Reddit.Models.Inputs
{
    [Serializable]
    public class ListingInput
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
        /// a positive integer (default: 0)
        /// </summary>
        public int count { get; set; }
    }
}
