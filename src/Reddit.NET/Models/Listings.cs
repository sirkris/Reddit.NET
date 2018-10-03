using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Listings : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Listings(string accessToken, RestClient restClient) : base(accessToken, restClient) { }
    }
}
