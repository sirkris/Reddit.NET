using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveThreadCreateResult : BaseResult
    {
        [JsonProperty("data")]
        public LiveThreadCreateResultData Data { get; set; }
    }
}
