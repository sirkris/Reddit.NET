using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Snoomoji
    {
        [JsonProperty("url")]
        public string URL { get; set; }

        /// <summary>
        /// ID of user who created this flair.
        /// </summary>
        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("user_flair_allowed")]
        public bool UserFlairAllowed { get; set; }

        [JsonProperty("post_flair_allowed")]
        public bool PostFlairAllowed { get; set; }

        [JsonProperty("mod_flair_only")]
        public bool ModFlairOnly { get; set; }
    }
}
