using System;

namespace Reddit.Inputs.Modmail
{
    [Serializable]
    public class ModmailBulkReadInput : BaseInput
    {
        /// <summary>
        /// comma-delimited list of subreddit names
        /// </summary>
        public string entity { get; set; }

        /// <summary>
        /// one of (new, inprogress, mod, notifications, archived, highlighted, all)
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// Set values for entity and state in bulk message retrieval.
        /// </summary>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        public ModmailBulkReadInput(string entity = "", string state = "all")
        {
            this.entity = entity;
            this.state = state;
        }
    }
}
