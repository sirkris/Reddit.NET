using System;

namespace Reddit.Exceptions
{
    public class RedditUserBlockedException : Exception
    {
        public RedditUserBlockedException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUserBlockedException(string message)
            : base(message) { }

        public RedditUserBlockedException() { }
    }
}
