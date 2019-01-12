using System;

namespace Reddit.Inputs.Flair
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
