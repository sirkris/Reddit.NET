using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WikiPageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPage Data;
    }
}
