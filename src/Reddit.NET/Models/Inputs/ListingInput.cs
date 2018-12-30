using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs
{
    [Serializable]
    public class ListingInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        [JsonProperty("after")]
        public string after { get; set; }

        /// <summary>
        /// fullname of a thing
        /// </summary>
        [JsonProperty("before")]
        public string before { get; set; }

        /// <summary>
        /// the maximum number of items desired
        /// </summary>
        [JsonProperty("limit")]
        public int limit { get; set; }

        /// <summary>
        /// a positive integer (default: 0)
        /// </summary>
        [JsonProperty("count")]
        public int count { get; set; }
    }
}
