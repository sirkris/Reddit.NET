using System;

namespace Reddit.NET.Exceptions
{
    public class RedditBadRequestException : Exception
    {
        public RedditBadRequestException(string message, Exception inner)
            : base(message, inner) { }

        public RedditBadRequestException(string message)
            : base(message) { }

        public RedditBadRequestException() { }
    }
}
