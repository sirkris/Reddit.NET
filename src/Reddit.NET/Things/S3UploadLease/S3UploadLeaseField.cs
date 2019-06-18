using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class S3UploadLeaseField
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
