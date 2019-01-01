using System;

namespace Reddit.Models.Inputs
{
    [Serializable]
    public class APITypeInput
    {
        /// <summary>
        /// Required by the API for certain endpoints.
        /// </summary>
        public string api_type { get; set; }

        /// <summary>
        /// Set the api_type.
        /// </summary>
        public APITypeInput()
        {
            api_type = "json";
        }
    }
}
