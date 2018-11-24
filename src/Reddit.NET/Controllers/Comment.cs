using Reddit.NET.Controllers.Structures;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
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
        public string Body;
        public string BodyHTML;
        public string ParentId;
        public string ParentFullname;
        public string CollapsedReason;
        public bool Collapsed;
        public bool IsSubmitter;
        public bool ScoreHidden;
        public int Depth;

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

        internal override ref Models.Internal.Monitor MonitorModel => ref MonitorNull;
        internal override ref MonitoringSnapshot Monitoring => ref MonitoringSnapshotNull;

        public RedditThings.Comment Listing;

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

        internal readonly Dispatch Dispatch;

        public Comment(Dispatch dispatch, RedditThings.Comment listing)
        {
            Dispatch = dispatch;
            Import(listing);
        }
        
        public Comment(Dispatch dispatch, string subreddit, string author, string body, string parentFullname, string bodyHtml = null,
            string collapsedReason = null, bool collapsed = false, bool isSubmitter = false,
            List<Comment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string name = null, 
            string permalink = null, DateTime created = default(DateTime), DateTime edited = default(DateTime), 
            int score = 0, int upVotes = 0, int downVotes = 0, bool removed = false, bool spam = false)
        {
            Dispatch = dispatch;
            Import(subreddit, author, body, bodyHtml, parentFullname, collapsedReason, collapsed, isSubmitter, replies, scoreHidden,
                depth, id, name, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        public Comment(Dispatch dispatch, string name)
        {
            Dispatch = dispatch;
            Fullname = name;
        }

        public Comment(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        private Comments InitComments()
        {
            Comments = new Comments(new Post(Dispatch, Root.Fullname).About(), this);
            return Comments;
        }

        private void Import(RedditThings.Comment listing)
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
            Replies = GetComments(listing.Replies, Dispatch);
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
            List<Comment> replies = null, bool scoreHidden = false, int depth = 0, string id = null, string name = null,
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
            Fullname = name;
            Permalink = permalink;
            Created = created;
            Edited = edited;
            Score = score;
            UpVotes = upVotes;
            DownVotes = downVotes;
            Removed = removed;
            Spam = spam;

            Listing = new RedditThings.Comment(this);
        }

        public Post GetRoot(string fullname = null)
        {
            fullname = fullname ?? ParentFullname;
            if (string.IsNullOrWhiteSpace(fullname))
            {
                return null;
            }

            RedditThings.Info info = null;
            do
            {
                info = Validate(Dispatch.LinksAndComments.Info(fullname, Subreddit));
                fullname = GetInfoPostOrCommentParentFullname(info);
            } while (info != null && info.Comments != null && info.Comments.Count > 0
                && !string.IsNullOrWhiteSpace(fullname) && !fullname.StartsWith("t3_"));

            return new Post(Dispatch, fullname).About();
        }

        private string GetInfoPostOrCommentParentFullname(RedditThings.Info info)
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
        /// <param name="body">raw markdown text</param>
        /// <returns>An instance of this class populated with the return data.</returns>
        public Comment Submit(string body)
        {
            return new Comment(Dispatch, Validate(Dispatch.LinksAndComments.Comment(false, null, body, Fullname)).JSON.Data.Things[0].Data);
        }

        /// <summary>
        /// Return information about the current Comment instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Comment About()
        {
            RedditThings.Info info = Validate(Dispatch.LinksAndComments.Info(Fullname, Subreddit));
            if (info == null
                || info.Comments == null
                || info.Comments.Count == 0
                || Fullname.Equals(info.Comments[0].Name))
            {
                throw new RedditControllerException("Unable to retrieve comment data.");
            }

            return new Comment(Dispatch, info.Comments[0]);
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
        public async void DeleteAsync()
        {
            await Task.Run(() =>
            {
                Delete();
            });
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
            Validate(Dispatch.LinksAndComments.Report(additionalInfo, banEvadingAccountsNames, customText, fromHelpCenter, otherReason, reason,
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
        public async void ReportAsync(string additionalInfo, string banEvadingAccountsNames, string customText, bool fromHelpCenter,
            string otherReason, string reason, string ruleReason, string siteReason, string violatorUsername)
        {
            await Task.Run(() =>
            {
                Report(additionalInfo, banEvadingAccountsNames, customText, fromHelpCenter, otherReason, reason, ruleReason, siteReason, violatorUsername);
            });
        }

        /// <summary>
        /// Save this comment.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// </summary>
        /// <param name="category">a category name</param>
        public void Save(string category)
        {
            Dispatch.LinksAndComments.Save(category, Fullname);
        }

        /// <summary>
        /// Save this comment asynchronously.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// </summary>
        /// <param name="category">a category name</param>
        public async void SaveAsync(string category)
        {
            await Task.Run(() =>
            {
                Save(category);
            });
        }

        /// <summary>
        /// Enable inbox replies for this comment.
        /// </summary>
        public void EnableSendReplies()
        {
            Dispatch.LinksAndComments.SendReplies(Fullname, true);
        }

        /// <summary>
        /// Enable inbox replies for this comment asynchronously.
        /// </summary>
        public async void EnableSendRepliesAsync()
        {
            await Task.Run(() =>
            {
                EnableSendReplies();
            });
        }

        /// <summary>
        /// Disable inbox replies for this comment.
        /// </summary>
        public void DisableSendReplies()
        {
            Dispatch.LinksAndComments.SendReplies(Fullname, false);
        }

        /// <summary>
        /// Disable inbox replies for this comment asynchronously.
        /// </summary>
        public async void DisableSendRepliesAsync()
        {
            await Task.Run(() =>
            {
                DisableSendReplies();
            });
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
        public async void UnsaveAsync()
        {
            await Task.Run(() =>
            {
                Unsave();
            });
        }

        /// <summary>
        /// Edit the body text of this comment.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        /// <returns>This instance populated with the modified post data returned by the API.</returns>
        public Comment Edit(string text)
        {
            Import(Validate(Dispatch.LinksAndComments.EditUserTextComment(false, null, text, Fullname)).JSON.Data.Things[0].Data);

            return this;
        }

        /// <summary>
        /// Edit the body text of this comment asynchronously.  This instance will be automatically updated with the return data.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        public async void EditAsync(string text)
        {
            await Task.Run(() =>
            {
                Edit(text);
            });
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
        public RedditThings.MoreChildren MoreChildren(bool limitChildren, string sort, string id = null)
        {
            return Validate(Dispatch.LinksAndComments.MoreChildren(Id, limitChildren, ParentFullname, sort, id));
        }

        // TODO - Vote methods.  --Kris
    }
}
