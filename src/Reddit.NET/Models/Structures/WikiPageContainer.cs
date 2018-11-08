using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WikiPageContainer : BaseContainer
    {
        [JsonProperty("data")]
        public WikiPage Data;
    }
}
