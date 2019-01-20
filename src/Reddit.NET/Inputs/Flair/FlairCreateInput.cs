using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairCreateInput : FlairLinkInput
    {
        /// <summary>
        /// a valid subreddit image name
        /// </summary>
        public string css_class { get; set; }

        /// <summary>
        /// a string no longer than 64 characters
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Required by the API.
        /// </summary>
        public string api_type { get; set; }

        /// <summary>
        /// Create a new flair.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="link">a fullname of a link</param>
        /// <param name="name">a user by name</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public FlairCreateInput(string text, string link = null, string name = null, string cssClass = "")
        {
            this.text = text;
            this.link = link;
            this.name = name;
            css_class = cssClass;
            api_type = "json";
        }
    }
}
