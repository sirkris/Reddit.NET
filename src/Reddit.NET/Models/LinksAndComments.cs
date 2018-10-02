using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class LinksAndComments
    {
        private readonly Account Account;

        public LinksAndComments(Account account)
        {
            this.Account = account;
        }
    }
}
