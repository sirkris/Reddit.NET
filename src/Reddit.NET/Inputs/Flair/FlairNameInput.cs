using System;

namespace Reddit.Inputs.Flair
{
    [Serializable]
    public class FlairNameInput : BaseInput
    {
        /// <summary>
        /// a user by name
        /// </summary>
        public string name { get; set; }
    }
}
