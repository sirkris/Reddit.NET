using System;

namespace Reddit.Exceptions
{
    public class RedditSubredditExistsException : Exception
    {
        public RedditSubredditExistsException(string message, Exception inner)
            : base(message, inner) { }

        public RedditSubredditExistsException(string message)
            : base(message) { }

        public RedditSubredditExistsException() { }
    }
}
