using System;

namespace Reddit.Exceptions
{
    public class RedditInvalidOptionException : Exception
    {
        public RedditInvalidOptionException(string message, Exception inner)
            : base(message, inner) { }

        public RedditInvalidOptionException(string message)
            : base(message) { }

        public RedditInvalidOptionException() { }
    }
}
