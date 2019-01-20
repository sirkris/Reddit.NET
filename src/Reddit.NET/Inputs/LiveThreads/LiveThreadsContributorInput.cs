using System;

namespace Reddit.Inputs.LiveThreads
{
    [Serializable]
    public class LiveThreadsContributorInput : APITypeInput
    {
        /// <summary>
        /// the name of an existing user
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// permission description e.g. +update,+edit,-manage
        /// </summary>
        public string permissions { get; set; }

        /// <summary>
        /// one of (liveupdate_contributor_invite, liveupdate_contributor)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Set data pertaining to a live thread contributor.
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public LiveThreadsContributorInput(string name = "", string permissions = "+update", string type = "liveupdate_contributor_invite")
            : base()
        {
            this.name = name;
            this.permissions = permissions;
            this.type = type;
        }
    }
}
