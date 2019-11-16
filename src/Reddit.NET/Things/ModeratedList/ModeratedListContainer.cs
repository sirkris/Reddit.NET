using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ModeratedListContainer : BaseContainer
    {
        [JsonProperty("data")]
        public List<ModeratedListItem> Data { get; set; }
    }
}
