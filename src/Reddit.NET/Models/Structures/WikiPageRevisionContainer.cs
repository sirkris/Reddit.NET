using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPageRevisionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPageRevisionData Data;
    }
}
