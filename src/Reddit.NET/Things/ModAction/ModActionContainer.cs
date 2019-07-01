using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ModActionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public ModActionData Data { get; set; }
    }
}
