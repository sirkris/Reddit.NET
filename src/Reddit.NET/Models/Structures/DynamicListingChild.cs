using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class DynamicListingChild : BaseContainer
    {
        [JsonProperty("data")]
        public dynamic Data;
    }
}
