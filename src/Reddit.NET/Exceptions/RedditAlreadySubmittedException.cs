using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditAlreadySubmittedException : Exception
    {
        public RedditAlreadySubmittedException(string message, Exception inner)
            : base(message, inner) { }

        public RedditAlreadySubmittedException(string message)
            : base(message) { }

        public RedditAlreadySubmittedException() { }

        protected RedditAlreadySubmittedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
