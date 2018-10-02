using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class PrivateMessages : BaseModel
    {
        private readonly Account Account;
        internal override RestClient RestClient { get; set; }

        public PrivateMessages(Account account, RestClient restClient) : base(account, restClient) { }
    }
}
