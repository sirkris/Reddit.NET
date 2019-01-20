using System;

namespace Reddit.Inputs.Modmail
{
    [Serializable]
    public class ModmailNewConversationInput : ModmailMessageBodyInput
    {
        /// <summary>
        /// subreddit name
        /// </summary>
        public string srName { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// Modmail conversation recipient username
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// Creates a new conversation for a particular SR.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="srName">subreddit name</param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="to">Modmail conversation recipient username</param>
        public ModmailNewConversationInput(string body = "", bool isAuthorHidden = false, string srName = "", string subject = "", string to = "")
            : base(body, isAuthorHidden)
        {
            this.srName = srName;
            this.subject = subject;
            this.to = to;
        }
    }
}
