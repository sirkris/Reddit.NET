using System;

namespace Reddit.Exceptions
{
    public class RedditNoResponseException : Exception
    {
        public RedditNoResponseException(string message, Exception inner)
            : base(message, inner) { }

        public RedditNoResponseException(string message)
            : base(message) { }

        public RedditNoResponseException() { }
    }
}
