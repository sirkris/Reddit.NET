using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairTypeInput
    {
        /// <summary>
        /// one of (USER_FLAIR, LINK_FLAIR)
        /// </summary>
        [JsonProperty("flair_type")]
        public string FlairType;
    }
}
