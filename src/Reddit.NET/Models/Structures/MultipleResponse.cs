using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class MultipleResponse : BaseResult
    {
        [JsonProperty("data")]
        public MultipleResponseData Data;
    }
}
