using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class PostData : BaseData
    {
        [JsonProperty("children")]
        public List<PostChild> Children;
    }
}
