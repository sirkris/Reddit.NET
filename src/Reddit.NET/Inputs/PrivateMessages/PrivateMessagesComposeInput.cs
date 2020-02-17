using System;

namespace Reddit.Inputs.PrivateMessages
{
    [Serializable]
    public class PrivateMessagesComposeInput : APITypeInput
    {
        /// <summary>
        /// subreddit name
        /// </summary>
        public string from_sr { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// the name of an existing user (or subreddit for modmail)
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// Handles message composition under /message/compose.
        /// </summary>
        /// <param name="fromSr">subreddit name</param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="to">the name of an existing user</param>
        public PrivateMessagesComposeInput(string fromSr = "", string subject = "", string text = "", string to = "")
            : base()
        {
            from_sr = fromSr;
            this.subject = subject;
            this.text = text;
            this.to = to;
        }
    }
}
