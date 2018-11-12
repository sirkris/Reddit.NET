using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Reddit.NET.Controllers.Structures
{
    public class SubredditPosts : BaseController
    {
        public event EventHandler<PostsUpdateEventArgs> BestUpdated;
        public event EventHandler<PostsUpdateEventArgs> HotUpdated;
        public event EventHandler<PostsUpdateEventArgs> NewUpdated;
        public event EventHandler<PostsUpdateEventArgs> RisingUpdated;
        public event EventHandler<PostsUpdateEventArgs> TopUpdated;
        public event EventHandler<PostsUpdateEventArgs> ControversialUpdated;

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
        private List<Post> best;

        public List<Post> Hot
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? hot : GetHot());
            }
            private set
            {
                hot = value;
            }
        }
        private List<Post> hot;

        public List<Post> New
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? newPosts : GetNew());
            }
            private set
            {
                newPosts = value;
            }
        }
        private List<Post> newPosts;

        public List<Post> Rising
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? rising : GetRising());
            }
            private set
            {
                rising = value;
            }
        }
        private List<Post> rising;

        public List<Post> Top
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? top : GetTop());
            }
            private set
            {
                top = value;
            }
        }
        private List<Post> top;

        public List<Post> Controversial
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? controversial : GetControversial());
            }
            private set
            {
                controversial = value;
            }
        }
        private List<Post> controversial;

        private DateTime? BestLastUpdated;
        private DateTime? HotLastUpdated;
        private DateTime? NewLastUpdated;
        private DateTime? RisingLastUpdated;
        private DateTime? TopLastUpdated;
        private DateTime? ControversialLastUpdated;

        private Dictionary<string, Thread> Threads;

        public Subreddit Subreddit
        {
            get;
            private set;
        }

        public SubredditPosts(Subreddit subreddit, List<Post> best = null, List<Post> hot = null, List<Post> newPosts = null,
            List<Post> rising = null, List<Post> top = null, List<Post> controversial = null)
        {
            Best = best ?? new List<Post>();
            Hot = hot ?? new List<Post>();
            New = newPosts ?? new List<Post>();
            Rising = rising ?? new List<Post>();
            Top = top ?? new List<Post>();
            Controversial = controversial ?? new List<Post>();

            BestLastUpdated = null;
            HotLastUpdated = null;
            NewLastUpdated = null;
            RisingLastUpdated = null;
            TopLastUpdated = null;
            ControversialLastUpdated = null;

            Threads = new Dictionary<string, Thread>();

            Subreddit = subreddit;

            MonitoringUpdated += C_MonitoringUpdated;
        }

        // Let's just pretend this one belongs to the "all" subreddit so we can put it here with the others.  --Kris
        public List<Post> GetBest(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Subreddit.Dispatch.Listings.Best(after, before, false, limit: limit));

            BestLastUpdated = DateTime.Now;

            Best = posts;
            return posts;
        }

        public List<Post> GetHot(string g = "", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Subreddit.Dispatch.Listings.Hot(g, after, before, false, limit: limit, subreddit: Subreddit.Name));

            HotLastUpdated = DateTime.Now;

            Hot = posts;
            return posts;
        }

        public List<Post> GetNew(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Subreddit.Dispatch.Listings.New(after, before, false, limit: limit, subreddit: Subreddit.Name));

            NewLastUpdated = DateTime.Now;

            New = posts;
            return posts;
        }

        public List<Post> GetRising(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Subreddit.Dispatch.Listings.Rising(after, before, false, limit: limit, subreddit: Subreddit.Name));

            RisingLastUpdated = DateTime.Now;

            Rising = posts;
            return posts;
        }

        public List<Post> GetTop(string t = "all", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Subreddit.Dispatch.Listings.Top(t, after, before, false, limit: limit, subreddit: Subreddit.Name));

            TopLastUpdated = DateTime.Now;

            Top = posts;
            return posts;
        }

        public List<Post> GetControversial(string t = "all", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Subreddit.Dispatch.Listings.Controversial(t, after, before, false, limit: limit, subreddit: Subreddit.Name));

            ControversialLastUpdated = DateTime.Now;

            Controversial = posts;
            return posts;
        }

        private List<Post> GetPosts(RedditThings.PostContainer postContainer)
        {
            List<Post> posts = new List<Post>();
            foreach (RedditThings.PostChild postChild in postContainer.Data.Children)
            {
                if (postChild.Data != null)
                {
                    if (postChild.Data.IsSelf)
                    {
                        posts.Add(new SelfPost(Subreddit.Dispatch, postChild.Data));
                    }
                    else
                    {
                        posts.Add(new LinkPost(Subreddit.Dispatch, postChild.Data));
                    }
                }
            }

            return posts;
        }

        private bool Monitor(string key, Thread thread)
        {
            if (Subreddit.Monitoring.ContainsKey(key))
            {
                // Stop monitoring.  --Kris
                Subreddit.Monitoring.Remove(key);
                WaitOrDie(Threads[key]);

                // Event handler to populate Monitoring across all controllers.  --Kris
                MonitoringUpdateEventArgs args = new MonitoringUpdateEventArgs
                {
                    Monitoring = Subreddit.Monitoring
                };
                OnMonitoringUpdated(args);

                return false;
            }
            else
            {
                // Start monitoring.  --Kris
                Subreddit.Monitoring.Add(key, null);

                Threads.Add(key, thread);
                Threads[key].Start();
                while (!Threads[key].IsAlive) { }

                // Event handler to populate Monitoring across all controllers.  --Kris
                MonitoringUpdateEventArgs args = new MonitoringUpdateEventArgs
                {
                    Monitoring = Subreddit.Monitoring
                };
                OnMonitoringUpdated(args);

                return true;
            }
        }

        private void MonitorThread(string key, string type)
        {
            while (Subreddit.Monitoring.ContainsKey(key))
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
                    }
                }

                Thread.Sleep(Subreddit.Monitoring.Count * MonitoringWaitDelayMS);
            }
        }

        /// <summary>
        /// Monitor Reddit for new "Best" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorBest()
        {
            string key = "BestPosts";
            return Monitor(key, new Thread(() => MonitorBestThread(key)));
        }

        private void MonitorBestThread(string key)
        {
            MonitorThread(key, "best");
        }

        protected virtual void OnBestUpdated(PostsUpdateEventArgs e)
        {
            BestUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Hot" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorHot()
        {
            string key = Subreddit.Name + "_HotPosts";
            return Monitor(key, new Thread(() => MonitorHotThread(key)));
        }

        private void MonitorHotThread(string key)
        {
            MonitorThread(key, "hot");
        }

        protected virtual void OnHotUpdated(PostsUpdateEventArgs e)
        {
            HotUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorNew()
        {
            string key = Subreddit.Name + "_NewPosts";
            return Monitor(key, new Thread(() => MonitorNewThread(key)));
        }

        private void MonitorNewThread(string key)
        {
            MonitorThread(key, "new");
        }

        protected virtual void OnNewUpdated(PostsUpdateEventArgs e)
        {
            NewUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Rising" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorRising()
        {
            string key = Subreddit.Name + "_RisingPosts";
            return Monitor(key, new Thread(() => MonitorRisingThread(key)));
        }

        private void MonitorRisingThread(string key)
        {
            MonitorThread(key, "rising");
        }

        protected virtual void OnRisingUpdated(PostsUpdateEventArgs e)
        {
            RisingUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Top" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorTop()
        {
            string key = Subreddit.Name + "_TopPosts";
            return Monitor(key, new Thread(() => MonitorTopThread(key)));
        }

        private void MonitorTopThread(string key)
        {
            MonitorThread(key, "top");
        }

        protected virtual void OnTopUpdated(PostsUpdateEventArgs e)
        {
            TopUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Controversial" posts.
        /// </summary>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorControversial()
        {
            string key = Subreddit.Name + "_ControversialPosts";
            return Monitor(key, new Thread(() => MonitorControversialThread(key)));
        }

        private void MonitorControversialThread(string key)
        {
            MonitorThread(key, "controversial");
        }

        protected virtual void OnControversialUpdated(PostsUpdateEventArgs e)
        {
            ControversialUpdated?.Invoke(this, e);
        }
    }
}
