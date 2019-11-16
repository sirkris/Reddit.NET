using System;

namespace Reddit.Inputs
{
    [Serializable]
    public class ImageUploadInput : BaseInput
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
        /// <param name="filePath">name and extension of the image file e.g. image1.png</param>
        /// <param name="mimeType">mime type of the image e.g. image/png</param>
        public ImageUploadInput(string filePath, string mimeType)
        {
            filepath = filePath;
            mimetype = mimeType;
        }
    }
}
