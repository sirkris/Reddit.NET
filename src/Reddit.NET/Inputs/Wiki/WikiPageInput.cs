using System;

namespace Reddit.Inputs.Wiki
{
    [Serializable]
    public class WikiPageInput : BaseInput
    {
        /// <summary>
        /// the name of an existing wiki page
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// Set input data pertaining to a wiki page.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        public WikiPageInput(string page = "")
        {
            this.page = page;
        }
    }
}
