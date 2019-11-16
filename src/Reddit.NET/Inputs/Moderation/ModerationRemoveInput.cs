using System;

namespace Reddit.Inputs.Moderation
{
    [Serializable]
    public class ModerationRemoveInput : BaseInput
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool spam { get; set; }

        /// <summary>
        /// Remove a link, comment, or modmail message.
        /// If the thing is a link, it will be removed from all subreddit listings. If the thing is a comment, it will be redacted and removed from all subreddit comment listings.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="spam">boolean value</param>
        public ModerationRemoveInput(string id = "", bool spam = false)
        {
            this.id = id;
            this.spam = spam;
        }
    }
}
