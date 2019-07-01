using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditNotFoundException : Exception
    {
        public RedditNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public RedditNotFoundException(string message)
            : base(message) { }

        public RedditNotFoundException() { }

        protected RedditNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
