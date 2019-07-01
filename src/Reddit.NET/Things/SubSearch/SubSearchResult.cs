using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubSearchResult
    {
        [JsonProperty("active_user_count")]
        public int? ActiveUserCount { get; set; }

        [JsonProperty("icon_img")]
        public string IconImg { get; set; }

        [JsonProperty("key_color")]
        public string KeyColor { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subscriber_count")]
        public int SubscriberCount { get; set; }

        [JsonProperty("allow_images")]
        public bool AllowImages { get; set; }
    }
}
