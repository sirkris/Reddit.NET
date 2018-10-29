using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
