using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LiveThreadCreateResultContainer
    {
        [JsonProperty("json")]
        public LiveThreadCreateResult JSON;
    }
}
