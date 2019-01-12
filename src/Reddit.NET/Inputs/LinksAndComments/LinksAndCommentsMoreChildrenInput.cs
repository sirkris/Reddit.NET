using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsMoreChildrenInput : APITypeInput
    {
        /// <summary>
        /// a comma-delimited list of comment ID36s
        /// </summary>
        public string children { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool limit_children { get; set; }

        /// <summary>
        /// fullname of a link
        /// </summary>
        public string link_id { get; set; }

        /// <summary>
        /// one of (confidence, top, new, controversial, old, random, qa, live)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// (optional) id of the associated MoreChildren object
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Retrieve additional comments omitted from a base comment tree.
        /// When a comment tree is rendered, the most relevant comments are selected for display first.
        /// Remaining comments are stubbed out with "MoreComments" links. 
        /// This API call is used to retrieve the additional comments represented by those stubs, up to 100 at a time.
        /// The two core parameters required are link and children. link is the fullname of the link whose comments are being fetched. 
        /// children is a comma-delimited list of comment ID36s that need to be fetched.
        /// If id is passed, it should be the ID of the MoreComments object this call is replacing. This is needed only for the HTML UI's purposes and is optional otherwise.
        /// NOTE: you may only make one request at a time to this API endpoint. Higher concurrency will result in an error being returned.
        /// If limit_children is True, only return the children requested.
        /// </summary>
        /// <param name="children">a comma-delimited list of comment ID36s</param>
        /// <param name="limitChildren">boolean value</param>
        /// <param name="linkId">fullname of a link</param>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live)</param>
        /// <param name="id">(optional) id of the associated MoreChildren object</param>
        public LinksAndCommentsMoreChildrenInput(string children = "", bool limitChildren = false, string linkId = "", string sort = "new", string id = "")
            : base()
        {
            this.children = children;
            limit_children = limitChildren;
            link_id = linkId;
            this.sort = sort;
            this.id = id;
        }
    }
}
