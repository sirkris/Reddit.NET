using System;

namespace Reddit.Exceptions
{
    public class RedditInternalServerErrorException : Exception
    {
        public RedditInternalServerErrorException(string message, Exception inner)
            : base(message, inner) { }

        public RedditInternalServerErrorException(string message)
            : base(message) { }

        public RedditInternalServerErrorException() { }
    }
}
