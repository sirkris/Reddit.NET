using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public Account(string accessToken, RestClient restClient) : base(accessToken, restClient) { }

        public User Me()
        {
            return JsonConvert.DeserializeObject<User>(RestClient.Execute(PrepareRequest("api/v1/me.json", Method.GET)).Content);

            // This won't work because RestSharp's deserializer sucks and replacing it with a custom one would be bulky.  --Kris
            //return RestClient.Execute<User>(PrepareRequest("api/v1/me.json", Method.GET)).Data;
        }

        public object Karma()
        {
            return JsonConvert.DeserializeObject(RestClient.Execute(PrepareRequest("api/v1/me/karma.json", Method.GET)).Content);
        }

        public object Prefs()
        {
            return JsonConvert.DeserializeObject(RestClient.Execute(PrepareRequest("api/v1/me/prefs.json", Method.GET)).Content);
        }

        public object Trophies()
        {
            return JsonConvert.DeserializeObject(RestClient.Execute(PrepareRequest("api/v1/me/trophies.json", Method.GET)).Content);
        }

        public object Prefs(string where, string after = null, string before = null, int count = 0, int limit = 25, string show = "all",
            bool srDetail = false, bool includeCategories = true)
        {
            RestRequest restRequest = PrepareRequest("prefs/" + where, Method.GET);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);
            restRequest.AddParameter("include_categories", includeCategories);

            return JsonConvert.DeserializeObject(RestClient.Execute(restRequest).Content);
        }
    }
}
