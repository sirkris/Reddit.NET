using Reddit.Models.EventArgs;
using System;

namespace Reddit.Models
{
    /// <summary>
    /// Makes the Reddit OAuth credentials accessible to the calling app.
    /// Aside from populating these values for that reason, this class is not used by the library, itself.
    /// Each model class stores these credentials internally.
    /// </summary>
    [Serializable]
    public class OAuthCredentials
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string DeviceID { get; set; }

        public event EventHandler<TokenUpdateEventArgs> TokenUpdated;

        public OAuthCredentials(string appId = null, string appSecret = null, string refreshToken = null, string accessToken = null, string deviceId = null)
        {
            AppID = appId;
            AppSecret = appSecret;
            RefreshToken = refreshToken;
            AccessToken = accessToken;
            DeviceID = deviceId;
        }

        public void UpdateAccessToken(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
