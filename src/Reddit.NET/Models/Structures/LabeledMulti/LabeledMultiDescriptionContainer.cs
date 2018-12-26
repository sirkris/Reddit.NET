using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LabeledMultiDescriptionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public LabeledMultiDescription Data;
    }
}
