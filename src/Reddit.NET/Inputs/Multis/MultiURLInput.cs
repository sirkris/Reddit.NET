using System;

namespace Reddit.Inputs.Multis
{
    [Serializable]
    public class MultiURLInput : BaseInput
    {
        /// <summary>
        /// a string no longer than 50 characters
        /// </summary>
        public string display_name { get; set; }

        /// <summary>
        /// multireddit url path
        /// </summary>
        public string from { get; set; }

        /// <summary>
        /// destination multireddit url path
        /// </summary>
        public string to { get; set; }

        /// <summary>
        /// Specify an old and new multireddit URL path for copy or rename.
        /// </summary>
        /// <param name="displayName">a string no longer than 50 characters</param>
        /// <param name="from">multireddit url path</param>
        /// <param name="to">destination multireddit url path</param>
        public MultiURLInput(string displayName = "", string from = "", string to = "")
        {
            display_name = displayName;
            this.from = from;
            this.to = to;
        }
    }
}
