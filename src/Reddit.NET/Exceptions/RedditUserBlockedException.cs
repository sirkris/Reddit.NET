using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditUserBlockedException : Exception
    {
        public RedditUserBlockedException(string message, Exception inner)
            : base(message, inner) { }

        public RedditUserBlockedException(string message)
            : base(message) { }

        public RedditUserBlockedException() { }

        protected RedditUserBlockedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
