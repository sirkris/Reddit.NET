using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class SubredditAutocompleteResultContainer
    {
        [JsonProperty("subreddits")]
        public List<SubredditAutocompleteResult> Subreddits { get; set; }
    }
}
