using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ImageUploadResult : BaseResult
    {
        [JsonProperty("errors")]
        public List<string> Errors;

        [JsonProperty("img_src")]
        public string ImgSrc;

        [JsonProperty("errors_values")]
        public List<string> ErrorsValues;
    }
}
