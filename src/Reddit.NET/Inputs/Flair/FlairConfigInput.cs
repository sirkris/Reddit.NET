using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairConfigInput : FlairEnabledInput
    {
        /// <summary>
        /// one of (left, right)
        /// </summary>
        public string flair_position { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool flair_self_assign_enabled { get; set; }

        /// <summary>
        /// one of (left, right)
        /// </summary>
        public string link_flair_position { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool link_flair_self_assign_enabled { get; set; }

        /// <summary>
        /// Flair config inputs.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        /// <param name="flairSelfAssignEnabled">boolean value</param>
        /// <param name="flairPosition">one of (left, right)</param>
        /// <param name="linkFlairSelfAssignEnabled">boolean value</param>
        /// <param name="linkFlairPosition">one of (left, right)</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        public FlairConfigInput(bool flairEnabled = true, bool flairSelfAssignEnabled = true, string flairPosition = "right",
            bool linkFlairSelfAssignEnabled = true, string linkFlairPosition = "left") 
            : base(flairEnabled)
        {
            flair_position = flairPosition;
            flair_self_assign_enabled = flairSelfAssignEnabled;
            link_flair_position = linkFlairPosition;
            link_flair_self_assign_enabled = linkFlairSelfAssignEnabled;
        }
    }
}
