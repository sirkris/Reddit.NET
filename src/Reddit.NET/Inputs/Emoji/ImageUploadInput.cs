using System;

namespace Reddit.Inputs.Emoji
{
    [Serializable]
    public class ImageUploadInput
    {
        /// <summary>
        /// name and extension of the image file e.g. image1.png
        /// </summary>
        public string filepath { get; set; }

        /// <summary>
        /// mime type of the image e.g. image/png
        /// </summary>
        public string mimetype { get; set; }

        /// <summary>
        /// Data for image to be uploaded.
        /// </summary>
        /// <param name="fileName">name and extension of the image file e.g. image1.png</param>
        /// <param name="mimeType">mime type of the image e.g. image/png</param>
        public ImageUploadInput(string fileName, string mimeType)
        {
            filepath = fileName;
            mimetype = mimeType;
        }
    }
}
