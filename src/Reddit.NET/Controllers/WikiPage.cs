using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    public class WikiPage : BaseController
    {
        public bool MayRevise;
        public DateTime RevisionDate;
        public string ContentHTML;
        public User RevisionBy;
        public string ContentMd;

        public string Name;
        public string Subreddit;

        internal override ref Models.Internal.Monitor MonitorModel => ref MonitorNull;
        internal override ref MonitoringSnapshot Monitoring => ref MonitoringSnapshotNull;

        internal readonly Dispatch Dispatch;

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

        public WikiPage(Dispatch dispatch, RedditThings.WikiPage wikiPage, string subreddit = null, string name = null)
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

        public WikiPage(Dispatch dispatch, string subreddit = null, string name = null)
        {
            Dispatch = dispatch;
            Subreddit = subreddit;
            Name = name;
        }

        public WikiPage(string subreddit, string name = null)
        {
            Subreddit = subreddit;
            Name = name;
        }

        public WikiPage() { }

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
        public async void AllowEditorAsync(string username)
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
        public async void DenyEditorAsync(string username)
        {
            await Task.Run(() =>
            {
                DenyEditor(username);
            });
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
        public async void EditAsync(string reason, string content = null, string previous = "")
        {
            await Task.Run(() =>
            {
                Edit(reason, content, previous);
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
        public async void HideAsync(string revision)
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
        /// Revert a wiki page to revision asynchronously.
        /// </summary>
        /// <param name="revision">a wiki revision ID</param>
        public async void RevertAsync(string revision)
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
        public async void UpdatePermissionsAsync(bool listed, int permLevel)
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
        public async void UpdatePermissionsAsync(RedditThings.WikiPageSettings wikiPageSettings)
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
        public WikiPage About(string v, string v2)
        {
            return new WikiPage(Dispatch, ((RedditThings.WikiPageContainer)Validate(Dispatch.Wiki.Page(Name, v, v2, Subreddit))).Data, Subreddit, Name);
        }
    }
}
