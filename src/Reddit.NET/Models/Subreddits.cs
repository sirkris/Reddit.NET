using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Subreddits
    {
        private readonly Account Account;

        public Subreddits(Account account)
        {
            this.Account = account;
        }
    }
}
