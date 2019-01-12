using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairLinkInput : FlairNameInput
    {
        /// <summary>
        /// a fullname of a link
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// Specify a link and name.
        /// </summary>
        /// <param name="link">a fullname of a link</param>
        /// <param name="name">a user by name</param>
        public FlairLinkInput(string link = "", string name = "")
        {
            this.link = link;
            this.name = name;
        }
    }
}
