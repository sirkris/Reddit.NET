using System;

namespace Reddit.Inputs.Wiki
{
    [Serializable]
    public class WikiPageEditorInput : BaseInput
    {
        /// <summary>
        /// the name of an existing wiki page
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// the name of an existing user
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Input data pertaining to a wiki page editor.
        /// </summary>
        /// <param name="page">the name of an existing wiki page</param>
        /// <param name="username">the name of an existing user</param>
        public WikiPageEditorInput(string page = "", string username = "")
        {
            this.page = page;
            this.username = username;
        }
    }
}
