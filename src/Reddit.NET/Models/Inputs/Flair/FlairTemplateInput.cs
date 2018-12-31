using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairTemplateInput : FlairCSSClassInput
    {
        /// <summary>
        /// the ID of the template to modify
        /// </summary>
        public string flair_template_id { get; set; }

        /// <summary>
        /// one of (USER_FLAIR, LINK_FLAIR)
        /// </summary>
        public string flair_type { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? text_editable { get; set; }

        /// <summary>
        /// Required by the API.
        /// </summary>
        public string api_type { get; set; }

        /// <summary>
        /// Create or update a flair template.  Null values are ignored.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        /// <param name="flairTemplateId">the ID of the template to modify; leave blank to create new</param>
        public FlairTemplateInput(string text = null, string flairType = null, bool? textEditable = null, string cssClass = "", string flairTemplateId = "")
        {
            this.text = text;
            flair_type = flairType;
            text_editable = textEditable;
            css_class = cssClass;
            flair_template_id = flairTemplateId;
            api_type = "json";
        }
    }
}
