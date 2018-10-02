using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Flair
    {
        private readonly Account Account;

        public Flair(Account account)
        {
            this.Account = account;
        }
    }
}
