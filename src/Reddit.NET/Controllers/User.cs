using Reddit.NET.Controllers.Structures;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Fullname;
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

        internal override ref Models.Internal.Monitor MonitorModel => ref MonitorNull;
        internal override ref MonitoringSnapshot Monitoring => ref MonitoringSnapshotNull;

        public RedditThings.User UserData;

        private Dispatch Dispatch;

        public User(ref Dispatch dispatch, RedditThings.User user)
        {
            Import(user);
            Dispatch = dispatch;
        }

        public User(ref Dispatch dispatch, User user)
        {
            Import(user);
            Dispatch = dispatch;
        }

        public User(ref Dispatch dispatch, string name, string id = null, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            Import(name, id, isFriend, profanityFilter, isSuspended, hasGoldSubscription, numFriends, IsVerified, hasNewModmail, over18, isGold, isMod,
                hasVerifiedEmail, iconImg, hasModmail, linkKarma, inboxCount, hasMail, created, commentKarma, hasSubscribed);

            Dispatch = dispatch;
        }

        public User(ref Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        private void Import(RedditThings.User user)
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

            UserData = new RedditThings.User(this);
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
        public async Task AddRelationshipAsync(string banContext, string banMessage, string banReason, string container, int duration,
            string permissions, string type, string subreddit = null)
        {
            await Task.Run(() =>
            {
                AddRelationship(banContext, banMessage, banReason, container, duration, permissions, type, subreddit);
            });
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
        /// <param name="container"></param>
        /// <param name="type">one of (friend, enemy, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="subreddit">A subreddit</param>
        public void RemoveRelationship(string container, string type, string subreddit = null)
        {
            Dispatch.Users.Unfriend(container, Fullname, Name, type, subreddit);
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
        public async Task ReportAsync(string details, string reason)
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
        public async Task SetPermissionsAsync(string subreddit, string permissions, string type)
        {
            await Task.Run(() =>
            {
                SetPermissions(subreddit, permissions, type);
            });
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
            return new User(ref Dispatch, ((RedditThings.UserChild)Validate(Dispatch.Users.About(Name))).Data);
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
        public List<Post> PostHistory(string where = "overview", int context = 3, string t = "all", int limit = 25, string sort = "",
            string after = "", string before = "", bool includeCategories = false, string show = "all", bool srDetail = false,
            int count = 0)
        {
            if (sort.Equals("newForced", StringComparison.OrdinalIgnoreCase))
            {
                return ForceNewSort(GetPosts(Validate(Dispatch.Users.PostHistory(Name, where, context, show, "new", t, after, before, includeCategories, count, limit, srDetail)),
                    Dispatch));
            }
            else
            {
                return GetPosts(Validate(Dispatch.Users.PostHistory(Name, where, context, show, sort, t, after, before, includeCategories, count, limit, srDetail)), Dispatch);
            }
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
            await Task.Run(() =>
            {
                DeleteFlair(subreddit);
            });
        }

        /// <summary>
        /// Create a new user flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateFlair(string subreddit, string text, string cssClass = "")
        {
            Validate(Dispatch.Flair.Create(cssClass, "", Name, text, subreddit));
        }

        /// <summary>
        /// Create a new user flair asynchronously.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateFlairAsync(string subreddit, string text, string cssClass = "")
        {
            await Task.Run(() =>
            {
                CreateFlair(subreddit, text, cssClass);
            });
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
        public List<RedditThings.FlairListResult> FlairList(string subreddit = "", int limit = 25, string after = "", string before = "", int count = 0,
            string show = "all", bool srDetail = false)
        {
            return Validate(Dispatch.Flair.FlairList(after, before, Name, subreddit, count, limit, show, srDetail)).Users;
        }

        /// <summary>
        /// Return information about a users's flair options.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>Flair results.</returns>
        public RedditThings.FlairSelectorResultContainer FlairSelector(string subreddit)
        {
            return Validate(Dispatch.Flair.FlairSelector(Name, subreddit));
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
            Validate(Dispatch.LiveThreads.InviteContributor(thread, Name, permissions, type));
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
            await Task.Run(() =>
            {
                InviteToLiveThread(thread, permissions, type);
            });
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
            await Task.Run(() =>
            {
                RemoveFromLiveThread(thread);
            });
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
            await Task.Run(() =>
            {
                RevokeLiveThreadInvitation(thread);
            });
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
            Validate(Dispatch.LiveThreads.SetContributorPermissions(thread, Name, permissions, type));
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
            await Task.Run(() =>
            {
                SetLiveThreadPermissions(thread, permissions, type);
            });
        }

        /// <summary>
        /// Post an update to a live thread.
        /// Requires the update permission for this thread.
        /// </summary>
        /// <param name="id">The ID of the live thread</param>
        /// <param name="body">raw markdown text</param>
        public void UpdateLiveThread(string id, string body)
        {
            Validate(Dispatch.LiveThreads.Update(id, body));
        }

        /// <summary>
        /// Post an update to a live thread asynchronously.
        /// Requires the update permission for this thread.
        /// </summary>
        /// <param name="id">The ID of the live thread</param>
        /// <param name="body">raw markdown text</param>
        public async Task UpdateLiveThreadAsync(string id, string body)
        {
            await Task.Run(() =>
            {
                UpdateLiveThread(id, body);
            });
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
            await Task.Run(() =>
            {
                AcceptLiveThreadInvite(id);
            });
        }

        /// <summary>
        /// Fetch a list of public multis belonging to this user.
        /// </summary>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A list of multis.</returns>
        public List<RedditThings.LabeledMulti> Multis(bool expandSrs = false)
        {
            List<RedditThings.LabeledMultiContainer> labeledMultiContainers = Dispatch.Multis.User(Name, expandSrs);

            List<RedditThings.LabeledMulti> res = new List<RedditThings.LabeledMulti>();
            if (labeledMultiContainers != null)
            {
                foreach (RedditThings.LabeledMultiContainer labeledMultiContainer in labeledMultiContainers)
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
            Dispatch.Wiki.AllowEditor(page, Name, subreddit);
        }

        /// <summary>
        /// Asynchronously allow this user to edit the specified wiki page on the specified subreddit.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task AllowWikiEditAsync(string page, string subreddit = null)
        {
            await Task.Run(() =>
            {
                AllowWikiEdit(page, subreddit);
            });
        }

        /// <summary>
        /// Deny this user from editing the specified wiki page on the specified subreddit.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void DenyWikiEdit(string page, string subreddit = null)
        {
            Dispatch.Wiki.DenyEditor(page, Name, subreddit);
        }

        /// <summary>
        /// Asynchronously deny this user from editing the specified wiki page on the specified subreddit.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task DenyWikiEditAsync(string page, string subreddit = null)
        {
            await Task.Run(() =>
            {
                DenyWikiEdit(page, subreddit);
            });
        }

        /// <summary>
        /// Block this user.
        /// </summary>
        public void Block()
        {
            Validate(Dispatch.Users.BlockUser(Fullname ?? null, Name ?? null));
        }

        /// <summary>
        /// Block this user asynchronously.
        /// </summary>
        public async Task BlockAsync()
        {
            await Task.Run(() =>
            {
                Block();
            });
        }
    }
}
