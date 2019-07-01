using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditForbiddenException : Exception
    {
        public RedditForbiddenException(string message, Exception inner)
            : base(message, inner) { }

        public RedditForbiddenException(string message)
            : base(message) { }

        public RedditForbiddenException() { }

        protected RedditForbiddenException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
