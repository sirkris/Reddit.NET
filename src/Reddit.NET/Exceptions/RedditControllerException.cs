using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditControllerException : Exception
    {
        public RedditControllerException(string message, Exception inner)
            : base(message, inner) { }

        public RedditControllerException(string message)
            : base(message) { }

        public RedditControllerException() { }

        protected RedditControllerException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
