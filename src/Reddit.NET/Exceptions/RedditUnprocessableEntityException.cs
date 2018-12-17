using System;

namespace Reddit.NET.Exceptions
{
    public class RedditUnprocessableEntityException : Exception
    {
        public RedditUnprocessableEntityException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUnprocessableEntityException(string message)
            : base(message) { }

        public RedditUnprocessableEntityException() { }
    }
}
