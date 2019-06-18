using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class MultipleResponseData
    {
        [JsonProperty("things")]
        public List<DynamicListingChild> Things { get; set; }
    }
}
