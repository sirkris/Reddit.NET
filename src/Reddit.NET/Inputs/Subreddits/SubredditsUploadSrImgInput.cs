using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsUploadSrImgInput : BaseInput
    {
        /// <summary>
        /// file upload with maximum size of 500 KiB
        /// </summary>
        public byte[] file { get; set; }

        /// <summary>
        /// an integer between 0 and 1
        /// </summary>
        public int header { get; set; }

        /// <summary>
        /// a valid subreddit image name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// one of (img, header, icon, banner)
        /// </summary>
        public string upload_type { get; set; }

        /// <summary>
        /// one of png or jpg (default: png)
        /// </summary>
        public string img_type { get; set; }

        /// <summary>
        /// (optional) can be ignored
        /// </summary>
        public string formid { get; set; }

        /// <summary>
        /// Add or replace a subreddit image, custom header logo, custom mobile icon, or custom mobile banner.
        /// If the upload_type value is img, an image for use in the subreddit stylesheet is uploaded with the name specified in name.
        /// If the upload_type value is header then the image uploaded will be the subreddit's new logo and name will be ignored.
        /// If the upload_type value is icon then the image uploaded will be the subreddit's new mobile icon and name will be ignored.
        /// If the upload_type value is banner then the image uploaded will be the subreddit's new mobile banner and name will be ignored.
        /// For backwards compatibility, if upload_type is not specified, the header field will be used instead:
        /// If the header field has value 0, then upload_type is img.
        /// If the header field has value 1, then upload_type is header.
        /// The img_type field specifies whether to store the uploaded image as a PNG or JPEG.
        /// Subreddits have a limited number of images that can be in use at any given time. If no image with the specified name already exists, one of the slots will be consumed
        /// If an image with the specified name already exists, it will be replaced. This does not affect the stylesheet immediately, but will take effect the next time the stylesheet is saved.
        /// </summary>
        /// <param name="file">file upload with maximum size of 500 KiB</param>
        /// <param name="header">an integer between 0 and 1</param>
        /// <param name="name">a valid subreddit image name</param>
        /// <param name="uploadType">one of (img, header, icon, banner)></param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <param name="formId">(optional) can be ignored</param>
        public SubredditsUploadSrImgInput(byte[] file, int header = 0, string name = "", string uploadType = "img", string imgType = "png", string formId = "")
        {
            this.file = file;
            this.header = header;
            this.name = name;
            upload_type = uploadType;
            img_type = imgType;
            formid = formId;
        }
    }
}
