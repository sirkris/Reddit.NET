using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LabeledMultiContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LabeledMulti Data;
    }
}
