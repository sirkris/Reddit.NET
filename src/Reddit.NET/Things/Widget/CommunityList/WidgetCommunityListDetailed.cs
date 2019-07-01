using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCommunityListDetailed : BaseContainer
    {
        [JsonProperty("data")]
        public List<WidgetCommunityListData> Data { get; set; }  // List of subreddits.  --Kris

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("styles")]
        public WidgetStyles Styles { get; set; }

        public WidgetCommunityListDetailed(List<WidgetCommunityListData> data, string shortName, WidgetStyles styles)
        {
            Data = data;
            ShortName = shortName;
            Styles = styles;
            Kind = "community-list";
        }

        public WidgetCommunityListDetailed() { }
    }
}
