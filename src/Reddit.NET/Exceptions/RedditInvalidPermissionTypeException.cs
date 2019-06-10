using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditInvalidPermissionTypeException : Exception
    {
        public RedditInvalidPermissionTypeException(string message, Exception inner)
            : base(message, inner) { }

        public RedditInvalidPermissionTypeException(string message)
            : base(message) { }

        public RedditInvalidPermissionTypeException() { }

        protected RedditInvalidPermissionTypeException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
