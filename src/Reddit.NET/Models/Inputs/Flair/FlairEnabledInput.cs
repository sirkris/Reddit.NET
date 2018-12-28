using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairEnabledInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        [JsonProperty("flair_enabled")]
        public bool FlairEnabled;
    }
}
