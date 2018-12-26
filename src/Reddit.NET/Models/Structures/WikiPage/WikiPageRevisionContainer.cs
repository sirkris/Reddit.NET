using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WikiPageRevisionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPageRevisionData Data;
    }
}
