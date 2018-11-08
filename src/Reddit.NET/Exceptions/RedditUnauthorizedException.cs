using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Exceptions
{
    public class RedditUnauthorizedException : Exception
    {
        public RedditUnauthorizedException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUnauthorizedException(string message)
            : base(message) { }

        public RedditUnauthorizedException() { }
    }
}
