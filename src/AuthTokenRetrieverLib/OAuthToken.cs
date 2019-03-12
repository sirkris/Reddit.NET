using Newtonsoft.Json;
using System;

namespace Reddit.AuthTokenRetriever
{
    [Serializable]
    public class OAuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken;

        [JsonProperty("refresh_token")]
        public string RefreshToken;

        public OAuthToken(string accessToken = null, string refreshToken = null)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
