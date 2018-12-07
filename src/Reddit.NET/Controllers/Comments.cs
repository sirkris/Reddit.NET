using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Structures;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Reddit.NET.Controllers
{
    public class Comments : BaseController
    {
        public event EventHandler<CommentsUpdateEventArgs> ConfidenceUpdated;
        public event EventHandler<CommentsUpdateEventArgs> TopUpdated;
        public event EventHandler<CommentsUpdateEventArgs> NewUpdated;
        public event EventHandler<CommentsUpdateEventArgs> ControversialUpdated;
        public event EventHandler<CommentsUpdateEventArgs> OldUpdated;
        public event EventHandler<CommentsUpdateEventArgs> RandomUpdated;
        public event EventHandler<CommentsUpdateEventArgs> QAUpdated;
        public event EventHandler<CommentsUpdateEventArgs> LiveUpdated;

        private DateTime? ConfidenceLastUpdated;
        private DateTime? TopLastUpdated;
        private DateTime? NewLastUpdated;
        private DateTime? ControversialLastUpdated;
        private DateTime? OldLastUpdated;
        private DateTime? RandomLastUpdated;
        private DateTime? QALastUpdated;
        private DateTime? LiveLastUpdated;

        internal override ref Models.Internal.Monitor MonitorModel => ref Post.Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

        public List<Comment> Confidence
        {
            get
            {
                return (ConfidenceLastUpdated.HasValue
                    && ConfidenceLastUpdated.Value.AddSeconds(15) > DateTime.Now ? confidence : GetConfidence());
            }
            private set
            {
                confidence = value;
            }
        }
        internal List<Comment> confidence;

        public List<Comment> Top
        {
            get
            {
                return (TopLastUpdated.HasValue
                    && TopLastUpdated.Value.AddSeconds(15) > DateTime.Now ? top : GetTop());
            }
            private set
            {
                top = value;
            }
        }
        internal List<Comment> top;

        public List<Comment> New
        {
            get
            {
                return (NewLastUpdated.HasValue
                    && NewLastUpdated.Value.AddSeconds(15) > DateTime.Now ? newComments : GetNew());
            }
            private set
            {
                newComments = value;
            }
        }
        internal List<Comment> newComments;

        public List<Comment> Controversial
        {
            get
            {
                return (ControversialLastUpdated.HasValue
                    && ControversialLastUpdated.Value.AddSeconds(15) > DateTime.Now ? controversial : GetControversial());
            }
            private set
            {
                controversial = value;
            }
        }
        internal List<Comment> controversial;

        public List<Comment> Old
        {
            get
            {
                return (OldLastUpdated.HasValue
                    && OldLastUpdated.Value.AddSeconds(15) > DateTime.Now ? old : GetOld());
            }
            private set
            {
                old = value;
            }
        }
        internal List<Comment> old;

        public List<Comment> Random
        {
            get
            {
                return (RandomLastUpdated.HasValue
                    && RandomLastUpdated.Value.AddSeconds(15) > DateTime.Now ? random : GetRandom());
            }
            private set
            {
                random = value;
            }
        }
        internal List<Comment> random;

        public List<Comment> QA
        {
            get
            {
                return (QALastUpdated.HasValue
                    && QALastUpdated.Value.AddSeconds(15) > DateTime.Now ? qa : GetQA());
            }
            private set
            {
                qa = value;
            }
        }
        internal List<Comment> qa;

        public List<Comment> Live
        {
            get
            {
                return (LiveLastUpdated.HasValue
                    && LiveLastUpdated.Value.AddSeconds(15) > DateTime.Now ? live : GetLive());
            }
            private set
            {
                live = value;
            }
        }
        internal List<Comment> live;
        
        public Post Post
        {
            get;
            private set;
        }

        public Comment Comment
        {
            get;
            private set;
        }

        public string SubKey;

        public Comments(Post post = null, Comment comment = null, List<Comment> confidence = null, List<Comment> top = null, List<Comment> newComments = null, List<Comment> controversial = null,
            List<Comment> old = null, List<Comment> random = null, List<Comment> qa = null, List<Comment> live = null) 
            : base()
        {
            Confidence = confidence ?? new List<Comment>();
            Top = top ?? new List<Comment>();
            New = newComments ?? new List<Comment>();
            Controversial = controversial ?? new List<Comment>();
            Old = old ?? new List<Comment>();
            Random = random ?? new List<Comment>();
            QA = qa ?? new List<Comment>();
            Live = live ?? new List<Comment>();

            Post = post;
            Comment = comment;

            SubKey = (comment?.Fullname != null ? comment.Fullname : post.Fullname);
        }

        /// <summary>
        /// Retrieve comment replies to this comment.
        /// </summary>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live)</param>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetComments(string sort = "new", int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            List<Comment> comments = GetComments(Post.Dispatch.Listings.GetComments(Post.Id, context, showEdits, showMore, sort, threaded, truncate, Post.Subreddit, Comment?.Id,
                depth, limit, srDetail), Post.Dispatch);

            switch (sort)
            {
                case "confidence":
                    ConfidenceLastUpdated = DateTime.Now;
                    Confidence = comments[0].Replies;
                    break;
                case "top":
                    TopLastUpdated = DateTime.Now;
                    Top = comments[0].Replies;
                    break;
                case "new":
                    NewLastUpdated = DateTime.Now;
                    New = comments[0].Replies;
                    break;
                case "controversial":
                    ControversialLastUpdated = DateTime.Now;
                    Controversial = comments[0].Replies;
                    break;
                case "old":
                    OldLastUpdated = DateTime.Now;
                    Old = comments[0].Replies;
                    break;
                case "random":
                    RandomLastUpdated = DateTime.Now;
                    Random = comments[0].Replies;
                    break;
                case "qa":
                    QALastUpdated = DateTime.Now;
                    QA = comments[0].Replies;
                    break;
                case "live":
                    LiveLastUpdated = DateTime.Now;
                    Live = comments[0].Replies;
                    break;
            }

            return comments[0].Replies;
        }

        public List<Comment> GetConfidence(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("confidence", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        public List<Comment> GetTop(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("top", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        public List<Comment> GetNew(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("new", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        public List<Comment> GetControversial(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("controversial", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        public List<Comment> GetOld(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("old", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        public List<Comment> GetRandom(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("random", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        public List<Comment> GetQA(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("qa", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        public List<Comment> GetLive(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("live", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Monitor Reddit for new "confidence" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorConfidence()
        {
            string key = "ConfidenceComments";
            return Monitor(key, new Thread(() => MonitorConfidenceThread(key)), SubKey, this);
        }

        private void MonitorConfidenceThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "confidence", SubKey);
        }

        internal virtual void OnConfidenceUpdated(CommentsUpdateEventArgs e)
        {
            ConfidenceUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "top" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorTop()
        {
            string key = "TopComments";
            return Monitor(key, new Thread(() => MonitorTopThread(key)), SubKey, this);
        }

        private void MonitorTopThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "confidence", SubKey);
        }

        internal virtual void OnTopUpdated(CommentsUpdateEventArgs e)
        {
            TopUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "new" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorNew()
        {
            string key = "NewComments";
            return Monitor(key, new Thread(() => MonitorNewThread(key)), SubKey, this);
        }

        private void MonitorNewThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "new", SubKey);
        }

        internal virtual void OnNewUpdated(CommentsUpdateEventArgs e)
        {
            NewUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "controversial" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorControversial()
        {
            string key = "ControversialComments";
            return Monitor(key, new Thread(() => MonitorControversialThread(key)), SubKey, this);
        }

        private void MonitorControversialThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "controversial", SubKey);
        }

        internal virtual void OnControversialUpdated(CommentsUpdateEventArgs e)
        {
            ControversialUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "old" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorOld()
        {
            string key = "OldComments";
            return Monitor(key, new Thread(() => MonitorOldThread(key)), SubKey, this);
        }

        private void MonitorOldThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "old", SubKey);
        }

        internal virtual void OnOldUpdated(CommentsUpdateEventArgs e)
        {
            OldUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "random" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorRandom()
        {
            string key = "RandomComments";
            return Monitor(key, new Thread(() => MonitorRandomThread(key)), SubKey, this);
        }

        private void MonitorRandomThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "random", SubKey);
        }

        internal virtual void OnRandomUpdated(CommentsUpdateEventArgs e)
        {
            RandomUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "qa" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorQA()
        {
            string key = "QAComments";
            return Monitor(key, new Thread(() => MonitorQAThread(key)), SubKey, this);
        }

        private void MonitorQAThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "qa", SubKey);
        }

        internal virtual void OnQAUpdated(CommentsUpdateEventArgs e)
        {
            QAUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "live" comments on this thread.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorLive()
        {
            string key = "LiveComments";
            return Monitor(key, new Thread(() => MonitorLiveThread(key)), SubKey, this);
        }

        private void MonitorLiveThread(string key)
        {
            MonitorCommentsThread(Monitoring, this, key, "live", SubKey);
        }

        internal virtual void OnLiveUpdated(CommentsUpdateEventArgs e)
        {
            LiveUpdated?.Invoke(this, e);
        }
    }
}
