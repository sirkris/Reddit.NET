using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditRateLimitException : Exception
    {
        public RedditRateLimitException(string message, Exception inner)
            : base(message, inner) { }

        public RedditRateLimitException(string message)
            : base(message) { }

        public RedditRateLimitException() { }

        protected RedditRateLimitException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
