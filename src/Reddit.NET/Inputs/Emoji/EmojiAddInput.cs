using System;

namespace Reddit.Inputs.Emoji
{
    [Serializable]
    public class EmojiAddInput
    {
        /// <summary>
        /// Name of the emoji to be created. It can be alphanumeric without any special characters except '-' & '_' and cannot exceed 24 characters
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// S3 key of the uploaded image which can be obtained from the S3 url. This is of the form subreddit/hash_value
        /// </summary>
        public string s3_key { get; set; }

        public bool mod_flair_only { get; set; }

        public bool post_flair_allowed { get; set; }

        public bool user_flair_allowed { get; set; }

        /// <summary>
        /// Data for emoji to be uploaded.
        /// </summary>
        /// <param name="name">Name of the emoji to be created. It can be alphanumeric without any special characters except '-' & '_' and cannot exceed 24 characters</param>
        /// <param name="s3Key">S3 key of the uploaded image which can be obtained from the S3 url. This is of the form subreddit/hash_value</param>
        /// <param name="modFlairOnly">If this emoji is exclusive to mods' flairs (or mod-assigned flairs).</param>
        /// <param name="postFlairAllowed">If this emoji should be usable on post flairs.</param>
        /// <param name="userFlairAllowed">If this emoji should be usable on user flairs.</param>
        public EmojiAddInput(string name, string s3Key, bool modFlairOnly, bool postFlairAllowed, bool userFlairAllowed)
        {
            this.name = name;
            s3_key = s3Key;
            mod_flair_only = modFlairOnly;
            post_flair_allowed = postFlairAllowed;
            user_flair_allowed = userFlairAllowed;
        }
    }
}
