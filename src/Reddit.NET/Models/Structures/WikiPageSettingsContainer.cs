using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPageSettingsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPageSettings Data;
    }
}
