using System;

namespace Reddit.Inputs.Wiki
{
    [Serializable]
    public class WikiPageContentInput : BaseInput
    {
        /// <summary>
        /// a wiki revision ID
        /// </summary>
        public string v { get; set; }

        /// <summary>
        /// a wiki revision ID
        /// </summary>
        public string v2 { get; set; }

        /// <summary>
        /// Return the content of a wiki page.
        /// If v is given, show the wiki page as it was at that version. If both v and v2 are given, show a diff of the two.
        /// </summary>
        /// <param name="v">a wiki revision ID</param>
        /// <param name="v2">a wiki revision ID</param>
        public WikiPageContentInput(string v = "", string v2 = "")
        {
            this.v = v;
            this.v2 = v2;
        }
    }
}
