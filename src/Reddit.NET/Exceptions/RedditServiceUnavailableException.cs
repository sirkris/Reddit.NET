using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Exceptions
{
    public class RedditServiceUnavailableException : Exception
    {
        public RedditServiceUnavailableException(string message, Exception inner)
            : base(message, inner) { }

        public RedditServiceUnavailableException(string message)
            : base(message) { }

        public RedditServiceUnavailableException() { }
    }
}
