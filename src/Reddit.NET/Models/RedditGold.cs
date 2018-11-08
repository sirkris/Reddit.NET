using Newtonsoft.Json;
using RestSharp;

namespace Reddit.NET.Models
{
    public class RedditGold : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public RedditGold(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// Gild.
        /// </summary>
        /// <param name="fullname">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object Gild(string fullname)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/gold/gild/" + fullname, Method.POST));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Give Reddit gold to the specified user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="months">an integer between 1 and 36</param>
        /// <returns>(TODO - Untested)</returns>
        public object Give(string username, int months)
        {
            RestRequest restRequest = PrepareRequest("api/v1/gold/give/" + username, Method.POST);

            restRequest.AddParameter("months", months);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
