using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class SubredditAutocompleteResultContainer
    {
        [JsonProperty("subreddits")]
        public List<SubredditAutocompleteResult> Subreddits;
    }
}
