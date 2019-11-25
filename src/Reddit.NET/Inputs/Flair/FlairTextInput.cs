using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairTextInput : BaseInput
    {
        /// <summary>
        /// a string no longer than 64 characters
        /// </summary>
        public string text { get; set; }
    }
}
