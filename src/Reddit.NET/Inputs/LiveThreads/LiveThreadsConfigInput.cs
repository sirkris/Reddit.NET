using System;

namespace Reddit.Inputs.LiveThreads
{

    [Serializable]
    public class LiveThreadsConfigInput : APITypeInput
    {
        /// <summary>
        /// raw markdown text
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool nsfw { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string resources { get; set; }

        /// <summary>
        /// a string no longer than 120 characters
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Configuration values for a live thread.
        /// </summary>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        public LiveThreadsConfigInput(string title = "", string description = "", bool nsfw = false, string resources = "") 
            : base()
        {
            this.title = title;
            this.description = description;
            this.nsfw = nsfw;
            this.resources = resources;
        }
    }
}
