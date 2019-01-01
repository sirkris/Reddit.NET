using System;

namespace Reddit.Models.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsIdInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Set the id.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public LinksAndCommentsIdInput(string id = "")
        {
            this.id = id;
        }
    }
}
