using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsSubredditStylesheetInput : APITypeInput
    {
        /// <summary>
        /// one of (save, preview)
        /// </summary>
        public string op { get; set; }

        /// <summary>
        /// a string up to 256 characters long, consisting of printable characters
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// the new stylesheet content
        /// </summary>
        public string stylesheet_contents { get; set; }

        /// <summary>
        /// Update a subreddit's stylesheet.
        /// op should be save to update the contents of the stylesheet.
        /// </summary>
        /// <param name="stylesheetContents">the new stylesheet content</param>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="op">one of (save, preview)</param>
        public SubredditsSubredditStylesheetInput(string stylesheetContents = "", string reason = "", string op = "save")
            : base()
        {
            stylesheet_contents = stylesheetContents;
            this.reason = reason;
            this.op = op;
        }
    }
}
