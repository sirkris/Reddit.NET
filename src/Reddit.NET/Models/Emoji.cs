using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Emoji : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Emoji(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        public object Add(string subreddit, string name, string s3Key)
        {
            RestRequest restRequest = PrepareRequest("api/v1/" + subreddit + "/emoji.json", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("s3_key", s3Key);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Delete(string subreddit, string emojiName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/" + subreddit + "/emoji/" + emojiName, Method.DELETE));
        }

        public object AcquireLease(string subreddit, string filePath, string mimeType)
        {
            RestRequest restRequest = PrepareRequest("api/v1/" + subreddit + "/emoji_asset_upload_s3.json", Method.POST);

            restRequest.AddParameter("filepath", filePath);
            restRequest.AddParameter("mimetype", mimeType);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object CustomSize(string subreddit, int height = 0, int width = 0)
        {
            RestRequest restRequest = PrepareRequest("api/v1/" + subreddit + "/emoji_custom_size", Method.POST);

            restRequest.AddParameter("height", height);
            restRequest.AddParameter("width", width);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object All(string subreddit)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/" + subreddit + "/emojis/all"));
        }
    }
}
