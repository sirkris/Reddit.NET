using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

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
        public object Karma()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/karma.json"));
        }

        /// <summary>
        /// Return the preference settings of the logged in user.
        /// </summary>
        /// <returns>The preference settings of the logged in user.</returns>
        public object Prefs()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/prefs.json"));
        }

        /// <summary>
        /// Update preferences.
        /// </summary>
        /// <param name="json">See https://www.reddit.com/dev/api/#PATCH_api_v1_me_prefs for required format</param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdatePrefs(string json)
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/prefs", Method.PATCH);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return a list of trophies for the current user.
        /// </summary>
        /// <returns>A list of trophies for the current user.</returns>
        public object Trophies()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/trophies.json"));
        }

        /// <summary>
        /// Get users with whom the current user has friended, blocked, or trusted.
        /// </summary>
        /// <param name="where">One of (friends, blocked, messaging, trusted)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <returns>A listing of users.</returns>
        public object Prefs(string where, string after = null, string before = null, int count = 0, int limit = 25, string show = "all",
            bool srDetail = false, bool includeCategories = true)
        {
            RestRequest restRequest = PrepareRequest("prefs/" + where);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);
            restRequest.AddParameter("include_categories", includeCategories);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
