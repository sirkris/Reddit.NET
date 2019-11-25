using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.Things;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Converters
{
    public class CommentRepliesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(MoreChildren));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);
            return (jToken.Type == JTokenType.Object ? BuildResult(jToken) : new MoreChildren());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        private MoreChildren BuildResult(JToken jToken)
        {
            CommentContainer listing = jToken.ToObject<CommentContainer>();
            if (listing.Data != null
                && listing.Data.Children != null
                && !listing.Data.Children.Count.Equals(0))
            {
                switch (listing.Data.Children[0].Kind)
                {
                    default:
                        return new MoreChildren();
                    case "t1":
                        List<Comment> comments = new List<Comment>();
                        foreach (CommentChild commentChild in listing.Data.Children)
                        {
                            comments.Add(commentChild.Data);
                        }

                        return new MoreChildren(comments, null);
                    case "more":
                        List<More> more = new List<More>();
                        foreach (MoreChild moreChild in jToken.ToObject<MoreContainer>().Data.Children)
                        {
                            more.Add(moreChild.Data);
                        }

                        return new MoreChildren(null, more);
                }
            }
            else
            {
                return new MoreChildren();
            }
        }
    }
}
