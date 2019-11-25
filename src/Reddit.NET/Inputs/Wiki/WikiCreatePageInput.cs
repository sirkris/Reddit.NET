using System;

namespace Reddit.Inputs.Wiki
{
    [Serializable]
    public class WikiCreatePageInput : BaseInput
    {
        /// <summary>
        /// The page content
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// the name of an existing page or a new page to create
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// a string up to 256 characters long, consisting of printable characters
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// Create a wiki page.
        /// </summary>
        /// <param name="content">The page content</param>
        /// <param name="page">the name of the new page being created</param>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        public WikiCreatePageInput(string content = "", string page = "", string reason = "")
        {
            Import(content, page, reason);
        }

        /// <summary>
        /// Create a wiki page.
        /// </summary>
        /// <param name="wikiEditPageInput">A valid WikiEditPageInput instance</param>
        public WikiCreatePageInput(WikiEditPageInput wikiEditPageInput)
        {
            Import(wikiEditPageInput.content, wikiEditPageInput.page, wikiEditPageInput.reason);
        }

        private void Import(string content = "", string page = "", string reason = "")
        {
            this.content = content;
            this.page = page;
            this.reason = reason;
        }
    }
}
