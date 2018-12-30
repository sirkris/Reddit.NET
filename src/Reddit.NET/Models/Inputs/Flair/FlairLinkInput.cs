using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairLinkInput : FlairNameInput
    {
        /// <summary>
        /// a fullname of a link
        /// </summary>
        [JsonProperty("link")]
        public string link { get; set; }
    }
}
