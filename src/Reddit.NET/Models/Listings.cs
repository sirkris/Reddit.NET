using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Listings
    {
        private readonly Account Account;

        public Listings(Account account)
        {
            this.Account = account;
        }
    }
}
