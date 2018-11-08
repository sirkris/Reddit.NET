using System;

namespace Reddit.NET.Exceptions
{
    public class RedditGatewayTimeoutException : Exception
    {
        public RedditGatewayTimeoutException(string message, Exception inner)
            : base(message, inner) { }

        public RedditGatewayTimeoutException(string message)
            : base(message) { }

        public RedditGatewayTimeoutException() { }
    }
}
