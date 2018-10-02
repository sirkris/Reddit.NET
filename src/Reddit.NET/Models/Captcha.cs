using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Captcha
    {
        private readonly Account Account;

        public Captcha(Account account)
        {
            this.Account = account;
        }
    }
}
