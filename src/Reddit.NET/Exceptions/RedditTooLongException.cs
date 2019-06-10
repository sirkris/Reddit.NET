using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditTooLongException : Exception
    {
        public RedditTooLongException(string message, Exception inner)
            : base(message, inner) { }

        public RedditTooLongException(string message)
            : base(message) { }

        public RedditTooLongException() { }

        protected RedditTooLongException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
