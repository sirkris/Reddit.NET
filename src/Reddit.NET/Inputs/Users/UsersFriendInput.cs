using System;

namespace Reddit.Inputs.Users
{
    [Serializable]
    public class UsersFriendInput : APITypeInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string ban_context { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string ban_message { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string ban_reason { get; set; }

        /// <summary>
        /// an integer between 1 and 999
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// A valid permissions string (e.g. "+mail")
        /// </summary>
        public string permissions { get; set; }

        /// <summary>
        /// TODO - Purpose unknown.
        /// </summary>
        public string container { get; set; }

        /// <summary>
        /// the name of an existing user
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// one of (friend, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Create a relationship between a user and another user or subreddit.
        /// OAuth2 use requires appropriate scope based on the 'type' of the relationship:
        /// moderator: Use "moderator_invite"
        /// moderator_invite: modothers
        /// contributor: modcontributors
        /// banned: modcontributors
        /// muted: modcontributors
        /// wikibanned: modcontributors and modwiki
        /// wikicontributor: modcontributors and modwiki
        /// friend: Use /api/v1/me/friends/{username}
        /// enemy: Use /api/block
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="type">one of (friend, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="duration">an integer between 1 and 999</param>
        /// <param name="permissions">A valid permissions string (e.g. "+mail")</param>
        /// <param name="banContext">fullname of a thing</param>
        /// <param name="banMessage">raw markdown text</param>
        /// <param name="banReason">a string no longer than 100 characters</param>
        /// <param name="container"></param>
        public UsersFriendInput(string name = "", string type = "friend", int duration = 999, string permissions = "", string banContext = "", string banMessage = "", 
            string banReason = "", string container = "")
            : base()
        {
            this.name = name;
            this.type = type;
            this.duration = duration;
            this.permissions = permissions;
            ban_context = banContext;
            ban_message = banMessage;
            ban_reason = banReason;
            this.container = container;
        }
    }
}
