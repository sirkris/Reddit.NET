using System;

namespace Reddit.Exceptions
{
    public class RedditTooLongException : Exception
    {
        public RedditTooLongException(string message, Exception inner)
            : base(message, inner) { }

        public RedditTooLongException(string message)
            : base(message) { }

        public RedditTooLongException() { }
    }
}
