using Newtonsoft.Json;
using Reddit.Inputs.Users;
using Reddit.Things;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class Users : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Users(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// For blocking a user.
        /// </summary>
        /// <param name="usersBlockUserInput">A valid UserActionResult input</param>
        /// <returns>An object containing basic info on the target user and the datetime of this action.</returns>
        public UserActionResult BlockUser(UsersBlockUserInput usersBlockUserInput)
        {
            return SendRequest<UserActionResult>("api/block_user", usersBlockUserInput, Method.POST);
        }

        /// <summary>
        /// For blocking a user asynchronously.
        /// </summary>
        /// <param name="usersBlockUserInput">A valid UserActionResult input</param>
        /// <returns>An object containing basic info on the target user and the datetime of this action.</returns>
        public async Task<UserActionResult> BlockUserAsync(UsersBlockUserInput usersBlockUserInput)
        {
            return await SendRequestAsync<UserActionResult>("api/block_user", usersBlockUserInput, Method.POST);
        }

        // TODO - Specifying "friend" as type causes API to return 400 no matter what I try.  Did Reddit deprecate this??  --Kris
        /// <summary>
        /// Create a relationship between a user and another user or subreddit.
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
        /// <param name="usersFriendInput">A valid UsersFriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer Friend(UsersFriendInput usersFriendInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/friend", usersFriendInput, Method.POST);
        }

        /// <summary>
        /// Create a relationship between a user and another user or subreddit asynchronously.
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
        /// <param name="usersFriendInput">A valid UsersFriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> FriendAsync(UsersFriendInput usersFriendInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/friend", usersFriendInput, Method.POST);
        }

        // Note - I tested this one manually.  Leaving out of automated tests so as not to spam the Reddit admins.  --Kris
        /// <summary>
        /// Report a user. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="usersReportUserInput">A valid UsersReportUserInput instance</param>
        public void ReportUser(UsersReportUserInput usersReportUserInput)
        {
            SendRequest<object>("api/report_user", usersReportUserInput, Method.POST);
        }

        /// <summary>
        /// Report a user asynchronously. Reporting a user brings it to the attention of a Reddit admin.
        /// </summary>
        /// <param name="usersReportUserInput">A valid UsersReportUserInput instance</param>
        public async Task ReportUserAsync(UsersReportUserInput usersReportUserInput)
        {
            await SendRequestAsync<object>("api/report_user", usersReportUserInput, Method.POST);
        }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="usersSetPermissionsInput">A valid UsersSetPermissionsInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer SetPermissions(UsersSetPermissionsInput usersSetPermissionsInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/setpermissions", usersSetPermissionsInput, Method.POST);
        }

        /// <summary>
        /// Set permissions asynchronously.
        /// </summary>
        /// <param name="usersSetPermissionsInput">A valid UsersSetPermissionsInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> SetPermissionsAsync(UsersSetPermissionsInput usersSetPermissionsInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/setpermissions", usersSetPermissionsInput, Method.POST);
        }

        /// <summary>
        /// Remove a relationship between a user and another user or subreddit.
        /// The user can either be passed in by name (nuser) or by fullname (iuser).
        /// If type is friend or enemy, 'container' MUST be the current user's fullname; for other types, the subreddit must be set via URL (e.g., /r/funny/api/unfriend).
        /// OAuth2 use requires appropriate scope based on the 'type' of the relationship:
        /// moderator: modothers
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
        /// <param name="usersUnfriendInput">A valid UsersUnfriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        public void Unfriend(UsersUnfriendInput usersUnfriendInput, string subreddit = null)
        {
            SendRequest<object>(Sr(subreddit) + "api/unfriend", usersUnfriendInput, Method.POST);
        }

        /// <summary>
        /// Remove a relationship between a user and another user or subreddit asynchronously.
        /// The user can either be passed in by name (nuser) or by fullname (iuser).
        /// If type is friend or enemy, 'container' MUST be the current user's fullname; for other types, the subreddit must be set via URL (e.g., /r/funny/api/unfriend).
        /// OAuth2 use requires appropriate scope based on the 'type' of the relationship:
        /// moderator: modothers
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
        /// <param name="usersUnfriendInput">A valid UsersUnfriendInput instance</param>
        /// <param name="subreddit">A subreddit</param>
        public async Task UnfriendAsync(UsersUnfriendInput usersUnfriendInput, string subreddit = null)
        {
            await SendRequestAsync<object>(Sr(subreddit) + "api/unfriend", usersUnfriendInput, Method.POST);
        }

        /// <summary>
        /// Get user data by account IDs.
        /// </summary>
        /// <param name="ids">A comma-separated list of account fullnames</param>
        /// <returns>A dictionary of user summary objects.</returns>
        public Dictionary<string, UserSummary> UserDataByAccountIds(string ids)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, UserSummary>>(ExecuteRequest(PrepareUserDataByAccountIds(ids)));
        }

        private RestRequest PrepareUserDataByAccountIds(string ids)
        {
            RestRequest restRequest = PrepareRequest("api/user_data_by_account_ids");

            restRequest.AddParameter("ids", ids);

            return restRequest;
        }

        /// <summary>
        /// Check whether a username is available for registration.
        /// </summary>
        /// <param name="user">a valid, unused username</param>
        /// <returns>Boolean or null if error (i.e. invalid username).</returns>
        public bool? UsernameAvailable(string user)
        {
            RestRequest restRequest = PrepareRequest("api/username_available");

            restRequest.AddParameter("user", user);

            object res = JsonConvert.DeserializeObject(ExecuteRequest(restRequest));

            return (res != null && bool.TryParse(res.ToString(), out bool dump) ? (bool?)res : null);
        }

        /// <summary>
        /// Stop being friends with a user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        public void DeleteFriend(string username)
        {
            ExecuteRequest("api/v1/me/friends/" + username, Method.DELETE);
        }

        /// <summary>
        /// Stop being friends with a user asynchronously.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        public async Task DeleteFriendAsync(string username)
        {
            await ExecuteRequestAsync("api/v1/me/friends/" + username, Method.DELETE);
        }

        // Note - Returns 400 if you are not friends with the specified user.  --Kris
        /// <summary>
        /// Get information about a specific 'friend', such as notes.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>An object containing basic info on the target user and the datetime of this action.</returns>
        public UserActionResult GetFriend(string username)
        {
            return JsonConvert.DeserializeObject<UserActionResult>(ExecuteRequest("api/v1/me/friends/" + username));
        }

        // Note - If I include the recommended JSON fields, API returns the error, "you must have an active reddit gold subscription to do that".  --Kris
        /// <summary>
        /// Create or update a "friend" relationship.
        /// This operation is idempotent. It can be used to add a new friend, or update an existing friend (e.g., add/change the note on that friend).
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="json">{
        /// "name": A valid, existing reddit username
        /// "note": a string no longer than 300 characters
        /// }</param>
        /// <returns>An object containing basic info on the target user and the datetime of this action.</returns>
        public UserActionResult UpdateFriend(string username, string json = "{}")
        {
            return JsonConvert.DeserializeObject<UserActionResult>(ExecuteRequest(PrepareUpdateFriend(username, json)));
        }

        /// <summary>
        /// Create or update a "friend" relationship asynchronously.
        /// This operation is idempotent. It can be used to add a new friend, or update an existing friend (e.g., add/change the note on that friend).
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="json">{
        /// "name": A valid, existing reddit username
        /// "note": a string no longer than 300 characters
        /// }</param>
        /// <returns>An object containing basic info on the target user and the datetime of this action.</returns>
        public async Task<UserActionResult> UpdateFriendAsync(string username, string json = "{}")
        {
            return JsonConvert.DeserializeObject<UserActionResult>(await ExecuteRequestAsync(PrepareUpdateFriend(username, json)));
        }

        private RestRequest PrepareUpdateFriend(string username, string json = "{}")
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/friends/" + username, Method.PUT);

            restRequest.AddParameter("json", json);

            return restRequest;
        }

        /// <summary>
        /// Return a list of trophies for the given user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>A list of trophies.</returns>
        public TrophyList Trophies(string username)
        {
            return JsonConvert.DeserializeObject<TrophyList>(ExecuteRequest("api/v1/user/" + username + "/trophies"));
        }

        /// <summary>
        /// Return information about the user, including karma and gold status.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <returns>A user listing.</returns>
        public UserChild About(string username)
        {
            return JsonConvert.DeserializeObject<UserChild>(ExecuteRequest("user/" + username + "/about"));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="where">One of (overview, submitted, upvotes, downvoted, hidden, saved, gilded, moderated_subreddits)</param>
        /// <param name="usersHistoryInput">A valid UsersHistoryInput instance</param>
        /// <returns>A list of objects containing the requested data.</returns>
        public T GetUser<T>(string username, string where, UsersHistoryInput usersHistoryInput)
        {
            RestRequest restRequest = PrepareRequest("user/" + username + "/" + where);

            restRequest.AddObject(usersHistoryInput);

            return JsonConvert.DeserializeObject<T>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Retrieve a list of subreddits that the user moderates.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="usersHistoryInput">A valid UsersHistoryInput instance</param>
        /// <returns>A list of objects containing the requested data.</returns>
        public ModeratedListContainer ModeratedSubreddits(string username, UsersHistoryInput usersHistoryInput)
        {
            return GetUser<ModeratedListContainer>(username, "moderated_subreddits", usersHistoryInput);
        }

        /// <summary>
        /// Get a user's post and comment history.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="usersHistoryInput">A valid UsersHistoryInput instance</param>
        /// <returns>A list of objects containing the requested data.</returns>
        public OverviewContainer Overview(string username, UsersHistoryInput usersHistoryInput)
        {
            usersHistoryInput.sort = (usersHistoryInput.sort.Equals("newForced", StringComparison.OrdinalIgnoreCase) ? "new" : usersHistoryInput.sort);
            return GetUser<OverviewContainer>(username, "overview", usersHistoryInput);
        }

        /// <summary>
        /// Get a user's post history.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="where">One of (submitted, upvotes, downvoted, hidden, saved, gilded)</param>
        /// <param name="usersHistoryInput">A valid UsersHistoryInput instance</param>
        /// <returns>A list of objects containing the requested data.</returns>
        public PostContainer PostHistory(string username, string where, UsersHistoryInput usersHistoryInput)
        {
            usersHistoryInput.sort = (usersHistoryInput.sort.Equals("newForced", StringComparison.OrdinalIgnoreCase) ? "new" : usersHistoryInput.sort);
            return GetUser<PostContainer>(username, where, usersHistoryInput);
        }

        /// <summary>
        /// Get a user's comment history.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="where">One of (comments, saved, gilded)</param>
        /// <param name="usersHistoryInput">A valid UsersHistoryInput instance</param>
        /// <returns>A list of objects containing the requested data.</returns>
        public CommentContainer CommentHistory(string username, string where, UsersHistoryInput usersHistoryInput)
        {
            return GetUser<CommentContainer>(username, where, usersHistoryInput);
        }
    }
}
