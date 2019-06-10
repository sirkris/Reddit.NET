using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditInvalidOptionException : Exception
    {
        public RedditInvalidOptionException(string message, Exception inner)
            : base(message, inner) { }

        public RedditInvalidOptionException(string message)
            : base(message) { }

        public RedditInvalidOptionException() { }

        protected RedditInvalidOptionException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
