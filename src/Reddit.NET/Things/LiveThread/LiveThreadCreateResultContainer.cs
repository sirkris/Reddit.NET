using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveThreadCreateResultContainer
    {
        [JsonProperty("json")]
        public LiveThreadCreateResult JSON { get; set; }
    }
}
