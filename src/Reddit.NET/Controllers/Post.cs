using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.Flair;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.Listings;
using Reddit.Inputs.Moderation;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Base controller for posts.
    /// </summary>
    public class Post : Monitors
    {
        public event EventHandler<PostUpdateEventArgs> PostDataUpdated;
        public event EventHandler<PostUpdateEventArgs> PostScoreUpdated;

        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }

        /// <summary>
        /// The subreddit in which the post exists.
        /// </summary>
        public string Subreddit
        {
            get
            {
                return Listing?.Subreddit;
            }
            set
            {
                ImportToExisting(subreddit: value);
            }
        }

        /// <summary>
        /// The username of the post author.
        /// </summary>
        public string Author
        {
            get
            {
                return Listing?.Author;
            }
            set
            {
                ImportToExisting(author: value);
            }
        }


        /// <summary>
        /// The ID36 of the post.
        /// </summary>
        public string Id
        {
            get
            {
                return (string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(fullname)
                    ? fullname.Substring(3)
                    : id);
            }
            set
            {
                id = value;
            }
        }
        private string id
        {
            get
            {
                return Listing?.Id;
            }
            set
            {
                ImportToExisting(id: value);
            }
        }


        /// <summary>
        /// The fullname of the post.
        /// </summary>
        public string Fullname
        {
            get
            {
                return (string.IsNullOrEmpty(fullname) && !string.IsNullOrEmpty(id)
                    ? "t3_" + id
                    : fullname);
            }
            set
            {
                fullname = value;
            }
        }
        private string fullname
        {
            get
            {
                return Listing?.Name;
            }
            set
            {
                ImportToExisting(fullname: value);
            }
        }


        /// <summary>
        /// The permalink URL of the post.
        /// </summary>
        public string Permalink
        {
            get
            {
                return Listing?.Permalink;
            }
            set
            {
                ImportToExisting(permalink: value);
            }
        }

        /// <summary>
        /// When the post was created.
        /// </summary>
        public DateTime Created
        {
            get
            {
                return (Listing != null ? Listing.CreatedUTC : default(DateTime));
            }
            set
            {
                ImportToExisting(created: value);
            }
        }

        /// <summary>
        /// When the post was last edited.
        /// </summary>
        public DateTime Edited
        {
            get
            {
                return (Listing != null ? Listing.Edited : default(DateTime));
            }
            set
            {
                ImportToExisting(edited: value);
            }
        }

        /// <summary>
        /// Whether the post was removed.
        /// </summary>
        public bool Removed
        {
            get
            {
                return (Listing != null ? Listing.Removed : false);
            }
            set
            {
                ImportToExisting(removed: value);
            }
        }

        /// <summary>
        /// Whether the post was marked as spam.
        /// </summary>
        public bool Spam
        {
            get
            {
                return (Listing != null ? Listing.Spam : false);
            }
            set
            {
                ImportToExisting(spam: value);
            }
        }

        /// <summary>
        /// Whether the post was marked as NSFW.
        /// </summary>
        public bool NSFW
        {
            get
            {
                return (Listing != null ? Listing.Over18 : false);
            }
            set
            {
                ImportToExisting(nsfw: value);
            }
        }

        /// <summary>
        /// The post score.
        /// </summary>
        public int Score
        {
            get
            {
                return (Listing != null ? Listing.Score : 0);
            }
            set
            {
                ImportToExisting(score: value);
            }
        }

        /// <summary>
        /// The number of upvotes received.
        /// </summary>
        public int UpVotes
        {
            get
            {
                return (Listing != null ? Listing.Ups : 0);
            }
            set
            {
                ImportToExisting(upVotes: value);
            }
        }

        /// <summary>
        /// The number of upvotes received divided by the total number of votes.
        /// </summary>
        public double UpvoteRatio
        {
            get
            {
                return (Listing != null ? Listing.UpvoteRatio : 0);
            }
            set
            {
                ImportToExisting(upvoteRatio: value);
            }
        }


        /// <summary>
        /// Whether the post has been upvoted by the authenticated user.
        /// </summary>
        public bool IsUpvoted
        {
            get
            {
                return (Listing != null && Listing.Likes.HasValue && Listing.Likes.Value);
            }
            private set { }
        }

        /// <summary>
        /// Whether the post has been downvoted by the authenticated user.
        /// </summary>
        public bool IsDownvoted
        {
            get
            {
                return (Listing != null && Listing.Likes.HasValue && !Listing.Likes.Value);
            }
            private set { }
        }

        /// <summary>
        /// Any awards applied to the post.
        /// </summary>
        public Awards Awards
        {
            get
            {
                if (awards == null)
                {
                    awards = new Awards(Listing);
                }

                return awards;
            }
            set
            {
                awards = value;
            }
        }
        private Awards awards;

        // API no longer returns a value for "downs", so let's just calculate it, instead.  --Kris
        /// <summary>
        /// The number of downvotes received.
        /// </summary>
        public int DownVotes
        {
            get
            {
                return Score - (int)(Score * UpvoteRatio);
            }
            private set { }
        }

        // Monitoring event fires when score changes by either value, whichever is greater.  This is to account for "vote fuzzing".  --Kris
        private int MinScoreMonitoringThreshold { get; set; } = 4;
        private int ScoreMonitoringPercentThreshold { get; set; } = 8;

        private int? MonitoringCancellationThresholdMinutes { get; set; } = null;
        private DateTime? LastMonitoringScoreUpdate { get; set; } = null;

        /// <summary>
        /// The title of the post.
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = Parsing.HtmlDecode(value);
            }
        }
        private string title
        {
            get
            {
                return Listing?.Title;
            }
            set
            {
                ImportToExisting(title: value);
            }
        }

        /// <summary>
        /// The full Listing object returned by the Reddit API;
        /// </summary>
        public Things.Post Listing { get; set; }

        /// <summary>
        /// Comment replies to this post.
        /// </summary>
        public Comments Comments
        {
            get
            {
                return comments ?? InitComments();
            }
            private set
            {
                comments = value;
            }
        }
        private Comments comments = null;

        internal Dispatch Dispatch;

        /// <summary>
        /// Create a new post controller instance from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="listing"></param>
        public Post(Dispatch dispatch, Things.Post listing)
        {
            Dispatch = dispatch;
            Import(listing);
        }

        /// <summary>
        /// Create a new post controller instance, populated manually.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">The subreddit to which the post belongs</param>
        /// <param name="title">The title of the post</param>
        /// <param name="author">The post author's username</param>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="permalink"></param>
        /// <param name="created"></param>
        /// <param name="edited"></param>
        /// <param name="score"></param>
        /// <param name="upVotes"></param>
        /// <param name="downVotes"></param>
        /// <param name="removed"></param>
        /// <param name="spam"></param>
        /// <param name="nsfw"></param>
        public Post(Dispatch dispatch, string subreddit, string title = null, string author = null, string id = null, string fullname = null, string permalink = null,
            DateTime created = default(DateTime), DateTime edited = default(DateTime), int score = 0, int upVotes = 0,
            int downVotes = 0, bool removed = false, bool spam = false, bool nsfw = false)
        {
            Dispatch = dispatch;
            Import(subreddit, title, author, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam, nsfw);
        }

        /// <summary>
        /// Create a new post controller instance, populated with only its fullname.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="fullname">Fullname of the post</param>
        public Post(Dispatch dispatch, string fullname)
        {
            Dispatch = dispatch;
            Fullname = fullname;
        }

        /// <summary>
        /// Create a new post controller instance, populated with only its fullname and subreddit.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="fullname">Fullname of the post</param>
        /// <param name="subreddit">A valid subreddit name</param>
        public Post(Dispatch dispatch, string fullname, string subreddit)
        {
            Dispatch = dispatch;
            Fullname = fullname;
            Subreddit = subreddit;
        }

        /// <summary>
        /// Create an empty post controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public Post(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        private Comments InitComments()
        {
            Comments = new Comments(Dispatch, Id, Subreddit);
            return Comments;
        }

        internal void Import(Things.Post listing)
        {
            Listing = listing;
        }

        internal void Import(string subreddit, string title = null, string author = null, string id = null, string fullname = null, string permalink = null,
            DateTime created = default(DateTime), DateTime edited = default(DateTime), int score = 0, int upVotes = 0,
            double upvoteRatio = 0, bool removed = false, bool spam = false, bool nsfw = false)
        {
            Listing = new Things.Post
            {
                Subreddit = subreddit,
                Title = title,
                Author = author,
                Id = id,
                Name = fullname,
                Permalink = permalink,
                CreatedUTC = created,
                Edited = edited,
                Score = score,
                Ups = upVotes,
                UpvoteRatio = upvoteRatio,
                Removed = removed,
                Spam = spam,
                Over18 = nsfw
            };
        }

        internal void ImportToExisting(string subreddit = null, string title = null, string author = null, string id = null, string fullname = null, string permalink = null,
            DateTime? created = null, DateTime? edited = null, int? score = null, int? upVotes = null,
            double? upvoteRatio = null, bool? removed = null, bool? spam = null, bool? nsfw = null)
        {
            if (Listing == null)
            {
                Import(
                    subreddit, 
                    title, 
                    author, 
                    id, 
                    fullname, 
                    permalink, 
                    created ?? default(DateTime), 
                    edited ?? default(DateTime), 
                    score ?? 0, 
                    upVotes ?? 0, 
                    upvoteRatio ?? 0, 
                    removed ?? false, 
                    spam ?? false, 
                    nsfw ?? false
                );
            }
            else
            {
                Listing.Subreddit = (!string.IsNullOrEmpty(subreddit) ? subreddit : Listing.Subreddit);
                Listing.Title = (!string.IsNullOrEmpty(title) ? title : Listing.Title);
                Listing.Author = (!string.IsNullOrEmpty(author) ? author : Listing.Author);
                Listing.Id = (!string.IsNullOrEmpty(id) ? id : Listing.Id);
                Listing.Name = (!string.IsNullOrEmpty(fullname) ? fullname : Listing.Name);
                Listing.Permalink = (!string.IsNullOrEmpty(permalink) ? permalink : Listing.Permalink);
                Listing.CreatedUTC = (created ?? Listing.CreatedUTC);
                Listing.Edited = (edited ?? Listing.Edited);
                Listing.Score = (score ?? Listing.Score);
                Listing.Ups = (upVotes ?? Listing.Ups);
                Listing.UpvoteRatio = (upvoteRatio ?? Listing.UpvoteRatio);
                Listing.Removed = (removed ?? Listing.Removed);
                Listing.Spam = (spam ?? Listing.Spam);
                Listing.Over18 = (nsfw ?? Listing.Over18);
            }
        }

        /// <summary>
        /// Set the title manually without any automatic decoding.
        /// </summary>
        /// <param name="value">The title value you wish to set</param>
        public void SetTitle(string value)
        {
            title = value;
        }

        /// <summary>
        /// Create a new comment controller instance bound to this post, populated manually.
        /// </summary>
        /// <param name="body">The comment text</param>
        /// <param name="bodyHtml"></param>
        /// <param name="author">The username of the comment's author</param>
        /// <param name="collapsedReason"></param>
        /// <param name="collapsed"></param>
        /// <param name="isSubmitter"></param>
        /// <param name="replies"></param>
        /// <param name="more"></param>
        /// <param name="scoreHidden"></param>
        /// <param name="depth"></param>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="permalink"></param>
        /// <param name="created"></param>
        /// <param name="edited"></param>
        /// <param name="score"></param>
        /// <param name="upVotes"></param>
        /// <param name="downVotes"></param>
        /// <param name="removed"></param>
        /// <param name="spam"></param>
        /// <returns></returns>
        public Comment Comment(string body, string bodyHtml = null, string author = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, List<More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return new Comment(Dispatch, Subreddit, author, body, Fullname, bodyHtml, collapsedReason, collapsed, isSubmitter, replies, more, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Create a new comment controller instance bound to this post.
        /// </summary>
        /// <returns></returns>
        public Comment Comment()
        {
            return new Comment(Dispatch, Subreddit, null, null, Fullname);
        }

        /// <summary>
        /// Reply with a comment to this post.
        /// </summary>
        /// <param name="body">The comment reply text</param>
        /// <param name="bodyHtml"></param>
        /// <param name="author"></param>
        /// <param name="collapsedReason"></param>
        /// <param name="collapsed"></param>
        /// <param name="isSubmitter"></param>
        /// <param name="replies"></param>
        /// <param name="more"></param>
        /// <param name="scoreHidden"></param>
        /// <param name="depth"></param>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="permalink"></param>
        /// <param name="created"></param>
        /// <param name="edited"></param>
        /// <param name="score"></param>
        /// <param name="upVotes"></param>
        /// <param name="downVotes"></param>
        /// <param name="removed"></param>
        /// <param name="spam"></param>
        /// <returns>The newly-created comment reply.</returns>
        public Comment Reply(string body, string bodyHtml = null, string author = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, List<More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return Comment(body, bodyHtml, author, collapsedReason, collapsed, isSubmitter, replies, more, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam).Submit();
        }

        /// <summary>
        /// Reply with a comment to this post asynchronously.
        /// </summary>
        /// <param name="body">The comment reply text</param>
        /// <param name="bodyHtml"></param>
        /// <param name="author"></param>
        /// <param name="collapsedReason"></param>
        /// <param name="collapsed"></param>
        /// <param name="isSubmitter"></param>
        /// <param name="replies"></param>
        /// <param name="more"></param>
        /// <param name="scoreHidden"></param>
        /// <param name="depth"></param>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="permalink"></param>
        /// <param name="created"></param>
        /// <param name="edited"></param>
        /// <param name="score"></param>
        /// <param name="upVotes"></param>
        /// <param name="downVotes"></param>
        /// <param name="removed"></param>
        /// <param name="spam"></param>
        public async Task<Comment> ReplyAsync(string body, string bodyHtml = null, string author = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, List<More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return await Comment(body, bodyHtml, author, collapsedReason, collapsed, isSubmitter, replies, more, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam).SubmitAsync();
        }

        /// <summary>
        /// Return information about the current Post instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Post About()
        {
            if (string.IsNullOrEmpty(Subreddit))
            {
                Post post = Info();
                Id = post.Id;
                Subreddit = post.Subreddit;
            }

            return Validate(Lists.GetPosts(Dispatch.Listings.GetPost(Id, new ListingsGetCommentsInput(), Subreddit), Dispatch))[0];
        }

        /// <summary>
        /// Return information about the current Post instance via the api/info endpoint.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Post Info()
        {
            Info info = Validate(Dispatch.LinksAndComments.Info(Fullname, Subreddit));
            if (info == null
                || info.Posts == null
                || info.Posts.Count == 0
                || !Fullname.Equals(info.Posts[0].Name))
            {
                throw new RedditControllerException("Unable to retrieve post data.");
            }

            return new Post(Dispatch, info.Posts[0]);
        }

        /// <summary>
        /// Sets the link flair.
        /// </summary>
        /// <param name="flairText">The text to be displayed in the flair</param>
        /// <param name="flairTemplateId">(optional) A flair template ID</param>
        public void SetFlair(string flairText = "", string flairTemplateId = "")
        {
            Dispatch.Flair.SelectFlair(new FlairSelectFlairInput(text: flairText, flairTemplateId: flairTemplateId, link: Fullname), Subreddit);
        }

        /// <summary>
        /// Sets the link flair.
        /// </summary>
        /// <param name="flairSelectFlairInput">The text to be displayed in the flair</param>
        public void SetFlair(FlairSelectFlairInput flairSelectFlairInput)
        {
            flairSelectFlairInput.link = Fullname;
            Dispatch.Flair.SelectFlair(flairSelectFlairInput, Subreddit);
        }

        /// <summary>
        /// Distinguish a post's author with a sigil.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a link of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish. depends on user.
        /// </summary>
        /// <param name="how">one of (yes, no, admin, special)</param>
        /// <returns>The distinguished post object.</returns>
        public Post Distinguish(string how)
        {
            return Lists.GetPosts(Validate(Dispatch.Moderation.DistinguishPost(how, Fullname)), Dispatch)[0];
        }

        /// <summary>
        /// Distinguish a post's author with a sigil asynchronously.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a link of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish. depends on user.
        /// </summary>
        /// <param name="how">one of (yes, no, admin, special)</param>
        /// <returns>The distinguished post object.</returns>
        public async Task<Post> DistinguishAsync(string how)
        {
            return Lists.GetPosts(Validate(await Dispatch.Moderation.DistinguishPostAsync(how, Fullname)), Dispatch)[0];
        }

        /// <summary>
        /// Remove this post from all subreddit listings.
        /// </summary>
        /// <param name="spam">boolean value</param>
        public void Remove(bool spam = false)
        {
            Dispatch.Moderation.Remove(new ModerationRemoveInput(Fullname, spam));
        }

        /// <summary>
        /// Asynchronously remove this post from all subreddit listings.
        /// </summary>
        public async Task RemoveAsync(bool spam = false)
        {
            await Dispatch.Moderation.RemoveAsync(new ModerationRemoveInput(Fullname, spam));
        }

        /// <summary>
        /// Delete this post.
        /// </summary>
        public void Delete()
        {
            Dispatch.LinksAndComments.Delete(Fullname);
        }

        /// <summary>
        /// Delete this post asynchronously.
        /// </summary>
        public async Task DeleteAsync()
        {
            await Dispatch.LinksAndComments.DeleteAsync(Fullname);
        }

        /// <summary>
        /// Hide this post.
        /// This removes it from the user's default view of subreddit listings.
        /// </summary>
        public void Hide()
        {
            Dispatch.LinksAndComments.Hide(Fullname);
        }

        /// <summary>
        /// Hide this post asynchronously.
        /// This removes it from the user's default view of subreddit listings.
        /// </summary>
        public async Task HideAsync()
        {
            await Dispatch.LinksAndComments.HideAsync(Fullname);
        }

        /// <summary>
        /// Lock this post.
        /// Prevents a post from receiving new comments.
        /// </summary>
        public void Lock()
        {
            Dispatch.LinksAndComments.Lock(Fullname);
        }

        /// <summary>
        /// Lock this post asynchronously.
        /// Prevents a post from receiving new comments.
        /// </summary>
        public async Task LockAsync()
        {
            await Dispatch.LinksAndComments.LockAsync(Fullname);
        }

        /// <summary>
        /// Mark this post as NSFW.
        /// </summary>
        public void MarkNSFW()
        {
            Dispatch.LinksAndComments.MarkNSFW(Fullname);
        }

        /// <summary>
        /// Mark this post as NSFW asynchronously.
        /// </summary>
        public async Task MarkNSFWAsync()
        {
            await Dispatch.LinksAndComments.MarkNSFWAsync(Fullname);
        }

        /// <summary>
        /// Retrieve additional comments omitted from a base comment tree.
        /// When a comment tree is rendered, the most relevant comments are selected for display first.
        /// Remaining comments are stubbed out with "MoreComments" links. 
        /// This API call is used to retrieve the additional comments represented by those stubs, up to 100 at a time.
        /// The two core parameters required are link and children. link is the fullname of the link whose comments are being fetched. 
        /// children is a comma-delimited list of comment ID36s that need to be fetched.
        /// If id is passed, it should be the ID of the MoreComments object this call is replacing. This is needed only for the HTML UI's purposes and is optional otherwise.
        /// NOTE: you may only make one request at a time to this API endpoint. Higher concurrency will result in an error being returned.
        /// If limit_children is True, only return the children requested.
        /// </summary>
        /// <param name="children">a comma-delimited list of comment ID36s</param>
        /// <param name="limitChildren">boolean value</param>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live)</param>
        /// <param name="id">(optional) id of the associated MoreChildren object</param>
        /// <returns>The requested comments.</returns>
        public MoreChildren MoreChildren(string children, bool limitChildren, string sort, string id = null)
        {
            return MoreChildren(new LinksAndCommentsMoreChildrenInput(children, limitChildren, Fullname, sort, id));
        }

        /// <summary>
        /// Retrieve additional comments omitted from a base comment tree.
        /// When a comment tree is rendered, the most relevant comments are selected for display first.
        /// Remaining comments are stubbed out with "MoreComments" links. 
        /// This API call is used to retrieve the additional comments represented by those stubs, up to 100 at a time.
        /// The two core parameters required are link and children. link is the fullname of the link whose comments are being fetched. 
        /// children is a comma-delimited list of comment ID36s that need to be fetched.
        /// If id is passed, it should be the ID of the MoreComments object this call is replacing. This is needed only for the HTML UI's purposes and is optional otherwise.
        /// NOTE: you may only make one request at a time to this API endpoint. Higher concurrency will result in an error being returned.
        /// If limit_children is True, only return the children requested.
        /// </summary>
        /// <param name="linksAndCommentsMoreChildrenInput">A valid LinksAndCommentsMoreChildrenInput instance</param>
        /// <returns>The requested comments.</returns>
        public MoreChildren MoreChildren(LinksAndCommentsMoreChildrenInput linksAndCommentsMoreChildrenInput)
        {
            linksAndCommentsMoreChildrenInput.link_id = Fullname;

            return Validate(Dispatch.LinksAndComments.MoreChildren(linksAndCommentsMoreChildrenInput));
        }

        /// <summary>
        /// Report this post to the subreddit moderators.  The post then becomes implicitly hidden, as well.
        /// </summary>
        /// <param name="additionalInfo">a string no longer than 2000 characters</param>
        /// <param name="banEvadingAccountsNames">a string no longer than 1000 characters</param>
        /// <param name="customText">a string no longer than 250 characters</param>
        /// <param name="fromHelpCenter">boolean value</param>
        /// <param name="otherReason">a string no longer than 100 characters</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        /// <param name="ruleReason">a string no longer than 100 characters</param>
        /// <param name="siteReason">a string no longer than 100 characters</param>
        /// <param name="violatorUsername">A valid Reddit username</param>
        public void Report(string additionalInfo, string banEvadingAccountsNames, string customText, bool fromHelpCenter,
            string otherReason, string reason, string ruleReason, string siteReason, string violatorUsername)
        {
            Report(new LinksAndCommentsReportInput(additionalInfo, banEvadingAccountsNames, customText, fromHelpCenter, otherReason, reason,
                ruleReason, siteReason, Subreddit, Fullname, violatorUsername));
        }

        /// <summary>
        /// Report this post to the subreddit moderators asynchronously.  The post then becomes implicitly hidden, as well.
        /// </summary>
        /// <param name="additionalInfo">a string no longer than 2000 characters</param>
        /// <param name="banEvadingAccountsNames">a string no longer than 1000 characters</param>
        /// <param name="customText">a string no longer than 250 characters</param>
        /// <param name="fromHelpCenter">boolean value</param>
        /// <param name="otherReason">a string no longer than 100 characters</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        /// <param name="ruleReason">a string no longer than 100 characters</param>
        /// <param name="siteReason">a string no longer than 100 characters</param>
        /// <param name="violatorUsername">A valid Reddit username</param>
        public async Task ReportAsync(string additionalInfo, string banEvadingAccountsNames, string customText, bool fromHelpCenter,
            string otherReason, string reason, string ruleReason, string siteReason, string violatorUsername)
        {
            await ReportAsync(new LinksAndCommentsReportInput(additionalInfo, banEvadingAccountsNames, customText, fromHelpCenter, otherReason, reason,
                ruleReason, siteReason, Subreddit, Fullname, violatorUsername));
        }

        /// <summary>
        /// Report this post to the subreddit moderators.  The post then becomes implicitly hidden, as well.
        /// </summary>
        /// <param name="linksAndCommentsReportInput">A valid LinksAndCommentsReportInput instance</param>
        public void Report(LinksAndCommentsReportInput linksAndCommentsReportInput)
        {
            Validate(Dispatch.LinksAndComments.Report(linksAndCommentsReportInput));
        }

        /// <summary>
        /// Report this post to the subreddit moderators asynchronously.  The post then becomes implicitly hidden, as well.
        /// </summary>
        /// <param name="linksAndCommentsReportInput">A valid LinksAndCommentsReportInput instance</param>
        public async Task ReportAsync(LinksAndCommentsReportInput linksAndCommentsReportInput)
        {
            Validate(await Dispatch.LinksAndComments.ReportAsync(linksAndCommentsReportInput));
        }

        /// <summary>
        /// Save this post.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// </summary>
        /// <param name="category">a category name</param>
        public void Save(string category)
        {
            Dispatch.LinksAndComments.Save(new LinksAndCommentsSaveInput(Fullname, category));
        }

        /// <summary>
        /// Save this post asynchronously.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// </summary>
        /// <param name="category">a category name</param>
        public async Task SaveAsync(string category)
        {
            await Dispatch.LinksAndComments.SaveAsync(new LinksAndCommentsSaveInput(Fullname, category));
        }

        /// <summary>
        /// Enable inbox replies for this post.
        /// </summary>
        public void EnableSendReplies()
        {
            Dispatch.LinksAndComments.SendReplies(new LinksAndCommentsStateInput(Fullname, true));
        }

        /// <summary>
        /// Enable inbox replies for this post asynchronously.
        /// </summary>
        public async Task EnableSendRepliesAsync()
        {
            await Dispatch.LinksAndComments.SendRepliesAsync(new LinksAndCommentsStateInput(Fullname, true));
        }

        /// <summary>
        /// Disable inbox replies for this post.
        /// </summary>
        public void DisableSendReplies()
        {
            Dispatch.LinksAndComments.SendReplies(new LinksAndCommentsStateInput(Fullname, false));
        }

        /// <summary>
        /// Disable inbox replies for this post asynchronously.
        /// </summary>
        public async Task DisableSendRepliesAsync()
        {
            await Dispatch.LinksAndComments.SendRepliesAsync(new LinksAndCommentsStateInput(Fullname, false));
        }

        /// <summary>
        /// Enable contest mode for this post.
        /// </summary>
        public void EnableContestMode()
        {
            Dispatch.LinksAndComments.SetContestMode(new LinksAndCommentsStateInput(Fullname, true));
        }

        /// <summary>
        /// Enable contest mode for this post asynchronously.
        /// </summary>
        public async Task EnableContestModeAsync()
        {
            await Dispatch.LinksAndComments.SetContestModeAsync(new LinksAndCommentsStateInput(Fullname, true));
        }

        /// <summary>
        /// Disable contest mode for this post.
        /// </summary>
        public void DisableContestMode()
        {
            Dispatch.LinksAndComments.SetContestMode(new LinksAndCommentsStateInput(Fullname, false));
        }

        /// <summary>
        /// Disable contest mode for this post asynchronously.
        /// </summary>
        public async Task DisableContestModeAsync()
        {
            await Dispatch.LinksAndComments.SetContestModeAsync(new LinksAndCommentsStateInput(Fullname, false));
        }

        /// <summary>
        /// Set this post as the sticky in its subreddit.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="num">an integer between 1 and 4</param>
        /// <param name="toProfile">boolean value</param>
        public void SetSubredditSticky(int num, bool toProfile)
        {
            Validate(Dispatch.LinksAndComments.SetSubredditSticky(new LinksAndCommentsStickyInput(Fullname, num, true, toProfile)));
        }

        /// <summary>
        /// Set this post as the sticky in its subreddit asynchronously.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="num">an integer between 1 and 4</param>
        /// <param name="toProfile">boolean value</param>
        public async Task SetSubredditStickyAsync(int num, bool toProfile)
        {
            Validate(await Dispatch.LinksAndComments.SetSubredditStickyAsync(new LinksAndCommentsStickyInput(Fullname, num, true, toProfile)));
        }

        /// <summary>
        /// Unset this post as the sticky in its subreddit.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="num">an integer between 1 and 4</param>
        /// <param name="toProfile">boolean value</param>
        public void UnsetSubredditSticky(int num, bool toProfile)
        {
            Validate(Dispatch.LinksAndComments.SetSubredditSticky(new LinksAndCommentsStickyInput(Fullname, num, false, toProfile)));
        }

        /// <summary>
        /// Unset this post as the sticky in its subreddit asynchronously.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="num">an integer between 1 and 4</param>
        /// <param name="toProfile">boolean value</param>
        public async Task UnsetSubredditStickyAsync(int num, bool toProfile)
        {
            Validate(await Dispatch.LinksAndComments.SetSubredditStickyAsync(new LinksAndCommentsStickyInput(Fullname, num, false, toProfile)));
        }

        /// <summary>
        /// Set a suggested sort for this post.
        /// Suggested sorts are useful to display comments in a certain preferred way for posts.
        /// For example, casual conversation may be better sorted by new by default, or AMAs may be sorted by Q&A.
        /// A sort of an empty string clears the default sort.
        /// </summary>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live, blank)</param>
        public void SetSuggestedSort(string sort)
        {
            Validate(Dispatch.LinksAndComments.SetSuggestedSort(new LinksAndCommentsSuggestedSortInput(Fullname, sort)));
        }

        /// <summary>
        /// Set a suggested sort for this post asynchronously.
        /// Suggested sorts are useful to display comments in a certain preferred way for posts.
        /// For example, casual conversation may be better sorted by new by default, or AMAs may be sorted by Q&A.
        /// A sort of an empty string clears the default sort.
        /// </summary>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live, blank)</param>
        public async Task SetSuggestedSortAsync(string sort)
        {
            Validate(await Dispatch.LinksAndComments.SetSuggestedSortAsync(new LinksAndCommentsSuggestedSortInput(Fullname, sort)));
        }

        /// <summary>
        /// Mark this post as containing spoilers.
        /// </summary>
        public void Spoiler()
        {
            Dispatch.LinksAndComments.Spoiler(Fullname);
        }

        /// <summary>
        /// Mark this post as containing spoilers asynchronously.
        /// </summary>
        public async Task SpoilerAsync()
        {
            await Dispatch.LinksAndComments.SpoilerAsync(Fullname);
        }

        /// <summary>
        /// Unhide this post.
        /// </summary>
        public void Unhide()
        {
            Dispatch.LinksAndComments.Unhide(Fullname);
        }

        /// <summary>
        /// Unhide this post asynchronously.
        /// </summary>
        public async Task UnhideAsync()
        {
            await Dispatch.LinksAndComments.UnhideAsync(Fullname);
        }

        /// <summary>
        /// Unlock this post.
        /// Allows this post to receive new comments.
        /// </summary>
        public void Unlock()
        {
            Dispatch.LinksAndComments.Unlock(Fullname);
        }

        /// <summary>
        /// Unlock this post asynchronously.
        /// Allows this post to receive new comments.
        /// </summary>
        public async Task UnlockAsync()
        {
            await Dispatch.LinksAndComments.UnlockAsync(Fullname);
        }

        /// <summary>
        /// Remove the NSFW marking from this post.
        /// </summary>
        public void UnmarkNSFW()
        {
            Dispatch.LinksAndComments.UnmarkNSFW(Fullname);
        }

        /// <summary>
        /// Remove the NSFW marking from this post asynchronously.
        /// </summary>
        public async Task UnmarkNSFWAsync()
        {
            await Dispatch.LinksAndComments.UnmarkNSFWAsync(Fullname);
        }

        /// <summary>
        /// Unsave this post.
        /// This removes the thing from the user's saved listings as well.
        /// </summary>
        public void Unsave()
        {
            Dispatch.LinksAndComments.Unsave(Fullname);
        }

        /// <summary>
        /// Unsave this post asynchronously.
        /// This removes the thing from the user's saved listings as well.
        /// </summary>
        public async Task UnsaveAsync()
        {
            await Dispatch.LinksAndComments.UnsaveAsync(Fullname);
        }

        /// <summary>
        /// Remove spoiler.
        /// </summary>
        public void Unspoiler()
        {
            Dispatch.LinksAndComments.Unspoiler(Fullname);
        }

        /// <summary>
        /// Remove spoiler asynchronously.
        /// </summary>
        public async Task UnspoilerAsync()
        {
            await Dispatch.LinksAndComments.UnspoilerAsync(Fullname);
        }

        /// <summary>
        /// Upvote this post.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public void Upvote()
        {
            Dispatch.LinksAndComments.Vote(new LinksAndCommentsVoteInput(Fullname, 1));
        }

        /// <summary>
        /// Upvote this post asynchronously.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public async Task UpvoteAsync()
        {
            await Dispatch.LinksAndComments.VoteAsync(new LinksAndCommentsVoteInput(Fullname, 1));
        }

        /// <summary>
        /// Downvote this post.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public void Downvote()
        {
            Dispatch.LinksAndComments.Vote(new LinksAndCommentsVoteInput(Fullname, -1));
        }

        /// <summary>
        /// Downvote this post asynchronously.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public async Task DownvoteAsync()
        {
            await Dispatch.LinksAndComments.VoteAsync(new LinksAndCommentsVoteInput(Fullname, -1));
        }

        /// <summary>
        /// Unvote this post.  This is equivalent to "un-voting" by clicking again on a highlighted arrow.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public void Unvote()
        {
            Dispatch.LinksAndComments.Vote(new LinksAndCommentsVoteInput(Fullname, 0));
        }

        /// <summary>
        /// Unvote this post asynchronously.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public async Task UnvoteAsync()
        {
            await Dispatch.LinksAndComments.VoteAsync(new LinksAndCommentsVoteInput(Fullname, 0));
        }

        /// <summary>
        /// Approve this post.
        /// If the thing was removed, it will be re-inserted into appropriate listings.
        /// Any reports on the approved thing will be discarded.
        /// </summary>
        public void Approve()
        {
            Dispatch.Moderation.Approve(Fullname);
        }

        /// <summary>
        /// Return information about a users's flair options.
        /// </summary>
        /// <param name="username">A valid Reddit username</param>
        /// <returns>Flair results.</returns>
        public FlairSelectorResultContainer FlairSelector(string username)
        {
            return Validate(Dispatch.Flair.FlairSelector(new FlairLinkInput(Fullname, username), Subreddit));
        }

        protected virtual void OnPostDataUpdated(PostUpdateEventArgs e)
        {
            PostDataUpdated?.Invoke(this, e);
        }

        protected virtual void OnPostScoreUpdated(PostUpdateEventArgs e)
        {
            PostScoreUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor this post for any configuration changes.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorPostData(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "PostData";
            return Monitor(key, new Thread(() => MonitorPostDataThread(key, monitoringDelayMs)), Id);
        }

        /// <summary>
        /// Monitor this post for any score changes.
        /// In order for the event to fire, *both* minScoreMonitoringThreshold AND scoreMonitoringPercentThreshold must be met.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="minScoreMonitoringThreshold">The minimum change in score value between events (default: 4)</param>
        /// <param name="scoreMonitoringPercentThreshold">The minimum score percent change between events (default: 8)</param>
        /// <param name="cancellationThresholdMinutes">If not null, monitoring will automatically stop if more than this time elapses between score updates (default: null)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorPostScore(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, int? minScoreMonitoringThreshold = null, int? scoreMonitoringPercentThreshold = null,
            int? cancellationThresholdMinutes = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null, DateTime? monitoringExpiration = null)
        {
            if (minScoreMonitoringThreshold.HasValue)
            {
                MinScoreMonitoringThreshold = minScoreMonitoringThreshold.Value;
            }

            if (scoreMonitoringPercentThreshold.HasValue)
            {
                ScoreMonitoringPercentThreshold = scoreMonitoringPercentThreshold.Value;
            }

            if (cancellationThresholdMinutes.HasValue)
            {
                MonitoringCancellationThresholdMinutes = cancellationThresholdMinutes.Value;
            }
            else
            {
                MonitoringCancellationThresholdMinutes = null;
            }

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

            string key = "PostScore";
            return Monitor(key, new Thread(() => MonitorPostScoreThread(key, monitoringDelayMs)), Id);
        }

        public bool PostDataIsMonitored()
        {
            return IsMonitored("PostData", Id);
        }

        public bool PostScoreIsMonitored()
        {
            return IsMonitored("PostScore", Id);
        }

        private void MonitorPostDataThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPost(key, "data", Id, monitoringDelayMs: monitoringDelayMs);
        }

        private void MonitorPostScoreThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPost(key, "score", Id, monitoringDelayMs: monitoringDelayMs);
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key : " + key + ".");
                case "PostData":
                    return new Thread(() => MonitorPost(key, "data", subKey, startDelayMs, monitoringDelayMs));
                case "PostScore":
                    return new Thread(() => MonitorPost(key, "score", subKey, startDelayMs, monitoringDelayMs));
            }
        }

        private void MonitorPost(string key, string type, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            monitoringDelayMs = (monitoringDelayMs.HasValue ? monitoringDelayMs : Monitoring.Count() * MonitoringWaitDelayMS);

            while (!Terminate
                && Monitoring.Get(key).Contains(Id))
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

                Post post;
                try
                {
                    switch (type)
                    {
                        default:
                            throw new RedditControllerException("Unrecognized type '" + type + "'.");
                        case "data":
                            post = About();
                            if (post != null)
                            {
                                if (!post.Edited.Equals(Edited)
                                    || !post.Removed.Equals(Removed)
                                    || !post.Spam.Equals(Spam)
                                    || !post.NSFW.Equals(NSFW))
                                {
                                    TriggerUpdate(post);

                                    // Data is automatically updated when the event fires.  --Kris
                                    Edited = post.Edited;
                                    Removed = post.Removed;
                                    Spam = post.Spam;
                                    NSFW = post.NSFW;
                                }
                            }
                            break;
                        case "score":
                            post = About();
                            if (post != null)
                            {
                                int scoreDiff = Math.Abs(post.Score - Score);
                                double scoreDiffPercent = (Score > 0 ? (scoreDiff / Score) * 100 : scoreDiff * 100);
                                if (scoreDiff >= MinScoreMonitoringThreshold
                                    && scoreDiffPercent >= ScoreMonitoringPercentThreshold)
                                {
                                    TriggerUpdate(post);

                                    Score = post.Score;  // Score is automatically updated when the event fires.  --Kris

                                    LastMonitoringScoreUpdate = DateTime.Now;
                                }
                                else if (MonitoringCancellationThresholdMinutes.HasValue
                                    && LastMonitoringScoreUpdate.HasValue
                                    && LastMonitoringScoreUpdate.Value.AddMinutes(MonitoringCancellationThresholdMinutes.Value) < DateTime.Now)
                                {
                                    // If the score hasn't changed by the required amount within the cancellation threshold (if set), stop monitoring automatically.  --Kris
                                    PostScoreUpdated = null;
                                    MonitorPostScore();
                                }
                            }
                            break;
                    }
                }
                catch (Exception) when (!BreakOnFailure) { }

                Wait(monitoringDelayMs.Value);
            }
        }

        private void TriggerUpdate(Post post)
        {
            // Event handler to alert the calling app that the score has changed.  --Kris
            PostUpdateEventArgs args = new PostUpdateEventArgs
            {
                OldPost = this,
                NewPost = post
            };
            OnPostScoreUpdated(args);
        }
    }
}
