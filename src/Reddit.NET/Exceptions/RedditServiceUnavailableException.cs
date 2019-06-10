using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditServiceUnavailableException : Exception
    {
        public RedditServiceUnavailableException(string message, Exception inner)
            : base(message, inner) { }

        public RedditServiceUnavailableException(string message)
            : base(message) { }

        public RedditServiceUnavailableException() { }

        protected RedditServiceUnavailableException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
