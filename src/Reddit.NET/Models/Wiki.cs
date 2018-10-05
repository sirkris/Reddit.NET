using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Wiki : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Wiki(string refreshToken, string accessToken, RestClient restClient) : base(refreshToken, accessToken, restClient) { }
    }
}
