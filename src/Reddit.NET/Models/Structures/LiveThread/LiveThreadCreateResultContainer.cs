using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LiveThreadCreateResultContainer
    {
        [JsonProperty("json")]
        public LiveThreadCreateResult JSON;
    }
}
