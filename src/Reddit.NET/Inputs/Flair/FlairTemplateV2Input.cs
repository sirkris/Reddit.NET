using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairTemplateV2Input : FlairTextEditableInput
    {
        /// <summary>
        /// a 6-digit rgb hex color, e.g. #AABBCC
        /// </summary>
        public string background_color { get; set; }

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
        public bool? mod_only { get; set; }

        /// <summary>
        /// one of (light, dark)
        /// </summary>
        public string text_color { get; set; }

        /// <summary>
        /// Create or update a flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="flairTemplateId">the ID of the template to modify; leave blank to create new</param>
        /// <param name="modOnly">boolean value</param>
        public FlairTemplateV2Input(string text = null, string flairType = null, bool? textEditable = null, string textColor = "dark",
            string backgroundColor = "#EEEEFF", string flairTemplateId = "", bool? modOnly = false)
        {
            this.text = text;
            flair_type = flairType;
            text_editable = textEditable;
            text_color = textColor;
            background_color = backgroundColor;
            flair_template_id = flairTemplateId;
            mod_only = modOnly;
        }
    }
}
