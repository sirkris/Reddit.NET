using System;

namespace Reddit.Models.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsSuggestedSortInput : LinksAndCommentsIdInput
    {
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
        {
            this.id = id;
            this.sort = sort;
        }
    }
}
