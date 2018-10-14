using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Users : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Users(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// For blocking a user.
        /// </summary>
        /// <param name="accountId">ullname of a account</param>
        /// <param name="name">A valid, existing reddit username</param>
        /// <returns>(TODO - Untested)</returns>
        public object BlockUser(string accountId, string name)
        {
            RestRequest restRequest = PrepareRequest("api/block_user", Method.POST);

            restRequest.AddParameter("account_id", accountId);
            restRequest.AddParameter("name", name);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Create a relationship between a user and another user or subreddit
        /// OAuth2 use requires appropriate scope based on the 'type' of the relationship:
        /// moderator: Use "moderator_invite"
        /// moderator_invite: modothers
        /// contributor: modcontributors
        /// banned: modcontributors
        /// muted: modcontributors
        /// wikibanned: modcontributors and modwiki
        /// wikicontributor: modcontributors and modwiki
        /// friend: Use /api/v1/me/friends/{username}
        /// enemy: Use /api/block
        /// Complement to POST_unfriend
        /// </summary>
        /// <param name="banContext">fullname of a thing</param>
        /// <param name="banMessage">raw markdown text</param>
        /// <param name="banReason">a string no longer than 100 characters</param>
        /// <param name="container"></param>
        /// <param name="duration">an integer between 1 and 999</param>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions"></param>
        /// <param name="type">one of (friend, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>An object indicating any errors.</returns>
        public object Friend(string banContext, string banMessage, string banReason, string container, int duration, string name,
            string permissions, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/friend", Method.POST);

            restRequest.AddParameter("ban_context", banContext);
            restRequest.AddParameter("ban_message", banMessage);
            restRequest.AddParameter("ban_reason", banReason);
            restRequest.AddParameter("container", container);
            restRequest.AddParameter("duration", duration);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("permissions", permissions);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Report a user. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="details">JSON data</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <returns>(TODO - Untested)</returns>
        public object ReportUser(string details, string reason, string user)
        {
            RestRequest restRequest = PrepareRequest("api/report_user", Method.POST);

            restRequest.AddParameter("details", details);
            restRequest.AddParameter("reason", reason);
            restRequest.AddParameter("user", user);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions"></param>
        /// <param name="type"></param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>(TODO - Untested)</returns>
        public object SetPermissions(string name, string permissions, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/setpermissions", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("permissions", permissions);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove a relationship between a user and another user or subreddit.
        /// The user can either be passed in by name (nuser) or by fullname (iuser).
        /// If type is friend or enemy, 'container' MUST be the current user's fullname; for other types, the subreddit must be set via URL (e.g., /r/funny/api/unfriend).
        /// OAuth2 use requires appropriate scope based on the 'type' of the relationship:
        /// moderator: Use "moderator_invite"
        /// moderator_invite: modothers
        /// contributor: modcontributors
        /// banned: modcontributors
        /// muted: modcontributors
        /// wikibanned: modcontributors and modwiki
        /// wikicontributor: modcontributors and modwiki
        /// friend: Use /api/v1/me/friends/{username}
        /// enemy: Use /api/block
        /// Complement to POST_friend
        /// </summary>
        /// <param name="container"></param>
        /// <param name="id">fullname of a thing</param>
        /// <param name="name">the name of an existing user</param>
        /// <param name="type">one of (friend, enemy, moderator, moderator_invite, contributor, banned, muted, wikibanned, wikicontributor)</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>(TODO - Untested)</returns>
        public object Unfriend(string container, string id, string name, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/unfriend", Method.POST);

            restRequest.AddParameter("container", container);
            restRequest.AddParameter("id", id);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("type", type);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get user data by account IDs.
        /// </summary>
        /// <param name="ids">A comma-separated list of account fullnames</param>
        /// <returns>A user object.</returns>
        public object UserDataByAccountIds(string ids)
        {
            RestRequest restRequest = PrepareRequest("api/user_data_by_account_ids");

            restRequest.AddParameter("ids", ids);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Check whether a username is available for registration.
        /// </summary>
        /// <param name="user">a valid, unused username</param>
        /// <returns>Boolean value or JSON object containing errors.</returns>
        public object UsernameAvailable(string user)
        {
            RestRequest restRequest = PrepareRequest("api/username_available");

            restRequest.AddParameter("user", user);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Stop being friends with a user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteFriend(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/friends/" + username, Method.DELETE));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get information about a specific 'friend', such as notes.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetFriend(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/friends/" + username));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Create or update a "friend" relationship.
        /// This operation is idempotent. It can be used to add a new friend, or update an existing friend (e.g., add/change the note on that friend).
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="json">{
        /// "name": A valid, existing reddit username
        /// "note": a string no longer than 300 characters
        /// }</param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdateFriend(string username, string json)
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/friends/" + username, Method.PUT);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return a list of trophies for the a given user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>A list of trophies.</returns>
        public object Trophies(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/user/" + username + "/trophies"));
        }

        /// <summary>
        /// Return information about the user, including karma and gold status.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <returns>A user listing.</returns>
        public object About(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("user/" + username + "/about"));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="where">One of (overview, submitted, comments, upvotes, downvoted, hidden, saved, gilded)</param>
        /// <param name="context">an integer between 2 and 10</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="sort">one of (hot, new, top, controversial)</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="type">one of (links, comments)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of objects containing the requested data.</returns>
        public ListingContainer History(string username, string where, int context, string show, string sort, string t, string type,
            string after, string before, bool includeCategories, int count = 0, int limit = 25, bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("user/" + username + "/" + where);

            restRequest.AddParameter("context", context);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("t", t);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("sr_detail", srDetail);
            
            return JsonConvert.DeserializeObject<ListingContainer>(ExecuteRequest(restRequest));
        }
    }
}
