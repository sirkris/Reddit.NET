using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class PostLinkFlair
    {
        // Assumed Titles based off content found -- Usually seems to be "text"

        [JsonProperty("e")]
        public string Type { get; set; }

        [JsonProperty("t")]
        public string Flair { get; set; }


    }
}