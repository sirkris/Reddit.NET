using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.NET.Controllers;
using Reddit.NET.Models.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostChild
    {
        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("data")]
        public Post Data;
    }
}
