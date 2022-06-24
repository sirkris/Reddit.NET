using System;
using Newtonsoft.Json;

namespace Reddit.NET.Things.Post
{
    [Serializable]
    public class GalleryDataItem
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        public long Id { get; set; }
    }
}
