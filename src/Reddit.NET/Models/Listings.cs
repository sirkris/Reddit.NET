using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.NET.Models.Structures;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.NET.Models
{
    public class Listings : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Listings(string appId, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, refreshToken, accessToken, ref restClient, deviceId) { }

        // TODO - API returns 400.  No idea why.  --Kris
        /// <summary>
        /// Return a list of trending subreddits, link to the comment in r/trendingsubreddits, and the comment count of that link.
        /// </summary>
        /// <returns>(TODO - Untested)</returns>
        public object TrendingSubreddits()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/trending_subreddits"));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Best(string after, string before, bool includeCategories, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("best");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject<PostContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get a listing of links by fullname.
        /// names is a list of fullnames for links separated by commas or spaces.
        /// </summary>
        /// <param name="names">A comma-separated list of link fullnames</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer GetByNames(string names)
        {
            return JsonConvert.DeserializeObject<PostContainer>(ExecuteRequest("by_id/" + names));
        }

        /// <summary>
        /// Get the comment tree for a given Link article.
        /// If supplied, comment is the ID36 of a comment in the comment tree for article. This comment will be the (highlighted) focal point of the returned view and context will be the number of parents shown.
        /// depth is the maximum depth of subtrees in the thread.
        /// limit is the maximum number of comments to return.
        /// See also: /api/morechildren and /api/comment.
        /// </summary>
        /// <param name="article">ID36 of a link</param>
        /// <param name="context">an integer between 0 and 8</param>
        /// <param name="showEdits">boolean value</param>
        /// <param name="showMore">boolean value</param>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live)</param>
        /// <param name="threaded">boolean value</param>
        /// <param name="truncate">an integer between 0 and 50</param>
        /// <param name="subreddit">The subreddit with the article.</param>
        /// <param name="comment">(optional) ID36 of a comment</param>
        /// <param name="depth">(optional) an integer</param>
        /// <param name="limit">(optional) an integer</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A post and comments tree.</returns>
        public List<(PostContainer, CommentContainer)> GetComments(string article, int context, bool showEdits, bool showMore, string sort, bool threaded, int truncate,
            string subreddit = null, string comment = null, int? depth = null, int? limit = null, bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "comments/" + article + (!string.IsNullOrWhiteSpace(comment) ? "/_/" + comment : ""));

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

            // Note - Deserializing directly to the tuple list resulted in null values.  --Kris
            JArray res = JsonConvert.DeserializeObject<JArray>(ExecuteRequest(restRequest));

            return new List<(PostContainer, CommentContainer)>
            {
                (JsonConvert.DeserializeObject<PostContainer>(JsonConvert.SerializeObject(res[0])),
                JsonConvert.DeserializeObject<CommentContainer>(JsonConvert.SerializeObject(res[1])))
            };
        }

        /// <summary>
        /// Return a list of other submissions of the same URL
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="article">The base 36 ID of a Link</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="crosspostsOnly">boolean value</param>
        /// <param name="sort">one of (num_comments, new)</param>
        /// <param name="sr">subreddit name</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of matching posts.</returns>
        public List<PostContainer> GetDuplicates(string article, string after, string before, bool crosspostsOnly, string sort, string sr,
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

            return JsonConvert.DeserializeObject<List<PostContainer>>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="g">one of (GLOBAL, US, AR, AU, BG, CA, CL, CO, HR, CZ, FI, GR, HU, IS, IN, IE, JP, MY, MX, NZ, PH, PL, PT, PR, RO, RS, SG, SE, TW, TH, TR, GB, US_WA, 
        /// US_DE, US_DC, US_WI, US_WV, US_HI, US_FL, US_WY, US_NH, US_NJ, US_NM, US_TX, US_LA, US_NC, US_ND, US_NE, US_TN, US_NY, US_PA, US_CA, US_NV, US_VA, US_CO, US_AK, 
        /// US_AL, US_AR, US_VT, US_IL, US_GA, US_IN, US_IA, US_OK, US_AZ, US_ID, US_CT, US_ME, US_MD, US_MA, US_OH, US_UT, US_MO, US_MN, US_MI, US_RI, US_KS, US_MT, US_MS, 
        /// US_SC, US_KY, US_OR, US_SD)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="subreddit">The subreddit with the listing.</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Hot(string g, string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
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

            return JsonConvert.DeserializeObject<PostContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="subreddit">The subreddit with the listing.</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer New(string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
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

            return JsonConvert.DeserializeObject<PostContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// The Serendipity button
        /// </summary>
        /// <param name="subreddit">The subreddit from which to retrieve the random listing</param>
        /// <returns>A random listing.</returns>
        public List<PostContainer> Random(string subreddit = null)
        {
            string blah = ExecuteRequest(Sr(subreddit) + "random");
            return JsonConvert.DeserializeObject<List<PostContainer>>(ExecuteRequest(Sr(subreddit) + "random"));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="subreddit">The subreddit with the listing.</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Rising(string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
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

            return JsonConvert.DeserializeObject<PostContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="subreddit">The subreddit with the listing.</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Top(string t, string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
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

            return JsonConvert.DeserializeObject<PostContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="subreddit">The subreddit with the listing.</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Controversial(string t, string after, string before, bool includeCategories, string subreddit = null, int count = 0, int limit = 25,
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

            return JsonConvert.DeserializeObject<PostContainer>(ExecuteRequest(restRequest));
        }
    }
}
