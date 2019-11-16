using System;

namespace Reddit.Inputs.Users
{
    [Serializable]
    public class UsersUnfriendInput : BaseInput
    {
        /// <summary>
        /// the name of an existing user
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// one of (friend, enemy, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// TODO - Purpose unknown.
        /// </summary>
        public string container { get; set; }

        /// <summary>
        /// Remove a relationship between a user and another user or subreddit.
        /// The user can either be passed in by name (nuser) or by fullname (iuser).
        /// If type is friend or enemy, 'container' MUST be the current user's fullname; for other types, the subreddit must be set via URL (e.g., /r/funny/api/unfriend).
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
        /// <param name="id">fullname of a thing</param>
        /// <param name="type">one of (friend, enemy, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="container"></param>
        public UsersUnfriendInput(string name = "", string id = "", string type = "friend", string container = "")
        {
            this.name = name;
            this.id = id;
            this.type = type;
            this.container = container;
        }
    }
}
