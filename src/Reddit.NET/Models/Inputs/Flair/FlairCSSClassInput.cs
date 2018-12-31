using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairCSSClassInput : FlairTextInput
    {
        /// <summary>
        /// a valid subreddit image name
        /// </summary>
        public string css_class { get; set; }
    }
}
