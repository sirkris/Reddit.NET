using System;

namespace Reddit.Inputs.Wiki
{
    [Serializable]
    public class WikiPageRevisionInput : WikiPageInput
    {
        /// <summary>
        /// a wiki revision ID
        /// </summary>
        public string revision { get; set; }

        /// <summary>
        /// Set input data pertaining to a wiki page/revision.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="revision">a wiki revision ID</param>
        public WikiPageRevisionInput(string page = "", string revision = "")
            : base(page)
        {
            this.revision = revision;
        }
    }
}
