using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditUserRequiredException : Exception
    {
        public RedditUserRequiredException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUserRequiredException(string message)
            : base(message) { }

        public RedditUserRequiredException() { }

        protected RedditUserRequiredException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
