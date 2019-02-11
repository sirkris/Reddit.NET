using System;

namespace Reddit.Exceptions
{
    public class RedditAlreadySubmittedException : Exception
    {
        public RedditAlreadySubmittedException(string message, Exception inner)
            : base(message, inner) { }

        public RedditAlreadySubmittedException(string message)
            : base(message) { }

        public RedditAlreadySubmittedException() { }
    }
}
