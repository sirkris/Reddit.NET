using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairSelectFlairInput : APITypeInput
    {
        /// <summary>
        /// a 6-digit rgb hex color, e.g. #AABBCC
        /// </summary>
        public string background_color { get; set; }

        /// <summary>
        /// a valid flair template ID
        /// </summary>
        public string flair_template_id { get; set; }

        /// <summary>
        /// a fullname of a link
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// a user by name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// [all|only|none]: "all" saves attributes and returns rtjson, "only" only returns rtjson, and "none" only saves attributes
        /// </summary>
        public string return_rtson { get; set; }

        /// <summary>
        /// a string no longer than 64 characters
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// one of (light, dark)
        /// </summary>
        public string text_color { get; set; }

        /// <summary>
        /// Sets a link flair.
        /// </summary>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="flairTemplateId">a valid flair template ID</param>
        /// <param name="link">a fullname of a link</param>
        /// <param name="name">a user by name</param>
        /// <param name="returnRtson">[all|only|none]: "all" saves attributes and returns rtjson, "only" only returns rtjson, and "none" only saves attributes</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textColor">one of (light, dark)</param>
        public FlairSelectFlairInput(string backgroundColor = "#88BBFF", string flairTemplateId = null, string link = "", string name = "", string returnRtson = "all", string text = "",
            string textColor = "dark")
            : base()
        {
            background_color = backgroundColor;
            flair_template_id = flairTemplateId;
            this.link = link;
            this.name = name;
            return_rtson = returnRtson;
            this.text = text;
            text_color = textColor;
        }
    }
}
