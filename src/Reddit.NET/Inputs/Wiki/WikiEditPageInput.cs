using System;

namespace Reddit.Inputs.Wiki
{
    [Serializable]
    public class WikiEditPageInput : WikiCreatePageInput
    {
        /// <summary>
        /// the starting point revision for this edit
        /// </summary>
        public string previous { get; set; }

        /// <summary>
        /// Edit a wiki page.
        /// </summary>
        /// <param name="content">The page content</param>
        /// <param name="page">the name of an existing page or a new page to create</param>
        /// <param name="previous">the starting point revision for this edit</param>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        public WikiEditPageInput(string content = "", string page = "", string reason = "", string previous = "")
            : base(content, page, reason)
        {
            this.previous = previous;
        }

        /// <summary>
        /// Edit a wiki page.
        /// </summary>
        /// <param name="wikiCreatePageInput">A valid WikiCreatePageInput instance</param>
        public WikiEditPageInput(WikiCreatePageInput wikiCreatePageInput)
            : base(wikiCreatePageInput.content, wikiCreatePageInput.page, wikiCreatePageInput.reason) { }
    }
}
