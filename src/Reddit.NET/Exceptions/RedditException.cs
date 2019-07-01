using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditException : Exception
    {
        public RedditException(string message, Exception inner)
            : base(message, inner) { }

        public RedditException(string message)
            : base(message) { }

        public RedditException() { }

        protected RedditException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
