using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs;
using Reddit.Inputs.Listings;
using Reddit.Inputs.Moderation;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for subreddit post listings.
    /// </summary>
    public class SubredditPosts : Monitors
    {
        /// <summary>
        /// Event handler for monitoring best.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> BestUpdated;

        /// <summary>
        /// Event handler for monitoring hot.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> HotUpdated;

        /// <summary>
        /// Event handler for monitoring new.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> NewUpdated;

        /// <summary>
        /// Event handler for monitoring rising.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> RisingUpdated;

        /// <summary>
        /// Event handler for monitoring top.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> TopUpdated;

        /// <summary>
        /// Event handler for monitoring controversial.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> ControversialUpdated;

        /// <summary>
        /// Event handler for monitoring modqueu.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> ModQueueUpdated;

        /// <summary>
        /// Event handler for monitoring modqueue reports.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> ModQueueReportsUpdated;

        /// <summary>
        /// Event handler for monitoring modqueue spam.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> ModQueueSpamUpdated;

        /// <summary>
        /// Event handler for monitoring modqueue unmoderated.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> ModQueueUnmoderatedUpdated;

        /// <summary>
        /// Event handler for monitoring modqueue edited.
        /// </summary>
        public event EventHandler<PostsUpdateEventArgs> ModQueueEditedUpdated;

        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }
        internal override HashSet<string> UseCache { get; set; } = new HashSet<string>();

        /// <summary>
        /// (deprecated) List of posts using "best" sort.
        /// </summary>
        [Obsolete("This property has been deprecated.  Please use the top-level RedditClient.FrontPage property instead.")]
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

        /// <summary>
        /// (deprecated) IList of posts using "best" sort.
        /// </summary>
        [Obsolete("This property has been deprecated.  Please use the top-level RedditClient.FrontPage property instead.")]
        public IList<Post> IBest
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? ibest : GetBest(isInterface: true));
            }
            private set
            {
                ibest = value;
            }
        }
        internal IList<Post> ibest;

        /// <summary>
        /// List of posts using "hot" sort.
        /// </summary>
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

        /// <summary>
        /// IList of posts using "hot" sort.
        /// </summary>
        public IList<Post> IHot
        {
            get
            {
                return (HotLastUpdated.HasValue
                    && HotLastUpdated.Value.AddSeconds(15) > DateTime.Now ? ihot : GetHot(isInterface: true));
            }
            private set
            {
                ihot = value;
            }
        }
        internal IList<Post> ihot;

        /// <summary>
        /// List of posts using "new" sort.
        /// </summary>
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

        /// <summary>
        /// IList of posts using "new" sort.
        /// </summary>
        public IList<Post> INew
        {
            get
            {
                return (NewLastUpdated.HasValue
                    && NewLastUpdated.Value.AddSeconds(15) > DateTime.Now ? inewPosts : GetNew(isInterface: true));
            }
            private set
            {
                inewPosts = value;
            }
        }
        internal IList<Post> inewPosts;

        /// <summary>
        /// List of posts using "rising" sort.
        /// </summary>
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

        /// <summary>
        /// IList of posts using "rising" sort.
        /// </summary>
        public IList<Post> IRising
        {
            get
            {
                return (RisingLastUpdated.HasValue
                    && RisingLastUpdated.Value.AddSeconds(15) > DateTime.Now ? irising : GetRising(isInterface: true));
            }
            private set
            {
                irising = value;
            }
        }
        internal IList<Post> irising;

        /// <summary>
        /// List of posts using "top" sort.
        /// </summary>
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

        /// <summary>
        /// IList of posts using "top" sort.
        /// </summary>
        public IList<Post> ITop
        {
            get
            {
                return (TopLastUpdated.HasValue
                    && TopLastUpdated.Value.AddSeconds(15) > DateTime.Now ? itop : GetTop(TopT, isInterface: true));
            }
            private set
            {
                itop = value;
            }
        }
        internal IList<Post> itop;

        /// <summary>
        /// List of posts using "controversial" sort.
        /// </summary>
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

        /// <summary>
        /// IList of posts using "controversial" sort.
        /// </summary>
        public IList<Post> IControversial
        {
            get
            {
                return (ControversialLastUpdated.HasValue
                    && ControversialLastUpdated.Value.AddSeconds(15) > DateTime.Now ? icontroversial : GetControversial(ControversialT, isInterface: true));
            }
            private set
            {
                icontroversial = value;
            }
        }
        internal IList<Post> icontroversial;

        /// <summary>
        /// List of posts in the mod queue.
        /// </summary>
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

        /// <summary>
        /// IList of posts in the mod queue.
        /// </summary>
        public IList<Post> IModQueue
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? imodQueue : GetModQueue(isInterface: true));
            }
            private set
            {
                imodQueue = value;
            }
        }
        internal IList<Post> imodQueue;

        /// <summary>
        /// List of reported posts in the mod queue.
        /// </summary>
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

        /// <summary>
        /// IList of reported posts in the mod queue.
        /// </summary>
        public IList<Post> IModQueueReports
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? imodQueueReports : GetModQueue(isInterface: true));
            }
            private set
            {
                imodQueueReports = value;
            }
        }
        internal IList<Post> imodQueueReports;

        /// <summary>
        /// List of spammed posts in the mod queue.
        /// </summary>
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

        /// <summary>
        /// IList of spammed posts in the mod queue.
        /// </summary>
        public IList<Post> IModQueueSpam
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? imodQueueSpam : GetModQueue(isInterface: true));
            }
            private set
            {
                imodQueueSpam = value;
            }
        }
        internal IList<Post> imodQueueSpam;

        /// <summary>
        /// List of unmoderated posts in the mod queue.
        /// </summary>
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

        /// <summary>
        /// IList of unmoderated posts in the mod queue.
        /// </summary>
        public IList<Post> IModQueueUnmoderated
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? imodQueueUnmoderated : GetModQueue(isInterface: true));
            }
            private set
            {
                imodQueueUnmoderated = value;
            }
        }
        internal IList<Post> imodQueueUnmoderated;

        /// <summary>
        /// List of edited posts in the mod queue.
        /// </summary>
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

        /// <summary>
        /// IList of edited posts in the mod queue.
        /// </summary>
        public IList<Post> IModQueueEdited
        {
            get
            {
                return (ModQueueLastUpdated.HasValue
                    && ModQueueLastUpdated.Value.AddSeconds(15) > DateTime.Now ? imodQueueEdited : GetModQueue(isInterface: true));
            }
            private set
            {
                imodQueueEdited = value;
            }
        }
        internal IList<Post> imodQueueEdited;

        private DateTime? BestLastUpdated { get; set; }
        private DateTime? HotLastUpdated { get; set; }
        private DateTime? NewLastUpdated { get; set; }
        private DateTime? RisingLastUpdated { get; set; }
        private DateTime? TopLastUpdated { get; set; }
        private DateTime? ControversialLastUpdated { get; set; }

        private DateTime? ModQueueLastUpdated { get; set; }
        private DateTime? ModQueueReportsLastUpdated { get; set; }
        private DateTime? ModQueueSpamLastUpdated { get; set; }
        private DateTime? ModQueueUnmoderatedLastUpdated { get; set; }
        private DateTime? ModQueueEditedLastUpdated { get; set; }

        private string Subreddit { get; set; }

        private Dispatch Dispatch;

        private string TopT { get; set; } = "all";
        private string ControversialT { get; set; } = "all";

        /// <summary>
        /// Create a new instance of the subreddit posts controller.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">The name of the subreddit</param>
        /// <param name="best"></param>
        /// <param name="hot"></param>
        /// <param name="newPosts"></param>
        /// <param name="rising"></param>
        /// <param name="top"></param>
        /// <param name="controversial"></param>
        /// <param name="modQueue"></param>
        /// <param name="modQueueReports"></param>
        /// <param name="modQueueSpam"></param>
        /// <param name="modQueueUnmoderated"></param>
        /// <param name="modQueueEdited"></param>
        public SubredditPosts(Dispatch dispatch, string subreddit, List<Post> best = null, List<Post> hot = null, List<Post> newPosts = null,
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

            MonitoringCache = new Dictionary<string, HashSet<string>>
            {
                { "best", new HashSet<string>() },
                { "hot", new HashSet<string>() },
                { "new", new HashSet<string>() },
                { "rising", new HashSet<string>() },
                { "top", new HashSet<string>() },
                { "controversial", new HashSet<string>() },
                { "modqueue", new HashSet<string>() },
                { "modqueuespam", new HashSet<string>() },
                { "modqueuereports", new HashSet<string>() },
                { "modqueueunmoderated", new HashSet<string>() },
                { "modqueueedited", new HashSet<string>() }
            };
        }

        /// <summary>
        /// Retrieve a list of posts using "best" sort.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetBest(string after = "", string before = "", int limit = 100, bool isInterface = false)
        {
            return GetBest(new CategorizedSrListingInput(after, before, limit: limit), isInterface);
        }

        /// <summary>
        /// Retrieve a list of posts using "best" sort.
        /// </summary>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetBest(CategorizedSrListingInput categorizedSrListingInput, bool isInterface = false)
        {
            List<Post> posts = Lists.GetPosts(Dispatch.Listings.Best(categorizedSrListingInput), Dispatch);

            BestLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                Best = posts;
            }
            else
            {
                IBest = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of posts using "hot" sort.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetHot(string g = "", string after = "", string before = "", int limit = 100, bool isInterface = false)
        {
            return GetHot(new ListingsHotInput(g, after, before, limit: limit), isInterface);
        }

        /// <summary>
        /// Retrieve a list of posts using "hot" sort.
        /// </summary>
        /// <param name="listingsHotInput">A valid ListingsHotInput instance</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetHot(ListingsHotInput listingsHotInput, bool isInterface = false)
        {
            List<Post> posts = Lists.GetPosts(Dispatch.Listings.Hot(listingsHotInput, Subreddit), Dispatch);

            HotLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                Hot = posts;
            }
            else
            {
                IHot = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of posts using "new" sort.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetNew(string after = "", string before = "", int limit = 100, bool isInterface = false)
        {
            return GetNew(new CategorizedSrListingInput(after, before, limit: limit), isInterface);
        }

        /// <summary>
        /// Retrieve a list of posts using "new" sort.
        /// </summary>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetNew(CategorizedSrListingInput categorizedSrListingInput, bool isInterface = false)
        {
            List<Post> posts = Lists.GetPosts(Dispatch.Listings.New(categorizedSrListingInput, Subreddit), Dispatch);

            NewLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                New = posts;
            }
            else
            {
                INew = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of posts using "rising" sort.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetRising(string after = "", string before = "", int limit = 100, bool isInterface = false)
        {
            return GetRising(new CategorizedSrListingInput(after, before, limit: limit), isInterface);
        }

        /// <summary>
        /// Retrieve a list of posts using "rising" sort.
        /// </summary>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetRising(CategorizedSrListingInput categorizedSrListingInput, bool isInterface = false)
        {
            List<Post> posts = Lists.GetPosts(Dispatch.Listings.Rising(categorizedSrListingInput, Subreddit), Dispatch);

            RisingLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                Rising = posts;
            }
            else
            {
                IRising = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of posts using "top" sort.
        /// </summary>
        /// <param name="t">one of(hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetTop(string t = "all", string after = "", string before = "", int limit = 100, bool isInterface = false)
        {
            return GetTop(new TimedCatSrListingInput(t, after, before, limit: limit), isInterface);
        }

        /// <summary>
        /// Retrieve a list of posts using "top" sort.
        /// </summary>
        /// <param name="timedCatSrListingInput">A valid TimedCatSrListingInput instance</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetTop(TimedCatSrListingInput timedCatSrListingInput, bool isInterface = false)
        {
            List<Post> posts = Lists.GetPosts(Dispatch.Listings.Top(timedCatSrListingInput, Subreddit), Dispatch);

            TopLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                Top = posts;
            }
            else
            {
                ITop = posts;
            }
            TopT = timedCatSrListingInput.t;
            return posts;
        }

        /// <summary>
        /// Retrieve a list of posts using "controversial" sort.
        /// </summary>
        /// <param name="t">one of(hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetControversial(string t = "all", string after = "", string before = "", int limit = 100, bool isInterface = false)
        {
            return GetControversial(new TimedCatSrListingInput(t, after, before, limit: limit), isInterface);
        }

        /// <summary>
        /// Retrieve a list of posts using "controversial" sort.
        /// </summary>
        /// <param name="timedCatSrListingInput">A valid TimedCatSrListingInput instance</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetControversial(TimedCatSrListingInput timedCatSrListingInput, bool isInterface = false)
        {
            List<Post> posts = Lists.GetPosts(Dispatch.Listings.Controversial(timedCatSrListingInput, Subreddit), Dispatch);

            ControversialLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                Controversial = posts;
            }
            else
            {
                IControversial = posts;
            }
            ControversialT = timedCatSrListingInput.t;
            return posts;
        }

        private List<Post> GetModQueuePosts(string location, string after = "", string before = "", int limit = 100, string show = "all",
            bool srDetail = false, int count = 0)
        {
            return Lists.GetPosts(Dispatch.Moderation.ModQueue(new ModerationModQueueInput("links", after, before, limit, count, srDetail, show), location, Subreddit), Dispatch);
        }

        /// <summary>
        /// Retrieve a list of posts in the mod queue.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetModQueue(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0, bool isInterface = false)
        {
            List<Post> posts = GetModQueuePosts("modqueue", after, before, limit, show, srDetail, count);

            ModQueueLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                ModQueue = posts;
            }
            else
            {
                IModQueue = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of reported posts in the mod queue.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetModQueueReports(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0, bool isInterface = false)
        {
            List<Post> posts = GetModQueuePosts("reports", after, before, limit, show, srDetail, count);

            ModQueueReportsLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                ModQueueReports = posts;
            }
            else
            {
                IModQueueReports = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of spammed posts in the mod queue.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetModQueueSpam(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0, bool isInterface = false)
        {
            List<Post> posts = GetModQueuePosts("spam", after, before, limit, show, srDetail, count);

            ModQueueSpamLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                ModQueueSpam = posts;
            }
            else
            {
                IModQueueSpam = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of unmoderated posts in the mod queue.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetModQueueUnmoderated(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0, bool isInterface = false)
        {
            List<Post> posts = GetModQueuePosts("unmoderated", after, before, limit, show, srDetail, count);

            ModQueueUnmoderatedLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                ModQueueUnmoderated = posts;
            }
            else
            {
                IModQueueUnmoderated = posts;
            }
            return posts;
        }

        /// <summary>
        /// Retrieve a list of edited posts in the mod queue.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">The maximum number of results to be retrieved (default: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="isInterface">(optional) whether to store the result cache in the interface</param>
        /// <returns>A list of posts.</returns>
        public List<Post> GetModQueueEdited(string after = "", string before = "", int limit = 100, string show = "all", bool srDetail = false, int count = 0, bool isInterface = false)
        {
            List<Post> posts = GetModQueuePosts("edited", after, before, limit, show, srDetail, count);

            ModQueueEditedLastUpdated = DateTime.Now;

            if (!isInterface)
            {
                ModQueueEdited = posts;
            }
            else
            {
                IModQueueEdited = posts;
            }
            return posts;
        }

        /// <summary>
        /// Monitor Reddit for new "Best" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorBest(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "best");

            string key = "BestPosts";
            return Monitor(key, new Thread(() => MonitorBestThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorBestThread(string key, int? monitoringDelayMs = null, bool? breakOnFailure = null)
        {
            MonitorPostsThread(Monitoring, key, "best", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnBestUpdated(PostsUpdateEventArgs e)
        {
            BestUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Hot" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorHot(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "hot");

            string key = "HotPosts";
            return Monitor(key, new Thread(() => MonitorHotThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorHotThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "hot", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnHotUpdated(PostsUpdateEventArgs e)
        {
            HotUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorNew(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "new");

            string key = "NewPosts";
            return Monitor(key, new Thread(() => MonitorNewThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorNewThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "new", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnNewUpdated(PostsUpdateEventArgs e)
        {
            NewUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Rising" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorRising(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "rising");

            string key = "RisingPosts";
            return Monitor(key, new Thread(() => MonitorRisingThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorRisingThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "rising", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnRisingUpdated(PostsUpdateEventArgs e)
        {
            RisingUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Top" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorTop(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "top");

            string key = "TopPosts";
            return Monitor(key, new Thread(() => MonitorTopThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorTopThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "top", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnTopUpdated(PostsUpdateEventArgs e)
        {
            TopUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit for new "Controversial" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorControversial(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "controversial");

            string key = "ControversialPosts";
            return Monitor(key, new Thread(() => MonitorControversialThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorControversialThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "controversial", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnControversialUpdated(PostsUpdateEventArgs e)
        {
            ControversialUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "modqueue" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueue(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "modqueue");

            string key = "ModQueuePosts";
            return Monitor(key, new Thread(() => MonitorModQueueThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorModQueueThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "modqueue", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnModQueueUpdated(PostsUpdateEventArgs e)
        {
            ModQueueUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "reports" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueReports(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "modqueuereports");

            string key = "ModQueueReportsPosts";
            return Monitor(key, new Thread(() => MonitorModQueueReportsThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorModQueueReportsThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "modqueuereports", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnModQueueReportsUpdated(PostsUpdateEventArgs e)
        {
            ModQueueReportsUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "spam" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueSpam(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "modqueuespam");

            string key = "ModQueueSpamPosts";
            return Monitor(key, new Thread(() => MonitorModQueueSpamThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorModQueueSpamThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "modqueuespam", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnModQueueSpamUpdated(PostsUpdateEventArgs e)
        {
            ModQueueSpamUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "unmoderated" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueUnmoderated(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "modqueueunmoderated");

            string key = "ModQueueUnmoderatedPosts";
            return Monitor(key, new Thread(() => MonitorModQueueUnmoderatedThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorModQueueUnmoderatedThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "modqueueunmoderated", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnModQueueUnmoderatedUpdated(PostsUpdateEventArgs e)
        {
            ModQueueUnmoderatedUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor the subreddit's modqueue for new "edited" posts.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <param name="useCache">Whether to cache the IDs of the monitoring results to prevent duplicate fires (default: true)</param>
        /// <returns>True if this action turned monitoring on, false if this action turned it off.</returns>
        public bool MonitorModQueueEdited(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null, bool useCache = true)
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

            InitMonitoringCache(useCache, "modqueueedited");

            string key = "ModQueueEditedPosts";
            return Monitor(key, new Thread(() => MonitorModQueueEditedThread(key, monitoringDelayMs)), Subreddit);
        }

        private void MonitorModQueueEditedThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPostsThread(Monitoring, key, "modqueueedited", Subreddit, monitoringDelayMs: monitoringDelayMs);
        }

        internal virtual void OnModQueueEditedUpdated(PostsUpdateEventArgs e)
        {
            ModQueueEditedUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Whether best is being monitored.
        /// </summary>
        /// <returns>Whether best is being monitored.</returns>
        public bool BestPostsIsMonitored()
        {
            return IsMonitored("BestPosts", Subreddit);
        }

        /// <summary>
        /// Whether hot is being monitored.
        /// </summary>
        /// <returns>Whether hot is being monitored.</returns>
        public bool HotPostsIsMonitored()
        {
            return IsMonitored("HotPosts", Subreddit);
        }

        /// <summary>
        /// Whether new is being monitored.
        /// </summary>
        /// <returns>Whether new is being monitored.</returns>
        public bool NewPostsIsMonitored()
        {
            return IsMonitored("NewPosts", Subreddit);
        }

        /// <summary>
        /// Whether rising is being monitored.
        /// </summary>
        /// <returns>Whether rising is being monitored.</returns>
        public bool RisingPostsIsMonitored()
        {
            return IsMonitored("RisingPosts", Subreddit);
        }

        /// <summary>
        /// Whether top is being monitored.
        /// </summary>
        /// <returns>Whether top is being monitored.</returns>
        public bool TopPostsIsMonitored()
        {
            return IsMonitored("TopPosts", Subreddit);
        }

        /// <summary>
        /// Whether controversial is being monitored.
        /// </summary>
        /// <returns>Whether controversial is being monitored.</returns>
        public bool ControversialPostsIsMonitored()
        {
            return IsMonitored("ControversialPosts", Subreddit);
        }

        /// <summary>
        /// Whether modqueue is being monitored.
        /// </summary>
        /// <returns>Whether modqueue is being monitored.</returns>
        public bool ModQueuePostsIsMonitored()
        {
            return IsMonitored("ModQueuePosts", Subreddit);
        }

        /// <summary>
        /// Whether modqueue reports is being monitored.
        /// </summary>
        /// <returns>Whether modqueue reports is being monitored.</returns>
        public bool ModQueueReportsPostsIsMonitored()
        {
            return IsMonitored("ModQueueReportsPosts", Subreddit);
        }

        /// <summary>
        /// Whether modqueue spam is being monitored.
        /// </summary>
        /// <returns>Whether modqueue spam is being monitored.</returns>
        public bool ModQueueSpamPostsIsMonitored()
        {
            return IsMonitored("ModQueueSpamPosts", Subreddit);
        }

        /// <summary>
        /// Whether modqueue unmoderated is being monitored.
        /// </summary>
        /// <returns>Whether modqueue unmoderated is being monitored.</returns>
        public bool ModQueueUnmoderatedPostsIsMonitored()
        {
            return IsMonitored("ModQueueUnmoderatedPosts", Subreddit);
        }

        /// <summary>
        /// Whether modqueue edited is being monitored.
        /// </summary>
        /// <returns>Whether modqueue edited is being monitored.</returns>
        public bool ModQueueEditedPostsIsMonitored()
        {
            return IsMonitored("ModQueueEditedPosts", Subreddit);
        }

        /// <summary>
        /// Creates a new monitoring thread.
        /// </summary>
        /// <param name="key">Monitoring key</param>
        /// <param name="subKey">Monitoring subKey</param>
        /// <param name="startDelayMs">How long to wait before starting the thread in milliseconds (default: 0)</param>
        /// <param name="monitoringDelayMs">How long to wait between monitoring queries; pass null to leave it auto-managed (default: null)</param>
        /// <returns>The newly-created monitoring thread.</returns>
        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "BestPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "best", subKey, startDelayMs, monitoringDelayMs));
                case "HotPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "hot", subKey, startDelayMs, monitoringDelayMs));
                case "NewPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "new", subKey, startDelayMs, monitoringDelayMs));
                case "RisingPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "rising", subKey, startDelayMs, monitoringDelayMs));
                case "TopPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "top", subKey, startDelayMs, monitoringDelayMs));
                case "ControversialPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "controversial", subKey, startDelayMs, monitoringDelayMs));
                case "ModQueuePosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueue", subKey, startDelayMs, monitoringDelayMs));
                case "ModQueueReportsPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueuereports", subKey, startDelayMs, monitoringDelayMs));
                case "ModQueueSpamPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueuespam", subKey, startDelayMs, monitoringDelayMs));
                case "ModQueueUnmoderatedPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueueunmoderated", subKey, startDelayMs, monitoringDelayMs));
                case "ModQueueEditedPosts":
                    return new Thread(() => MonitorPostsThread(Monitoring, key, "modqueueedited", subKey, startDelayMs, monitoringDelayMs));
            }
        }

        private void MonitorPostsThread(MonitoringSnapshot monitoring, string key, string type, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
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

                List<Post> oldList;
                List<Post> newList;
                try
                {
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

                    if (Lists.ListDiff(oldList, newList, out List<Post> added, out List<Post> removed, (UseCache.Contains(type) ? MonitoringCache[type] : null)))
                    {
                        // Add the new entries to the appropriate cache, if enabled.  --Kris
                        if (UseCache.Contains(type))
                        {
                            foreach (Post post in added)
                            {
                                MonitoringCache[type].Add(post.Id);
                            }
                        }

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
                }
                catch (Exception) when (!BreakOnFailure) { }

                Wait(monitoringDelayMs.Value);
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
