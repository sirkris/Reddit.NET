using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
