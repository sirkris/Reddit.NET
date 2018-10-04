using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Account : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Account(string accessToken, RestClient restClient) : base(accessToken, restClient) { }

        public User Me()
        {
            return JsonConvert.DeserializeObject<User>(RestClient.Execute(PrepareRequest("api/v1/me.json", Method.GET)).Content);

            // This won't work because RestSharp's deserializer sucks and replacing it with a custom one would be bulky.  --Kris
            //return RestClient.Execute<User>(PrepareRequest("api/v1/me.json", Method.GET)).Data;
        }
    }
}
