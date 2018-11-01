using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LabeledMultiSubmit
    {
        [JsonProperty("display_name")]
        public string DisplayName;

        [JsonProperty("subreddits")]
        public List<SubredditName> Subreddits;

        [JsonProperty("visibility")]
        public string Visibility;

        [JsonProperty("icon_name")]
        public string IconName;

        [JsonProperty("weighting_scheme")]
        public string WeightingScheme;

        [JsonProperty("key_color")]
        public string KeyColor;

        [JsonProperty("description_md")]
        public string DescriptionMd;

        public LabeledMultiSubmit(string descriptionMd, string displayName, string iconName, string keyColor, List<SubredditName> subreddits,
            string visibility, string weightingScheme)
        {
            Import(descriptionMd, displayName, iconName, keyColor, subreddits, visibility, weightingScheme);
        }

        public LabeledMultiSubmit(string descriptionMd, string displayName, string iconName, string keyColor, List<string> subreddits,
            string visibility, string weightingScheme)
        {
            List<SubredditName> subs = new List<SubredditName>();
            foreach (string sub in subreddits)
            {
                subs.Add(new SubredditName(sub));
            }

            Import(descriptionMd, displayName, iconName, keyColor, subs, visibility, weightingScheme);
        }

        public LabeledMultiSubmit() { }

        private void Import(string descriptionMd, string displayName, string iconName, string keyColor, List<SubredditName> subreddits,
            string visibility, string weightingScheme)
        {
            DescriptionMd = descriptionMd;
            DisplayName = displayName;
            IconName = iconName;
            KeyColor = keyColor;
            Subreddits = subreddits;
            Visibility = visibility;
            WeightingScheme = weightingScheme;
        }
    }
}
