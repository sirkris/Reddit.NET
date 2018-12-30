using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs
{
    [Serializable]
    public class SrListingInput : ListingInput
    {
        /// <summary>
        /// (optional) the string all
        /// </summary>
        [JsonProperty("show")]
        public string show { get; set; }

        /// <summary>
        /// (optional) expand subreddits
        /// </summary>
        [JsonProperty("sr_detail")]
        public bool sr_detail { get; set; }
    }
}
