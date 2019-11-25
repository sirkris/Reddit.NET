using Newtonsoft.Json;
using Reddit.Inputs;
using Reddit.Inputs.Emoji;
using Reddit.Things;
using RestSharp;
using System;
using System.Linq;

namespace Reddit.Models
{
    public class Emoji : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Emoji(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        // TODO - Needs testing.
        /// <summary>
        /// Add an emoji to the DB by posting a message on emoji_upload_q.
        /// A job processor that listens on a queue uses the s3_key provided in the request to locate the image in S3 Temp Bucket and moves it to the PERM bucket.
        /// It also adds it to the DB using name as the column and sr_fullname as the key and sends the status on the websocket URL that is provided as part of this response.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="emojiAddInput">A valid EmojiAddInput instance</param>
        /// <returns>(TODO - Untested)</returns>
        public object Add(string subreddit, EmojiAddInput emojiAddInput)
        {
            return SendRequest<object>("api/v1/" + subreddit + "/emoji.json", emojiAddInput, Method.POST);
        }

        // TODO - Needs testing.
        /// <summary>
        /// Delete a Subreddit emoji. Remove the emoji from Cassandra and purge the assets from S3 and the image resizing provider.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="emojiName">The name of the emoji to be deleted</param>
        /// <returns>(TODO - Untested)</returns>
        public object Delete(string subreddit, string emojiName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/" + subreddit + "/emoji/" + emojiName, Method.DELETE));
        }

        /// <summary>
        /// Acquire and return an upload lease to s3 temp bucket.
        /// The return value of this function is a json object containing credentials for uploading assets to S3 bucket, S3 url for upload request and the key to use for uploading.
        /// Using this lease the client will upload the emoji image to S3 temp bucket (included as part of the S3 URL). This lease is used by S3 to verify that the upload is authorized.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="imageUploadInput">A valid ImageUploadInput instance</param>
        /// <returns>An S3 lease.</returns>
        public S3UploadLeaseContainer AcquireLease(string subreddit, ImageUploadInput imageUploadInput)
        {
            return SendRequest<S3UploadLeaseContainer>("api/v1/" + subreddit + "/emoji_asset_upload_s3.json", imageUploadInput, Method.POST);
        }

        // TODO - Can't get this to work.  Action URL keeps returning 403.  --Kris
        // See:  https://www.reddit.com/r/redditdev/comments/9s1pio/getting_aws_error_when_trying_to_upload_emoji/
        // Update:  Switching headers to parameters helped, but now it's complaining about the content-type on the image for some reason.  --Kris
        /// <summary>
        /// Upload an Emoji.
        /// </summary>
        /// <param name="imageData">Raw image data.</param>
        /// <param name="s3">The data retrieved by AcquireLease.</param>
        /// <returns>(TODO - Untested)</returns>
        public void UploadLeaseImage(byte[] imageData, S3UploadLeaseContainer s3)
        {
            RestClient = new RestClient("https:" + s3.S3UploadLease.Action);
            RestRequest restRequest = new RestRequest(Method.POST);

            foreach (S3UploadLeaseField s3Field in s3.S3UploadLease.Fields)
            {
                if (!s3Field.Name.Equals("content-type", StringComparison.OrdinalIgnoreCase))
                {
                    restRequest.AddParameter(s3Field.Name, s3Field.Value);
                }
            }

            //restRequest.AddBody(JsonConvert.SerializeObject(s3.S3UploadLease.Fields));

            restRequest.AddHeader("content-type", "multipart/form-data");
            //restRequest.AddHeader("key", s3.S3UploadLease.Fields.First(item => item.Name.Equals("key", StringComparison.OrdinalIgnoreCase)).Value);
            //restRequest.AddParameter("key", s3.S3UploadLease.Fields.First(item => item.Name.Equals("key", StringComparison.OrdinalIgnoreCase)).Value);

            restRequest.AddFileBytes("file", imageData, "birdie.jpg", s3.S3UploadLease.Fields.First(
                item => item.Name.Equals("content-type", StringComparison.OrdinalIgnoreCase)).Value);

            ExecuteRequest(restRequest);
        }

        // TODO - Needs testing.
        /// <summary>
        /// Set custom emoji size. Omitting width or height will disable custom emoji sizing.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="height">an integer between 1 and 40 (default: 0)</param>
        /// <param name="width">an integer between 1 and 40 (default: 0)</param>
        /// <returns>(TODO - Untested)</returns>
        public object CustomSize(string subreddit, int height = 0, int width = 0)
        {
            RestRequest restRequest = PrepareRequest("api/v1/" + subreddit + "/emoji_custom_size", Method.POST);

            restRequest.AddParameter("height", height);
            restRequest.AddParameter("width", width);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get all emojis for a SR. The response includes reddit emojis as well as emojis for the SR specified in the request.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <returns>Emojis.</returns>
        public SnoomojiContainer All(string subreddit)
        {
            return JsonConvert.DeserializeObject<SnoomojiContainer>(ExecuteRequest("api/v1/" + subreddit + "/emojis/all"));
        }
    }
}
