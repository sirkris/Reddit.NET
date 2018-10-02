using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public abstract class BaseModel
    {
        private readonly string AccessToken;
        private readonly Account Account;
        internal abstract RestClient RestClient { get; set; }

        public BaseModel(string accessToken, RestClient restClient)
        {
            this.AccessToken = accessToken;
            this.RestClient = restClient;
        }

        public BaseModel(Account account, RestClient restClient)
        {
            this.Account = account;
            this.AccessToken = account.AccessToken;
            this.RestClient = restClient;
        }

        public RestRequest PrepareRequest(string url, Method method)
        {
            RestRequest restRequest = new RestRequest(url, method);
            restRequest.AddHeader("Authorization", "bearer " + AccessToken);

            return restRequest;
        }
    }
}
