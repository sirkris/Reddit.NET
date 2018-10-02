using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Account
    {
        private readonly string AccessToken;

        public Account(string accessToken)
        {
            this.AccessToken = accessToken;
        }
    }
}
