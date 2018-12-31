using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairNameInput
    {
        /// <summary>
        /// a user by name
        /// </summary>
        public string name { get; set; }
    }
}
