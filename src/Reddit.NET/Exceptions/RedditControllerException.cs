using System;

namespace Reddit.Exceptions
{
    public class RedditCoordinatorException : Exception
    {
        public RedditCoordinatorException(string message, Exception inner)
            : base(message, inner) { }

        public RedditCoordinatorException(string message)
            : base(message) { }

        public RedditCoordinatorException() { }
    }
}
