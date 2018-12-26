using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class MultipleResponseData
    {
        [JsonProperty("things")]
        public List<DynamicListingChild> Things;
    }
}
