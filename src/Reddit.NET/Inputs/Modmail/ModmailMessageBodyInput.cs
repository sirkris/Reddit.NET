using System;

namespace Reddit.Inputs.Modmail
{
    [Serializable]
    public class ModmailMessageBodyInput : BaseInput
    {
        /// <summary>
        /// raw markdown text
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool isAuthorHidden { get; set; }

        /// <summary>
        /// Set values for a modmail conversation body.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        /// <param name="isAuthorHidden">boolean value</param>
        public ModmailMessageBodyInput(string body = "", bool isAuthorHidden = false)
        {
            this.body = body;
            this.isAuthorHidden = isAuthorHidden;
        }
    }
}
