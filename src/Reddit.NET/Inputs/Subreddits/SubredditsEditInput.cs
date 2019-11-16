using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsEditInput : BaseInput
    {
        /// <summary>
        /// one of (true, false)
        /// </summary>
        public bool created { get; set; }

        /// <summary>
        /// TODO - Purpose unknown.
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// Get the current settings of a subreddit.
        /// This returns the current settings of the subreddit as used by /api/site_admin.
        /// </summary>
        /// <param name="created">one of (true, false)</param>
        /// <param name="location"></param>
        public SubredditsEditInput(bool created = false, string location = "")
        {
            this.created = created;
            this.location = location;
        }
    }
}
