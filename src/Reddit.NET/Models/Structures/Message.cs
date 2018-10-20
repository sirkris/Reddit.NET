using Newtonsoft.Json;
using Reddit.NET.Models.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class Message
    {
        [JsonProperty("first_message")]
        public string FirstMessage;

        [JsonProperty("first_message_name")]
        public string FirstMessageName;

        [JsonProperty("subreddit")]
        public string Subreddit;

        [JsonProperty("likes")]
        public string Likes;

        [JsonProperty("replies")]
        public object Replies;  // TODO - Determine type.  --Kris

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("subject")]
        public string Subject;

        [JsonProperty("was_comment")]
        public bool WasComment;

        [JsonProperty("score")]
        public int Score;

        [JsonProperty("author")]
        public string Author;

        [JsonProperty("num_comments")]
        public int? NumComments;

        [JsonProperty("parent_id")]
        public string ParentId;

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed;

        [JsonProperty("new")]
        public bool New;

        [JsonProperty("body")]
        public string Body;

        [JsonProperty("dest")]
        public string Dest;

        [JsonProperty("body_html")]
        public string BodyHTML;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("created")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Created;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime CreatedUTC;

        [JsonProperty("context")]
        public string Context;

        [JsonProperty("distinguished")]
        public string Distinguished;
    }
}
