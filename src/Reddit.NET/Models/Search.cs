using System.Linq;

using Newtonsoft.Json;
using Reddit.Inputs.Search;
using Reddit.Things;
using RestSharp;

namespace Reddit.Models
{
    public class Search : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Search(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// Search links page.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <param name="subreddit">The subreddit being searched</param>
        /// <returns>A listing of things that match the search criteria.</returns>
        public T GetSearch<T>(SearchGetSearchInput searchGetSearchInput, string subreddit = null)
        {
            return SendRequest<T>(Sr(subreddit) + "search", searchGetSearchInput);
        }

        /// <summary>
        /// Search Reddit and return the results as subreddit listings.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <param name="subreddit">The subreddit being searched</param>
        /// <returns>A listing of subreddits that match the search criteria.</returns>
        public SubredditContainer SearchSubreddits(SearchGetSearchInput searchGetSearchInput, string subreddit = null)
        {
            searchGetSearchInput.type = "sr";
            return GetSearch<SubredditContainer>(searchGetSearchInput, subreddit);
        }

        /// <summary>
        /// Search Reddit and return the results as post listings.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <param name="subreddit">The subreddit being searched</param>
        /// <returns>A listing of posts that match the search criteria.</returns>
        public PostContainer SearchPosts(SearchGetSearchInput searchGetSearchInput, string subreddit = null)
        {
            searchGetSearchInput.type = "link";
            return GetSearch<PostContainer>(searchGetSearchInput, subreddit);
        }

        /// <summary>
        /// Search Reddit and return the results as user listings.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <param name="subreddit">The subreddit being searched</param>
        /// <returns>A listing of users that match the search criteria.</returns>
        public UserContainer SearchUsers(SearchGetSearchInput searchGetSearchInput, string subreddit = null)
        {
            searchGetSearchInput.type = "user";
            return GetSearch<UserContainer>(searchGetSearchInput, subreddit);
        }

        /// <summary>
        /// Search Reddit and return the results as mixed listings.
        /// Use this method if you're specifying multiple values for the "type" parameter.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <param name="subreddit">The subreddit being searched</param>
        /// <returns>A listing of things that match the search criteria.</returns>
        public MultiSearchResults MultiSearch(SearchGetSearchInput searchGetSearchInput, string subreddit = null)
        {
            MixedListingContainer mix = GetSearch<MixedListingContainer>(searchGetSearchInput, subreddit);

            MultiSearchResults res = new MultiSearchResults();
            foreach (MixedListingChild mixedListingChild in mix.Data.Children)
            {
                switch (mixedListingChild.Kind)
                {
                    case "t2":
                        res.Users.Add(JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(mixedListingChild.Data)));
                        break;
                    case "t3":
                        res.Posts.Add(JsonConvert.DeserializeObject<Post>(JsonConvert.SerializeObject(mixedListingChild.Data)));
                        break;
                    case "t5":
                        res.Subreddits.Add(JsonConvert.DeserializeObject<Subreddit>(JsonConvert.SerializeObject(mixedListingChild.Data)));
                        break;
                }
            }

            res.First = mix?.Data?.Children?.First()?.Data?["name"]?.ToString();
            res.Last = mix?.Data?.Children?.Last()?.Data?["name"]?.ToString();

            return res;
        }
    }
}
