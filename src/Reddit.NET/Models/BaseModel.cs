using Reddit.Models.Internal;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Reddit.Models
{
    public abstract class BaseModel : Request
    {
        internal override string AppId { get; set; }
        internal override string AppSecret { get; set; }
        internal override string AccessToken { get; set; }
        internal override string RefreshToken { get; set; }
        internal override string DeviceId { get; set; }

        internal override List<DateTime> Requests { get; set; }

        public BaseModel(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        public string Sr(string subreddit)
        {
            return (!string.IsNullOrWhiteSpace(subreddit) ? "/r/" + subreddit + "/" : "");
        }
    }
}
