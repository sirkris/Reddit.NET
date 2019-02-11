using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsStateInput : APITypeInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool state { get; set; }

        /// <summary>
        /// Set the id and state.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="state">boolean value</param>
        public LinksAndCommentsStateInput(string id = "", bool state = false) 
            : base()
        {
            this.id = id;
            this.state = state;
        }
    }
}
