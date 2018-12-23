using RestSharp;
using System;

namespace Reddit.NET.Models
{
    public class Captcha : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Captcha(string appId, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, refreshToken, accessToken, ref restClient, deviceId) { }

        public object NeedsCaptcha()
        {
            throw new NotImplementedException("Reddit has deprecated this endpoint.");
            //return JsonConvert.DeserializeObject(ExecuteRequest("api/needs_captcha"));
        }
    }
}
