using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LabeledMultiContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LabeledMulti Data { get; set; }
    }
}
