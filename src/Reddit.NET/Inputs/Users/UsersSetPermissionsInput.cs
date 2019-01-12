using System;

namespace Reddit.Inputs.Users
{
    [Serializable]
    public class UsersSetPermissionsInput : APITypeInput
    {
        /// <summary>
        /// the name of an existing user
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// A valid permissions string (e.g. "+mail")
        /// </summary>
        public string permissions { get; set; }

        /// <summary>
        /// one of (friend, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">A valid permissions string (e.g. "+mail")</param>
        /// <param name="type">one of (friend, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        public UsersSetPermissionsInput(string name = "", string permissions = "", string type = "")
            : base()
        {
            this.name = name;
            this.permissions = permissions;
            this.type = type;
        }
    }
}
