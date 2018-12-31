using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairTypeInput
    {
        /// <summary>
        /// one of (USER_FLAIR, LINK_FLAIR)
        /// </summary>
        public string flair_type { get; set; }
    }
}
