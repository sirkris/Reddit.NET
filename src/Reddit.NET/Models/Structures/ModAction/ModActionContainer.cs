using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class ModActionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public ModActionData Data;
    }
}
