using System;

namespace Reddit.NET.Exceptions
{
    public class RedditControllerException : Exception
    {
        public RedditControllerException(string message, Exception inner)
            : base(message, inner) { }

        public RedditControllerException(string message)
            : base(message) { }

        public RedditControllerException() { }
    }
}
