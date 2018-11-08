using System;

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
