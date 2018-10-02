using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Users
    {
        private readonly Account Account;

        public Users(Account account)
        {
            this.Account = account;
        }
    }
}
