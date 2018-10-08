using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Search : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Search(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        public object GetSearch(string after, string before, string category, bool includeFacets, string q, bool restrictSr, string sort, string t,
            string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false, string type = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "search");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("category", category);
            restRequest.AddParameter("include_facets", includeFacets);
            restRequest.AddParameter("restrict_sr", restrictSr);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("t", t);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);
            restRequest.AddParameter("type", type);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
