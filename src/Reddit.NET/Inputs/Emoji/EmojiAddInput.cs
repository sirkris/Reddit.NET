using System;

namespace Reddit.Inputs.Emoji
{
    [Serializable]
    public class EmojiAddInput : BaseInput
    {
        /// <summary>
        /// Name of the emoji to be created. It can be alphanumeric without any special characters except '-' & '_' and cannot exceed 24 characters
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// S3 key of the uploaded image which can be obtained from the S3 url. This is of the form subreddit/hash_value
        /// </summary>
        public string s3_key { get; set; }

        /// <summary>
        /// Data for emoji to be uploaded.
        /// </summary>
        /// <param name="name">Name of the emoji to be created. It can be alphanumeric without any special characters except '-' & '_' and cannot exceed 24 characters</param>
        /// <param name="s3Key">S3 key of the uploaded image which can be obtained from the S3 url. This is of the form subreddit/hash_value</param>
        public EmojiAddInput(string name, string s3Key)
        {
            this.name = name;
            s3_key = s3Key;
        }
    }
}
