using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Listings : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Listings(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        public object TrendingSubreddits()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/trending_subreddits"));
        }

        public object Best(string after, string before, bool includeCategories, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("best");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object GetByNames(string names)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("by_id/" + names));
        }

        public object GetComments(string article, int context, bool showEdits, bool showMore, string sort, bool threaded, int truncate,
            string subreddit = null, string comment = null, int? depth = null, int? limit = null, bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "comments/" + article);

            restRequest.AddParameter("context", context);
            restRequest.AddParameter("showedits", showEdits);
            restRequest.AddParameter("showmore", showMore);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("threaded", threaded);
            restRequest.AddParameter("truncate", truncate);
            restRequest.AddParameter("comment", comment);
            if (depth.HasValue)
            {
                restRequest.AddParameter("depth", depth.Value);
            }
            if (limit.HasValue)
            {
                restRequest.AddParameter("limit", limit.Value);
            }
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object GetDuplicates(string article, string after, string before, bool crosspostsOnly, string sort, string sr,
            int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("duplicates/" + article);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("crossposts_only", crosspostsOnly);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("sr", sr);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Hot(string g, string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "hot");

            restRequest.AddParameter("g", g);
            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object New(string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "new");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Random(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "/random"));
        }

        public object Rising(string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "rising");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }


        public object Top(string t, string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "top");

            restRequest.AddParameter("t", t);
            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Controversial(string t, string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "controversial");

            restRequest.AddParameter("t", t);
            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
