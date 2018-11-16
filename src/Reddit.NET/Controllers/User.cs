using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    public class User : BaseController
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

        // TODO - Break this fucker up into multiple methods.  --Kris
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
        /// Complement to POST_unfriend
        /// </summary>
        /// <param name="banContext">fullname of a thing</param>
        /// <param name="banMessage">raw markdown text</param>
        /// <param name="banReason">a string no longer than 100 characters</param>
        /// <param name="container"></param>
        /// <param name="duration">an integer between 1 and 999</param>
        /// <param name="permissions"></param>
        /// <param name="type">one of (friend, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="subreddit">A subreddit</param>
        public void AddRelationship(string banContext, string banMessage, string banReason, string container, int duration,
            string permissions, string type, string subreddit = null)
        {
            Validate(Dispatch.Users.Friend(banContext, banMessage, banReason, container, duration, Name, permissions, type, subreddit));
        }

        // TODO - Break this fucker up into multiple methods.  --Kris
        /// <summary>
        /// Asynchronously create a relationship between a user and another user or subreddit.
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
        /// Complement to POST_unfriend
        /// </summary>
        /// <param name="banContext">fullname of a thing</param>
        /// <param name="banMessage">raw markdown text</param>
        /// <param name="banReason">a string no longer than 100 characters</param>
        /// <param name="container"></param>
        /// <param name="duration">an integer between 1 and 999</param>
        /// <param name="permissions"></param>
        /// <param name="type">one of (friend, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="subreddit">A subreddit</param>
        public async void AddRelationshipAsync(string banContext, string banMessage, string banReason, string container, int duration,
            string permissions, string type, string subreddit = null)
        {
            await Task.Run(() =>
            {
                AddRelationship(banContext, banMessage, banReason, container, duration, permissions, type, subreddit);
            });
        }

        // Note - I tested this one manually.  Leaving out of automated tests so as not to spam the Reddit admins.  --Kris
        /// <summary>
        /// Report a user. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="details">JSON data</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        public void Report(string details, string reason)
        {
            Dispatch.Users.ReportUser(details, reason, Name);
        }

        /// <summary>
        /// Report a user asynchronously. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="details">JSON data</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        public async void ReportAsync(string details, string reason)
        {
            await Task.Run(() =>
            {
                Report(details, reason);
            });
        }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="subreddit">the name of an existing subreddit</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public void SetPermissions(string subreddit, string permissions, string type)
        {
            Validate(Dispatch.Users.SetPermissions(Name, permissions, type, subreddit));
        }

        /// <summary>
        /// Set permissions asynchronously.
        /// </summary>
        /// <param name="usernsubredditame">the name of an existing subreddit</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public async void SetPermissionsAsync(string subreddit, string permissions, string type)
        {
            await Task.Run(() =>
            {
                SetPermissions(subreddit, permissions, type);
            });
        }

        // TODO - Users.Unfriend method.  Model still needs testing; will come back to this one later.  --Kris

        /// <summary>
        /// Check whether this instance's username is available for registration.
        /// </summary>
        /// <param name="user">a valid, unused username</param>
        /// <returns>Boolean or null if error (i.e. invalid username).</returns>
        public bool? UsernameAvailable()
        {
            return Dispatch.Users.UsernameAvailable(Name);
        }

        /// <summary>
        /// Return a list of trophies for the given user.
        /// </summary>
        /// <returns>A list of trophies.</returns>
        public List<RedditThings.Award> Trophies()
        {
            RedditThings.TrophyList trophyList = Dispatch.Users.Trophies(Name);
            if (trophyList == null || trophyList.Data == null || trophyList.Data.Trophies == null)
            {
                return null;
            }

            List<RedditThings.Award> res = new List<RedditThings.Award>();
            foreach (RedditThings.AwardContainer awardContainer in trophyList.Data.Trophies)
            {
                res.Add(awardContainer.Data);
            }

            return res;
        }

        /// <summary>
        /// Return information about the user, including karma and gold status.
        /// </summary>
        /// <returns>A user listing.</returns>
        public User About()
        {
            return new User(Dispatch, ((RedditThings.UserChild)Validate(Dispatch.Users.About(Name))).Data);
        }

        /// <summary>
        /// Retrieve the user's post history.
        /// </summary>
        /// <param name="where">One of (overview, submitted, upvotes, downvoted, hidden, saved, gilded)</param>
        /// <param name="context">an integer between 2 and 10</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="sort">one of (hot, new, top, controversial)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of posts.</returns>
        public List<Post> PostHistory(string where, int context = 3, string t = "all", int limit = 25, string sort = "",
            string after = "", string before = "", bool includeCategories = false, string show = "all", bool srDetail = false,
            int count = 0)
        {
            return GetPosts(Validate(Dispatch.Users.PostHistory(Name, where, context, show, sort, t, after, before, includeCategories, count, limit, srDetail)), Dispatch);
        }

        /// <summary>
        /// Retrieve the user's comment history.
        /// </summary>
        /// <param name="context">an integer between 2 and 10</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="sort">one of (hot, new, top, controversial)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> CommentHistory(int context = 3, string t = "all", int limit = 25, string sort = "",
            string after = "", string before = "", bool includeCategories = false, string show = "all", bool srDetail = false,
            int count = 0)
        {
            return GetComments(Validate(Dispatch.Users.CommentHistory(Name, "comments", context, show, sort, t, after, before, includeCategories, count, limit, 
                srDetail)), Dispatch);
        }
    }
}
