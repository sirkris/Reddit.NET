using System;

namespace Reddit.Inputs.LiveThreads
{
    [Serializable]
    public class LiveThreadsReportTypeInput : APITypeInput
    {
        /// <summary>
        /// one of (spam, vote-manipulation, personal-information, sexualizing-minors, site-breaking)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Set a live thread report type.
        /// </summary>
        /// <param name="type">one of (spam, vote-manipulation, personal-information, sexualizing-minors, site-breaking)</param>
        public LiveThreadsReportTypeInput(string type = "")
            : base()
        {
            this.type = type;
        }
    }
}
