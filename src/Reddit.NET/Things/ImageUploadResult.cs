using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ImageUploadResult
    {
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        [JsonProperty("img_src")]
        public string ImgSrc { get; set; }

        [JsonProperty("errors_values")]
        public List<string> ErrorsValues { get; set; }
    }
}
