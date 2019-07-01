using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditGatewayTimeoutException : Exception
    {
        public RedditGatewayTimeoutException(string message, Exception inner)
            : base(message, inner) { }

        public RedditGatewayTimeoutException(string message)
            : base(message) { }

        public RedditGatewayTimeoutException() { }

        protected RedditGatewayTimeoutException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
