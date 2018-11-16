using RedditThings = Reddit.NET.Models.Structures;
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

        public RedditThings.User UserData;

        private readonly Dispatch Dispatch;

        public User(Dispatch dispatch, RedditThings.User user)
        {
            IsFriend = user.IsFriend;
            ProfanityFilter = user.PrefNoProfanity;
            IsSuspended = user.IsSuspended;
            HasGoldSubscription = user.HasGoldSubscription;
            NumFriends = user.NumFriends;
            IsVerified = user.Verified;
            HasNewModmail = user.NewModmailExists;
            Id = user.Id;
            Over18 = user.Over18;
            IsGold = user.IsGold;
            IsMod = user.IsMod;
            HasVerifiedEmail = user.HasVerifiedEmail;
            IconImg = user.IconImg;
            HasModmail = user.HasModMail;
            LinkKarma = user.LinkKarma;
            InboxCount = user.InboxCount;
            HasMail = user.HasMail;
            Name = user.Name;
            Created = user.Created;
            CommentKarma = user.CommentKarma;
            HasSubscribed = user.HasSubscribed;

            UserData = user;

            Dispatch = dispatch;
        }

        public User(Dispatch dispatch, string name, string id = null, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            IsFriend = isFriend;
            ProfanityFilter = profanityFilter;
            IsSuspended = isSuspended;
            HasGoldSubscription = hasGoldSubscription;
            NumFriends = numFriends;
            this.IsVerified = IsVerified;
            HasNewModmail = hasNewModmail;
            Id = id;
            Over18 = over18;
            IsGold = isGold;
            IsMod = isMod;
            HasVerifiedEmail = hasVerifiedEmail;
            IconImg = iconImg;
            HasModmail = hasModmail;
            LinkKarma = linkKarma;
            InboxCount = inboxCount;
            HasMail = hasMail;
            Name = name;
            Created = created;
            CommentKarma = commentKarma;
            HasSubscribed = hasSubscribed;

            UserData = new RedditThings.User(this);

            Dispatch = dispatch;
        }

        public User(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }
    }
}
