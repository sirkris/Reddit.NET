using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.NET.Models
{
    public class Account : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Account(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        /// <summary>
        /// Returns the identity of the user.
        /// </summary>
        /// <returns>The identity of the user.</returns>
        public User Me()
        {
            return JsonConvert.DeserializeObject<User>(ExecuteRequest("api/v1/me.json"));
        }

        /// <summary>
        /// Return a breakdown of subreddit karma.
        /// </summary>
        /// <returns>A breakdown of subreddit karma.</returns>
        public UserKarmaContainer Karma()
        {
            return JsonConvert.DeserializeObject<UserKarmaContainer>(ExecuteRequest("api/v1/me/karma.json"));
        }

        /// <summary>
        /// Return the preference settings of the logged in user.
        /// </summary>
        /// <returns>The preference settings of the logged in user.</returns>
        public AccountPrefs Prefs()
        {
            return JsonConvert.DeserializeObject<AccountPrefs>(ExecuteRequest("api/v1/me/prefs.json"));
        }

        /// <summary>
        /// Update preferences.
        /// </summary>
        /// <param name="json">See https://www.reddit.com/dev/api/#PATCH_api_v1_me_prefs for required format</param>
        /// <returns>The updated preference settings of the logged in user.</returns>
        public AccountPrefs UpdatePrefs(string json)
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/prefs", Method.PATCH);

            restRequest.AddParameter("json", json);

            return JsonConvert.DeserializeObject<AccountPrefs>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Update preferences.
        /// </summary>
        /// <param name="accountPrefs">A valid AccountPrefs instance.</param>
        /// <returns>The updated preference settings of the logged in user.</returns>
        public AccountPrefs UpdatePrefs(AccountPrefsSubmit accountPrefs)
        {
            return UpdatePrefs(JsonConvert.SerializeObject(accountPrefs));
        }

        /// <summary>
        /// Return a list of trophies for the current user.
        /// </summary>
        /// <returns>A list of trophies for the current user.</returns>
        public TrophyList Trophies()
        {
            return JsonConvert.DeserializeObject<TrophyList>(ExecuteRequest("api/v1/me/trophies.json"));
        }

        /// <summary>
        /// Get users with whom the current user has friended, blocked, or trusted.
        /// </summary>
        /// <param name="where">One of (friends, messaging)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <returns>A listing of users.</returns>
        public List<UserPrefsContainer> PrefsList(string where, string after = null, string before = null, int count = 0, int limit = 25, string show = "all",
            bool srDetail = false, bool includeCategories = false)
        {
            RestRequest restRequest = PrepareRequest("prefs/" + where);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);
            restRequest.AddParameter("include_categories", includeCategories);

            return JsonConvert.DeserializeObject<List<UserPrefsContainer>>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get users with whom the current user has friended, blocked, or trusted.
        /// </summary>
        /// <param name="where">One of (blocked, trusted)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <returns>A listing of users.</returns>
        public UserPrefsContainer PrefsSingle(string where, string after = null, string before = null, int count = 0, int limit = 25, string show = "all",
            bool srDetail = false, bool includeCategories = false)
        {
            RestRequest restRequest = PrepareRequest("prefs/" + where);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);
            restRequest.AddParameter("include_categories", includeCategories);

            return JsonConvert.DeserializeObject<UserPrefsContainer>(ExecuteRequest(restRequest));
        }
    }
}
