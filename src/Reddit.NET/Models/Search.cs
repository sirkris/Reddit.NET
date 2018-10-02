using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Search
    {
        private readonly Account Account;

        public Search(Account account)
        {
            this.Account = account;
        }
    }
}
