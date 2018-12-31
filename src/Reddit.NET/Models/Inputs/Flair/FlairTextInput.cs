using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairTextInput
    {
        /// <summary>
        /// a string no longer than 64 characters
        /// </summary>
        public string text { get; set; }
    }
}
