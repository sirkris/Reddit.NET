using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class NextStepReason
    {
        [JsonProperty("nextStepHeader")]
        public string NextStepHeader;

        [JsonProperty("reasonTextToShow")]
        public string ReasonTextToShow;

        [JsonProperty("reasonText")]
        public string ReasonText;

        [JsonProperty("nextStepReasons")]
        public List<NextStepReason> NextStepReasons;
    }
}
