using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditInternalServerErrorException : Exception
    {
        public RedditInternalServerErrorException(string message, Exception inner)
            : base(message, inner) { }

        public RedditInternalServerErrorException(string message)
            : base(message) { }

        public RedditInternalServerErrorException() { }

        protected RedditInternalServerErrorException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
