using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.Flair;
using Reddit.Inputs.LiveThreads;
using Reddit.Inputs.Users;
using Reddit.Inputs.Wiki;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for users.
    /// </summary>
    public class User : Monitors
    {
        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }

        public event EventHandler<PostsUpdateEventArgs> PostHistoryUpdated;
        public event EventHandler<CommentsUpdateEventArgs> CommentHistoryUpdated;

        public bool IsFriend { get; set; }
        public bool ProfanityFilter { get; set; }
        public bool IsSuspended { get; set; }
        public bool HasGoldSubscription { get; set; }
        public int NumFriends { get; set; }
        public bool IsVerified { get; set; }
        public bool HasNewModmail { get; set; }
        public string Id { get; set; }
        public string Fullname { get; set; }
        public bool Over18 { get; set; }
        public bool IsGold { get; set; }
        public bool IsMod { get; set; }
        public bool HasVerifiedEmail { get; set; }
        public string IconImg { get; set; }
        public bool HasModmail { get; set; }
        public int LinkKarma { get; set; }
        public int InboxCount { get; set; }
        public bool HasMail { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int CommentKarma { get; set; }
        public bool HasSubscribed { get; set; }

        public List<Post> PostHistory
        {
            get
            {
                return (PostHistoryLastUpdated.HasValue
                    && PostHistoryLastUpdated.Value.AddSeconds(15) > DateTime.Now ? postHistory : GetPostHistory());
            }
            private set
            {
                postHistory = value;
            }
        }
        internal List<Post> postHistory;
        internal DateTime? PostHistoryLastUpdated { get; set; }

        public List<Comment> CommentHistory
        {
            get
            {
                return (CommentHistoryLastUpdated.HasValue
                    && CommentHistoryLastUpdated.Value.AddSeconds(15) > DateTime.Now ? commentHistory : GetCommentHistory());
            }
            private set
            {
                commentHistory = value;
            }
        }
        internal List<Comment> commentHistory;
        internal DateTime? CommentHistoryLastUpdated { get; set; }

        /// <summary>
        /// Full user data from the API.
        /// </summary>
        public Things.User UserData { get; set; }

        private Dispatch Dispatch;

        /// <summary>
        /// Create a new user controller instance from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="user"></param>
        public User(Dispatch dispatch, Things.User user)
        {
            Import(user);
            Dispatch = dispatch;
        }

        /// <summary>
        /// Copy another user controller instance onto this one.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="user"></param>
        public User(Dispatch dispatch, User user)
        {
            Import(user);
            Dispatch = dispatch;
        }

        /// <summary>
        /// Create a new user controller instance, populated manually.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="name">A valid Reddit username</param>
        /// <param name="id"></param>
        /// <param name="isFriend"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="isSuspended"></param>
        /// <param name="hasGoldSubscription"></param>
        /// <param name="numFriends"></param>
        /// <param name="IsVerified"></param>
        /// <param name="hasNewModmail"></param>
        /// <param name="over18"></param>
        /// <param name="isGold"></param>
        /// <param name="isMod"></param>
        /// <param name="hasVerifiedEmail"></param>
        /// <param name="iconImg"></param>
        /// <param name="hasModmail"></param>
        /// <param name="linkKarma"></param>
        /// <param name="inboxCount"></param>
        /// <param name="hasMail"></param>
        /// <param name="created"></param>
        /// <param name="commentKarma"></param>
        /// <param name="hasSubscribed"></param>
        public User(Dispatch dispatch, string name, string id = null, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            Import(name, id, isFriend, profanityFilter, isSuspended, hasGoldSubscription, numFriends, IsVerified, hasNewModmail, over18, isGold, isMod,
                hasVerifiedEmail, iconImg, hasModmail, linkKarma, inboxCount, hasMail, created, commentKarma, hasSubscribed);

            Dispatch = dispatch;
        }

        /// <summary>
        /// Create an empty user controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public User(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        private void Import(Things.User user)
        {
            Import(user.Name, user.Id, user.IsFriend, user.PrefNoProfanity, user.IsSuspended, user.HasGoldSubscription, user.NumFriends,
                user.Verified, user.NewModmailExists, user.Over18, user.IsGold, user.IsMod, user.HasVerifiedEmail, user.IconImg, user.HasModMail,
                user.LinkKarma, user.InboxCount, user.HasMail, user.Created, user.CommentKarma, user.HasSubscribed);

            UserData = user;
        }

        private void Import(User user)
        {
            Import(user.Name, user.Id, user.IsFriend, user.ProfanityFilter, user.IsSuspended, user.HasGoldSubscription, user.NumFriends,
                user.IsVerified, user.HasNewModmail, user.Over18, user.IsGold, user.IsMod, user.HasVerifiedEmail, user.IconImg, user.HasModmail,
                user.LinkKarma, user.InboxCount, user.HasMail, user.Created, user.CommentKarma, user.HasSubscribed);

            UserData = user.UserData;
        }

        private void Import(string name, string id = null, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool? hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            IsFriend = isFriend;
            ProfanityFilter = profanityFilter;
            IsSuspended = isSuspended;
            HasGoldSubscription = hasGoldSubscription;
            NumFriends = numFriends;
            this.IsVerified = IsVerified;
            HasNewModmail = hasNewModmail ?? false;
            Id = id;
            Fullname = (!string.IsNullOrWhiteSpace(Id) ? "t2_" + Id : null);
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

            UserData = new Things.User(this);
        }

        /// <summary>
        /// For use in methods whose endpoints require a fullname.
        /// </summary>
        private void CheckFullname()
        {
            if (Fullname == null)
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    throw new RedditControllerException("This action requires a named user instance.");
                }

                Import(About());
            }
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
            AddRelationship(new UsersFriendInput(Name, type, duration, permissions, banContext, banMessage, banReason, container), subreddit);
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
        public async Task AddRelationshipAsync(string banContext, string banMessage, string banReason, string container, int duration,
            string permissions, string type, string subreddit = null)
        {
            await AddRelationshipAsync(new UsersFriendInput(Name, type, duration, permissions, banContext, banMessage, banReason, container), subreddit);
        }

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
        /// <param name="usersFriendInput">A valid UsersFriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        public void AddRelationship(UsersFriendInput usersFriendInput, string subreddit = null)
        {
            usersFriendInput.name = Name;

            Validate(Dispatch.Users.Friend(usersFriendInput, subreddit));
        }

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
        /// <param name="usersFriendInput">A valid UsersFriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        public async Task AddRelationshipAsync(UsersFriendInput usersFriendInput, string subreddit = null)
        {
            usersFriendInput.name = Name;

            Validate(await Dispatch.Users.FriendAsync(usersFriendInput, subreddit));
        }

        // TODO - Break this fucker up into multiple methods.  --Kris
        /// <summary>
        /// Remove a relationship between a user and another user or subreddit.
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
        /// Complement to POST_friend
        /// </summary>
        /// <param name="type">one of (friend, enemy, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="container"></param>
        /// <param name="subreddit">A subreddit</param>
        public void RemoveRelationship(string type, string container = "", string subreddit = null)
        {
            RemoveRelationship(new UsersUnfriendInput(Name, Fullname, type, container), subreddit);
        }

        // TODO - Break this fucker up into multiple methods.  --Kris
        /// <summary>
        /// Asynchronously remove a relationship between a user and another user or subreddit.
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
        /// Complement to POST_friend
        /// </summary>
        /// <param name="type">one of (friend, enemy, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="container"></param>
        /// <param name="subreddit">A subreddit</param>
        public async Task RemoveRelationshipAsync(string type, string container = "", string subreddit = null)
        {
            await RemoveRelationshipAsync(new UsersUnfriendInput(Name, Fullname, type, container), subreddit);
        }

        /// <summary>
        /// Remove a relationship between a user and another user or subreddit.
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
        /// Complement to POST_friend
        /// </summary>
        /// <param name="usersUnfriendInput">A valid UsersUnfriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        public void RemoveRelationship(UsersUnfriendInput usersUnfriendInput, string subreddit = null)
        {
            usersUnfriendInput.name = Name;

            Dispatch.Users.Unfriend(usersUnfriendInput, subreddit);
        }

        /// <summary>
        /// Asynchronously remove a relationship between a user and another user or subreddit.
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
        /// Complement to POST_friend
        /// </summary>
        /// <param name="usersUnfriendInput">A valid UsersUnfriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        public async Task RemoveRelationshipAsync(UsersUnfriendInput usersUnfriendInput, string subreddit = null)
        {
            usersUnfriendInput.name = Name;

            await Dispatch.Users.UnfriendAsync(usersUnfriendInput, subreddit);
        }

        // Note - I tested this one manually.  Leaving out of automated tests so as not to spam the Reddit admins.  --Kris
        /// <summary>
        /// Report a user. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="details">JSON data</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        public void Report(string reason, string details = "{}")
        {
            Dispatch.Users.ReportUser(new UsersReportUserInput(Name, reason, details));
        }

        /// <summary>
        /// Report a user asynchronously. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="details">JSON data</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        public async Task ReportAsync(string reason, string details = "{}")
        {
            await Dispatch.Users.ReportUserAsync(new UsersReportUserInput(Name, reason, details));
        }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="subreddit">the name of an existing subreddit</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public void SetPermissions(string subreddit, string permissions, string type)
        {
            SetPermissions(new UsersSetPermissionsInput(Name, permissions, type), subreddit);
        }

        /// <summary>
        /// Set permissions asynchronously.
        /// </summary>
        /// <param name="subreddit">the name of an existing subreddit</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public async Task SetPermissionsAsync(string subreddit, string permissions, string type)
        {
            await SetPermissionsAsync(new UsersSetPermissionsInput(Name, permissions, type), subreddit);
        }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="usersSetPermissionsInput">A valid UsersSetPermissionsInput instance</param>
        /// <param name="subreddit">the name of an existing subreddit</param>
        public void SetPermissions(UsersSetPermissionsInput usersSetPermissionsInput, string subreddit = null)
        {
            usersSetPermissionsInput.name = Name;

            Validate(Dispatch.Users.SetPermissions(usersSetPermissionsInput, subreddit));
        }

        /// <summary>
        /// Set permissions asynchronously.
        /// </summary>
        /// <param name="usersSetPermissionsInput">A valid UsersSetPermissionsInput instance</param>
        /// <param name="subreddit">the name of an existing subreddit</param>
        public async Task SetPermissionsAsync(UsersSetPermissionsInput usersSetPermissionsInput, string subreddit = null)
        {
            usersSetPermissionsInput.name = Name;

            await Validate(Dispatch.Users.SetPermissionsAsync(usersSetPermissionsInput, subreddit));
        }

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
        public List<Award> Trophies()
        {
            TrophyList trophyList = Dispatch.Users.Trophies(Name);
            if (trophyList == null || trophyList.Data == null || trophyList.Data.Trophies == null)
            {
                return null;
            }

            List<Award> res = new List<Award>();
            foreach (AwardContainer awardContainer in trophyList.Data.Trophies)
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
            return new User(Dispatch, ((UserChild)Validate(Dispatch.Users.About(Name))).Data);
        }

        /// <summary>
        /// Retrieve the user's post history.
        /// </summary>
        /// <param name="where">One of (overview, submitted, upvotes, downvoted, hidden, saved, gilded)</param>
        /// <param name="context">an integer between 2 and 10</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="sort">one of (hot, new, newForced, top, controversial)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetPostHistory(string where = "overview", int context = 3, string t = "all", int limit = 25, string sort = "",
            string after = "", string before = "", bool includeCategories = false, string show = "all", bool srDetail = false,
            int count = 0)
        {
            PostHistoryLastUpdated = DateTime.Now;
            return GetPostHistory(new UsersHistoryInput("links", t, sort, context, after, before, count, limit, show, srDetail, includeCategories), where);
        }

        /// <summary>
        /// Retrieve the user's post history.
        /// </summary>
        /// <param name="usersHistoryInput">A valid UsersHistoryInput instance</param>
        /// <param name="where">One of (overview, submitted, upvotes, downvoted, hidden, saved, gilded)</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetPostHistory(UsersHistoryInput usersHistoryInput, string where = "overview")
        {
            return (usersHistoryInput.sort.Equals("newForced", StringComparison.OrdinalIgnoreCase)
                ? SanitizePosts(Lists.ForceNewSort(Lists.GetPosts(Validate(Dispatch.Users.PostHistory(Name, where, usersHistoryInput)),
                    Dispatch)))
                : SanitizePosts(Lists.GetPosts(Validate(Dispatch.Users.PostHistory(Name, where, usersHistoryInput)), Dispatch)));
        }

        /// <summary>
        /// Strip out any comments erroneously returned by the Reddit API.
        /// This is necessary because the API sometimes includes comments in post results when it's not supposed to.
        /// </summary>
        /// <param name="posts">A list of posts.</param>
        /// <returns>A list of posts.</returns>
        private List<Post> SanitizePosts(List<Post> posts)
        {
            List<Post> res = new List<Post>();
            foreach (Post post in posts)
            {
                if (post != null
                    && post.Fullname.StartsWith("t3"))
                {
                    res.Add(post);
                }
            }

            return res;
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
        public List<Comment> GetCommentHistory(int context = 3, string t = "all", int limit = 25, string sort = "",
            string after = "", string before = "", bool includeCategories = false, string show = "all", bool srDetail = false,
            int count = 0)
        {
            return GetCommentHistory(new UsersHistoryInput("comments", t, sort, context, after, before, count, limit, show, srDetail, includeCategories));
        }

        /// <summary>
        /// Retrieve the user's comment history.
        /// </summary>
        /// <param name="usersHistoryInput">A valid UsersHistoryInput instance</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetCommentHistory(UsersHistoryInput usersHistoryInput)
        {
            return Lists.GetComments(Validate(Dispatch.Users.CommentHistory(Name, "comments", usersHistoryInput)), Dispatch);
        }

        /// <summary>
        /// Delete flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        public void DeleteFlair(string subreddit)
        {
            Validate(Dispatch.Flair.DeleteFlair(Name, subreddit));
        }

        /// <summary>
        /// Delete flair asynchronously.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        public async Task DeleteFlairAsync(string subreddit)
        {
            Validate(await Dispatch.Flair.DeleteFlairAsync(Name, subreddit));
        }

        /// <summary>
        /// Create a new user flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateFlair(string subreddit, string text, string cssClass = "")
        {
            CreateFlair(new FlairCreateInput(text, "", Name, cssClass), subreddit);
        }

        /// <summary>
        /// Create a new user flair asynchronously.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateFlairAsync(string subreddit, string text, string cssClass = "")
        {
            await CreateFlairAsync(new FlairCreateInput(text, "", Name, cssClass), subreddit);
        }

        /// <summary>
        /// Create a new user flair.
        /// </summary>
        /// <param name="flairCreateInput">A valid FlairCreateInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        public void CreateFlair(FlairCreateInput flairCreateInput, string subreddit = null)
        {
            Validate(Dispatch.Flair.Create(flairCreateInput, subreddit));
        }

        /// <summary>
        /// Create a new user flair asynchronously.
        /// </summary>
        /// <param name="flairCreateInput">A valid FlairCreateInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        public async Task CreateFlairAsync(FlairCreateInput flairCreateInput, string subreddit = null)
        {
            Validate(await Dispatch.Flair.CreateAsync(flairCreateInput, subreddit));
        }

        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 1000)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>Flair list results.</returns>
        public List<FlairListResult> FlairList(string subreddit = "", int limit = 25, string after = "", string before = "", int count = 0,
            string show = "all", bool srDetail = false)
        {
            return FlairList(new FlairNameListingInput(Name, after, before, limit, count, show, srDetail), subreddit);
        }

        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="flairNameListingInput">A valid FlairNameListingInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>Flair list results.</returns>
        public List<FlairListResult> FlairList(FlairNameListingInput flairNameListingInput, string subreddit = "")
        {
            flairNameListingInput.name = Name;

            return Validate(Dispatch.Flair.FlairList(flairNameListingInput, subreddit)).Users;
        }

        /// <summary>
        /// Return information about a users's flair options.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>Flair results.</returns>
        public FlairSelectorResultContainer FlairSelector(string subreddit)
        {
            return Validate(Dispatch.Flair.FlairSelector(new FlairLinkInput(name: Name), subreddit));
        }

        /// <summary>
        /// Invite another user to contribute to a live thread.
        /// Requires the manage permission for this thread. If the recipient accepts the invite, they will be granted the permissions specified.
        /// </summary>
        /// <param name="thread">id</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public void InviteToLiveThread(string thread, string permissions, string type)
        {
            InviteToLiveThread(new LiveThreadsContributorInput(Name, permissions, type), thread);
        }

        /// <summary>
        /// Asynchronously invite another user to contribute to a live thread.
        /// Requires the manage permission for this thread. If the recipient accepts the invite, they will be granted the permissions specified.
        /// </summary>
        /// <param name="thread">id</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public async Task InviteToLiveThreadAsync(string thread, string permissions, string type)
        {
            await InviteToLiveThreadAsync(new LiveThreadsContributorInput(Name, permissions, type), thread);
        }

        /// <summary>
        /// Invite another user to contribute to a live thread.
        /// Requires the manage permission for this thread. If the recipient accepts the invite, they will be granted the permissions specified.
        /// </summary>
        /// <param name="liveThreadsContributorInput">A valid LiveThreadsContributorInput instance</param>
        /// <param name="thread">id</param>
        public void InviteToLiveThread(LiveThreadsContributorInput liveThreadsContributorInput, string thread = "")
        {
            liveThreadsContributorInput.name = Name;

            Validate(Dispatch.LiveThreads.InviteContributor(thread, liveThreadsContributorInput));
        }

        /// <summary>
        /// Asynchronously invite another user to contribute to a live thread.
        /// Requires the manage permission for this thread. If the recipient accepts the invite, they will be granted the permissions specified.
        /// </summary>
        /// <param name="liveThreadsContributorInput">A valid LiveThreadsContributorInput instance</param>
        /// <param name="thread">id</param>
        public async Task InviteToLiveThreadAsync(LiveThreadsContributorInput liveThreadsContributorInput, string thread = "")
        {
            liveThreadsContributorInput.name = Name;

            Validate(await Dispatch.LiveThreads.InviteContributorAsync(thread, liveThreadsContributorInput));
        }

        /// <summary>
        /// Revoke another user's contributorship.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="thread">id</param>
        public void RemoveFromLiveThread(string thread)
        {
            CheckFullname();

            Validate(Dispatch.LiveThreads.RemoveContributor(thread, Fullname));
        }

        /// <summary>
        /// Revoke another user's contributorship asynchronously.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="thread">id</param>
        public async Task RemoveFromLiveThreadAsync(string thread)
        {
            CheckFullname();

            Validate(await Dispatch.LiveThreads.RemoveContributorAsync(thread, Fullname));
        }

        /// <summary>
        /// Revoke an outstanding contributor invite.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="thread">id</param>
        public void RevokeLiveThreadInvitation(string thread)
        {
            CheckFullname();

            Validate(Dispatch.LiveThreads.RemoveContributorInvite(thread, Fullname));
        }

        /// <summary>
        /// Revoke an outstanding contributor invite asynchronously.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="thread">id</param>
        public async Task RevokeLiveThreadInvitationAsync(string thread)
        {
            CheckFullname();

            Validate(await Dispatch.LiveThreads.RemoveContributorInviteAsync(thread, Fullname));
        }

        /// <summary>
        /// Change a contributor or contributor invite's permissions.
        /// Requires the manage permission for this thread.
        /// Note that permissions overrides the previous value completely.
        /// </summary>
        /// <param name="thread">id</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public void SetLiveThreadPermissions(string thread, string permissions, string type)
        {
            Validate(Dispatch.LiveThreads.SetContributorPermissions(thread, new LiveThreadsContributorInput(Name, permissions, type)));
        }

        /// <summary>
        /// Change a contributor or contributor invite's permissions asynchronously.
        /// Requires the manage permission for this thread.
        /// Note that permissions overrides the previous value completely.
        /// </summary>
        /// <param name="thread">id</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public async Task SetLiveThreadPermissionsAsync(string thread, string permissions, string type)
        {
            Validate(await Dispatch.LiveThreads.SetContributorPermissionsAsync(thread, new LiveThreadsContributorInput(Name, permissions, type)));
        }

        /// <summary>
        /// Change a contributor or contributor invite's permissions.
        /// Requires the manage permission for this thread.
        /// Note that permissions overrides the previous value completely.
        /// </summary>
        /// <param name="liveThreadsContributorInput">A valid LiveThreadsContributorInput instance</param>
        /// <param name="thread">id</param>
        public void SetLiveThreadPermissions(LiveThreadsContributorInput liveThreadsContributorInput, string thread = "")
        {
            SetLiveThreadPermissions(liveThreadsContributorInput, thread);
        }

        /// <summary>
        /// Change a contributor or contributor invite's permissions asynchronously.
        /// Requires the manage permission for this thread.
        /// Note that permissions overrides the previous value completely.
        /// </summary>
        /// <param name="liveThreadsContributorInput">A valid LiveThreadsContributorInput instance</param>
        /// <param name="thread">id</param>
        public async Task SetLiveThreadPermissionsAsync(LiveThreadsContributorInput liveThreadsContributorInput, string thread = "")
        {
            await SetLiveThreadPermissionsAsync(liveThreadsContributorInput, thread);
        }

        /// <summary>
        /// Post an update to a live thread.
        /// Requires the update permission for this thread.
        /// </summary>
        /// <param name="id">The ID of the live thread</param>
        /// <param name="body">raw markdown text</param>
        public void UpdateLiveThread(string id = "", string body = "")
        {
            Validate(Dispatch.LiveThreads.Update(id, body));
        }

        /// <summary>
        /// Post an update to a live thread asynchronously.
        /// Requires the update permission for this thread.
        /// </summary>
        /// <param name="id">The ID of the live thread</param>
        /// <param name="body">raw markdown text</param>
        public async Task UpdateLiveThreadAsync(string id = "", string body = "")
        {
            Validate(await Dispatch.LiveThreads.UpdateAsync(id, body));
        }

        /// <summary>
        /// Accept a pending invitation to contribute to the thread.
        /// </summary>
        /// <param name="id">The ID of the live thread</param>
        public void AcceptLiveThreadInvite(string id)
        {
            Validate(Dispatch.LiveThreads.AcceptContributorInvite(id));
        }

        /// <summary>
        /// Asynchronously accept a pending invitation to contribute to the thread.
        /// </summary>
        /// <param name="id">The ID of the live thread</param>
        public async Task AcceptLiveThreadInviteAsync(string id)
        {
            Validate(await Dispatch.LiveThreads.AcceptContributorInviteAsync(id));
        }

        /// <summary>
        /// Fetch a list of public multis belonging to this user.
        /// </summary>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A list of multis.</returns>
        public List<LabeledMulti> Multis(bool expandSrs = false)
        {
            List<LabeledMultiContainer> labeledMultiContainers = Dispatch.Multis.User(Name, expandSrs);

            List<LabeledMulti> res = new List<LabeledMulti>();
            if (labeledMultiContainers != null)
            {
                foreach (LabeledMultiContainer labeledMultiContainer in labeledMultiContainers)
                {
                    res.Add(labeledMultiContainer.Data);
                }
            }

            return res;
        }

        /// <summary>
        /// Allow this user to edit the specified wiki page on the specified subreddit.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void AllowWikiEdit(string page, string subreddit = null)
        {
            Dispatch.Wiki.AllowEditor(new WikiPageEditorInput(page, Name), subreddit);
        }

        /// <summary>
        /// Asynchronously allow this user to edit the specified wiki page on the specified subreddit.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task AllowWikiEditAsync(string page, string subreddit = null)
        {
            await Dispatch.Wiki.AllowEditorAsync(new WikiPageEditorInput(page, Name), subreddit);
        }

        /// <summary>
        /// Deny this user from editing the specified wiki page on the specified subreddit.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void DenyWikiEdit(string page, string subreddit = null)
        {
            Dispatch.Wiki.DenyEditor(new WikiPageEditorInput(page, Name), subreddit);
        }

        /// <summary>
        /// Asynchronously deny this user from editing the specified wiki page on the specified subreddit.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task DenyWikiEditAsync(string page, string subreddit = null)
        {
            await Dispatch.Wiki.DenyEditorAsync(new WikiPageEditorInput(page, Name), subreddit);
        }

        /// <summary>
        /// Block this user.
        /// </summary>
        public void Block()
        {
            Validate(Dispatch.Users.BlockUser(new UsersBlockUserInput(Fullname ?? null, Name ?? null)));
        }

        /// <summary>
        /// Block this user asynchronously.
        /// </summary>
        public async Task BlockAsync()
        {
            Validate(await Dispatch.Users.BlockUserAsync(new UsersBlockUserInput(Fullname ?? null, Name ?? null)));
        }

        /// <summary>
        /// Monitor the user for new posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorPostHistory(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null)
        {
            if (breakOnFailure.HasValue)
            {
                BreakOnFailure = breakOnFailure.Value;
            }

            if (schedule != null)
            {
                MonitoringSchedule = schedule;
            }

            if (monitoringBaseDelayMs.HasValue)
            {
                MonitoringWaitDelayMS = monitoringBaseDelayMs.Value;
            }

            if (monitoringExpiration.HasValue)
            {
                MonitoringExpiration = monitoringExpiration;
            }

            string key = "PostHistory";
            return Monitor(key, new Thread(() => MonitorPostHistoryThread(key, monitoringDelayMs)), Fullname);
        }

        private void MonitorPostHistoryThread(string key, int? monitoringDelayMs = null, bool? breakOnFailure = null)
        {
            MonitorHistoryThread(Monitoring, key, "posts", Fullname, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnPostHistoryUpdated(PostsUpdateEventArgs e)
        {
            PostHistoryUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the user for new posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorCommentHistory(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null)
        {
            if (breakOnFailure.HasValue)
            {
                BreakOnFailure = breakOnFailure.Value;
            }

            if (schedule != null)
            {
                MonitoringSchedule = schedule;
            }

            if (monitoringBaseDelayMs.HasValue)
            {
                MonitoringWaitDelayMS = monitoringBaseDelayMs.Value;
            }

            if (monitoringExpiration.HasValue)
            {
                MonitoringExpiration = monitoringExpiration;
            }

            string key = "CommentHistory";
            return Monitor(key, new Thread(() => MonitorCommentHistoryThread(key, monitoringDelayMs)), Fullname);
        }

        private void MonitorCommentHistoryThread(string key, int? monitoringDelayMs = null, bool? breakOnFailure = null)
        {
            MonitorHistoryThread(Monitoring, key, "comments", Fullname, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnCommentHistoryUpdated(CommentsUpdateEventArgs e)
        {
            CommentHistoryUpdated?.Invoke(this, e);
        }

        private void MonitorHistoryThread(MonitoringSnapshot monitoring, string key, string type, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            monitoringDelayMs = (monitoringDelayMs.HasValue ? monitoringDelayMs : Monitoring.Count() * MonitoringWaitDelayMS);

            while (!Terminate
                && Monitoring.Get(key).Contains(subKey))
            {
                if (MonitoringExpiration.HasValue
                    && DateTime.Now > MonitoringExpiration.Value)
                {
                    MonitorModel.RemoveMonitoringKey(key, subKey, ref Monitoring);
                    Threads.Remove(key);

                    break;
                }

                while (!IsScheduled())
                {
                    if (Terminate)
                    {
                        break;
                    }

                    Thread.Sleep(15000);
                }

                if (Terminate)
                {
                    break;
                }

                List<Post> oldPostsList;
                List<Post> newPostsList;
                List<Comment> oldCommentsList;
                List<Comment> newCommentsList;
                try
                {
                    switch (type)
                    {
                        default:
                            throw new RedditControllerException("Unrecognized type '" + type + "'.");
                        case "posts":
                            oldPostsList = postHistory;
                            newPostsList = GetPostHistory();

                            if (Lists.ListDiff(oldPostsList, newPostsList, out List<Post> addedPosts, out List<Post> removedPosts))
                            {
                                // Event handler to alert the calling app that the list has changed.  --Kris
                                PostsUpdateEventArgs args = new PostsUpdateEventArgs
                                {
                                    NewPosts = newPostsList,
                                    OldPosts = oldPostsList,
                                    Added = addedPosts,
                                    Removed = removedPosts
                                };
                                OnPostHistoryUpdated(args);
                            }
                            break;
                        case "comments":
                            oldCommentsList = commentHistory;
                            newCommentsList = GetCommentHistory();

                            if (Lists.ListDiff(oldCommentsList, newCommentsList, out List<Comment> addedComments, out List<Comment> removedComments))
                            {
                                // Event handler to alert the calling app that the list has changed.  --Kris
                                CommentsUpdateEventArgs args = new CommentsUpdateEventArgs
                                {
                                    NewComments = newCommentsList,
                                    OldComments = oldCommentsList,
                                    Added = addedComments,
                                    Removed = removedComments
                                };
                                OnCommentHistoryUpdated(args);
                            }
                            break;
                    }
                }
                catch (Exception) when (!BreakOnFailure) { }

                Wait(monitoringDelayMs.Value);
            }
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "PostHistory":
                    return new Thread(() => MonitorHistoryThread(Monitoring, key, "posts", subKey, startDelayMs, monitoringDelayMs));
                case "CommentHistory":
                    return new Thread(() => MonitorHistoryThread(Monitoring, key, "comments", subKey, startDelayMs, monitoringDelayMs));
            }
        }
    }
}
