using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Multis : BaseModel
    {
        private readonly Account Account;
        internal override RestClient RestClient { get; set; }

        public Multis(Account account, RestClient restClient) : base(account, restClient) { }
    }
}
