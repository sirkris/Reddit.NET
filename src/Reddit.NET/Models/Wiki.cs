using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Wiki
    {
        private readonly Account Account;

        public Wiki(Account account)
        {
            this.Account = account;
        }
    }
}
