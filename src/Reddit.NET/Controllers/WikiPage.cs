using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using RedditThings = Reddit.Models.Structures;
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
        public bool MayRevise;
        public DateTime RevisionDate;
        public string ContentHTML;
        public User RevisionBy;
        public string ContentMd;

        public string Name;
        public string Subreddit;

        public event EventHandler<WikiPageUpdateEventArgs> PageUpdated;

        internal override ref Models.Internal.Monitor MonitorModel => ref Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

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
        public WikiPage(ref Dispatch dispatch, bool mayRevise, DateTime revisionDate, string contentHtml, User revisionBy, string contentMd, 
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
        public WikiPage(ref Dispatch dispatch, RedditThings.WikiPage wikiPage, string subreddit = null, string name = null)
        {
            Dispatch = dispatch;

            MayRevise = wikiPage.MayRevise;
            RevisionDate = wikiPage.RevisionDate;
            ContentHTML = wikiPage.ContentHTML;
            RevisionBy = new User(ref Dispatch, wikiPage.RevisionBy.Data);
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
        public WikiPage(ref Dispatch dispatch, string subreddit = null, string name = null)
        {
            Dispatch = dispatch;
            Subreddit = subreddit;
            Name = name;
        }

        /// <summary>
        /// Create an empty wiki page controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public WikiPage(Dispatch dispatch)
        {

        }

        /// <summary>
        /// Allow username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public void AllowEditor(string username)
        {
            Dispatch.Wiki.AllowEditor(Name, username, Subreddit);
        }

        /// <summary>
        /// Asynchronously allow username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public async Task AllowEditorAsync(string username)
        {
            await Task.Run(() =>
            {
                AllowEditor(username);
            });
        }

        /// <summary>
        /// Deny username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public void DenyEditor(string username)
        {
            Dispatch.Wiki.DenyEditor(Name, username, Subreddit);
        }

        /// <summary>
        /// Asynchronously deny username to edit this wiki page.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        public async Task DenyEditorAsync(string username)
        {
            await Task.Run(() =>
            {
                DenyEditor(username);
            });
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
            Edit(reason, content, previous);
            return About();
        }

        /// <summary>
        /// Edit a wiki page.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public void Edit(string reason, string content = null, string previous = "")
        {
            Dispatch.Wiki.Edit(content, Name, previous, reason, Subreddit);
        }

        /// <summary>
        /// Edit a wiki page asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public async Task EditAsync(string reason, string content = null, string previous = "")
        {
            await Task.Run(() =>
            {
                Edit(reason, content, previous);
            });
        }

        /// <summary>
        /// Edit this wiki page.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public void SaveChanges(string reason, string previous = "")
        {
            Edit(reason, ContentMd);
        }

        /// <summary>
        /// Edit this wiki page asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="previous">the starting point revision for this edit</param>
        public async Task SaveChangesAsync(string reason, string previous = "")
        {
            await Task.Run(() =>
            {
                SaveChanges(reason, previous);
            });
        }

        /// <summary>
        /// Create a new wiki page and return an instance with the updated data.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        public WikiPage CreateAndReturn(string reason, string content = null)
        {
            Create(reason, content);
            return new WikiPage(ref Dispatch, Dispatch.Wiki.Page(Name.ToLower(), "", "", Subreddit).Data, Subreddit, Name);
        }

        /// <summary>
        /// Create a new wiki page.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        public void Create(string reason, string content = null)
        {
            Dispatch.Wiki.Create(content, Name, reason, Subreddit);
        }

        /// <summary>
        /// Create a new wiki page asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="content">The page content</param>
        public async Task CreateAsync(string reason, string content = null)
        {
            await Task.Run(() =>
            {
                Create(reason, content);
            });
        }

        /// <summary>
        /// Toggle the public visibility of a wiki page revision.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        /// <returns>A boolean indicating true if page was hidden, false if page was unhidden.</returns>
        public bool Hide(string revision)
        {
            return ((RedditThings.StatusResult)Validate(Dispatch.Wiki.Hide(Name, revision, Subreddit))).Status;
        }

        /// <summary>
        /// Toggle the public visibility of a wiki page revision asynchronously.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        public async Task HideAsync(string revision)
        {
            await Task.Run(() =>
            {
                Hide(revision);
            });
        }

        /// <summary>
        /// Revert a wiki page to revision.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        public void Revert(string revision)
        {
            Dispatch.Wiki.Revert(Name, revision, Subreddit);
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
            await Task.Run(() =>
            {
                Revert(revision);
            });
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
        public List<RedditThings.WikiPageRevision> Revisions(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = false, int count = 0)
        {
            return Validate(Dispatch.Wiki.PageRevisions(Name, after, before, Subreddit, count, limit, show, srDetail)).Data.Children;
        }

        /// <summary>
        /// Retrieve the current permission settings for page.
        /// </summary>
        /// <returns>An object containing wiki page settings.</returns>
        public RedditThings.WikiPageSettings GetPermissions()
        {
            return Validate(Dispatch.Wiki.GetPermissions(Name, Subreddit)).Data;
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="listed">boolean value (true = appear in /wiki/pages, false = don't appear in /wiki/pages)</param>
        /// <param name="permLevel">an integer (0 = use wiki perms, 1 = only approved users may edit, 2 = only mods may edit or view)</param>
        /// <returns>An object containing wiki page settings.</returns>
        public RedditThings.WikiPageSettings UpdatePermissions(bool listed, int permLevel)
        {
            return Validate(Dispatch.Wiki.UpdatePermissions(Name, listed, permLevel, Subreddit)).Data;
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page asynchronously.
        /// </summary>
        /// <param name="listed">boolean value (true = appear in /wiki/pages, false = don't appear in /wiki/pages)</param>
        /// <param name="permLevel">an integer (0 = use wiki perms, 1 = only approved users may edit, 2 = only mods may edit or view)</param>
        public async Task UpdatePermissionsAsync(bool listed, int permLevel)
        {
            await Task.Run(() =>
            {
                UpdatePermissions(listed, permLevel);
            });
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="wikiPageSettings">A valid instance of WikiPageSettings</param>
        /// <returns>An object containing wiki page settings.</returns>
        public RedditThings.WikiPageSettings UpdatePermissions(RedditThings.WikiPageSettings wikiPageSettings)
        {
            return Validate(Dispatch.Wiki.UpdatePermissions(Name, wikiPageSettings, Subreddit)).Data;
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page asynchronously.
        /// </summary>
        /// <param name="wikiPageSettings">A valid instance of WikiPageSettings</param>
        public async Task UpdatePermissionsAsync(RedditThings.WikiPageSettings wikiPageSettings)
        {
            await Task.Run(() =>
            {
                UpdatePermissions(wikiPageSettings);
            });
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
            return new WikiPage(ref Dispatch, ((RedditThings.WikiPageContainer)Validate(Dispatch.Wiki.Page(Name, v, v2, Subreddit))).Data, Subreddit, Name);
        }

        internal virtual void OnPagesUpdated(WikiPageUpdateEventArgs e)
        {
            PageUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Monitor this wiki page for any changes.
        /// </summary>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorPage()
        {
            string key = "WikiPage";
            return Monitor(key, new Thread(() => MonitorPageThread(key)), Name);
        }

        protected override Thread CreateMonitoringThread(string key, string subkey, int startDelayMs = 0)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "WikiPage":
                    return new Thread(() => MonitorPageThread(key, startDelayMs));
            }
        }

        private void MonitorPageThread(string key, int startDelayMs = 0)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            while (!Terminate
                && Monitoring.Get(key).Contains(Name))
            {
                WikiPage newPage = About();

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

                Thread.Sleep(Monitoring.Count() * MonitoringWaitDelayMS);
            }
        }
    }
}
