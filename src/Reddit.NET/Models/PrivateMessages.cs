using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class PrivateMessages
    {
        private readonly Account Account;

        public PrivateMessages(Account account)
        {
            this.Account = account;
        }
    }
}
