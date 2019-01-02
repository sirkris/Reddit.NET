using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.Models.Inputs;
using Reddit.Models.Inputs.Listings;
using Reddit.Things;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.Models
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
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Best(CategorizedSrListingInput categorizedSrListingInput)
        {
            return SendRequest<PostContainer>("best", categorizedSrListingInput);
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
        /// If supplied, comment is the ID36 of a comment in the comment tree for article. 
        /// This comment will be the (highlighted) focal point of the returned view and context will be the number of parents shown.
        /// depth is the maximum depth of subtrees in the thread.
        /// limit is the maximum number of comments to return.
        /// See also: /api/morechildren and /api/comment.
        /// </summary>
        /// <param name="article">ID36 of a link</param>
        /// <param name="listingsGetCommentsInput">A valid ListingsGetCommentsInput instance</param>
        /// <param name="subreddit">The subreddit with the article</param>
        /// <returns>A post and comments tree.</returns>
        public List<(PostContainer, CommentContainer)> GetComments(string article, ListingsGetCommentsInput listingsGetCommentsInput, string subreddit = null)
        {
            JArray res = SendRequest<JArray>(Sr(subreddit) + "comments/" + article +
                (!string.IsNullOrWhiteSpace(listingsGetCommentsInput.comment) ? "/_/" + listingsGetCommentsInput.comment : ""), listingsGetCommentsInput);

            // Note - Deserializing directly to the tuple list resulted in null values.  --Kris
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
        /// <param name="listingsGetDuplicatesInput">A valid ListingsGetDuplicatesInput instance</param>
        /// <returns>A list of matching posts.</returns>
        public List<PostContainer> GetDuplicates(string article, ListingsGetDuplicatesInput listingsGetDuplicatesInput)
        {
            return SendRequest<List<PostContainer>>("duplicates/" + article, listingsGetDuplicatesInput);
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="listingsHotInput">A valid ListingsHotInput instance</param>
        /// <param name="subreddit">The subreddit with the listing</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Hot(ListingsHotInput listingsHotInput, string subreddit = null)
        {
            return SendRequest<PostContainer>(Sr(subreddit) + "hot", listingsHotInput);
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <param name="subreddit">The subreddit with the listing</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer New(CategorizedSrListingInput categorizedSrListingInput, string subreddit = null)
        {
            return SendRequest<PostContainer>(Sr(subreddit) + "new", categorizedSrListingInput);
        }

        /// <summary>
        /// The Serendipity button
        /// </summary>
        /// <param name="subreddit">The subreddit from which to retrieve the random listing</param>
        /// <returns>A random listing.</returns>
        public List<PostContainer> Random(string subreddit = null)
        {
            return JsonConvert.DeserializeObject<List<PostContainer>>(ExecuteRequest(Sr(subreddit) + "random"));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <param name="subreddit">The subreddit with the listing</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Rising(CategorizedSrListingInput categorizedSrListingInput, string subreddit = null)
        {
            return SendRequest<PostContainer>(Sr(subreddit) + "rising", categorizedSrListingInput);
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="timedCatSrListingInput">A valid TimedCatSrListingInput instance</param>
        /// <param name="subreddit">The subreddit with the listing</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Top(TimedCatSrListingInput timedCatSrListingInput, string subreddit = null)
        {
            return SendRequest<PostContainer>(Sr(subreddit) + "top", timedCatSrListingInput);
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="timedCatSrListingInput">A valid TimedCatSrListingInput instance</param>
        /// <param name="subreddit">The subreddit with the listing</param>
        /// <returns>A list of Reddit posts.</returns>
        public PostContainer Controversial(TimedCatSrListingInput timedCatSrListingInput, string subreddit = null)
        {
            return SendRequest<PostContainer>(Sr(subreddit) + "controversial", timedCatSrListingInput);
        }
    }
}
