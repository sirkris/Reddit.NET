using System;

namespace Reddit.Exceptions
{
    public class RedditForbiddenException : Exception
    {
        public RedditForbiddenException(string message, Exception inner)
            : base(message, inner) { }

        public RedditForbiddenException(string message)
            : base(message) { }

        public RedditForbiddenException() { }
    }
}
