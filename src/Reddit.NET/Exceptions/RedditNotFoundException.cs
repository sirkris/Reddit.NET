using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Exceptions
{
    public class RedditNotFoundException : Exception
    {
        public RedditNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public RedditNotFoundException(string message)
            : base(message) { }

        public RedditNotFoundException() { }
    }
}
