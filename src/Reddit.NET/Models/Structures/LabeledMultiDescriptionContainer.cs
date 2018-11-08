using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LabeledMultiDescriptionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LabeledMultiDescription Data;
    }
}
