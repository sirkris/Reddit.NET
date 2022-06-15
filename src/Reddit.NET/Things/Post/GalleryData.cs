using System;
using Newtonsoft.Json;

namespace Reddit.NET.Things.Post
{
    [Serializable]
    public class GalleryData
    {
        [JsonProperty("items")]
        public GalleryDataItem[] Items { get; set; }
    }
}
