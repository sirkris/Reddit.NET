using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModActionContainer : BaseContainer
    {
        [JsonProperty("data")]
        public ModActionData Data;
    }
}
