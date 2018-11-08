using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModActionChild : BaseContainer
    {
        [JsonProperty("data")]
        public ModAction Data;
    }
}
