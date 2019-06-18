using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ModActionChild : BaseContainer
    {
        [JsonProperty("data")]
        public ModAction Data { get; set; }
    }
}
