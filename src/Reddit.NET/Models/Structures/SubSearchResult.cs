using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class SubSearchResult
    {
        [JsonProperty("active_user_count")]
        public int ActiveUserCount;

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
