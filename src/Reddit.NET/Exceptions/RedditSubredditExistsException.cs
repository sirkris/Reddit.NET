using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditSubredditExistsException : Exception
    {
        public RedditSubredditExistsException(string message, Exception inner)
            : base(message, inner) { }

        public RedditSubredditExistsException(string message)
            : base(message) { }

        public RedditSubredditExistsException() { }

        protected RedditSubredditExistsException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
