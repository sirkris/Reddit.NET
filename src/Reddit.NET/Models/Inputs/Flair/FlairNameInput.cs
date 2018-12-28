using Newtonsoft.Json;
using System;

namespace Reddit.Models.Inputs.Flair
{
    [Serializable]
    public class FlairNameInput
    {
        /// <summary>
        /// a user by name
        /// </summary>
        [JsonProperty("name")]
        public string Name;
    }
}
