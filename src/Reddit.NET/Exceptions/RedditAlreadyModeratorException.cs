using System;

namespace Reddit.Exceptions
{
    public class RedditAlreadyModeratorException : Exception
    {
        public RedditAlreadyModeratorException(string message, Exception inner)
            : base(message, inner) { }

        public RedditAlreadyModeratorException(string message)
            : base(message) { }

        public RedditAlreadyModeratorException() { }
    }
}
