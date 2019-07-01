using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditBadGatewayException : Exception
    {
        public RedditBadGatewayException(string message, Exception inner)
            : base(message, inner) { }

        public RedditBadGatewayException(string message)
            : base(message) { }

        public RedditBadGatewayException() { }

        protected RedditBadGatewayException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
