using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Moderation
    {
        private readonly Account Account;

        public Moderation(Account account)
        {
            this.Account = account;
        }
    }
}
