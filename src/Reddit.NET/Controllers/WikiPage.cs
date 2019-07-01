using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs;
using Reddit.Inputs.Wiki;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for wiki pages.
    /// </summary>
    public class WikiPage : Monitors
    {
        public bool MayRevise { get; set; }
        public DateTime RevisionDate { get; set; }
        public string ContentHTML { get; set; }
        public User RevisionBy { get; set; }
        public string ContentMd { get; set; }

        public string Name { get; set; }
        public string Subreddit { get; set; }

        public event EventHandler<WikiPageUpdateEventArgs> PageUpdated;

        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }

        private Dispatch Dispatch;

        /// <summary>
        /// Create a new wiki page controller instance, populated manually.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="mayRevise"></param>
        /// <param name="revisionDate"></param>
        /// <param name="contentHtml"></param>
        /// <param name="revisionBy"></param>
        /// <param name="contentMd"></param>
        /// <param name="subreddit"></param>
        /// <param name="name"></param>
        public WikiPage(Dispatch dispatch, bool mayRevise, DateTime revisionDate, string contentHtml, User revisionBy, string contentMd, 
            string subreddit = null, string name = null)
        {
            Dispatch = dispatch;

            MayRevise = mayRevise;
            RevisionDate = revisionDate;
            ContentHTML = contentHtml;
            RevisionBy = revisionBy;
            ContentMd = contentMd;

            Subreddit = subreddit;
            Name = name;
        }

        /// <summary>
        /// Create a new wiki page controller instance from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="wikiPage"></param>
        /// <param name="subreddit"></param>
        /// <param name="name"></param>
        public WikiPage(Dispatch dispatch, Things.WikiPage wikiPage, string subreddit = null, string name = null)
        {
            Dispatch = dispatch;

            MayRevise = wikiPage.MayRevise;
            RevisionDate = wikiPage.RevisionDate;
            ContentHTML = wikiPage.ContentHTML;
            RevisionBy = new User(Dispatch, wikiPage.RevisionBy.Data);
            ContentMd = wikiPage.ContentMd;

            Subreddit = subreddit;
            Name = name;
        }

        /// <summary>
        /// Create a new wiki page controller instance, populated only with subreddit and name.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit"></param>
        /// <param name="name"></param>
        public WikiPage(Dispatch dispatch, string subreddit = null, string name = null)
        {
            Dispatch = dispatch;
            Subreddit = subreddit;
            Name = name;
        }

        /// <summary>
        /// Create an empty wiki page controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public WikiPage(Dispatch dispatch) { }

        /// <summary>
        /// Allow username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public void AllowEditor(string username)
        {
            Dispatch.Wiki.AllowEditor(new WikiPageEditorInput(Name, username), Subreddit);
        }

        /// <summary>
        /// Asynchronously allow username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public async Task AllowEditorAsync(string username)
        {
            await Dispatch.Wiki.AllowEditorAsync(new WikiPageEditorInput(Name, username), Subreddit);
        }

        /// <summary>
        /// Deny username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public void DenyEditor(string username)
        {
            Dispatch.Wiki.DenyEditor(new WikiPageEditorInput(Name, username), Subreddit);
        }

        /// <summary>
        /// Asynchronously deny username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public async Task DenyEditorAsync(string username)
        {
            await Dispatch.Wiki.DenyEditorAsync(new WikiPageEditorInput(Name, username), Subreddit);
        }

        /// <summary>
        /// Edit a wiki page and return an instance with the updated data.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        /// <param name="previous">the starting point revision for this edit</param>
        /// <returns>The updated WikiPage.</returns>
        public WikiPage EditAndReturn(string reason, string content = null, string previous = "")
        {
            return EditAndReturn(new WikiEditPageInput(content, Name, reason, previous));
        }

        /// <summary>
        /// Edit a wiki page and return an instance with the updated data.
        /// </summary>
        /// <param name="wikiEditPageInput">A valid WikiEditPageInput instance</param>
        /// <returns>The updated WikiPage.</returns>
        public WikiPage EditAndReturn(WikiEditPageInput wikiEditPageInput)
        {
            Edit(wikiEditPageInput);
            return About();
        }

        /// <summary>
        /// Edit this wiki page.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public void Edit(string reason, string content = null, string previous = "")
        {
            Edit(new WikiEditPageInput(content, Name, reason, previous));
        }

        /// <summary>
        /// Edit this wiki page asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public async Task EditAsync(string reason, string content = null, string previous = "")
        {
            await EditAsync(new WikiEditPageInput(content, Name, reason, previous));
        }

        /// <summary>
        /// Edit this wiki page.
        /// </summary>
        /// <param name="wikiEditPageInput">A valid WikiEditPageInput instance</param>
        public void Edit(WikiEditPageInput wikiEditPageInput)
        {
            wikiEditPageInput.page = Name;

            Dispatch.Wiki.Edit(wikiEditPageInput, Subreddit);
        }

        /// <summary>
        /// Edit this wiki page asynchronously.
        /// </summary>
        /// <param name="wikiEditPageInput">A valid WikiEditPageInput instance</param>
        public async Task EditAsync(WikiEditPageInput wikiEditPageInput)
        {
            wikiEditPageInput.page = Name;

            await Dispatch.Wiki.EditAsync(wikiEditPageInput, Subreddit);
        }

        /// <summary>
        /// Edit this wiki page with the current values of this instance.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public void SaveChanges(string reason, string previous = "")
        {
            Edit(reason, ContentMd);
        }

        /// <summary>
        /// Edit this wiki page with the current values of this instance asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public async Task SaveChangesAsync(string reason, string previous = "")
        {
            await EditAsync(reason, ContentMd);
        }

        /// <summary>
        /// Create a new wiki page and return an instance with the updated data.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        public WikiPage CreateAndReturn(string reason, string content = null)
        {
            Create(reason, content);
            return new WikiPage(Dispatch, Dispatch.Wiki.Page(Name.ToLower(), new WikiPageContentInput(), Subreddit).Data, Subreddit, Name);
        }

        /// <summary>
        /// Create a new wiki page.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        public void Create(string reason, string content = null)
        {
            Dispatch.Wiki.Create(new WikiCreatePageInput(content, Name, reason), Subreddit);
        }

        /// <summary>
        /// Create a new wiki page asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        public async Task CreateAsync(string reason, string content = null)
        {
            await Dispatch.Wiki.CreateAsync(new WikiCreatePageInput(content, Name, reason), Subreddit);
        }

        /// <summary>
        /// Toggle the public visibility of a wiki page revision.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        /// <returns>A boolean indicating true if page was hidden, false if page was unhidden.</returns>
        public bool Hide(string revision)
        {
            return ((StatusResult)Validate(Dispatch.Wiki.Hide(new WikiPageRevisionInput(Name, revision), Subreddit))).Status;
        }

        /// <summary>
        /// Toggle the public visibility of a wiki page revision asynchronously.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        public async Task<bool> HideAsync(string revision)
        {
            return ((StatusResult)Validate(await Dispatch.Wiki.HideAsync(new WikiPageRevisionInput(Name, revision), Subreddit))).Status;
        }

        /// <summary>
        /// Revert a wiki page to revision.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        public void Revert(string revision)
        {
            Dispatch.Wiki.Revert(new WikiPageRevisionInput(Name, revision), Subreddit);
        }

        /// <summary>
        /// Revert a wiki page to revision and return an instance with the updated data.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        /// <returns>The updated WikiPage.</returns>
        public WikiPage RevertAndReturn(string revision)
        {
            Revert(revision);
            return About();
        }

        /// <summary>
        /// Revert a wiki page to revision asynchronously.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        public async Task RevertAsync(string revision)
        {
            await Dispatch.Wiki.RevertAsync(new WikiPageRevisionInput(Name, revision), Subreddit);
        }

        /// <summary>
        /// Retrieve a list of revisions of this wiki page.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of revisions.</returns>
        public List<WikiPageRevision> Revisions(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = false, int count = 0)
        {
            return Revisions(new SrListingInput(after, before, count, limit, srDetail, show));
        }

        /// <summary>
        /// Retrieve a list of revisions of this wiki page.
        /// </summary>
        /// <param name="srListingInput">A valid SrListingInput instance</param>
        /// <returns>A list of revisions.</returns>
        public List<WikiPageRevision> Revisions(SrListingInput srListingInput)
        {
            return Validate(Dispatch.Wiki.PageRevisions(Name, srListingInput, Subreddit)).Data.Children;
        }

        /// <summary>
        /// Retrieve the current permission settings for page.
        /// </summary>
        /// <returns>An object containing wiki page settings.</returns>
        public WikiPageSettings GetPermissions()
        {
            return Validate(Dispatch.Wiki.GetPermissions(Name, Subreddit)).Data;
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="listed">boolean value (true = appear in /wiki/pages, false = don't appear in /wiki/pages)</param>
        /// <param name="permLevel">an integer (0 = use wiki perms, 1 = only approved users may edit, 2 = only mods may edit or view)</param>
        /// <returns>An object containing wiki page settings.</returns>
        public WikiPageSettings UpdatePermissions(bool listed, int permLevel)
        {
            return UpdatePermissions(new WikiUpdatePermissionsInput(listed, permLevel));
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page asynchronously.
        /// </summary>
        /// <param name="listed">boolean value (true = appear in /wiki/pages, false = don't appear in /wiki/pages)</param>
        /// <param name="permLevel">an integer (0 = use wiki perms, 1 = only approved users may edit, 2 = only mods may edit or view)</param>
        public async Task<WikiPageSettings> UpdatePermissionsAsync(bool listed, int permLevel)
        {
            return await UpdatePermissionsAsync(new WikiUpdatePermissionsInput(listed, permLevel));
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="wikiUpdatePermissionsInput">A valid WikiUpdatePermissionsInput instance</param>
        /// <returns>An object containing wiki page settings.</returns>
        public WikiPageSettings UpdatePermissions(WikiUpdatePermissionsInput wikiUpdatePermissionsInput)
        {
            return Validate(Dispatch.Wiki.UpdatePermissions(Name, wikiUpdatePermissionsInput, Subreddit)).Data;
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page asynchronously.
        /// </summary>
        /// <param name="wikiUpdatePermissionsInput">A valid WikiUpdatePermissionsInput instance</param>
        public async Task<WikiPageSettings> UpdatePermissionsAsync(WikiUpdatePermissionsInput wikiUpdatePermissionsInput)
        {
            return Validate(await Dispatch.Wiki.UpdatePermissionsAsync(Name, wikiUpdatePermissionsInput, Subreddit)).Data;
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="wikiPageSettings">A valid instance of WikiPageSettings</param>
        /// <returns>An object containing wiki page settings.</returns>
        public WikiPageSettings UpdatePermissions(WikiPageSettings wikiPageSettings)
        {
            return Validate(Dispatch.Wiki.UpdatePermissions(Name, wikiPageSettings, Subreddit)).Data;
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page asynchronously.
        /// </summary>
        /// <param name="wikiPageSettings">A valid instance of WikiPageSettings</param>
        public async Task<WikiPageSettings> UpdatePermissionsAsync(WikiPageSettings wikiPageSettings)
        {
            return Validate(await Dispatch.Wiki.UpdatePermissionsAsync(Name, wikiPageSettings, Subreddit)).Data;
        }

        /// <summary>
        /// Return the content of a wiki page.
        /// If v is given, show the wiki page as it was at that version If both v and v2 are given, show a diff of the two.
        /// </summary>
        /// <param name="v">a wiki revision ID</param>
        /// <param name="v2">a wiki revision ID</param>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public WikiPage About(string v = "", string v2 = "")
        {
            return new WikiPage(Dispatch, ((WikiPageContainer)Validate(Dispatch.Wiki.Page(Name, new WikiPageContentInput(v, v2), Subreddit))).Data, Subreddit, Name);
        }

        internal virtual void OnPagesUpdated(WikiPageUpdateEventArgs e)
        {
            PageUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor this wiki page for any changes.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorPage(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "WikiPage";
            return Monitor(key, new Thread(() => MonitorPageThread(key, monitoringDelayMs: monitoringDelayMs)), Name);
        }

        public bool WikiPagesIsMonitored()
        {
            return IsMonitored("WikiPage", Name);
        }

        protected override Thread CreateMonitoringThread(string key, string subkey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "WikiPage":
                    return new Thread(() => MonitorPageThread(key, startDelayMs, monitoringDelayMs));
            }
        }

        private void MonitorPageThread(string key, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            monitoringDelayMs = (monitoringDelayMs.HasValue ? monitoringDelayMs : Monitoring.Count() * MonitoringWaitDelayMS);

            while (!Terminate
                && Monitoring.Get(key).Contains(Name))
            {
                if (MonitoringExpiration.HasValue
                    && DateTime.Now > MonitoringExpiration.Value)
                {
                    MonitorModel.RemoveMonitoringKey(key, Name, ref Monitoring);
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

                WikiPage newPage;
                try
                {
                    newPage = About();

                    if (!newPage.RevisionDate.Equals(RevisionDate))
                    {
                        // Event handler to alert the calling app that the list has changed.  --Kris
                        WikiPageUpdateEventArgs args = new WikiPageUpdateEventArgs
                        {
                            NewPage = newPage,
                            OldPage = this
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
