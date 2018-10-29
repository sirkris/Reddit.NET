using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class S3UploadLease
    {
        [JsonProperty("action")]
        public string Action;

        [JsonProperty("fields")]
        public List<S3UploadLeaseField> Fields;
    }
}
