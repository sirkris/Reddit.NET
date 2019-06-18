using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCommunityListData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isSubscribed")]
        public bool IsSubscribed { get; set; }

        [JsonProperty("iconUrl")]
        public string IconURL { get; set; }

        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        [JsonProperty("primaryColor")]
        public string PrimaryColor { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("communityIcon")]
        public string CommunityIcon { get; set; }
    }
}
