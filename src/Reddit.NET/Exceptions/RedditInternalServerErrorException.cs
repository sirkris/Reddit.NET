using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Exceptions
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
