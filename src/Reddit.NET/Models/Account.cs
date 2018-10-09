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

        public User Me()
        {
            return JsonConvert.DeserializeObject<User>(ExecuteRequest("api/v1/me.json"));
        }

        public object Karma()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/karma.json"));
        }

        public object Prefs()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/prefs.json"));
        }

        public object Trophies()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/trophies.json"));
        }

        public object UpdatePrefs(string json)
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/prefs", Method.PATCH);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

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
