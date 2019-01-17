using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class SubSearchResult
    {
        [JsonProperty("active_user_count")]
        public int? ActiveUserCount;

        [JsonProperty("icon_img")]
        public string IconImg;

        [JsonProperty("key_color")]
        public string KeyColor;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("subscriber_count")]
        public int SubscriberCount;

        [JsonProperty("allow_images")]
        public bool AllowImages;
    }
}
