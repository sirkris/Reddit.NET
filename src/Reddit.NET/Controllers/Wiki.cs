using Newtonsoft.Json;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using Reddit.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    public class Wiki : BaseController
    {
        public event EventHandler<WikiPagesUpdateEventArgs> PagesUpdated;

        internal override ref Models.Internal.Monitor MonitorModel => ref Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

        public List<string> Pages
        {
            get
            {
                return (PagesLastUpdated.HasValue
                    && PagesLastUpdated.Value.AddMinutes(1) > DateTime.Now ? pages : GetPages());
            }
            set
            {
                pages = value;
            }
        }
        private List<string> pages;
        private DateTime? PagesLastUpdated;

        private readonly string Subreddit;
        private readonly Dispatch Dispatch;

        public Wiki(Dispatch dispatch, string subreddit)
        {
            Dispatch = dispatch;
            Subreddit = subreddit;
        }

        /// <summary>
        /// Return the content of an existing wiki page.
        /// If v is given, show the wiki page as it was at that version. If both v and v2 are given, show a diff of the two.
        /// </summary>
        /// <param name="pageName">the name of an existing wiki page</param>
        /// <param name="v">a wiki revision ID</param>
        /// <param name="v2">a wiki revision ID</param>
        /// <returns>A new instance of the WikiPage controller populated with the return data.</returns>
        public WikiPage GetPage(string pageName, string v = "", string v2 = "")
        {
            return Page(pageName).About(v, v2);
        }

        /// <summary>
        /// Return a new instance of the WikiPage controller.
        /// </summary>
        /// <param name="pageName">the name of an existing wiki page</param>
        /// <param name="mayRevise">boolean value</param>
        /// <param name="revisionDate">Date of current revision</param>
        /// <param name="contentHtml">Page content as HTML</param>
        /// <param name="revisionBy">Author of current revision</param>
        /// <param name="contentMd">Page content as Markdown</param>
        /// <returns>A new instance of the WikiPage controller.</returns>
        public WikiPage Page(string pageName, bool mayRevise, DateTime revisionDate, string contentHtml, User revisionBy, string contentMd)
        {
            return new WikiPage(Dispatch, mayRevise, revisionDate, contentHtml, revisionBy, contentMd, Subreddit, pageName);
        }

        /// <summary>
        /// Return a new instance of the WikiPage controller.
        /// </summary>
        /// <param name="pageName">the name of an existing wiki page</param>
        /// <param name="wikiPage">A valid instance of Models.Structures.WikiPage</param>
        /// <returns>A new instance of the WikiPage controller.</returns>
        public WikiPage Page(string pageName, RedditThings.WikiPage wikiPage)
        {
            return new WikiPage(Dispatch, wikiPage, Subreddit, pageName);
        }

        /// <summary>
        /// Return a new instance of the WikiPage controller.
        /// </summary>
        /// <param name="pageName">the name of an existing wiki page</param>
        /// <returns>A new instance of the WikiPage controller.</returns>
        public WikiPage Page(string pageName)
        {
            return new WikiPage(Dispatch, Subreddit, pageName);
        }

        /// <summary>
        /// Get the approved submitters of this subreddit's wiki.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of subreddit contributors.</returns>
        public List<SubredditUser> GetContributors(string after = "", string before = "", int limit = 25, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("wikicontributors", after, before, user, includeCategories, Subreddit, count, limit,
                show, srDetail);

            Validate(res);

            return GetAboutChildren<SubredditUser>(res);
        }

        /// <summary>
        /// Get a list of users who were banned from this subreddit's wiki.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of banned users.</returns>
        public List<BannedUser> GetBannedUsers(string after = "", string before = "", int limit = 25, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("wikibanned", after, before, user, includeCategories, Subreddit, count, limit,
                show, srDetail);

            Validate(res);

            return GetAboutChildren<BannedUser>(res);
        }

        /// <summary>
        /// Retrieve a list of wiki pages in this subreddit.
        /// </summary>
        /// <returns>>A list of wiki pages.</returns>
        public List<string> GetPages()
        {
            Pages = ((RedditThings.WikiPageListing)Validate(Dispatch.Wiki.Pages(Subreddit))).Data;
            PagesLastUpdated = DateTime.Now;

            return Pages;
        }

        /// <summary>
        /// Retrieve a list of recently changed wiki pages in this subreddit.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of wiki pages.</returns>
        public List<RedditThings.WikiPageRevision> GetRecentPageRevisions(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = false, int count = 0)
        {
            return Validate(Dispatch.Wiki.Revisions(after, before, Subreddit, count, limit, show, srDetail)).Data.Children;
        }

        internal virtual void OnPagesUpdated(WikiPagesUpdateEventArgs e)
        {
            PagesUpdated?.Invoke(this, e);
        }

        public bool MonitorPages()
        {
            string key = "WikiPages";
            return Monitor(key, new Thread(() => MonitorPagesThread(key)));
        }

        private bool Monitor(string key, Thread thread)
        {
            bool res = Monitor(key, thread, Subreddit, out Thread newThread);

            RebuildThreads();
            LaunchThreadIfNotNull(key, newThread);

            return res;
        }

        internal void RebuildThreads()
        {
            Dictionary<string, Thread> oldThreads = Threads;
            KillThreads(oldThreads);

            int i = 0;
            foreach (KeyValuePair<string, Thread> pair in oldThreads)
            {
                Threads.Add(pair.Key, CreateMonitoringThread(pair.Key, Subreddit, (i * MonitoringWaitDelayMS)));
                i++;
            }
        }

        private Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "WikiPages":
                    return new Thread(() => MonitorPagesThread(key, startDelayMs));
            }
        }
        
        internal void MonitorPagesThread(string key, int startDelayMs = 0)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            while (!Terminate
                && Monitoring.Get(key).Contains(Subreddit))
            {
                List<string> oldList = pages;
                List<string> newList = GetPages();

                if (ListDiff(oldList, newList, out List<string> added, out List<string> removed))
                {
                    // Event handler to alert the calling app that the list has changed.  --Kris
                    WikiPagesUpdateEventArgs args = new WikiPagesUpdateEventArgs
                    {
                        NewPages = newList,
                        OldPages = oldList,
                        Added = added,
                        Removed = removed
                    };
                    OnPagesUpdated(args);
                }

                Thread.Sleep(Monitoring.Count() * MonitoringWaitDelayMS);
            }
        }
    }
}
