using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCommunityListData
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("isSubscribed")]
        public bool IsSubscribed;

        [JsonProperty("iconUrl")]
        public string IconURL;

        [JsonProperty("subscribers")]
        public int Subscribers;

        [JsonProperty("primaryColor")]
        public string PrimaryColor;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("communityIcon")]
        public string CommunityIcon;
    }
}
