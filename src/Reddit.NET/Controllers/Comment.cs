using Reddit.Controllers.Internal;
using Reddit.Exceptions;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.Moderation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for comment-related tasks.
    /// </summary>
    public class Comment : BaseController
    {
        public string Subreddit;
        public string Author;
        public string Id;
        public string Fullname;
        public string Permalink;
        public DateTime Created;
        public DateTime Edited;
        public int Score;
        public int UpVotes;
        public int DownVotes;
        public bool Removed;
        public bool Spam;
        public List<Comment> Replies;
        public string ParentId;
        public string ParentFullname;
        public string CollapsedReason;
        public bool Collapsed;
        public bool IsSubmitter;
        public bool ScoreHidden;
        public int Depth;

        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = Parsing.HtmlDecode(value);
            }
        }
        private string body;

        public string BodyHTML
        {
            get
            {
                return bodyHtml;
            }
            set
            {
                bodyHtml = Parsing.HtmlDecode(value);
            }
        }
        private string bodyHtml;

        /// <summary>
        /// The parent post.
        /// </summary>
        public Post Root
        {
            get
            {
                return (root ?? GetRoot(ParentFullname));
            }
            private set
            {
                root = value;
            }
        }
        private Post root = null;

        /// <summary>
        /// Full comment data returned by the API.
        /// </summary>
        public Things.Comment Listing;

        /// <summary>
        /// Comment replies to this comment.
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

        private Dispatch Dispatch;

        /// <summary>
        /// Create a new comment controller instance from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="listing"></param>
        public Comment(Dispatch dispatch, Things.Comment listing)
        {
            Dispatch = dispatch;
            Import(listing);
        }
        
        /// <summary>
        /// Create a new comment controller instance, populated manually.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">The subreddit to which the comment belongs</param>
        /// <param name="author">The username of the comment's author</param>
        /// <param name="body">The comment text</param>
        /// <param name="parentFullname">Fullname of the parent post or comment</param>
        /// <param name="bodyHtml"></param>
        /// <param name="collapsedReason"></param>
        /// <param name="collapsed"></param>
        /// <param name="isSubmitter"></param>
        /// <param name="replies"></param>
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
        public Comment(Dispatch dispatch, string subreddit, string author, string body, string parentFullname, string bodyHtml = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null, 
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime), 
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            Dispatch = dispatch;
            Import(subreddit, author, body, bodyHtml, parentFullname, collapsedReason, collapsed, isSubmitter, replies, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Create a new comment controller instance, populated only with its fullname.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="fullname">Fullname of the comment</param>
        public Comment(Dispatch dispatch, string fullname)
        {
            Dispatch = dispatch;
            Fullname = fullname;
        }

        /// <summary>
        /// Create an empty comment controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public Comment(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        private Comments InitComments()
        {
            Comments = new Comments(Dispatch, Root.Id, Subreddit, this);
            return Comments;
        }

        private void Import(Things.Comment listing)
        {
            Subreddit = listing.Subreddit;
            Author = listing.Author;
            Body = listing.Body;
            BodyHTML = listing.BodyHTML;
            ParentFullname = listing.ParentId;
            ParentId = (!string.IsNullOrEmpty(ParentFullname) && ParentFullname.StartsWith("t3_") ? ParentFullname.Substring(3) : null);
            CollapsedReason = listing.CollapsedReason;
            Collapsed = listing.Collapsed;
            IsSubmitter = listing.IsSubmitter;
            Replies = base.Lists.GetComments(listing.Replies, Dispatch);
            ScoreHidden = listing.ScoreHidden;
            Depth = listing.Depth;
            Id = listing.Id;
            Fullname = listing.Name;
            Permalink = listing.Permalink;
            Created = listing.Created;
            Edited = listing.Edited;
            Score = listing.Score;
            UpVotes = listing.Ups;
            DownVotes = listing.Downs;
            Removed = listing.Removed;
            Spam = listing.Spam;

            Listing = listing;
        }

        private void Import(string subreddit, string author, string body, string bodyHtml,
            string parentFullname = null, string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            Subreddit = subreddit;
            Author = author;
            Body = body;
            BodyHTML = bodyHtml;
            ParentFullname = parentFullname;
            ParentId = (!string.IsNullOrEmpty(ParentFullname) && (ParentFullname.StartsWith("t3_") || ParentFullname.StartsWith("t1_")) ? ParentFullname.Substring(3) : null); ;
            CollapsedReason = collapsedReason;
            Collapsed = collapsed;
            IsSubmitter = isSubmitter;
            Replies = replies;
            ScoreHidden = scoreHidden;
            Depth = depth;
            Id = id;
            Fullname = fullname;
            Permalink = permalink;
            Created = created;
            Edited = edited;
            Score = score;
            UpVotes = upVotes;
            DownVotes = downVotes;
            Removed = removed;
            Spam = spam;

            Listing = new Things.Comment(this);
        }

        /// <summary>
        /// Set the body manually without any automatic decoding.
        /// </summary>
        /// <param name="value">The body value you wish to set</param>
        public void SetBody(string value)
        {
            body = value;
        }

        /// <summary>
        /// Set the body HTML manually without any automatic decoding.
        /// </summary>
        /// <param name="value">The body HTML value you wish to set</param>
        public void SetBodyHTML(string value)
        {
            bodyHtml = value;
        }

        /// <summary>
        /// Get the post to which this comment belongs.
        /// </summary>
        /// <param name="fullname">The fullname of the comment whose post data we're looking for</param>
        /// <returns>The parent post of this comment.</returns>
        public Post GetRoot(string fullname = null)
        {
            fullname = fullname ?? ParentFullname;
            if (string.IsNullOrWhiteSpace(fullname))
            {
                return null;
            }

            Things.Info info = null;
            do
            {
                info = Validate(Dispatch.LinksAndComments.Info(fullname, Subreddit));
                fullname = GetInfoPostOrCommentParentFullname(info);
            } while (info != null && info.Comments != null && info.Comments.Count > 0
                && !string.IsNullOrWhiteSpace(fullname) && !fullname.StartsWith("t3_"));

            return new Post(Dispatch, fullname).About();
        }

        private string GetInfoPostOrCommentParentFullname(Things.Info info)
        {
            if (info == null)
            {
                return null;
            }

            return (info.Posts != null && info.Posts.Count > 0 ? info.Posts[0].Name : info.Comments[0].ParentId);
        }

        /// <summary>
        /// Submit this comment to Reddit.
        /// </summary>
        /// <returns>An instance of this class populated with the return data.</returns>
        public Comment Submit()
        {
            return new Comment(Dispatch, Validate(Dispatch.LinksAndComments.Comment(new LinksAndCommentsThingInput(Body, ParentFullname))).JSON.Data.Things[0].Data);
        }

        /// <summary>
        /// Submit this comment to Reddit asynchronously.
        /// </summary>
        /// <returns>An instance of this class populated with the return data.</returns>
        public async Task<Comment> SubmitAsync()
        {
            return new Comment(Dispatch, Validate(await Dispatch.LinksAndComments.CommentAsync(new LinksAndCommentsThingInput(Body, ParentFullname))).JSON.Data.Things[0].Data);
        }

        /// <summary>
        /// Reply to this comment.
        /// </summary>
        /// <param name="body">The comment reply text</param>
        /// <param name="bodyHtml"></param>
        /// <param name="author"></param>
        /// <param name="collapsedReason"></param>
        /// <param name="collapsed"></param>
        /// <param name="isSubmitter"></param>
        /// <param name="replies"></param>
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
            List<Comment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return BuildReply(body, bodyHtml, author, collapsedReason, collapsed, isSubmitter, replies, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam).Submit();
        }

        /// <summary>
        /// Reply to this comment asynchronously.
        /// </summary>
        /// <param name="body">The comment reply text</param>
        /// <param name="bodyHtml"></param>
        /// <param name="author"></param>
        /// <param name="collapsedReason"></param>
        /// <param name="collapsed"></param>
        /// <param name="isSubmitter"></param>
        /// <param name="replies"></param>
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
            List<Comment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return await BuildReply(body, bodyHtml, author, collapsedReason, collapsed, isSubmitter, replies, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam).SubmitAsync();
        }

        /// <summary>
        /// Create a comment reply object without submitting it to Reddit.
        /// </summary>
        /// <param name="body">The comment reply text</param>
        /// <param name="bodyHtml"></param>
        /// <param name="author"></param>
        /// <param name="collapsedReason"></param>
        /// <param name="collapsed"></param>
        /// <param name="isSubmitter"></param>
        /// <param name="replies"></param>
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
        /// <returns>The unsent comment reply instance.</returns>
        public Comment BuildReply(string body, string bodyHtml = null, string author = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return new Comment(Dispatch, Subreddit, author, body, Fullname, bodyHtml, collapsedReason, collapsed, isSubmitter, replies, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Return information about the current Comment instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Comment About()
        {
            Things.Info info = Validate(Dispatch.LinksAndComments.Info(Fullname, Subreddit));
            if (info == null
                || info.Comments == null
                || info.Comments.Count == 0
                || !Fullname.Equals(info.Comments[0].Name))
            {
                throw new RedditControllerException("Unable to retrieve comment data.");
            }

            return new Comment(Dispatch, info.Comments[0]);
        }

        /// <summary>
        /// Distinguish a comment's author with a sigil.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a comment of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish.depends on user.
        /// The first time a top-level comment is moderator distinguished, the author of the link the comment is in reply to will get a notification in their inbox.
        /// sticky is a boolean flag for comments, which will stick the distingushed comment to the top of all comments threads.
        /// If a comment is marked sticky, it will override any other stickied comment for that link (as only one comment may be stickied at a time). Only top-level comments may be stickied.
        /// </summary>
        /// <param name="how">one of (yes, no, admin, special)</param>
        /// <param name="sticky">boolean value</param>
        /// <returns>The distinguished comment object.</returns>
        public Comment Distinguish(string how, bool? sticky = null)
        {
            return base.Lists.GetComments(Validate(Dispatch.Moderation.DistinguishComment(how, Fullname, sticky)), Dispatch)[0];
        }

        /// <summary>
        /// Distinguish a comment's author with a sigil asynchronously.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a comment of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish.depends on user.
        /// The first time a top-level comment is moderator distinguished, the author of the link the comment is in reply to will get a notification in their inbox.
        /// sticky is a boolean flag for comments, which will stick the distingushed comment to the top of all comments threads.
        /// If a comment is marked sticky, it will override any other stickied comment for that link (as only one comment may be stickied at a time). Only top-level comments may be stickied.
        /// </summary>
        /// <param name="how">one of (yes, no, admin, special)</param>
        /// <param name="sticky">boolean value</param>
        /// <returns>The distinguished comment object.</returns>
        public async Task<Comment> DistinguishAsync(string how, bool? sticky = null)
        {
            return base.Lists.GetComments(Validate(await Dispatch.Moderation.DistinguishCommentAsync(how, Fullname, sticky)), Dispatch)[0];
        }

        /// <summary>
        /// Redact and remove this comment from all subreddit comment listings.
        /// </summary>
        public void Remove(bool spam = false)
        {
            Dispatch.Moderation.Remove(new ModerationRemoveInput(Fullname, spam));
        }

        /// <summary>
        /// Asynchronously redact and remove this comment from all subreddit comment listings.
        /// </summary>
        public async Task RemoveAsync(bool spam = false)
        {
            await Dispatch.Moderation.RemoveAsync(new ModerationRemoveInput(Fullname, spam));
        }

        /// <summary>
        /// Delete this comment.
        /// </summary>
        public void Delete()
        {
            Dispatch.LinksAndComments.Delete(Fullname);
        }

        /// <summary>
        /// Delete this comment asynchronously.
        /// </summary>
        public async Task DeleteAsync()
        {
            await Dispatch.LinksAndComments.DeleteAsync(Fullname);
        }

        /// <summary>
        /// Report this comment to the subreddit moderators.  The comment then becomes implicitly hidden, as well.
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
        /// Report this comment to the subreddit moderators asynchronously.  The comment then becomes implicitly hidden, as well.
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
            await Task.Run(() =>
            {
                Report(additionalInfo, banEvadingAccountsNames, customText, fromHelpCenter, otherReason, reason, ruleReason, siteReason, violatorUsername);
            });
        }

        /// <summary>
        /// Report this comment to the subreddit moderators.  The comment then becomes implicitly hidden, as well.
        /// </summary>
        /// <param name="linksAndCommentsReportInput">A valid LinksAndCommentsReportInput instance</param>
        public void Report(LinksAndCommentsReportInput linksAndCommentsReportInput)
        {
            Validate(Dispatch.LinksAndComments.Report(linksAndCommentsReportInput));
        }

        /// <summary>
        /// Report this comment to the subreddit moderators asynchronously.  The comment then becomes implicitly hidden, as well.
        /// </summary>
        /// <param name="linksAndCommentsReportInput">A valid LinksAndCommentsReportInput instance</param>
        public async Task ReportAsync(LinksAndCommentsReportInput linksAndCommentsReportInput)
        {
            Validate(await Dispatch.LinksAndComments.ReportAsync(linksAndCommentsReportInput));
        }

        /// <summary>
        /// Save this comment.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// </summary>
        /// <param name="category">a category name</param>
        public void Save(string category)
        {
            Dispatch.LinksAndComments.Save(new LinksAndCommentsSaveInput(Fullname, category));
        }

        /// <summary>
        /// Save this comment asynchronously.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// </summary>
        /// <param name="category">a category name</param>
        public async Task SaveAsync(string category)
        {
            await Dispatch.LinksAndComments.SaveAsync(new LinksAndCommentsSaveInput(Fullname, category));
        }

        /// <summary>
        /// Enable inbox replies for this comment.
        /// </summary>
        public void EnableSendReplies()
        {
            Dispatch.LinksAndComments.SendReplies(new LinksAndCommentsStateInput(Fullname, true));
        }

        /// <summary>
        /// Enable inbox replies for this comment asynchronously.
        /// </summary>
        public async Task EnableSendRepliesAsync()
        {
            await Dispatch.LinksAndComments.SendRepliesAsync(new LinksAndCommentsStateInput(Fullname, true));
        }

        /// <summary>
        /// Disable inbox replies for this comment.
        /// </summary>
        public void DisableSendReplies()
        {
            Dispatch.LinksAndComments.SendReplies(new LinksAndCommentsStateInput(Fullname, false));
        }

        /// <summary>
        /// Disable inbox replies for this comment asynchronously.
        /// </summary>
        public async Task DisableSendRepliesAsync()
        {
            await Dispatch.LinksAndComments.SendRepliesAsync(new LinksAndCommentsStateInput(Fullname, false));
        }

        /// <summary>
        /// Unsave this comment.
        /// This removes the thing from the user's saved listings as well.
        /// </summary>
        public void Unsave()
        {
            Dispatch.LinksAndComments.Unsave(Fullname);
        }

        /// <summary>
        /// Unsave this comment asynchronously.
        /// This removes the thing from the user's saved listings as well.
        /// </summary>
        public async Task UnsaveAsync()
        {
            await Dispatch.LinksAndComments.UnsaveAsync(Fullname);
        }

        /// <summary>
        /// Edit the body text of this comment.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        /// <returns>This instance populated with the modified post data returned by the API.</returns>
        public Comment Edit(string text)
        {
            Import(Validate(Dispatch.LinksAndComments.EditUserTextComment(new LinksAndCommentsThingInput(text, Fullname))).JSON.Data.Things[0].Data);

            return this;
        }

        /// <summary>
        /// Edit the body text of this comment asynchronously.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        public async Task<Comment> EditAsync(string text)
        {
            Import(Validate(await Dispatch.LinksAndComments.EditUserTextCommentAsync(new LinksAndCommentsThingInput(text, Fullname))).JSON.Data.Things[0].Data);

            return this;
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
        public Things.MoreChildren MoreChildren(bool limitChildren, string sort, string id = null)
        {
            return MoreChildren(new LinksAndCommentsMoreChildrenInput(Id, limitChildren, ParentFullname, sort, id));
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
        public Things.MoreChildren MoreChildren(LinksAndCommentsMoreChildrenInput linksAndCommentsMoreChildrenInput)
        {
            return Validate(Dispatch.LinksAndComments.MoreChildren(linksAndCommentsMoreChildrenInput));
        }

        /// <summary>
        /// Upvote this comment.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public void Upvote()
        {
            Dispatch.LinksAndComments.Vote(new LinksAndCommentsVoteInput(Fullname, 1));
        }

        /// <summary>
        /// Upvote this comment asynchronously.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public async Task UpvoteAsync()
        {
            await Dispatch.LinksAndComments.VoteAsync(new LinksAndCommentsVoteInput(Fullname, 1));
        }

        /// <summary>
        /// Downvote this comment.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public void Downvote()
        {
            Dispatch.LinksAndComments.Vote(new LinksAndCommentsVoteInput(Fullname, -1));
        }

        /// <summary>
        /// Downvote this comment asynchronously.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public async Task DownvoteAsync()
        {
            await Dispatch.LinksAndComments.VoteAsync(new LinksAndCommentsVoteInput(Fullname, -1));
        }

        /// <summary>
        /// Unvote this comment.  This is equivalent to "un-voting" by clicking again on a highlighted arrow.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public void Unvote()
        {
            Dispatch.LinksAndComments.Vote(new LinksAndCommentsVoteInput(Fullname, 0));
        }

        /// <summary>
        /// Unvote this comment asynchronously.
        /// Please note that votes must be cast by humans.  Automated bot-voting violates Reddit's rules.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the Reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        public async Task UnvoteAsync()
        {
            await Dispatch.LinksAndComments.VoteAsync(new LinksAndCommentsVoteInput(Fullname, 0));
        }
    }
}
