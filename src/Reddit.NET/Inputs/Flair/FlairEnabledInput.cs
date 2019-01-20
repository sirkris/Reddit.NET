using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairEnabledInput : APITypeInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool flair_enabled { get; set; }

        /// <summary>
        /// Set flair enabled.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        public FlairEnabledInput(bool flairEnabled = true)
            : base()
        {
            flair_enabled = flairEnabled;
        }
    }
}
