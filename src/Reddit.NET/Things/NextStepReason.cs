using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class NextStepReason
    {
        [JsonProperty("nextStepHeader")]
        public string NextStepHeader { get; set; }

        [JsonProperty("reasonTextToShow")]
        public string ReasonTextToShow { get; set; }

        [JsonProperty("reasonText")]
        public string ReasonText { get; set; }

        [JsonProperty("nextStepReasons")]
        public List<NextStepReason> NextStepReasons { get; set; }
    }
}
