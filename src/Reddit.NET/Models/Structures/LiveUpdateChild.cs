using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LiveUpdateChild : BaseContainer
    {
        [JsonProperty("data")]
        public LiveUpdate Data;
    }
}
