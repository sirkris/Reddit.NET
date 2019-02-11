using System;

namespace Reddit.Exceptions
{
    public class RedditConflictException : Exception
    {
        public RedditConflictException(string message, Exception inner)
            : base(message, inner) { }

        public RedditConflictException(string message)
            : base(message) { }

        public RedditConflictException() { }
    }
}
