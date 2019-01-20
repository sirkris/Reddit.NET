using System;

namespace Reddit.Inputs.LiveThreads
{
    [Serializable]
    public class LiveThreadsBodyInput : APITypeInput
    {
        /// <summary>
        /// raw markdown text
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// Set body value.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        public LiveThreadsBodyInput(string body = "")
            : base()
        {
            this.body = body;
        }
    }
}
