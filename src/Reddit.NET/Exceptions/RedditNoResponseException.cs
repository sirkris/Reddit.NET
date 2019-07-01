using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditNoResponseException : Exception
    {
        public RedditNoResponseException(string message, Exception inner)
            : base(message, inner) { }

        public RedditNoResponseException(string message)
            : base(message) { }

        public RedditNoResponseException() { }

        protected RedditNoResponseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
