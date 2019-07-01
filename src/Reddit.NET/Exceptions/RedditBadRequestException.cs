using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditBadRequestException : Exception
    {
        public RedditBadRequestException(string message, Exception inner)
            : base(message, inner) { }

        public RedditBadRequestException(string message)
            : base(message) { }

        public RedditBadRequestException() { }

        protected RedditBadRequestException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
