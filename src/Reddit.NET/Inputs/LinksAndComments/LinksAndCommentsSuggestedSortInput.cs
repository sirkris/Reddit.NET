using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsSuggestedSortInput : APITypeInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// one of (confidence, top, new, controversial, old, random, qa, live, blank)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// Set sort.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live, blank)</param>
        public LinksAndCommentsSuggestedSortInput(string id = "", string sort = "new")
            : base()
        {
            this.id = id;
            this.sort = sort;
        }
    }
}
