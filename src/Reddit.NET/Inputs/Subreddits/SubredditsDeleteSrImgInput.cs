using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsDeleteSrImgInput : APITypeInput
    {
        /// <summary>
        /// a valid subreddit image name
        /// </summary>
        public string img_name { get; set; }

        /// <summary>
        /// Remove an image from the subreddit's custom image set.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// </summary>
        /// <param name="imgName">a valid subreddit image name</param>
        public SubredditsDeleteSrImgInput(string imgName = "")
            : base()
        {
            img_name = imgName;
        }
    }
}
