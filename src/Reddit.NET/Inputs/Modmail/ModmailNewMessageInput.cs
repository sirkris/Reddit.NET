namespace Reddit.Inputs.Modmail
{
    public class ModmailNewMessageInput : ModmailMessageBodyInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool isInternal { get; set; }

        /// <summary>
        /// Creates a new message for a particular conversation.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="isInternal">boolean value</param>
        public ModmailNewMessageInput(string body = "", bool isAuthorHidden = false, bool isInternal = false)
            : base(body, isAuthorHidden)
        {
            this.isInternal = isInternal;
        }
    }
}
