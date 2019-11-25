using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.Exceptions;
using Reddit.Inputs.Listings;
using Reddit.Things;
using RestSharp;

namespace Reddit.Models.Internal
{
    public class Common : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Common(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId) { }

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
        public CommentContainer GetComments(string article, ListingsGetCommentsInput listingsGetCommentsInput, string subreddit = null)
        {
            JToken res = SendRequest<JToken>(Sr(subreddit) + "comments/" + article +
                (!string.IsNullOrWhiteSpace(listingsGetCommentsInput.comment) ? "/_/" + listingsGetCommentsInput.comment : ""), listingsGetCommentsInput);

            if (string.IsNullOrEmpty(article))
            {
                return JsonConvert.DeserializeObject<CommentContainer>(JsonConvert.SerializeObject(res));
            }

            return JsonConvert.DeserializeObject<CommentContainer>(JsonConvert.SerializeObject(res[1]));
        }

        /// <summary>
        /// Get information on a given link via the comments endpoint.
        /// </summary>
        /// <param name="article">ID36 of a link</param>
        /// <param name="listingsGetCommentsInput">A valid ListingsGetCommentsInput instance</param>
        /// <param name="subreddit">The subreddit with the article</param>
        /// <returns>A post and comments tree.</returns>
        public PostContainer GetPost(string article, ListingsGetCommentsInput listingsGetCommentsInput, string subreddit = null)
        {
            if (string.IsNullOrEmpty(article))
            {
                throw new RedditException("You must specify a valid article link ID36.");
            }

            JToken res = SendRequest<JToken>(Sr(subreddit) + "comments/" + article +
                (!string.IsNullOrWhiteSpace(listingsGetCommentsInput.comment) ? "/_/" + listingsGetCommentsInput.comment : ""), listingsGetCommentsInput);

            return JsonConvert.DeserializeObject<PostContainer>(JsonConvert.SerializeObject(res[0]));
        }
    }
}
