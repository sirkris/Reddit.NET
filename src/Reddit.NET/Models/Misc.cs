using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Misc : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Misc(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// Retrieve the advisory text about saving media for relevant media links.
        /// This endpoint returns a notice for display during the post submission process that is pertinent to media links.
        /// </summary>
        /// <param name="url">a valid URL</param>
        /// <param name="subreddit">A subreddit</param>
        /// <returns>(TODO - Untested)</returns>
        public object SavedMediaText(string url, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/saved_media_text");

            restRequest.AddParameter("url", url);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Retrieve descriptions of reddit's OAuth2 scopes.
        /// If no scopes are given, information on all scopes are returned.
        /// Invalid scope(s) will result in a 400 error with body that indicates the invalid scope(s).
        /// </summary>
        /// <param name="scopes">(optional) An OAuth2 scope string</param>
        /// <returns>A list of scopes.</returns>
        public object Scopes(string scopes = null)
        {
            RestRequest restRequest = PrepareRequest("api/v1/scopes");

            restRequest.AddParameter("scopes", scopes);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
