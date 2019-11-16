using System;

namespace Reddit.Inputs
{
    [Serializable]
    public class BaseInput
    {
        /// <summary>
        /// Required by the API for all endpoints.  Prevents <, >, and & in the response from being htmlencoded by the API.
        /// See: https://www.reddit.com/dev/api#response_body_encoding
        /// </summary>
        public int raw_json { get; set; }

        /// <summary>
        /// Set the raw_json.
        /// </summary>
        public BaseInput()
        {
            raw_json = 1;
        }
    }
}
