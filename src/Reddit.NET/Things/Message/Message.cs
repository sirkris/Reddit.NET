using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Message
    {
        [JsonProperty("first_message")]
        public string FirstMessage { get; set; }

        [JsonProperty("first_message_name")]
        public string FirstMessageName { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("likes")]
        public string Likes { get; set; }

        [JsonProperty("replies")]
        public object Replies { get; set; }  // TODO - Determine type.  --Kris

        [JsonProperty("id")]
        public string Id { get; set; }

        public string Fullname => "t4_" + Id;

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("was_comment")]
        public bool WasComment { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("num_comments")]
        public int? NumComments { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; set; }

        [JsonProperty("new")]
        public bool New { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("dest")]
        public string Dest { get; set; }

        [JsonProperty("body_html")]
        public string BodyHTML { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("distinguished")]
        public string Distinguished { get; set; }
    }
}
