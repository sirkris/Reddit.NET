using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditUnprocessableEntityException : Exception
    {
        public RedditUnprocessableEntityException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUnprocessableEntityException(string message)
            : base(message) { }

        public RedditUnprocessableEntityException() { }

        protected RedditUnprocessableEntityException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
