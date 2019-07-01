using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs;
using Reddit.Inputs.Subreddits;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for a subreddit's wiki.
    /// </summary>
    public class Wiki : Monitors
    {
        public event EventHandler<WikiPagesUpdateEventArgs> PagesUpdated;

        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }

        /// <summary>
        /// List of pages on this wiki.
        /// </summary>
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

        private DateTime? PagesLastUpdated { get; set; }
        private string Subreddit { get; set; }

        private Dispatch Dispatch;

        /// <summary>
        /// Create a new instance of the wiki controller.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">The name of the subreddit to which this wiki belongs</param>
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
        /// <param name="wikiPage">A valid instance of Things.WikiPage</param>
        /// <returns>A new instance of the WikiPage controller.</returns>
        public WikiPage Page(string pageName, Things.WikiPage wikiPage)
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
            return GetContributors(new SubredditsAboutInput(user, after, before, count, limit, show, srDetail, includeCategories));
        }

        /// <summary>
        /// Get the approved submitters of this subreddit's wiki.
        /// </summary>
        /// <param name="subredditsAboutInput">A valid SubredditsAboutInput instance</param>
        /// <returns>A list of subreddit contributors.</returns>
        public List<SubredditUser> GetContributors(SubredditsAboutInput subredditsAboutInput)
        {
            return Lists.GetAboutChildren<SubredditUser>(Validate(Dispatch.Subreddits.About("wikicontributors", subredditsAboutInput, Subreddit)));
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
            return GetBannedUsers(new SubredditsAboutInput(user, after, before, count, limit, show, srDetail, includeCategories));
        }

        /// <summary>
        /// Get the approved submitters of this subreddit's wiki.
        /// </summary>
        /// <param name="subredditsAboutInput">A valid SubredditsAboutInput instance</param>
        /// <returns>A list of subreddit contributors.</returns>
        public List<BannedUser> GetBannedUsers(SubredditsAboutInput subredditsAboutInput)
        {
            return Lists.GetAboutChildren<BannedUser>(Validate(Dispatch.Subreddits.About("wikibanned", subredditsAboutInput, Subreddit)));
        }

        /// <summary>
        /// Retrieve a list of wiki pages in this subreddit.
        /// </summary>
        /// <returns>>A list of wiki pages.</returns>
        public List<string> GetPages()
        {
            Pages = ((Things.WikiPageListing)Validate(Dispatch.Wiki.Pages(Subreddit))).Data;
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
        public List<Things.WikiPageRevision> GetRecentPageRevisions(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = false, int count = 0)
        {
            return GetRecentPageRevisions(new SrListingInput(after, before, count, limit, srDetail, show));
        }

        /// <summary>
        /// Retrieve a list of recently changed wiki pages in this subreddit.
        /// </summary>
        /// <param name="srListingInput">A valid SrListingInput instance</param>
        /// <returns>A list of wiki pages.</returns>
        public List<Things.WikiPageRevision> GetRecentPageRevisions(SrListingInput srListingInput)
        {
            return Validate(Dispatch.Wiki.Revisions(srListingInput, Subreddit)).Data.Children;
        }

        internal virtual void OnPagesUpdated(WikiPagesUpdateEventArgs e)
        {
            PagesUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor this wiki for added/removed pages.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorPages(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "WikiPages";
            return Monitor(key, new Thread(() => MonitorPagesThread(key, monitoringDelayMs: monitoringDelayMs)), Subreddit);
        }

        public bool WikiPagesIsMonitored()
        {
            return IsMonitored("WikiPages", Subreddit);
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "WikiPages":
                    return new Thread(() => MonitorPagesThread(key, startDelayMs, monitoringDelayMs));
            }
        }
        
        private void MonitorPagesThread(string key, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            monitoringDelayMs = (monitoringDelayMs.HasValue ? monitoringDelayMs : Monitoring.Count() * MonitoringWaitDelayMS);

            while (!Terminate
                && Monitoring.Get(key).Contains(Subreddit))
            {
                if (MonitoringExpiration.HasValue
                    && DateTime.Now > MonitoringExpiration.Value)
                {
                    MonitorModel.RemoveMonitoringKey(key, Subreddit, ref Monitoring);
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

                List<string> oldList;
                List<string> newList;
                try
                {
                    oldList = pages;
                    newList = GetPages();
                    if (Lists.ListDiff(oldList, newList, out List<string> added, out List<string> removed))
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
                }
                catch (Exception) when (!BreakOnFailure) { }

                Wait(monitoringDelayMs.Value);
            }
        }
    }
}
