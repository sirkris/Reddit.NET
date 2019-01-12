using System;

namespace Reddit.Inputs.LinksAndComments
{
    [Serializable]
    public class LinksAndCommentsThingInput : APITypeInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool return_rtjson { get; set; }

        /// <summary>
        /// JSON data
        /// </summary>
        public string richtext_json { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string thing_id { get; set; }

        /// <summary>
        /// Data for creating or modifying a post or comment.
        /// </summary>
        /// <param name="text">raw markdown text</param>
        /// <param name="thingId">fullname of a thing</param>
        /// <param name="returnRtjson">boolean value</param>
        /// <param name="richtextJson">JSON data</param>
        public LinksAndCommentsThingInput(string text = "", string thingId = "", bool returnRtjson = false, string richtextJson = "")
            : base()
        {
            this.text = text;
            thing_id = thingId;
            return_rtjson = returnRtjson;
            richtext_json = richtextJson;
        }
    }
}
