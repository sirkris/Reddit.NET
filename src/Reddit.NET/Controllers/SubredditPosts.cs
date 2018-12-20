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
    public class SubredditPosts : BaseController
    {
        public event EventHandler<PostsUpdateEventArgs> BestUpdated;
        public event EventHandler<PostsUpdateEventArgs> HotUpdated;
        public event EventHandler<PostsUpdateEventArgs> NewUpdated;
        public event EventHandler<PostsUpdateEventArgs> RisingUpdated;
        public event EventHandler<PostsUpdateEventArgs> TopUpdated;
        public event EventHandler<PostsUpdateEventArgs> ControversialUpdated;

        public event EventHandler<PostsUpdateEventArgs> ModQueueUpdated;
        public event EventHandler<PostsUpdateEventArgs> ModQueueReportsUpdated;
        public event EventHandler<PostsUpdateEventArgs> ModQueueSpamUpdated;
        public event EventHandler<PostsUpdateEventArgs> ModQueueUnmoderatedUpdated;
        public event EventHandler<PostsUpdateEventArgs> ModQueueEditedUpdated;

        internal override ref Models.Internal.Monitor MonitorModel => ref Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

        public List<Post> Best
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? best : GetBest());
            }
            private set
            {
                best = value;
            }
        }
        internal List<Post> best;

        public List<Post> Hot
        {
            get
            {
                return (HotLastUpdated.HasValue
                    && HotLastUpdated.Value.AddSeconds(15) > DateTime.Now ? hot : GetHot());
            }
            private set
            {
                hot = value;
            }
        }
        internal List<Post> hot;

        public List<Post> New
        {
            get
            {
                return (NewLastUpdated.HasValue
                    && NewLastUpdated.Value.AddSeconds(15) > DateTime.Now ? newPosts : GetNew());
            }
            private set
            {
                newPosts = value;
            }
        }
        internal List<Post> newPosts;

        public List<Post> Rising
        {
            get
            {
                return (RisingLastUpdated.HasValue
                    && RisingLastUpdated.Value.AddSeconds(15) > DateTime.Now ? rising : GetRising());
            }
            private set
            {
                rising = value;
            }
        }
        internal List<Post> rising;

        public List<Post> Top
        {
            get
            {
                return (TopLastUpdated.HasValue
                    && TopLastUpdated.Value.AddSeconds(15) > DateTime.Now ? top : GetTop(TopT));
            }
            private set
            {
                top = value;
            }
        }
        internal List<Post> top;

        public List<Post> Controversial
        {
            get
            {
                return (ControversialLastUpdated.HasValue
                    && ControversialLastUpdated.Value.AddSeconds(15) > DateTime.Now ? controversial : GetControversial(ControversialT));
            }
            private set
            {
                controversial = value;
            }
        }
        internal List<Post> controversial;

        public List<Post> ModQueue
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? modQueue : GetModQueue());
            }
            private set
            {
                modQueue = value;
            }
        }
        internal List<Post> modQueue;

        public List<Post> ModQueueReports
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? modQueueReports : GetModQueue());
            }
            private set
            {
                modQueueReports = value;
            }
        }
        internal List<Post> modQueueReports;

        public List<Post> ModQueueSpam
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? modQueueSpam : GetModQueue());
            }
            private set
            {
                modQueueSpam = value;
            }
        }
        internal List<Post> modQueueSpam;

        public List<Post> ModQueueUnmoderated
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? modQueueUnmoderated : GetModQueue());
            }
            private set
            {
                modQueueUnmoderated = value;
            }
        }
        internal List<Post> modQueueUnmoderated;

        public List<Post> ModQueueEdited
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? modQueueEdited : GetModQueue());
            }
            private set
            {
                modQueueEdited = value;
            }
        }
        internal List<Post> modQueueEdited;

        private DateTime? BestLastUpdated;
        private DateTime? HotLastUpdated;
        private DateTime? NewLastUpdated;
        private DateTime? RisingLastUpdated;
        private DateTime? TopLastUpdated;
        private DateTime? ControversialLastUpdated;

        private DateTime? ModQueueLastUpdated;
        private DateTime? ModQueueReportsLastUpdated;
        private DateTime? ModQueueSpamLastUpdated;
        private DateTime? ModQueueUnmoderatedLastUpdated;
        private DateTime? ModQueueEditedLastUpdated;

        private string Subreddit;
        private Dispatch Dispatch;

        private string TopT = "all";
        private string ControversialT = "all";

        public SubredditPosts(ref Dispatch dispatch, string subreddit, List<Post> best = null, List<Post> hot = null, List<Post> newPosts = null,
            List<Post> rising = null, List<Post> top = null, List<Post> controversial = null, List<Post> modQueue = null, 
            List<Post> modQueueReports = null, List<Post> modQueueSpam = null, List<Post> modQueueUnmoderated = null, 
            List<Post> modQueueEdited = null) 
            : base()
        {
            Dispatch = dispatch;
            Subreddit = subreddit;

            Best = best ?? new List<Post>();
            Hot = hot ?? new List<Post>();
            New = newPosts ?? new List<Post>();
            Rising = rising ?? new List<Post>();
            Top = top ?? new List<Post>();
            Controversial = controversial ?? new List<Post>();

            ModQueue = modQueue ?? new List<Post>();
            ModQueueReports = modQueueReports ?? new List<Post>();
            ModQueueSpam = modQueueSpam ?? new List<Post>();
            ModQueueUnmoderated = modQueueUnmoderated ?? new List<Post>();
            ModQueueEdited = modQueueEdited ?? new List<Post>();
        }

        // Let's just pretend this one belongs to the "all" subreddit so we can put it here with the others.  --Kris
        public List<Post> GetBest(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Best(after, before, false, limit: limit), Dispatch);

            BestLastUpdated = DateTime.Now;

            Best = posts;
            return posts;
        }

        public List<Post> GetHot(string g = "", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Hot(g, after, before, false, limit: limit, subreddit: Subreddit), Dispatch);

            HotLastUpdated = DateTime.Now;

            Hot = posts;
            return posts;
        }

        public List<Post> GetNew(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.New(after, before, false, limit: limit, subreddit: Subreddit), Dispatch);

            NewLastUpdated = DateTime.Now;

            New = posts;
            return posts;
        }

        public List<Post> GetRising(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Rising(after, before, false, limit: limit, subreddit: Subreddit), Dispatch);

            RisingLastUpdated = DateTime.Now;

            Rising = posts;
            return posts;
        }

        public List<Post> GetTop(string t = "all", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Top(t, after, before, false, limit: limit, subreddit: Subreddit), Dispatch);

            TopLastUpdated = DateTime.Now;

            Top = posts;
            TopT = t;
            return posts;
        }

        public List<Post> GetControversial(string t = "all", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Controversial(t, after, before, false, limit: limit, subreddit: Subreddit), Dispatch);

            ControversialLastUpdated = DateTime.Now;

            Controversial = posts;
            ControversialT = t;
            return posts;
        }

        public List<Post> GetModQueuePosts(string location, string after = "", string before = "", int limit = 100, string show = "all",
            bool srDetail = false, int count = 0)
        {
            return GetPosts(Dispatch.Moderation.ModQueue(location, after, before, "links", Subreddit, count, limit, show, srDetail), Dispatch);
        }

        public List<Post> GetModQueue(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0)
        {
            List<Post> posts = GetModQueuePosts("modqueue", after, before, limit, show, srDetail, count);

            ModQueueLastUpdated = DateTime.Now;

            ModQueue = posts;
            return posts;
        }

        public List<Post> GetModQueueReports(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0)
        {
            List<Post> posts = GetModQueuePosts("reports", after, before, limit, show, srDetail, count);

            ModQueueReportsLastUpdated = DateTime.Now;

            ModQueueReports = posts;
            return posts;
        }

        public List<Post> GetModQueueSpam(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0)
        {
            List<Post> posts = GetModQueuePosts("spam", after, before, limit, show, srDetail, count);

            ModQueueSpamLastUpdated = DateTime.Now;

            ModQueueSpam = posts;
            return posts;
        }

        public List<Post> GetModQueueUnmoderated(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0)
        {
            List<Post> posts = GetModQueuePosts("unmoderated", after, before, limit, show, srDetail, count);

            ModQueueUnmoderatedLastUpdated = DateTime.Now;

            ModQueueUnmoderated = posts;
            return posts;
        }

        public List<Post> GetModQueueEdited(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0)
        {
            List<Post> posts = GetModQueuePosts("edited", after, before, limit, show, srDetail, count);

            ModQueueEditedLastUpdated = DateTime.Now;

            ModQueueEdited = posts;
            return posts;
        }

        /// <summary>
        /// Monitor Reddit for new "Best" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorBest()
        {
            string key = "BestPosts";
            return Monitor(key, new Thread(() => MonitorBestThread(key)), Subreddit);
        }

        private void MonitorBestThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "best", Subreddit);
        }

        internal virtual void OnBestUpdated(PostsUpdateEventArgs e)
        {
            BestUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Hot" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorHot()
        {
            string key = "HotPosts";
            return Monitor(key, new Thread(() => MonitorHotThread(key)), Subreddit);
        }

        private void MonitorHotThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "hot", Subreddit);
        }

        internal virtual void OnHotUpdated(PostsUpdateEventArgs e)
        {
            HotUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorNew()
        {
            string key = "NewPosts";
            return Monitor(key, new Thread(() => MonitorNewThread(key)), Subreddit);
        }

        private void MonitorNewThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "new", Subreddit);
        }

        internal virtual void OnNewUpdated(PostsUpdateEventArgs e)
        {
            NewUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Rising" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorRising()
        {
            string key = "RisingPosts";
            return Monitor(key, new Thread(() => MonitorRisingThread(key)), Subreddit);
        }

        private void MonitorRisingThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "rising", Subreddit);
        }

        internal virtual void OnRisingUpdated(PostsUpdateEventArgs e)
        {
            RisingUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Top" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorTop()
        {
            string key = "TopPosts";
            return Monitor(key, new Thread(() => MonitorTopThread(key)), Subreddit);
        }

        private void MonitorTopThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "top", Subreddit);
        }

        internal virtual void OnTopUpdated(PostsUpdateEventArgs e)
        {
            TopUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Controversial" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorControversial()
        {
            string key = "ControversialPosts";
            return Monitor(key, new Thread(() => MonitorControversialThread(key)), Subreddit);
        }

        private void MonitorControversialThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "controversial", Subreddit);
        }

        internal virtual void OnControversialUpdated(PostsUpdateEventArgs e)
        {
            ControversialUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "modqueue" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueue()
        {
            string key = "ModQueuePosts";
            return Monitor(key, new Thread(() => MonitorModQueueThread(key)), Subreddit);
        }

        private void MonitorModQueueThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "modqueue", Subreddit);
        }

        internal virtual void OnModQueueUpdated(PostsUpdateEventArgs e)
        {
            ModQueueUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "reports" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueReports()
        {
            string key = "ModQueueReportsPosts";
            return Monitor(key, new Thread(() => MonitorModQueueReportsThread(key)), Subreddit);
        }

        private void MonitorModQueueReportsThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "modqueuereports", Subreddit);
        }

        internal virtual void OnModQueueReportsUpdated(PostsUpdateEventArgs e)
        {
            ModQueueReportsUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "spam" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueSpam()
        {
            string key = "ModQueueSpamPosts";
            return Monitor(key, new Thread(() => MonitorModQueueSpamThread(key)), Subreddit);
        }

        private void MonitorModQueueSpamThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "modqueuespam", Subreddit);
        }

        internal virtual void OnModQueueSpamUpdated(PostsUpdateEventArgs e)
        {
            ModQueueSpamUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "unmoderated" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueUnmoderated()
        {
            string key = "ModQueueUnmoderatedPosts";
            return Monitor(key, new Thread(() => MonitorModQueueUnmoderatedThread(key)), Subreddit);
        }

        private void MonitorModQueueUnmoderatedThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "modqueueunmoderated", Subreddit);
        }

        internal virtual void OnModQueueUnmoderatedUpdated(PostsUpdateEventArgs e)
        {
            ModQueueUnmoderatedUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "edited" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueEdited()
        {
            string key = "ModQueueEditedPosts";
            return Monitor(key, new Thread(() => MonitorModQueueEditedThread(key)), Subreddit);
        }

        private void MonitorModQueueEditedThread(string key)
        {
            MonitorPostsThread(Monitoring, key, "modqueueedited", Subreddit);
        }

        internal virtual void OnModQueueEditedUpdated(PostsUpdateEventArgs e)
        {
            ModQueueEditedUpdated?.Invoke(this, e);
        }

        internal bool Monitor(string key, Thread thread, string subKey)
        {
            bool res = Monitor(key, thread, subKey, out Thread newThread);

            RebuildThreads();
            LaunchThreadIfNotNull(key, newThread);

            return res;
        }

        internal void RebuildThreads()
        {
            List<string> oldThreads = new List<string>(Threads.Keys);
            KillThreads(oldThreads);

            int i = 0;
            foreach (string key in oldThreads)
            {
                Threads.Add(key, CreateMonitoringThread(key, Subreddit, (i * MonitoringWaitDelayMS)));
                Threads[key].Start();
                i++;
            }
        }

        private Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "BestPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "best", Subreddit, startDelayMs));
                case "HotPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "hot", Subreddit, startDelayMs));
                case "NewPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "new", Subreddit, startDelayMs));
                case "RisingPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "rising", Subreddit, startDelayMs));
                case "TopPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "top", Subreddit, startDelayMs));
                case "ControversialPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "controversial", Subreddit, startDelayMs));
                case "ModQueuePosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueue", Subreddit, startDelayMs));
                case "ModQueueReportsPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueuereports", Subreddit, startDelayMs));
                case "ModQueueSpamPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueuespam", Subreddit, startDelayMs));
                case "ModQueueUnmoderatedPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueueunmoderated", Subreddit, startDelayMs));
                case "ModQueueEditedPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueueedited", Subreddit, startDelayMs));
            }
        }

        private void MonitorPostsThread(MonitoringSnapshot monitoring, string key, string type, string subKey, int startDelayMs = 0)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            while (!Terminate
                && Monitoring.Get(key).Contains(subKey))
            {
                List<Post> oldList;
                List<Post> newList;
                switch (type)
                {
                    default:
                        throw new RedditControllerException("Unrecognized type '" + type + "'.");
                    case "best":
                        oldList = best;
                        newList = GetBest();
                        break;
                    case "hot":
                        oldList = hot;
                        newList = GetHot();
                        break;
                    case "new":
                        oldList = newPosts;
                        newList = GetNew();
                        break;
                    case "rising":
                        oldList = rising;
                        newList = GetRising();
                        break;
                    case "top":
                        oldList = top;
                        newList = GetTop();
                        break;
                    case "controversial":
                        oldList = controversial;
                        newList = GetControversial();
                        break;
                    case "modqueue":
                        oldList = modQueue;
                        newList = GetModQueue();
                        break;
                    case "modqueuereports":
                        oldList = modQueueReports;
                        newList = GetModQueueReports();
                        break;
                    case "modqueuespam":
                        oldList = modQueueSpam;
                        newList = GetModQueueSpam();
                        break;
                    case "modqueueunmoderated":
                        oldList = modQueueUnmoderated;
                        newList = GetModQueueUnmoderated();
                        break;
                    case "modqueueedited":
                        oldList = modQueueEdited;
                        newList = GetModQueueEdited();
                        break;
                }

                if (ListDiff(oldList, newList, out List<Post> added, out List<Post> removed))
                {
                    // Event handler to alert the calling app that the list has changed.  --Kris
                    PostsUpdateEventArgs args = new PostsUpdateEventArgs
                    {
                        NewPosts = newList,
                        OldPosts = oldList,
                        Added = added,
                        Removed = removed
                    };
                    TriggerUpdate(args, type);
                }

                Thread.Sleep(Monitoring.Count() * MonitoringWaitDelayMS);
            }
        }

        private void TriggerUpdate(PostsUpdateEventArgs args, string type)
        {
            switch (type)
            {
                case "best":
                    OnBestUpdated(args);
                    break;
                case "hot":
                    OnHotUpdated(args);
                    break;
                case "new":
                    OnNewUpdated(args);
                    break;
                case "rising":
                    OnRisingUpdated(args);
                    break;
                case "top":
                    OnTopUpdated(args);
                    break;
                case "controversial":
                    OnControversialUpdated(args);
                    break;
                case "modqueue":
                    OnModQueueUpdated(args);
                    break;
                case "modqueuereports":
                    OnModQueueReportsUpdated(args);
                    break;
                case "modqueuespam":
                    OnModQueueSpamUpdated(args);
                    break;
                case "modqueueunmoderated":
                    OnModQueueUnmoderatedUpdated(args);
                    break;
                case "modqueueedited":
                    OnModQueueEditedUpdated(args);
                    break;
            }
        }
    }
}
