using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairTextEditableInput : FlairTextInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool? text_editable { get; set; }
    }
}
