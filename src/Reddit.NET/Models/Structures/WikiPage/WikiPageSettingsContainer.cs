using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WikiPageSettingsContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPageSettings Data;
    }
}
