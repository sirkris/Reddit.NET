using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class RulesContainer
    {
        [JsonProperty("rules")]
        public List<Rule> Rules;

        [JsonProperty("site_rules")]
        public List<string> SiteRules;

        [JsonProperty("site_rules_flow")]
        public List<NextStepReason> SiteRulesFlow;
    }
}
