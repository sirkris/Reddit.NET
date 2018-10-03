using Reddit.NET.Controllers;
using ModelStructures = Reddit.NET.Models.Structures;
using RestSharp;
using System;

namespace Reddit.NET
{
    public class Reddit
    {
        public Dispatch Models
        {
            get;
            private set;
        }

        public Reddit(string accessToken)
        {
            this.Models = new Dispatch(accessToken, new RestClient("https://oauth.reddit.com"));
        }

        public User User(ModelStructures.User user)
        {
            return new User(Models, user);
        }

        public User User(string id, string name, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            return new User(Models, id, name, isFriend, profanityFilter, isSuspended, hasGoldSubscription, numFriends, IsVerified, hasNewModmail, over18, isGold, isMod,
                hasVerifiedEmail, iconImg, hasModmail, linkKarma, inboxCount, hasMail, created, commentKarma, hasSubscribed);
        }

        public User User()
        {
            return new User(Models);
        }
    }
}
