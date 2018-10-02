using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Flair : BaseModel
    {
        private readonly Account Account;
        internal override RestClient RestClient { get; set; }

        public Flair(Account account, RestClient restClient) : base(account, restClient) { }
    }
}
