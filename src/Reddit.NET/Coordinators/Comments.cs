using Reddit.Coordinators.EventArgs;
using Reddit.Coordinators.Internal;
using Reddit.Coordinators.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.Listings;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.Coordinators
{
    /// <summary>
    /// Coordinator class for comment replies.
    /// </summary>
    public class Comments : Monitors
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

        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

        /// <summary>
        /// A list of comments using "confidence" sort.
        /// </summary>
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

        /// <summary>
        /// A list of comments using "top" sort.
        /// </summary>
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

        /// <summary>
        /// A list of comments using "new" sort.
        /// </summary>
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

        /// <summary>
        /// A list of comments using "controversial" sort.
        /// </summary>
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

        /// <summary>
        /// A list of comments using "old" sort.
        /// </summary>
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

        /// <summary>
        /// A list of comments using "random" sort.
        /// </summary>
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

        /// <summary>
        /// A list of comments using "qa" sort.
        /// </summary>
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

        /// <summary>
        /// A list of comments using "live" sort.
        /// </summary>
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

        /// <summary>
        /// The parent comment (if one exists).
        /// </summary>
        public Comment Comment
        {
            get;
            private set;
        }

        public string SubKey;
        private Dispatch Dispatch;

        /// <summary>
        /// The name of the parent subreddit.
        /// </summary>
        private readonly string Subreddit;

        /// <summary>
        /// The ID36 of the parent post.
        /// </summary>
        private readonly string PostId;

        /// <summary>
        /// Create a new instance of the comments controller.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="postId">The ID36 of the parent post</param>
        /// <param name="subreddit">The name of the parent subreddit</param>
        /// <param name="comment">The parent comment instance</param>
        /// <param name="confidence"></param>
        /// <param name="top"></param>
        /// <param name="newComments"></param>
        /// <param name="controversial"></param>
        /// <param name="old"></param>
        /// <param name="random"></param>
        /// <param name="qa"></param>
        /// <param name="live"></param>
        public Comments(Dispatch dispatch, string postId = null, string subreddit = null, Comment comment = null, List<Comment> confidence = null, List<Comment> top = null, 
            List<Comment> newComments = null, List<Comment> controversial = null, List<Comment> old = null, List<Comment> random = null, List<Comment> qa = null, List<Comment> live = null) 
            : base()
        {
            Dispatch = dispatch;
            Subreddit = subreddit;
            PostId = postId;

            Confidence = confidence ?? new List<Comment>();
            Top = top ?? new List<Comment>();
            New = newComments ?? new List<Comment>();
            Controversial = controversial ?? new List<Comment>();
            Old = old ?? new List<Comment>();
            Random = random ?? new List<Comment>();
            QA = qa ?? new List<Comment>();
            Live = live ?? new List<Comment>();

            Comment = comment;

            SubKey = (comment?.Fullname != null ? comment.Fullname : "t3_" + PostId);
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
            List<Comment> comments = Lists.GetComments(Dispatch.Listings.GetComments(PostId, new ListingsGetCommentsInput(context, showEdits, showMore, sort, threaded, truncate, Comment?.Id,
                depth, limit, srDetail), Subreddit), Dispatch);

            List<Comment> replies = (Comment != null ? comments[0].Replies : comments);
            switch (sort)
            {
                case "confidence":
                    ConfidenceLastUpdated = DateTime.Now;
                    Confidence = replies;
                    break;
                case "top":
                    TopLastUpdated = DateTime.Now;
                    Top = replies;
                    break;
                case "new":
                    NewLastUpdated = DateTime.Now;
                    New = replies;
                    break;
                case "controversial":
                    ControversialLastUpdated = DateTime.Now;
                    Controversial = replies;
                    break;
                case "old":
                    OldLastUpdated = DateTime.Now;
                    Old = replies;
                    break;
                case "random":
                    RandomLastUpdated = DateTime.Now;
                    Random = replies;
                    break;
                case "qa":
                    QALastUpdated = DateTime.Now;
                    QA = replies;
                    break;
                case "live":
                    LiveLastUpdated = DateTime.Now;
                    Live = replies;
                    break;
            }

            return replies;
        }

        /// <summary>
        /// Retrieve a list of comments using "confidence" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetConfidence(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("confidence", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Retrieve a list of comments using "top" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetTop(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("top", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Retrieve a list of comments using "new" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetNew(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("new", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Retrieve a list of comments using "controversial" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetControversial(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("controversial", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Retrieve a list of comments using "old" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetOld(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("old", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Retrieve a list of comments using "random" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetRandom(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("random", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Retrieve a list of comments using "qa" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetQA(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false)
        {
            return GetComments("qa", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail);
        }

        /// <summary>
        /// Retrieve a list of comments using "live" sort.
        /// </summary>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of comments.</returns>
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
            return Monitor(key, new Thread(() => MonitorConfidenceThread(key)), SubKey);
        }

        private void MonitorConfidenceThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "confidence", SubKey);
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
            return Monitor(key, new Thread(() => MonitorTopThread(key)), SubKey);
        }

        private void MonitorTopThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "confidence", SubKey);
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
            return Monitor(key, new Thread(() => MonitorNewThread(key)), SubKey);
        }

        private void MonitorNewThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "new", SubKey);
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
            return Monitor(key, new Thread(() => MonitorControversialThread(key)), SubKey);
        }

        private void MonitorControversialThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "controversial", SubKey);
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
            return Monitor(key, new Thread(() => MonitorOldThread(key)), SubKey);
        }

        private void MonitorOldThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "old", SubKey);
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
            return Monitor(key, new Thread(() => MonitorRandomThread(key)), SubKey);
        }

        private void MonitorRandomThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "random", SubKey);
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
            return Monitor(key, new Thread(() => MonitorQAThread(key)), SubKey);
        }

        private void MonitorQAThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "qa", SubKey);
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
            return Monitor(key, new Thread(() => MonitorLiveThread(key)), SubKey);
        }

        private void MonitorLiveThread(string key)
        {
            MonitorCommentsThread(Monitoring, key, "live", SubKey);
        }

        internal virtual void OnLiveUpdated(CommentsUpdateEventArgs e)
        {
            LiveUpdated?.Invoke(this, e);
        }

        private void MonitorCommentsThread(MonitoringSnapshot monitoring, string key, string type, string subKey, int startDelayMs = 0)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            while (!Terminate
                && Monitoring.Get(key).Contains(subKey))
            {
                List<Comment> oldList;
                List<Comment> newList;
                switch (type)
                {
                    default:
                        throw new RedditCoordinatorException("Unrecognized type '" + type + "'.");
                    case "confidence":
                        oldList = confidence;
                        newList = GetConfidence();
                        break;
                    case "top":
                        oldList = top;
                        newList = GetTop();
                        break;
                    case "new":
                        oldList = newComments;
                        newList = GetNew();
                        break;
                    case "controversial":
                        oldList = controversial;
                        newList = GetControversial();
                        break;
                    case "old":
                        oldList = old;
                        newList = GetOld();
                        break;
                    case "random":
                        oldList = random;
                        newList = GetRandom();
                        break;
                    case "qa":
                        oldList = qa;
                        newList = GetQA();
                        break;
                    case "live":
                        oldList = live;
                        newList = GetLive();
                        break;
                }
                
                if (Lists.ListDiff(oldList, newList, out List<Comment> added, out List<Comment> removed))
                {
                    // Event handler to alert the calling app that the list has changed.  --Kris
                    CommentsUpdateEventArgs args = new CommentsUpdateEventArgs
                    {
                        NewComments = newList,
                        OldComments = oldList,
                        Added = added,
                        Removed = removed
                    };
                    TriggerUpdate(args, type);
                }

                Thread.Sleep(Monitoring.Count() * MonitoringWaitDelayMS);
            }
        }

        protected void TriggerUpdate(CommentsUpdateEventArgs args, string type)
        {
            switch (type)
            {
                case "confidence":
                    OnConfidenceUpdated(args);
                    break;
                case "top":
                    OnTopUpdated(args);
                    break;
                case "new":
                    OnNewUpdated(args);
                    break;
                case "controversial":
                    OnControversialUpdated(args);
                    break;
                case "old":
                    OnOldUpdated(args);
                    break;
                case "random":
                    OnRandomUpdated(args);
                    break;
                case "qa":
                    OnQAUpdated(args);
                    break;
                case "live":
                    OnLiveUpdated(args);
                    break;
            }
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0)
        {
            switch (key)
            {
                default:
                    throw new RedditCoordinatorException("Unrecognized key.");
                case "ConfidenceComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "confidence", SubKey, startDelayMs));
                case "TopComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "top", SubKey, startDelayMs));
                case "NewComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "new", SubKey, startDelayMs));
                case "ControversialComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "controversial", SubKey, startDelayMs));
                case "OldComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "old", SubKey, startDelayMs));
                case "RandomComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "random", SubKey, startDelayMs));
                case "QAComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "qa", SubKey, startDelayMs));
                case "LiveComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "live", SubKey, startDelayMs));
            }
        }
    }
}
