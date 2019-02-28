using Reddit.Inputs.Search;
using Reddit.Things;
using RestSharp;

namespace Reddit.Models
{
    public class Search : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Search(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId) { }

        /// <summary>
        /// Search links page.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <param name="subreddit">The subreddit being searched</param>
        /// <returns>A listing of posts that match the search criteria.</returns>
        public PostContainer GetSearch(SearchGetSearchInput searchGetSearchInput, string subreddit = null)
        {
            return SendRequest<PostContainer>(Sr(subreddit) + "search", searchGetSearchInput);
        }
    }
}
