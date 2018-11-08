using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModActionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public ModActionData Data;
    }
}
