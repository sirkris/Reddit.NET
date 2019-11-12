using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using RestSharp;
using Reddit.Inputs.Emoji;
using Reddit.Things;
using Reddit.Exceptions;

namespace Reddit.Models
{
    public class Emoji : BaseModel
    {
        // Used for deserializing S3PostResponse from S3 emoji post. --MingweiSamuel
        private static readonly XmlSerializer S3PostResponseXmlSerializer = new XmlSerializer(typeof(S3PostResponse));

        internal override RestClient RestClient { get; set; }

        public Emoji(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId) { }

        // TODO - Needs testing.
        /// <summary>
        /// Add an emoji to the DB by posting a message on emoji_upload_q.
        /// A job processor that listens on a queue uses the s3_key provided in the request to locate the image in S3 Temp Bucket and moves it to the PERM bucket.
        /// It also adds it to the DB using name as the column and sr_fullname as the key and sends the status on the websocket URL that is provided as part of this response.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="emojiAddInput">A valid EmojiAddInput instance</param>
        /// <returns>(TODO - Untested)</returns>
        // TODO returns {"json": {"errors": []}}
        public object Add(string subreddit, EmojiAddInput emojiAddInput)
        {
            return SendRequest<object>("api/v1/" + subreddit + "/emoji.json", emojiAddInput, Method.POST);
        }

        // TODO - Needs testing.
        /// <summary>
        /// Add an emoji to the DB by posting a message on emoji_upload_q.
        /// A job processor that listens on a queue uses the s3_key provided in the request to locate the image in S3 Temp Bucket and moves it to the PERM bucket.
        /// It also adds it to the DB using name as the column and sr_fullname as the key and sends the status on the websocket URL that is provided as part of this response.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="emojiAddInput">A valid EmojiAddInput instance</param>
        /// <returns>(TODO - Untested)</returns>
        // TODO returns {"json": {"errors": []}}
        public async Task<object> AddAsync(string subreddit, EmojiAddInput emojiAddInput)
        {
            return await SendRequestAsync<object>("api/v1/" + subreddit + "/emoji.json", emojiAddInput, Method.POST);
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
        
        // TODO - Needs testing.
        /// <summary>
        /// Delete a Subreddit emoji. Remove the emoji from Cassandra and purge the assets from S3 and the image resizing provider.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="emojiName">The name of the emoji to be deleted</param>
        /// <returns>(TODO - Untested)</returns>
        public async Task<object> DeleteAsync(string subreddit, string emojiName)
        {
            return JsonConvert.DeserializeObject(await ExecuteRequestAsync("api/v1/" + subreddit + "/emoji/" + emojiName, Method.DELETE));
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

        /// <summary>
        /// Acquire and return an upload lease to s3 temp bucket.
        /// The return value of this function is a json object containing credentials for uploading assets to S3 bucket, S3 url for upload request and the key to use for uploading.
        /// Using this lease the client will upload the emoji image to S3 temp bucket (included as part of the S3 URL). This lease is used by S3 to verify that the upload is authorized.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <param name="imageUploadInput">A valid ImageUploadInput instance</param>
        /// <returns>An S3 lease.</returns>
        public async Task<S3UploadLeaseContainer> AcquireLeaseAsync(string subreddit, ImageUploadInput imageUploadInput)
        {
            return await SendRequestAsync<S3UploadLeaseContainer>("api/v1/" + subreddit + "/emoji_asset_upload_s3.json", imageUploadInput, Method.POST);
        }

        #region UploadLeaseImage
        // TODO: write tests for these.

        /// <summary>
        /// Upload an Emoji to S3.
        /// </summary>
        /// <param name="s3">The data retrieved by AcquireLease.</param>
        /// <param name="imageData">Raw image data.</param>
        /// <param name="imageUploadInput">File name (with extension) and mime type.</param>
        /// <returns>Decoded PostResponse including Key.</returns>
        public S3PostResponse UploadLeaseImage(S3UploadLeaseContainer s3, byte[] imageData, ImageUploadInput imageUploadInput)
        {
            return HelperUploadLeaseImage(s3.S3UploadLease, new RestRequest(Method.POST)
                .AddFile("file", imageData, imageUploadInput.filepath, imageUploadInput.mimetype));
        }

        /// <summary>
        /// Upload an Emoji to S3.
        /// </summary>
        /// <param name="s3">The data retrieved by AcquireLease.</param>
        /// <param name="imageData"></param>
        /// <param name="imageUploadInput">File name (with extension) and mime type.</param>
        /// <param name="contentLength">Optional length of imageData. Otherwise imageData.Length will be used.</param>
        /// <returns>Decoded PostResponse including Key.</returns>
        public S3PostResponse UploadLeaseImage(S3UploadLeaseContainer s3, Stream imageData, ImageUploadInput imageUploadInput, long? contentLength = null)
        {
            return HelperUploadLeaseImage(s3.S3UploadLease, new RestRequest(Method.POST)
                .AddFile("file", imageData.CopyTo, imageUploadInput.filepath, contentLength ?? imageData.Length, imageUploadInput.mimetype));
        }

        // Helper for upload lease image.
        private S3PostResponse HelperUploadLeaseImage(S3UploadLease s3Lease, IRestRequest restRequest)
        {
            RestClient s3RestClient = new RestClient("https:" + s3Lease.Action);
            foreach (S3UploadLeaseField s3Field in s3Lease.Fields)
            {
                restRequest.AddParameter(s3Field.Name, s3Field.Value);
            }

            IRestResponse response = s3RestClient.Execute(restRequest);
            return HandleUploadLeaseImageResponse(response);
        }

        /// <summary>
        /// Upload an Emoji to S3.
        /// </summary>
        /// <param name="s3">The data retrieved by AcquireLease.</param>
        /// <param name="imageData">Raw image data.</param>
        /// <param name="imageUploadInput">File name (with extension) and mime type.</param>
        /// <returns>Task which may contain exceptions.</returns>
        /// <returns>Decoded PostResponse including Key.</returns>
        public async Task<S3PostResponse> UploadLeaseImageAsync(
            S3UploadLeaseContainer s3, byte[] imageData, ImageUploadInput imageUploadInput)
        {
            return await HelperUploadLeaseImageAsync(s3.S3UploadLease, new RestRequest(Method.POST)
                .AddFile("file", imageData, imageUploadInput.filepath, imageUploadInput.mimetype));
        }

        /// <summary>
        /// Upload an Emoji to S3.
        /// </summary>
        /// <param name="s3">The data retrieved by AcquireLease.</param>
        /// <param name="imageData"></param>
        /// <param name="imageUploadInput">File name (with extension) and mime type.</param>
        /// <param name="contentLength">Optional length of imageData. Otherwise imageData.Length will be used.</param>
        /// <returns>Decoded PostResponse including Key.</returns>
        public async Task<S3PostResponse> UploadLeaseImageAsync(
            S3UploadLeaseContainer s3, Stream imageData, ImageUploadInput imageUploadInput, long? contentLength = null)
        {
            return await HelperUploadLeaseImageAsync(s3.S3UploadLease, new RestRequest(Method.POST)
                .AddFile("file", imageData.CopyTo, imageUploadInput.filepath, contentLength ?? imageData.Length, imageUploadInput.mimetype));
        }

        // Helper for upload lease image (async).
        private async Task<S3PostResponse> HelperUploadLeaseImageAsync(S3UploadLease s3Lease, IRestRequest restRequest)
        {
            RestClient s3RestClient = new RestClient("https:" + s3Lease.Action);
            foreach (S3UploadLeaseField s3Field in s3Lease.Fields)
            {
                restRequest.AddParameter(s3Field.Name, s3Field.Value);
            }

            IRestResponse response = await s3RestClient.ExecuteTaskAsync<S3PostResponse>(restRequest);
            return HandleUploadLeaseImageResponse(response);
        }

        private S3PostResponse HandleUploadLeaseImageResponse(IRestResponse s3Response)
        {
            if (null != s3Response.ErrorException || HttpStatusCode.Created != s3Response.StatusCode)
                throw new RedditException("Failed to upload image to s3.", s3Response.ErrorException);

            return (S3PostResponse) S3PostResponseXmlSerializer.Deserialize(new StringReader(s3Response.Content));
        }
        #endregion

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

        /// <summary>
        /// Get all emojis for a SR. The response includes reddit emojis as well as emojis for the SR specified in the request.
        /// </summary>
        /// <param name="subreddit">The subreddit with the emojis</param>
        /// <returns>Emojis.</returns>
        public async Task<SnoomojiContainer> AllAsync(string subreddit)
        {
            return JsonConvert.DeserializeObject<SnoomojiContainer>(await ExecuteRequestAsync("api/v1/" + subreddit + "/emojis/all"));
        }
    }
}
