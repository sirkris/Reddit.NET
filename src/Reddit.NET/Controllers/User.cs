using ModelStructures = Reddit.NET.Models.Structures;
using System;

namespace Reddit.NET.Controllers
{
    public class User
    {
        public bool IsFriend;
        public bool ProfanityFilter;
        public bool IsSuspended;
        public bool HasGoldSubscription;
        public int NumFriends;
        public bool IsVerified;
        public bool HasNewModmail;
        public string Id;
        public bool Over18;
        public bool IsGold;
        public bool IsMod;
        public bool HasVerifiedEmail;
        public string IconImg;
        public bool HasModmail;
        public int LinkKarma;
        public int InboxCount;
        public bool HasMail;
        public string Name;
        public DateTime Created;
        public int CommentKarma;
        public bool HasSubscribed;

        public ModelStructures.User UserData;

        private readonly Dispatch Dispatch;

        public User(Dispatch dispatch, ModelStructures.User user)
        {
            this.IsFriend = user.IsFriend;
            this.ProfanityFilter = user.PrefNoProfanity;
            this.IsSuspended = user.IsSuspended;
            this.HasGoldSubscription = user.HasGoldSubscription;
            this.NumFriends = user.NumFriends;
            this.IsVerified = user.Verified;
            this.HasNewModmail = user.NewModmailExists;
            this.Id = user.Id;
            this.Over18 = user.Over18;
            this.IsGold = user.IsGold;
            this.IsMod = user.IsMod;
            this.HasVerifiedEmail = user.HasVerifiedEmail;
            this.IconImg = user.IconImg;
            this.HasModmail = user.HasModMail;
            this.LinkKarma = user.LinkKarma;
            this.InboxCount = user.InboxCount;
            this.HasMail = user.HasMail;
            this.Name = user.Name;
            this.Created = user.Created;
            this.CommentKarma = user.CommentKarma;
            this.HasSubscribed = user.HasSubscribed;

            this.UserData = user;

            this.Dispatch = dispatch;
        }

        public User(Dispatch dispatch, string id, string name, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            this.IsFriend = isFriend;
            this.ProfanityFilter = profanityFilter;
            this.IsSuspended = isSuspended;
            this.HasGoldSubscription = hasGoldSubscription;
            this.NumFriends = numFriends;
            this.IsVerified = IsVerified;
            this.HasNewModmail = hasNewModmail;
            this.Id = id;
            this.Over18 = over18;
            this.IsGold = isGold;
            this.IsMod = isMod;
            this.HasVerifiedEmail = hasVerifiedEmail;
            this.IconImg = iconImg;
            this.HasModmail = hasModmail;
            this.LinkKarma = linkKarma;
            this.InboxCount = inboxCount;
            this.HasMail = hasMail;
            this.Name = name;
            this.Created = created;
            this.CommentKarma = commentKarma;
            this.HasSubscribed = hasSubscribed;

            this.UserData = new ModelStructures.User(this);

            this.Dispatch = dispatch;
        }

        public User(Dispatch dispatch)
        {
            this.Dispatch = dispatch;
        }

        /// <summary>
        /// Returns a User instance with the data returned from a call to the "me" endpoint.
        /// </summary>
        public User Me()
        {
            // Recommended usage:  User me = ((Reddit) reddit).User().Me();
            return new User(Dispatch, Dispatch.Account.Me());
        }
    }
}
