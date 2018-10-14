using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    /// <summary>
    /// Real-time updates on reddit.
    /// In addition to the standard reddit API, WebSockets play a huge role in reddit live. Receiving push notification of changes to the thread via websockets is much better than polling the thread repeatedly.
    /// To connect to the websocket server, fetch /live/thread/about.json and get the websocket_url field. The websocket URL expires after a period of time; if it does, fetch a new one from that endpoint.
    /// Once connected to the socket, a variety of messages can come in. All messages will be in text frames containing a JSON object with two keys: type and payload. Live threads can send messages with many types:
    /// update - a new update has been posted in the thread. the payload contains the JSON representation of the update.
    /// activity - periodic update of the viewer counts for the thread.
    /// settings - the thread's settings have changed. the payload is an object with each key being a property of the thread (as in about.json) and its new value.
    /// delete - an update has been deleted (removed from the thread). the payload is the ID of the deleted update.
    /// strike - an update has been stricken (marked incorrect and crossed out). the payload is the ID of the stricken update.embeds_ready - a previously posted update has been parsed and embedded media 
    /// is available for it now. the payload contains a liveupdate_id and list of embeds to add to it.
    /// complete - the thread has been marked complete. no further updates will be sent.
    /// See /r/live for more information.
    /// </summary>
    public class LiveThreads : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public LiveThreads(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// Get a listing of live events by id.
        /// </summary>
        /// <param name="names">a comma-delimited list of live thread fullnames or IDs</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetById(string names)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/live/by_id/" + names));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Create a new live thread.
        /// Once created, the initial settings can be modified with /api/live/thread/edit and new updates can be posted with /api/live/thread/update.
        /// </summary>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <returns>(TODO - Untested)</returns>
        public object Create(string description, bool nsfw, string resources, string title)
        {
            RestRequest restRequest = PrepareRequest("api/live/create", Method.POST);

            restRequest.AddParameter("description", description);
            restRequest.AddParameter("nsfw", nsfw);
            restRequest.AddParameter("resources", resources);
            restRequest.AddParameter("title", title);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get some basic information about the currently featured live thread.
        /// Returns an empty 204 response for api requests if no thread is currently featured.
        /// See also: /api/live/thread/about.
        /// </summary>
        /// <returns>(TODO - Untested)</returns>
        public object HappeningNow()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/live/happening_now"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Accept a pending invitation to contribute to the thread.
        /// See also: /api/live/thread/leave_contributor.
        /// </summary>
        /// <param name="thread"></param>
        /// <returns>(TODO - Untested)</returns>
        public object AcceptContributorInvite(string thread)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/accept_contributor_invite", Method.POST);

            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Permanently close the thread, disallowing future updates.
        /// Requires the close permission for this thread.
        /// </summary>
        /// <param name="thread"></param>
        /// <returns>(TODO - Untested)</returns>
        public object CloseThread(string thread)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/close_thread", Method.POST);

            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Delete an update from the thread.
        /// Requires that specified update must have been authored by the user or that you have the edit permission for this thread.
        /// See also: /api/live/thread/update.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="id">the ID of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteUpdate(string thread, string id)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/delete_update", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Configure the thread.
        /// Requires the settings permission for this thread.
        /// See also: /live/thread/about.json.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <returns>(TODO - Untested)</returns>
        public object Edit(string thread, string description, bool nsfw, string resources, string title)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/edit", Method.POST);

            restRequest.AddParameter("description", description);
            restRequest.AddParameter("nsfw", nsfw);
            restRequest.AddParameter("resources", resources);
            restRequest.AddParameter("title", title);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Hide a linked comment thread from the discussions sidebar and listing.
        /// Requires the discussions permission for this thread.
        /// See also: /api/live/thread/unhide_discussion.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="link">The base 36 ID of a Link</param>
        /// <returns>(TODO - Untested)</returns>
        public object HideDiscussion(string thread, string link)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/hide_discussion", Method.POST);

            restRequest.AddParameter("link", link);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Invite another user to contribute to the thread.
        /// Requires the manage permission for this thread. If the recipient accepts the invite, they will be granted the permissions specified.
        /// See also: /api/live/thread/accept_contributor_invite, and /api/live/thread/rm_contributor_invite.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        /// <returns>(TODO - Untested)</returns>
        public object InviteContributor(string thread, string name, string permissions, string type)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/invite_contributor", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("permissions", permissions);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Abdicate contributorship of the thread.
        /// See also: /api/live/thread/accept_contributor_invite, and /api/live/thread/invite_contributor.
        /// </summary>
        /// <param name="thread"></param>
        /// <returns>(TODO - Untested)</returns>
        public object LeaveContributor(string thread)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/leave_contributor", Method.POST);

            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Report the thread for violating the rules of reddit.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="type">one of (spam, vote-manipulation, personal-information, sexualizing-minors, site-breaking)</param>
        /// <returns>(TODO - Untested)</returns>
        public object Report(string thread, string type)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/report", Method.POST);

            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Revoke another user's contributorship.
        /// Requires the manage permission for this thread.
        /// See also: /api/live/thread/invite_contributor.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="id">fullname of an account</param>
        /// <returns>(TODO - Untested)</returns>
        public object RemoveContributor(string thread, string id)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/rm_contributor", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Revoke an outstanding contributor invite.
        /// Requires the manage permission for this thread.
        /// See also: /api/live/thread/invite_contributor.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="id">fullname of an account</param>
        /// <returns>(TODO - Untested)</returns>
        public object RemoveContributorInvite(string thread, string id)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/rm_contributor_invite", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Change a contributor or contributor invite's permissions.
        /// Requires the manage permission for this thread.
        /// See also: /api/live/thread/invite_contributor and /api/live/thread/rm_contributor.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        /// <returns>(TODO - Untested)</returns>
        public object SetContributorPermissions(string thread, string name, string permissions, string type)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/set_contributor_permissions", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("permissions", permissions);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Strike (mark incorrect and cross out) the content of an update.
        /// Requires that specified update must have been authored by the user or that you have the edit permission for this thread.
        /// See also: /api/live/thread/update.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="id">the ID of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        /// <returns>(TODO - Untested)</returns>
        public object StrikeUpdate(string thread, string id)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/strike_update", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Unhide a linked comment thread from the discussions sidebar and listing.
        /// Requires the discussions permission for this thread.
        /// See also: /api/live/thread/hide_discussion.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="link">The base 36 ID of a Link</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnhideDiscussion(string thread, string link)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/unhide_discussion", Method.POST);

            restRequest.AddParameter("link", link);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Post an update to the thread.
        /// Requires the update permission for this thread.
        /// See also: /api/live/thread/strike_update, and /api/live/thread/delete_update.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="body">raw markdown text</param>
        /// <returns>(TODO - Untested)</returns>
        public object Update(string thread, string body)
        {
            RestRequest restRequest = PrepareRequest("api/live/" + thread + "/update", Method.POST);

            restRequest.AddParameter("body", body);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get a list of updates posted in this thread.
        /// See also: /api/live/thread/update.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="styleSr">subreddit name</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetUpdates(string thread, string after, string before, string styleSr, int count = 0, int limit = 25)
        {
            RestRequest restRequest = PrepareRequest("live/" + thread);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("stylesr", styleSr);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get some basic information about the live thread.
        /// See also: /api/live/thread/edit.
        /// </summary>
        /// <param name="thread"></param>
        /// <returns>(TODO - Untested)</returns>
        public object About(string thread)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("live/" + thread + "/about"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get a list of users that contribute to this thread.
        /// See also: /api/live/thread/invite_contributor, and /api/live/thread/rm_contributor.
        /// </summary>
        /// <param name="thread"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Contributors(string thread)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("live/" + thread + "/contributors"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get a list of reddit submissions linking to this thread.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>(TODO - Untested)</returns>
        public object Discussions(string thread, string after, string before, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("live/" + thread + "/discussions");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get details about a specific update in a live thread.
        /// </summary>
        /// <param name="thread"></param>
        /// <param name="updateId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object GetUpdate(string thread, string updateId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("live/" + thread + "/updates/" + updateId));
        }
    }
}
