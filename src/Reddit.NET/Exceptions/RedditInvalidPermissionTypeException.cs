using System;

namespace Reddit.NET.Exceptions
{
    public class RedditInvalidPermissionTypeException : Exception
    {
        public RedditInvalidPermissionTypeException(string message, Exception inner)
            : base(message, inner) { }

        public RedditInvalidPermissionTypeException(string message)
            : base(message) { }

        public RedditInvalidPermissionTypeException() { }
    }
}
