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

        // TODO - Needs testing.
        /// <summary>
        /// Search links page.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="category">a string no longer than 5 characters</param>
        /// <param name="includeFacets">boolean value</param>
        /// <param name="q">a string no longer than 512 characters</param>
        /// <param name="restrictSr">boolean value</param>
        /// <param name="sort">one of (relevance, hot, top, new, comments)</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="subreddit">The subreddit being searched</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="type">(optional) comma-delimited list of result types (sr, link, user)</param>
        /// <returns>(TODO - Untested)</returns>
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
