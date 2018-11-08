using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ImageUploadResult : BaseResult
    {
        [JsonProperty("img_src")]
        public string ImgSrc;

        [JsonProperty("errors_values")]
        public List<string> ErrorsValues;
    }
}
