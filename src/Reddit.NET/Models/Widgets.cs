using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Widgets : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Widgets(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// Add and return a widget to the specified subreddit.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="json">See https://www.reddit.com/dev/api/#POST_api_widget for expected format</param>
        /// <returns>(TODO - Untested)</returns>
        public object Add(string json)
        {
            RestRequest restRequest = PrepareRequest("api/widget", Method.POST);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Delete a widget from the specified subreddit (if it exists).
        /// </summary>
        /// <param name="widgetId">id of an existing widget</param>
        /// <returns>(TODO - Untested)</returns>
        public object Delete(string widgetId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/widget/" + widgetId, Method.DELETE));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Update and return the data of a widget.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetId">a valid widget id</param>
        /// <param name="json">See https://www.reddit.com/dev/api/#PUT_api_widget_{widget_id} for expected format</param>
        /// <returns>(TODO - Untested)</returns>
        public object Update(string widgetId, string json)
        {
            RestRequest restRequest = PrepareRequest("api/widget/" + widgetId, Method.PUT);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Acquire and return an upload lease to s3 temp bucket.
        /// The return value of this function is a json object containing credentials for uploading assets to S3 bucket, S3 url for upload request and the key to use for uploading.
        /// Using this lease the client will upload the emoji image to S3 temp bucket (included as part of the S3 URL).
        /// This lease is used by S3 to verify that the upload is authorized.
        /// </summary>
        /// <param name="filePath">name and extension of the image file e.g. image1.png</param>
        /// <param name="mimeType">mime type of the image e.g. image/png</param>
        /// <returns>(TODO - Untested)</returns>
        public object WidgetImageUploadS3(string filePath, string mimeType)
        {
            RestRequest restRequest = PrepareRequest("api/widget_image_upload_s3", Method.POST);

            restRequest.AddParameter("filepath", filePath);
            restRequest.AddParameter("mimetype", mimeType);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Update the order of widget_ids in the specified subreddit.
        /// </summary>
        /// <param name="section">one of (sidebar)</param>
        /// <param name="json">json data:
        /// [
        /// a string,
        /// ...
        /// ]</param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdateOrder(string section, string json)
        {
            RestRequest restRequest = PrepareRequest("api/widget_order/" + section, Method.PATCH);

            restRequest.AddParameter("json", json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Return all widgets for the given subreddit.
        /// </summary>
        /// <param name="progressiveImages">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object Get(bool progressiveImages)
        {
            RestRequest restRequest = PrepareRequest("api/widgets");

            restRequest.AddParameter("progressive_images", progressiveImages);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
