using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    [Serializable]
    public class RedditMonitoringException : Exception
    {
        public RedditMonitoringException(string message, Exception inner)
            : base(message, inner) { }

        public RedditMonitoringException(string message)
            : base(message) { }

        public RedditMonitoringException() { }

        protected RedditMonitoringException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
