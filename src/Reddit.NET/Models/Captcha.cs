using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Captcha : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Captcha(string refreshToken, string accessToken, RestClient restClient) : base(refreshToken, accessToken, restClient) { }

        public object NeedsCaptcha()
        {
            throw new NotImplementedException("Reddit has deprecated this endpoint.");
            //return JsonConvert.DeserializeObject(ExecuteRequest("api/needs_captcha"));
        }
    }
}
