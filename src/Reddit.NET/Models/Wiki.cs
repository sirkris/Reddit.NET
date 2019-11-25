using Newtonsoft.Json;
using Reddit.Inputs;
using Reddit.Inputs.Wiki;
using Reddit.Things;
using RestSharp;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class Wiki : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Wiki(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// Allow username to edit this wiki page.
        /// </summary>
        /// <param name="wikiPageEditorInput">A valid WikiPageEditorInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void AllowEditor(WikiPageEditorInput wikiPageEditorInput, string subreddit = null)
        {
            SendRequest<object>(Sr(subreddit) + "api/wiki/alloweditor/add", wikiPageEditorInput, Method.POST);
        }

        /// <summary>
        /// Asynchronously allow username to edit this wiki page.
        /// </summary>
        /// <param name="wikiPageEditorInput">A valid WikiPageEditorInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task AllowEditorAsync(WikiPageEditorInput wikiPageEditorInput, string subreddit = null)
        {
            await SendRequestAsync<object>(Sr(subreddit) + "api/wiki/alloweditor/add", wikiPageEditorInput, Method.POST);
        }

        /// <summary>
        /// Deny username to edit this wiki page.
        /// </summary>
        /// <param name="wikiPageEditorInput">A valid WikiPageEditorInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void DenyEditor(WikiPageEditorInput wikiPageEditorInput, string subreddit = null)
        {
            SendRequest<object>(Sr(subreddit) + "api/wiki/alloweditor/del", wikiPageEditorInput, Method.POST);
        }

        /// <summary>
        /// Asynchronously deny username to edit this wiki page.
        /// </summary>
        /// <param name="wikiPageEditorInput">A valid WikiPageEditorInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task DenyEditorAsync(WikiPageEditorInput wikiPageEditorInput, string subreddit = null)
        {
            await SendRequestAsync<object>(Sr(subreddit) + "api/wiki/alloweditor/del", wikiPageEditorInput, Method.POST);
        }

        /// <summary>
        /// Edit a wiki page.
        /// </summary>
        /// <param name="wikiEditPageInput">A valid WikiEditPageInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void Edit(WikiEditPageInput wikiEditPageInput, string subreddit = null)
        {
            SendRequest<object>(Sr(subreddit) + "api/wiki/edit", wikiEditPageInput, Method.POST);
        }

        /// <summary>
        /// Edit a wiki page asynchronously.
        /// </summary>
        /// <param name="wikiEditPageInput">A valid WikiEditPageInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task EditAsync(WikiEditPageInput wikiEditPageInput, string subreddit = null)
        {
            await SendRequestAsync<object>(Sr(subreddit) + "api/wiki/edit", wikiEditPageInput, Method.POST);
        }

        /// <summary>
        /// Create a wiki page.
        /// </summary>
        /// <param name="wikiCreatePageInput">A valid WikiCreatePageInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void Create(WikiCreatePageInput wikiCreatePageInput, string subreddit = null)
        {
            Edit(new WikiEditPageInput(wikiCreatePageInput), subreddit);
        }

        /// <summary>
        /// Create a wiki page asynchronously.
        /// </summary>
        /// <param name="wikiCreatePageInput">A valid WikiCreatePageInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task CreateAsync(WikiCreatePageInput wikiCreatePageInput, string subreddit = null)
        {
            await EditAsync(new WikiEditPageInput(wikiCreatePageInput), subreddit);
        }

        /// <summary>
        /// Toggle the public visibility of a wiki page revision.
        /// </summary>
        /// <param name="wikiPageRevisionInput">A valid WikiPageRevisionInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>Status object indicating true if page was hidden, false if page was unhidden.</returns>
        public StatusResult Hide(WikiPageRevisionInput wikiPageRevisionInput, string subreddit = null)
        {
            return SendRequest<StatusResult>(Sr(subreddit) + "api/wiki/hide", wikiPageRevisionInput, Method.POST);
        }

        /// <summary>
        /// Toggle the public visibility of a wiki page revision asynchronously.
        /// </summary>
        /// <param name="wikiPageRevisionInput">A valid WikiPageRevisionInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>Status object indicating true if page was hidden, false if page was unhidden.</returns>
        public async Task<StatusResult> HideAsync(WikiPageRevisionInput wikiPageRevisionInput, string subreddit = null)
        {
            return await SendRequestAsync<StatusResult>(Sr(subreddit) + "api/wiki/hide", wikiPageRevisionInput, Method.POST);
        }

        /// <summary>
        /// Revert a wiki page to revision.
        /// </summary>
        /// <param name="wikiPageRevisionInput">A valid WikiPageRevisionInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public void Revert(WikiPageRevisionInput wikiPageRevisionInput, string subreddit = null)
        {
            SendRequest<object>(Sr(subreddit) + "api/wiki/revert", wikiPageRevisionInput, Method.POST);
        }

        /// <summary>
        /// Revert a wiki page to revision asynchronously.
        /// </summary>
        /// <param name="wikiPageRevisionInput">A valid WikiPageRevisionInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        public async Task RevertAsync(WikiPageRevisionInput wikiPageRevisionInput, string subreddit = null)
        {
            await SendRequestAsync<object>(Sr(subreddit) + "api/wiki/revert", wikiPageRevisionInput, Method.POST);
        }

        // TODO - Either this feature doesn't work or even the busiest subreddits have no Wiki discussion posts.  All my tests yield a listing container with no children.  --Kris
        /// <summary>
        /// Retrieve a list of discussions about this wiki page.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="srListingInput">A valid SrListingInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>(TODO - Untested)</returns>
        public object Discussions(string page, SrListingInput srListingInput, string subreddit = null)
        {
            return SendRequest<object>(Sr(subreddit) + "wiki/discussions/" + page, srListingInput);
        }

        /// <summary>
        /// Retrieve a list of wiki pages in this subreddit.
        /// </summary>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>A list of wiki pages.</returns>
        public WikiPageListing Pages(string subreddit = null)
        {
            return JsonConvert.DeserializeObject<WikiPageListing>(ExecuteRequest(Sr(subreddit) + "wiki/pages"));
        }

        /// <summary>
        /// Retrieve a list of recently changed wiki pages in this subreddit.
        /// </summary>
        /// <param name="srListingInput">A valid SrListingInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>A list of wiki pages.</returns>
        public WikiPageRevisionContainer Revisions(SrListingInput srListingInput, string subreddit = null)
        {
            return SendRequest<WikiPageRevisionContainer>(Sr(subreddit) + "wiki/revisions", srListingInput);
        }

        /// <summary>
        /// Retrieve a list of revisions of this wiki page.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="srListingInput">A valid SrListingInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>A list of revisions.</returns>
        public WikiPageRevisionContainer PageRevisions(string page, SrListingInput srListingInput, string subreddit = null)
        {
            return SendRequest<WikiPageRevisionContainer>(Sr(subreddit) + "wiki/revisions/" + page, srListingInput);
        }

        /// <summary>
        /// Retrieve the current permission settings for page.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>An object containing wiki page settings.</returns>
        public WikiPageSettingsContainer GetPermissions(string page, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<WikiPageSettingsContainer>(ExecuteRequest(Sr(subreddit) + "wiki/settings/" + page));
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="wikiUpdatePermissionsInput">A valid WikiUpdatePermissionsInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>An object containing wiki page settings.</returns>
        public WikiPageSettingsContainer UpdatePermissions(string page, WikiUpdatePermissionsInput wikiUpdatePermissionsInput, string subreddit = null)
        {
            return SendRequest<WikiPageSettingsContainer>(Sr(subreddit) + "wiki/settings/" + page, wikiUpdatePermissionsInput, Method.POST);
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page asynchronously.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="wikiUpdatePermissionsInput">A valid WikiUpdatePermissionsInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>An object containing wiki page settings.</returns>
        public async Task<WikiPageSettingsContainer> UpdatePermissionsAsync(string page, WikiUpdatePermissionsInput wikiUpdatePermissionsInput, string subreddit = null)
        {
            return await SendRequestAsync<WikiPageSettingsContainer>(Sr(subreddit) + "wiki/settings/" + page, wikiUpdatePermissionsInput, Method.POST);
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="wikiPageSettings">A valid instance of WikiPageSettings</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>An object containing wiki page settings.</returns>
        public WikiPageSettingsContainer UpdatePermissions(string page, WikiPageSettings wikiPageSettings, string subreddit = null)
        {
            return UpdatePermissions(page, new WikiUpdatePermissionsInput(wikiPageSettings.Listed, wikiPageSettings.PermLevel), subreddit);
        }

        /// <summary>
        /// Update the permissions and visibility of wiki page asynchronously.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="wikiPageSettings">A valid instance of WikiPageSettings</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>An object containing wiki page settings.</returns>
        public async Task<WikiPageSettingsContainer> UpdatePermissionsAsync(string page, WikiPageSettings wikiPageSettings, string subreddit = null)
        {
            return await UpdatePermissionsAsync(page, new WikiUpdatePermissionsInput(wikiPageSettings.Listed, wikiPageSettings.PermLevel), subreddit);
        }

        /// <summary>
        /// Return the content of a wiki page.
        /// If v is given, show the wiki page as it was at that version. If both v and v2 are given, show a diff of the two.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="wikiPageContentInput">A valid WikiPageContentInput instance</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>An object containing wiki page data.</returns>
        public WikiPageContainer Page(string page, WikiPageContentInput wikiPageContentInput, string subreddit = null)
        {
            return SendRequest<WikiPageContainer>(Sr(subreddit) + "wiki/" + page, wikiPageContentInput);
        }
    }
}
