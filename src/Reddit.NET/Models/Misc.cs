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
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="subreddit"></param>
        /// <returns>(TODO - Untested)</returns>
        public object SavedMediaText(string url, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/saved_media_text");

            restRequest.AddParameter("url", url);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public object Scopes(string scopes = null)
        {
            RestRequest restRequest = PrepareRequest("api/v1/scopes");

            restRequest.AddParameter("scopes", scopes);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
