using System;

namespace Reddit.NET.Exceptions
{
    public class RedditUserRequiredException : Exception
    {
        public RedditUserRequiredException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUserRequiredException(string message)
            : base(message) { }

        public RedditUserRequiredException() { }
    }
}
