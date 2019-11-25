using Newtonsoft.Json;
using Reddit.Things;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.Models
{
    public class Misc : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Misc(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// Retrieve the advisory text about saving media for relevant media links.
        /// This endpoint returns a notice for display during the post submission process that is pertinent to media links.
        /// </summary>
        /// <param name="url">a valid URL</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>A Reddit notice message.</returns>
        public Dictionary<string, string> SavedMediaText(string url, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/saved_media_text");

            restRequest.AddParameter("url", url);

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Retrieve descriptions of reddit's OAuth2 scopes.
        /// If no scopes are given, information on all scopes are returned.
        /// Invalid scope(s) will result in a 400 error with body that indicates the invalid scope(s).
        /// </summary>
        /// <param name="scopes">(optional) An OAuth2 scope string</param>
        /// <returns>A list of scopes.</returns>
        public Dictionary<string, Scope> Scopes(string scopes = null)
        {
            RestRequest restRequest = PrepareRequest("api/v1/scopes");

            restRequest.AddParameter("scopes", scopes);

            return JsonConvert.DeserializeObject<Dictionary<string, Scope>>(ExecuteRequest(restRequest));
        }
    }
}
