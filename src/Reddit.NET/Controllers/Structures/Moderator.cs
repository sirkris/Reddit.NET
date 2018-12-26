using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Controllers.Structures
{
    [Serializable]
    public class Moderator
    {
        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCSSClass;

        [JsonProperty("author_flair_text")]
        public string AuthorFlairText;

        [JsonProperty("mod_permissions")]
        public List<string> ModPermissions;

        [JsonProperty("date")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Date;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;
    }
}
