using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsSaveInput : LinksAndCommentsIdInput
    {
        /// <summary>
        /// a category name
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// Save a link or comment.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="category">a category name</param>
        public LinksAndCommentsSaveInput(string id = "", string category = "")
            : base(id)
        {
            this.category = category;
        }
    }
}
