using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Account : BaseModel
    {
        private readonly string AccessToken;
        internal override RestClient RestClient { get; set; }

        public Account(string accessToken, RestClient restClient) : base(accessToken, restClient) { }

        public User Me()
        {
            return RestClient.Execute<User>(PrepareRequest("api/me.json", Method.GET)).Data;
        }
    }
}
