using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.Things;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Converters
{
    public class UserOverviewConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<CommentOrPost>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);
            return (jToken.Type == JTokenType.Array && objectType.Equals(typeof(List<CommentOrPost>)) ? BuildResult(jToken) : new List<CommentOrPost>());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        private List<CommentOrPost> BuildResult(JToken jToken)
        {
            List<PostChild> posts = jToken.ToObject<List<PostChild>>();
            List<CommentChild> comments = jToken.ToObject<List<CommentChild>>();

            List<CommentOrPost> res = new List<CommentOrPost>();
            for (int i = 0; i < Math.Max(posts.Count, comments.Count); i++)
            {
                string kind = null;
                if (i < posts.Count)
                {
                    kind = posts[i].Kind;
                }
                else
                {
                    kind = comments[i].Kind;
                }

                switch (kind)
                {
                    case "t1":
                        res.Add(new CommentOrPost(comments[i].Data, null));
                        break;
                    case "t3":
                        res.Add(new CommentOrPost(null, posts[i].Data));
                        break;
                }
            }

            return res;
        }
    }
}
