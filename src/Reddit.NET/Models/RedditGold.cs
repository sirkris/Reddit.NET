using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class RedditGold : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public RedditGold(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Gild(string fullname)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/gold/gild/" + fullname, Method.POST));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="months"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Give(string username, int months)
        {
            RestRequest restRequest = PrepareRequest("api/v1/gold/give/" + username, Method.POST);

            restRequest.AddParameter("months", months);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
