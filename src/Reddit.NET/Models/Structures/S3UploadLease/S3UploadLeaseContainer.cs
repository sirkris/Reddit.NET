using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class S3UploadLeaseContainer
    {
        [JsonProperty("s3UploadLease")]
        public S3UploadLease S3UploadLease;

        [JsonProperty("websocketUrl")]
        public string WebSocketURL;
    }
}
