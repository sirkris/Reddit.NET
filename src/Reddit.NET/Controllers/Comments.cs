using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.Listings;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for comment replies.
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

        private DateTime? ConfidenceLastUpdated { get; set; }
        private DateTime? TopLastUpdated { get; set; }
        private DateTime? NewLastUpdated { get; set; }
        private DateTime? ControversialLastUpdated { get; set; }
        private DateTime? OldLastUpdated { get; set; }
        private DateTime? RandomLastUpdated { get; set; }
        private DateTime? QALastUpdated { get; set; }
        private DateTime? LiveLastUpdated { get; set; }
        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }

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
        /// A list interface of comments using "confidence" sort.
        /// </summary>
        public IList<Comment> IConfidence
        {
            get
            {
                return (ConfidenceLastUpdated.HasValue
                    && ConfidenceLastUpdated.Value.AddSeconds(15) > DateTime.Now ? iconfidence : GetConfidence(isInterface: true));
            }
            private set
            {
                iconfidence = value;
            }
        }
        internal IList<Comment> iconfidence;

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
        /// A list interface of comments using "top" sort.
        /// </summary>
        public IList<Comment> ITop
        {
            get
            {
                return (TopLastUpdated.HasValue
                    && TopLastUpdated.Value.AddSeconds(15) > DateTime.Now ? itop : GetTop(isInterface: true));
            }
            private set
            {
                itop = value;
            }
        }
        internal IList<Comment> itop;

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
        /// A list interface of comments using "new" sort.
        /// </summary>
        public IList<Comment> INew
        {
            get
            {
                return (NewLastUpdated.HasValue
                    && NewLastUpdated.Value.AddSeconds(15) > DateTime.Now ? inewComments : GetNew(isInterface: true));
            }
            private set
            {
                inewComments = value;
            }
        }
        internal IList<Comment> inewComments;

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
        /// A list interface of comments using "controversial" sort.
        /// </summary>
        public IList<Comment> IControversial
        {
            get
            {
                return (ControversialLastUpdated.HasValue
                    && ControversialLastUpdated.Value.AddSeconds(15) > DateTime.Now ? icontroversial : GetControversial(isInterface: true));
            }
            private set
            {
                icontroversial = value;
            }
        }
        internal IList<Comment> icontroversial;

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
        /// A list interface of comments using "old" sort.
        /// </summary>
        public IList<Comment> IOld
        {
            get
            {
                return (OldLastUpdated.HasValue
                    && OldLastUpdated.Value.AddSeconds(15) > DateTime.Now ? iold : GetOld(isInterface: true));
            }
            private set
            {
                iold = value;
            }
        }
        internal IList<Comment> iold;

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
        /// A list interface of comments using "random" sort.
        /// </summary>
        public IList<Comment> IRandom
        {
            get
            {
                return (RandomLastUpdated.HasValue
                    && RandomLastUpdated.Value.AddSeconds(15) > DateTime.Now ? irandom : GetRandom(isInterface: true));
            }
            private set
            {
                irandom = value;
            }
        }
        internal IList<Comment> irandom;

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
        /// A list interface of comments using "qa" sort.
        /// </summary>
        public IList<Comment> IQA
        {
            get
            {
                return (QALastUpdated.HasValue
                    && QALastUpdated.Value.AddSeconds(15) > DateTime.Now ? iqa : GetQA(isInterface: true));
            }
            private set
            {
                iqa = value;
            }
        }
        internal IList<Comment> iqa;

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
        /// A list interface of comments using "live" sort.
        /// </summary>
        public IList<Comment> ILive
        {
            get
            {
                return (LiveLastUpdated.HasValue
                    && LiveLastUpdated.Value.AddSeconds(15) > DateTime.Now ? ilive : GetLive(isInterface: true));
            }
            private set
            {
                ilive = value;
            }
        }
        internal IList<Comment> ilive;

        /// <summary>
        /// The parent comment (if one exists).
        /// </summary>
        public Comment Comment
        {
            get;
            private set;
        }

        public string SubKey { get; set; }
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

            SubKey = comment?.Fullname != null ? comment.Fullname : (!string.IsNullOrEmpty(PostId) ? "t3_" + PostId : subreddit);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetComments(string sort = "new", int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            List<Comment> comments = Lists.GetComments(Dispatch.Listings.GetComments(PostId, new ListingsGetCommentsInput(context, showEdits, showMore, sort, threaded, truncate, Comment?.Id,
                depth, limit, srDetail), Subreddit), Dispatch);

            List<Comment> replies = (Comment != null ? comments[0].Replies : comments);
            switch (sort)
            {
                case "confidence":
                    ConfidenceLastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        Confidence = replies;
                    }
                    else
                    {
                        IConfidence = replies;
                    }
                    break;
                case "top":
                    TopLastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        Top = replies;
                    }
                    else
                    {
                        ITop = replies;
                    }
                    break;
                case "new":
                    NewLastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        New = replies;
                    }
                    else
                    {
                        INew = replies;
                    }
                    break;
                case "controversial":
                    ControversialLastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        Controversial = replies;
                    }
                    else
                    {
                        IControversial = replies;
                    }
                    break;
                case "old":
                    OldLastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        Old = replies;
                    }
                    else
                    {
                        IOld = replies;
                    }
                    break;
                case "random":
                    RandomLastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        Random = replies;
                    }
                    else
                    {
                        IRandom = replies;
                    }
                    break;
                case "qa":
                    QALastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        QA = replies;
                    }
                    else
                    {
                        IQA = replies;
                    }
                    break;
                case "live":
                    LiveLastUpdated = DateTime.Now;
                    if (!isInterface)
                    {
                        Live = replies;
                    }
                    else
                    {
                        ILive = replies;
                    }
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetConfidence(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("confidence", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetTop(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("top", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetNew(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("new", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetControversial(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("controversial", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetOld(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("old", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetRandom(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("random", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetQA(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("qa", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
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
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of comments.</returns>
        public List<Comment> GetLive(int context = 3, int truncate = 0, bool showEdits = false, bool showMore = true,
            bool threaded = true, int? depth = null, int? limit = null, bool srDetail = false, bool isInterface = false)
        {
            return GetComments("live", context, truncate, showEdits, showMore, threaded, depth, limit, srDetail, isInterface);
        }

        /// <summary>
        /// Monitor Reddit for new "confidence" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorConfidence(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null, 
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

            string key = "ConfidenceComments";
            return Monitor(key, new Thread(() => MonitorConfidenceThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorConfidenceThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "confidence", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnConfidenceUpdated(CommentsUpdateEventArgs e)
        {
            ConfidenceUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "top" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorTop(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "TopComments";
            return Monitor(key, new Thread(() => MonitorTopThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorTopThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "confidence", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnTopUpdated(CommentsUpdateEventArgs e)
        {
            TopUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "new" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorNew(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "NewComments";
            return Monitor(key, new Thread(() => MonitorNewThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorNewThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "new", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnNewUpdated(CommentsUpdateEventArgs e)
        {
            NewUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "controversial" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorControversial(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "ControversialComments";
            return Monitor(key, new Thread(() => MonitorControversialThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorControversialThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "controversial", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnControversialUpdated(CommentsUpdateEventArgs e)
        {
            ControversialUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "old" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorOld(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "OldComments";
            return Monitor(key, new Thread(() => MonitorOldThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorOldThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "old", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnOldUpdated(CommentsUpdateEventArgs e)
        {
            OldUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "random" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorRandom(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "RandomComments";
            return Monitor(key, new Thread(() => MonitorRandomThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorRandomThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "random", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnRandomUpdated(CommentsUpdateEventArgs e)
        {
            RandomUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "qa" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorQA(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "QAComments";
            return Monitor(key, new Thread(() => MonitorQAThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorQAThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "qa", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnQAUpdated(CommentsUpdateEventArgs e)
        {
            QAUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor Reddit for new "live" comments on this thread.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorLive(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "LiveComments";
            return Monitor(key, new Thread(() => MonitorLiveThread(key, monitoringDelayMs)), SubKey);
        }

        private void MonitorLiveThread(string key, int? monitoringDelayMs = null)
        {
            MonitorCommentsThread(Monitoring, key, "live", SubKey, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnLiveUpdated(CommentsUpdateEventArgs e)
        {
            LiveUpdated?.Invoke(this, e);
        }

        private void MonitorCommentsThread(MonitoringSnapshot monitoring, string key, string type, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
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

                List<Comment> oldList;
                List<Comment> newList;
                try
                {
                    switch (type)
                    {
                        default:
                            throw new RedditControllerException("Unrecognized type '" + type + "'.");
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
                }
                catch (Exception) when (!BreakOnFailure) { }

                Wait(monitoringDelayMs.Value);
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

        public bool ConfidenceCommentsIsMonitored()
        {
            return IsMonitored("ConfidenceComments", SubKey);
        }

        public bool TopCommentsIsMonitored()
        {
            return IsMonitored("TopComments", SubKey);
        }

        public bool NewCommentsIsMonitored()
        {
            return IsMonitored("NewComments", SubKey);
        }

        public bool ControversialCommentsIsMonitored()
        {
            return IsMonitored("ControversialComments", SubKey);
        }

        public bool OldCommentsIsMonitored()
        {
            return IsMonitored("OldComments", SubKey);
        }

        public bool RandomCommentsIsMonitored()
        {
            return IsMonitored("RandomComments", SubKey);
        }

        public bool QACommentsIsMonitored()
        {
            return IsMonitored("QAComments", SubKey);
        }

        public bool LiveCommentsIsMonitored()
        {
            return IsMonitored("LiveComments", SubKey);
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "ConfidenceComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "confidence", SubKey, startDelayMs, monitoringDelayMs));
                case "TopComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "top", SubKey, startDelayMs, monitoringDelayMs));
                case "NewComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "new", SubKey, startDelayMs, monitoringDelayMs));
                case "ControversialComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "controversial", SubKey, startDelayMs, monitoringDelayMs));
                case "OldComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "old", SubKey, startDelayMs, monitoringDelayMs));
                case "RandomComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "random", SubKey, startDelayMs, monitoringDelayMs));
                case "QAComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "qa", SubKey, startDelayMs, monitoringDelayMs));
                case "LiveComments":
                    return new Thread(() => MonitorCommentsThread(Monitoring, key, "live", SubKey, startDelayMs, monitoringDelayMs));
            }
        }
    }
}
