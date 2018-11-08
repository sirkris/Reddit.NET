using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LabeledMultiContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LabeledMulti Data;
    }
}
