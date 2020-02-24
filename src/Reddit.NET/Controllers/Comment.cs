using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.Listings;
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
        /// <summary>
        /// The subreddit in which this comment exists.
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
        /// The username of the comment author.
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
        /// The comment ID36.
        /// </summary>
        public string Id
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
        /// The comment fullname.
        /// </summary>
        public string Fullname
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
        /// The permalink URL of the comment.
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
        /// When the comment was created.
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
        /// When the comment was last edited.
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
        /// The comment score.
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
        /// The number of downvotes received.
        /// </summary>
        public int DownVotes
        {
            get
            {
                return (Listing != null ? Listing.Downs : 0);
            }
            set
            {
                ImportToExisting(downVotes: value);
            }
        }

        /// <summary>
        /// Whether the comment has been removed.
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
        /// Whether the comment has been marked as spam.
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
        /// A list of Things.More objects.
        /// </summary>
        public List<Things.More> More { get; set; }

        /// <summary>
        /// The parent ID36.
        /// </summary>
        public string ParentId
        {
            get
            {
                return (!string.IsNullOrEmpty(Listing?.ParentId) && Listing.ParentId.Length > 3
                    ? Listing.ParentId.Substring(3)
                    : null);
            }
            private set { }
        }

        /// <summary>
        /// The parent fullname.
        /// </summary>
        public string ParentFullname
        {
            get
            {
                return Listing?.ParentId;
            }
            set
            {
                ImportToExisting(parentFullname: value);
            }
        }

        /// <summary>
        /// The reason the comment was collapsed (if applicable).
        /// </summary>
        public string CollapsedReason
        {
            get
            {
                return Listing?.CollapsedReason;
            }
            set
            {
                ImportToExisting(collapsedReason: value);
            }
        }

        /// <summary>
        /// Whether the comment was collapsed.
        /// </summary>
        public bool Collapsed
        {
            get
            {
                return (Listing != null ? Listing.Collapsed : false);
            }
            set
            {
                ImportToExisting(collapsed: value);
            }
        }

        /// <summary>
        /// Whether the comment was authored by the authenticated user.
        /// </summary>
        public bool IsSubmitter
        {
            get
            {
                return (Listing != null ? Listing.IsSubmitter : false);
            }
            set
            {
                ImportToExisting(isSubmitter: value);
            }
        }

        /// <summary>
        /// Whether the comment score should be hidden.
        /// </summary>
        public bool ScoreHidden
        {
            get
            {
                return (Listing != null ? Listing.ScoreHidden : false);
            }
            set
            {
                ImportToExisting(scoreHidden: value);
            }
        }

        /// <summary>
        /// The comment depth.
        /// </summary>
        public int Depth
        {
            get
            {
                return (Listing != null ? Listing.Depth : 0);
            }
            set
            {
                ImportToExisting(depth: value);
            }
        }

        /// <summary>
        /// Any awards applied to the comment.
        /// </summary>
        public Awards Awards
        {
            get
            {
                awards = awards ?? new Awards(Listing);
                return awards;
            }
            private set
            {
                awards = value;
            }
        }
        private Awards awards;

        /// <summary>
        /// Whether the comment has been upvoted by the authenticated user.
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
        /// Whether the comment has been downvoted by the authenticated user.
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
        /// A list of comment replies.
        /// </summary>
        public List<Comment> Replies
        {
            get
            {
                replies = replies ?? About().replies;
                return replies;
            }
            set
            {
                replies = value;
            }
        }
        public List<Comment> replies { get; private set; }

        /// <summary>
        /// The number of direct comment replies.  
        /// Unlike Replies, accessing this property does not require a separate API call.
        /// </summary>
        public int? NumReplies
        {
            get
            {
                return (More != null && !More.Count.Equals(0) && More[0] != null
                    ? More[0].Count
                    : (Listing?.Replies.Comments != null
                        ? Listing.Replies.Comments.Count
                        : (int?)null));
            }
            private set { }
        }

        /// <summary>
        /// The comment body.
        /// </summary>
        public string Body
        {
            get
            {
                return Parsing.HtmlDecode((!string.IsNullOrEmpty(body) ? body : Listing?.Body));
            }
            set
            {
                body = value;
            }
        }
        private string body;

        /// <summary>
        /// The comment body in HTML format.
        /// </summary>
        public string BodyHTML
        {
            get
            {
                return Parsing.HtmlDecode((!string.IsNullOrEmpty(bodyHtml) ? bodyHtml : Listing?.BodyHTML));
            }
            set
            {
                bodyHtml = value;
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
        public Comment(Dispatch dispatch, string subreddit, string author, string body, string parentFullname, string bodyHtml = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, List<Things.More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null, 
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime), 
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            Dispatch = dispatch;
            Import(subreddit, author, body, bodyHtml, parentFullname, collapsedReason, collapsed, isSubmitter, replies, more, scoreHidden,
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
            Listing = new Things.Comment { Name = fullname };
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
            Body = listing.Body;
            BodyHTML = listing.BodyHTML;
            Replies = (listing.Replies != null ? Lists.GetComments(listing.Replies.Comments, Dispatch) : new List<Comment>());
            More = (listing.Replies != null ? listing.Replies.MoreData : new List<Things.More>());

            Listing = listing;
        }

        private void Import(string subreddit, string author, string body, string bodyHtml,
            string parentFullname = null, string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, List<Things.More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            Listing = new Things.Comment
            {
                Subreddit = subreddit,
                Author = author,
                Body = body,
                BodyHTML = bodyHtml,
                ParentId = parentFullname,
                CollapsedReason = collapsedReason,
                Collapsed = collapsed,
                IsSubmitter = isSubmitter,
                ScoreHidden = scoreHidden,
                Depth = depth,
                Id = id,
                Name = fullname,
                Permalink = permalink,
                CreatedUTC = created,
                Edited = edited,
                Score = score,
                Ups = upVotes,
                Downs = downVotes,
                Removed = removed,
                Spam = spam
            };

            More = (more == null && Listing.Replies != null ? Listing.Replies.MoreData : (more ?? new List<Things.More>()));
            Replies = (replies == null && Listing.Replies != null ? Lists.GetComments(Listing.Replies.Comments, Dispatch) : (replies ?? new List<Comment>()));
        }

        private void ImportToExisting(string subreddit = null, string author = null, string body = null, string bodyHtml = null,
            string parentFullname = null, string collapsedReason = null, bool? collapsed = null, bool? isSubmitter = null,
            List<Comment> replies = null, List<Things.More> more = null, bool? scoreHidden = null, int? depth = null, string id = null, string fullname = null,
            string permalink = null, DateTime? created = null, DateTime? edited = null,
            int? score = null, int? upVotes = null, int? downVotes = null, bool? removed = null, bool? spam = null)
        {
            if (Listing == null)
            {
                Import(
                    subreddit,
                    author,
                    body,
                    bodyHtml,
                    parentFullname,
                    collapsedReason,
                    collapsed ?? false,
                    isSubmitter ?? false,
                    replies,
                    more,
                    scoreHidden ?? false,
                    depth ?? 0,
                    id,
                    fullname,
                    permalink,
                    created ?? default(DateTime),
                    edited ?? default(DateTime),
                    score ?? 0,
                    upVotes ?? 0,
                    downVotes ?? 0,
                    removed ?? false,
                    spam ?? false
                );
            }
            else
            {
                Listing.Subreddit = (!string.IsNullOrEmpty(subreddit) ? subreddit : Listing.Subreddit);
                Listing.Author = (!string.IsNullOrEmpty(author) ? author : Listing.Author);
                Listing.Body = (!string.IsNullOrEmpty(body) ? body : Listing.Body);
                Listing.BodyHTML = (!string.IsNullOrEmpty(bodyHtml) ? bodyHtml : Listing.BodyHTML);
                Listing.ParentId = (!string.IsNullOrEmpty(parentFullname) ? parentFullname : Listing.ParentId);
                Listing.CollapsedReason = (!string.IsNullOrEmpty(collapsedReason) ? collapsedReason : Listing.CollapsedReason);
                Listing.Collapsed = (collapsed ?? Listing.Collapsed);
                Listing.IsSubmitter = (isSubmitter ?? Listing.IsSubmitter);
                Listing.ScoreHidden = (scoreHidden ?? Listing.ScoreHidden);
                Listing.Depth = (depth ?? Listing.Depth);
                Listing.Id = (!string.IsNullOrEmpty(id) ? id : Listing.Id);
                Listing.Name = (!string.IsNullOrEmpty(fullname) ? fullname : Listing.Name);
                Listing.Permalink = (!string.IsNullOrEmpty(permalink) ? permalink : Listing.Permalink);
                Listing.CreatedUTC = (created ?? Listing.CreatedUTC);
                Listing.Edited = (edited ?? Listing.Edited);
                Listing.Score = (score ?? Listing.Score);
                Listing.Ups = (upVotes ?? Listing.Ups);
                Listing.Downs = (downVotes ?? Listing.Downs);
                Listing.Removed = (removed ?? Listing.Removed);
                Listing.Spam = (spam ?? Listing.Spam);
            }

            More = (more == null && Listing.Replies != null ? Listing.Replies.MoreData : (more ?? new List<Things.More>()));
            Replies = (replies == null && Listing.Replies != null ? Lists.GetComments(Listing.Replies.Comments, Dispatch) : (replies ?? new List<Comment>()));
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
                info = Validate(Dispatch.LinksAndComments.Info(fullname, Subreddit, false));
                fullname = GetInfoPostOrCommentParentFullname(info);
            } while (info != null && info.Comments != null && info.Comments.Count > 0
                && !string.IsNullOrWhiteSpace(fullname) && !fullname.StartsWith("t3_"));

            Root = new Post(Dispatch, fullname).About();

            return root;
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
            return new Comment(Dispatch, Validate(Dispatch.LinksAndComments.Comment<Things.CommentResultContainer>(
                new LinksAndCommentsThingInput(Body, ParentFullname))).JSON.Data.Things[0].Data);
        }

        /// <summary>
        /// Submit this comment to Reddit asynchronously.
        /// </summary>
        /// <returns>An instance of this class populated with the return data.</returns>
        public async Task<Comment> SubmitAsync()
        {
            return new Comment(Dispatch, Validate(await Dispatch.LinksAndComments.CommentAsync<Things.CommentResultContainer>(
                new LinksAndCommentsThingInput(Body, ParentFullname))).JSON.Data.Things[0].Data);
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
            List<Comment> replies = null, List<Things.More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return BuildReply(body, bodyHtml, author, collapsedReason, collapsed, isSubmitter, replies, more, scoreHidden,
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
            List<Comment> replies = null, List<Things.More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return await BuildReply(body, bodyHtml, author, collapsedReason, collapsed, isSubmitter, replies, more, scoreHidden,
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
        /// <returns>The unsent comment reply instance.</returns>
        public Comment BuildReply(string body, string bodyHtml = null, string author = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, List<Things.More> more = null, bool scoreHidden = false, int depth = 0, string id = null, string fullname = null,
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime),
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            return new Comment(Dispatch, Subreddit, author, body, Fullname, bodyHtml, collapsedReason, collapsed, isSubmitter, replies, more, scoreHidden,
                depth, id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Return information about the current Comment instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Comment About()
        {
            if (Root != null)
            {
                return Validate(Lists.GetComments(Dispatch.Listings.GetComments(Root.Id, new ListingsGetCommentsInput(comment: Id, depth: 8), Subreddit), Dispatch))[0];
            }
            else
            {
                return Info();
            }
        }

        /// <summary>
        /// Return information about the current Comment instance via the api/info endpoint.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Comment Info()
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
            return Lists.GetComments(Validate(Dispatch.Moderation.DistinguishComment(how, Fullname, sticky)), Dispatch)[0];
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
            return Lists.GetComments(Validate(await Dispatch.Moderation.DistinguishCommentAsync(how, Fullname, sticky)), Dispatch)[0];
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

        // Note - Calling these MoreChildren methods here is the same as calling the ones in Post.  --Kris
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
        public Things.MoreChildren MoreChildren(string children, bool limitChildren, string sort, string id = null)
        {
            return MoreChildren(new LinksAndCommentsMoreChildrenInput(children, limitChildren, ParentFullname, sort, id));
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
            linksAndCommentsMoreChildrenInput.link_id = ParentFullname;

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
