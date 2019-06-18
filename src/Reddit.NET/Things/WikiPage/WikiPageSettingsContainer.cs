using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageSettingsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPageSettings Data { get; set; }
    }
}
