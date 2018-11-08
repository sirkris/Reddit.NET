using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Exceptions
{
    public class RedditBadGatewayException : Exception
    {
        public RedditBadGatewayException(string message, Exception inner)
            : base(message, inner) { }

        public RedditBadGatewayException(string message)
            : base(message) { }

        public RedditBadGatewayException() { }
    }
}
