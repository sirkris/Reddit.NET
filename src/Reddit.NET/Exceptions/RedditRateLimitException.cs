using System;

namespace Reddit.Exceptions
{
    public class RedditRateLimitException : Exception
    {
        public RedditRateLimitException(string message, Exception inner)
            : base(message, inner) { }

        public RedditRateLimitException(string message)
            : base(message) { }

        public RedditRateLimitException() { }
    }
}
