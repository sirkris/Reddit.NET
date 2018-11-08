using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPage Data;
    }
}
