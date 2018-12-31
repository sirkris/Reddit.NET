using System;

namespace Reddit.Models.Inputs
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
    }
}
