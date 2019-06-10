using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditUnauthorizedException : Exception
    {
        public RedditUnauthorizedException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUnauthorizedException(string message)
            : base(message) { }

        public RedditUnauthorizedException() { }

        protected RedditUnauthorizedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
