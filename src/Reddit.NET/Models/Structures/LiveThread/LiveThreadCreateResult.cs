using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LiveThreadCreateResult : BaseResult
    {
        [JsonProperty("data")]
        public LiveThreadCreateResultData Data;
    }
}
