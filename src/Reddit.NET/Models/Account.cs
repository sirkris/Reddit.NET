using Newtonsoft.Json;
using Reddit.Inputs;
using Reddit.Things;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class Account : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Account(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

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
        /// <param name="accountPrefs">A valid AccountPrefs instance.</param>
        /// <returns>The updated preference settings of the logged in user.</returns>
        public AccountPrefs UpdatePrefs(AccountPrefsSubmit accountPrefs)
        {
            RestRequest restRequest = PrepareUpdatePrefs(accountPrefs);

            return JsonConvert.DeserializeObject<AccountPrefs>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Update preferences asynchronously.
        /// </summary>
        /// <param name="accountPrefs">A valid AccountPrefs instance.</param>
        /// <returns>The updated preference settings of the logged in user.</returns>
        public async Task<AccountPrefs> UpdatePrefsAsync(AccountPrefsSubmit accountPrefs)
        {
            RestRequest restRequest = PrepareUpdatePrefs(accountPrefs);

            return JsonConvert.DeserializeObject<AccountPrefs>(await ExecuteRequestAsync(restRequest));
        }

        private RestRequest PrepareUpdatePrefs(AccountPrefsSubmit accountPrefs)
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/prefs", Method.PATCH);

            restRequest.AddParameter("json", JsonConvert.SerializeObject(accountPrefs));

            return restRequest;
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
        /// <param name="accountPrefsInput">A valid AccountPrefsInput instance</param>
        /// <returns>A listing of users.</returns>
        public List<UserPrefsContainer> PrefsList(string where, CategorizedSrListingInput accountPrefsInput)
        {
            return SendRequest<List<UserPrefsContainer>>("prefs/" + where, accountPrefsInput);
        }

        /// <summary>
        /// Get users with whom the current user has friended, blocked, or trusted.
        /// </summary>
        /// <param name="where">One of (friends, messaging)</param>
        /// <param name="accountPrefsInput">A valid AccountPrefsInput instance</param>
        /// <returns>A listing of users.</returns>
        public UserPrefsContainer PrefsSingle(string where, CategorizedSrListingInput accountPrefsInput)
        {
            return SendRequest<UserPrefsContainer>("prefs/" + where, accountPrefsInput);
        }
    }
}
