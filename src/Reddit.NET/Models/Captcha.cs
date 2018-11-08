using RestSharp;
using System;

namespace Reddit.NET.Models
{
    public class Captcha : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Captcha(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        public object NeedsCaptcha()
        {
            throw new NotImplementedException("Reddit has deprecated this endpoint.");
            //return JsonConvert.DeserializeObject(ExecuteRequest("api/needs_captcha"));
        }
    }
}
