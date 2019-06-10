using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditAlreadyModeratorException : Exception
    {
        public RedditAlreadyModeratorException(string message, Exception inner)
            : base(message, inner) { }

        public RedditAlreadyModeratorException(string message)
            : base(message) { }

        public RedditAlreadyModeratorException() { }

        protected RedditAlreadyModeratorException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
