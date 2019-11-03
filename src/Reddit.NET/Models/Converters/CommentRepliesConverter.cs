using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.Models.Converters
{
    public class CommentRepliesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(CommentContainer) || objectType == typeof(MoreContainer));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);
            return (jToken.Type == JTokenType.Object ? BuildResult(jToken, objectType) : new MoreChildren());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        private MoreChildren BuildResult(JToken jToken, Type objectType)
        {
            if (objectType == typeof(CommentContainer))
            {
                List<Comment> comments = new List<Comment>();
                foreach (CommentChild commentChild in jToken.ToObject<CommentContainer>().Data.Children)
                {
                    comments.Add(commentChild.Data);
                }

                return new MoreChildren(comments, null);
            }
            else if (objectType == typeof(MoreContainer))
            {
                List<More> more = new List<More>();
                foreach (MoreChild moreChild in jToken.ToObject<MoreContainer>().Data.Children)
                {
                    more.Add(moreChild.Data);
                }

                return new MoreChildren(null, more);
            }
            else
            {
                return new MoreChildren();
            }
        }
    }
}
