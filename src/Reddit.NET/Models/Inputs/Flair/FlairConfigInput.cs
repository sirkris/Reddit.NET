using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairConfigInput : FlairEnabledInput
    {
        /// <summary>
        /// one of (left, right)
        /// </summary>
        [JsonProperty("flair_position")]
        public string FlairPosition;

        /// <summary>
        /// boolean value
        /// </summary>
        [JsonProperty("flair_self_assign_enabled")]
        public bool FlairSelfAssignEnabled;

        /// <summary>
        /// one of (left, right)
        /// </summary>
        [JsonProperty("link_flair_position")]
        public string LinkFlairPosition;

        /// <summary>
        /// boolean value
        /// </summary>
        [JsonProperty("link_flair_self_assign_enabled")]
        public bool LinkFlairSelfAssignEnabled;

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
        {
            FlairEnabled = flairEnabled;
            FlairPosition = flairPosition;
            FlairSelfAssignEnabled = flairSelfAssignEnabled;
            LinkFlairPosition = linkFlairPosition;
            LinkFlairSelfAssignEnabled = linkFlairSelfAssignEnabled;
        }
    }
}
