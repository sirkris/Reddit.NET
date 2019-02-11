using System;

namespace Reddit.Exceptions
{
    public class RedditMonitoringException : Exception
    {
        public RedditMonitoringException(string message, Exception inner)
            : base(message, inner) { }

        public RedditMonitoringException(string message)
            : base(message) { }

        public RedditMonitoringException() { }
    }
}
