using System;

namespace Reddit.Inputs.LiveThreads
{
    [Serializable]
    public class LiveThreadsIdInput : APITypeInput
    {
        /// <summary>
        /// the ID of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Set a live thread ID.
        /// </summary>
        /// <param name="id">the ID of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        public LiveThreadsIdInput(string id = "") 
            : base()
        {
            this.id = id;
        }
    }
}
