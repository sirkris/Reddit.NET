using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class S3UploadLeaseField
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("value")]
        public string Value;
    }
}
