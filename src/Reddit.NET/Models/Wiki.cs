using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Wiki : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Wiki(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// Allow username to edit this wiki page.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="username">the name of an existing user</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>(TODO - Untested)</returns>
        public object AllowEditor(string page, string username, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/alloweditor/add", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("username", username);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Deny username to edit this wiki page.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="username">the name of an existing user</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>(TODO - Untested)</returns>
        public object DenyEditor(string page, string username, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/alloweditor/del", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("username", username);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Edit a wiki page.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="page">the name of an existing page or a new page to create</param>
        /// <param name="previous">the starting point revision for this edit</param>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>(TODO - Untested)</returns>
        public object Edit(string content, string page, string previous, string reason, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/edit", Method.POST);

            restRequest.AddParameter("content", content);
            restRequest.AddParameter("previous", previous);
            restRequest.AddParameter("reason", reason);

            return JsonConvert.DeserializeObject(ExecuteRequest(reason));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Toggle the public visibility of a wiki page revision.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="revision">a wiki revision ID</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>(TODO - Untested)</returns>
        public object Hide(string page, string revision, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/hide", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("revision", revision);

            return JsonConvert.DeserializeObject(ExecuteRequest(revision));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Revert a wiki page to revision.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="revision">a wiki revision ID</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>(TODO - Untested)</returns>
        public object Revert(string page, string revision, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/revert", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("revision", revision);

            return JsonConvert.DeserializeObject(ExecuteRequest(revision));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Retrieve a list of discussions about this wiki page.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>(TODO - Untested)</returns>
        public object Discussions(string page, string after, string before, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/discussions/" + page);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
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
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of wiki pages.</returns>
        public WikiPageRevisionContainer Revisions(string after, string before, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/revisions");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject<WikiPageRevisionContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Retrieve a list of revisions of this wiki page.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of revisions.</returns>
        public WikiPageRevisionContainer PageRevisions(string page, string after, string before, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/revisions/" + page);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject<WikiPageRevisionContainer>(ExecuteRequest(restRequest));
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

        // TODO - Needs testing.
        /// <summary>
        /// Update the permissions and visibility of wiki page.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="listed">boolean value</param>
        /// <param name="permLevel">an integer</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdatePermissions(string page, bool listed, int permLevel, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/settings/" + page, Method.POST);

            restRequest.AddParameter("listed", listed);
            restRequest.AddParameter("permlevel", permLevel);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return the content of a wiki page.
        /// If v is given, show the wiki page as it was at that version If both v and v2 are given, show a diff of the two.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="v">a wiki revision ID</param>
        /// <param name="v2">a wiki revision ID</param>
        /// <param name="subreddit">The subreddit where the wiki lives</param>
        /// <returns>An object containing wiki page data.</returns>
        public WikiPageContainer Page(string page, string v, string v2, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/" + page);

            restRequest.AddParameter("v", v);
            restRequest.AddParameter("v2", v2);

            return JsonConvert.DeserializeObject<WikiPageContainer>(ExecuteRequest(restRequest));
        }
    }
}
