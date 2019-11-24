using Newtonsoft.Json;
using Reddit.Things;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.Models
{
    public class Widgets : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Widgets(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// Add and return a widget to the specified subreddit.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="json">See https://www.reddit.com/dev/api/#POST_api_widget for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public T Add<T>(string json, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/widget", Method.POST);

            restRequest.AddParameter("json", json);
            
            return JsonConvert.DeserializeObject<T>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Add and return a widget to the specified subreddit.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetTextArea">See https://www.reddit.com/dev/api/#POST_api_widget for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public WidgetTextArea Add(WidgetTextArea widgetTextArea, string subreddit = null)
        {
            return Add<WidgetTextArea>(JsonConvert.SerializeObject(widgetTextArea), subreddit);
        }

        /// <summary>
        /// Add and return a widget to the specified subreddit.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetCalendar">See https://www.reddit.com/dev/api/#POST_api_widget for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public WidgetCalendar Add(WidgetCalendar widgetCalendar, string subreddit = null)
        {
            return Add<WidgetCalendar>(JsonConvert.SerializeObject(widgetCalendar), subreddit);
        }

        /// <summary>
        /// Add and return a widget to the specified subreddit.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetCommunityList">See https://www.reddit.com/dev/api/#POST_api_widget for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public WidgetCommunityListDetailed Add(WidgetCommunityList widgetCommunityList, string subreddit = null)
        {
            return Add<WidgetCommunityListDetailed>(JsonConvert.SerializeObject(widgetCommunityList), subreddit);
        }
        
        // TODO - Add support for other widget input types.  --Kris

        /// <summary>
        /// Delete a widget from the specified subreddit (if it exists).
        /// </summary>
        /// <param name="widgetId">id of an existing widget</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        public void Delete(string widgetId, string subreddit = null)
        {
            ExecuteRequest(Sr(subreddit) + "api/widget/" + widgetId, Method.DELETE);
        }

        /// <summary>
        /// Update and return the data of a widget.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetId">a valid widget id</param>
        /// <param name="json">See https://www.reddit.com/dev/api/#PUT_api_widget_{widget_id} for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public T Update<T>(string widgetId, string json, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/widget/" + widgetId, Method.PUT);

            restRequest.AddParameter("json", json);

            return JsonConvert.DeserializeObject<T>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Update and return the data of a widget.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetId">a valid widget id</param>
        /// <param name="widgetTextArea">See https://www.reddit.com/dev/api/#PUT_api_widget_{widget_id} for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public WidgetTextArea Update(string widgetId, WidgetTextArea widgetTextArea, string subreddit = null)
        {
            return Update<WidgetTextArea>(widgetId, JsonConvert.SerializeObject(widgetTextArea), subreddit);
        }

        /// <summary>
        /// Update and return the data of a widget.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetId">a valid widget id</param>
        /// <param name="widgetCalendar">See https://www.reddit.com/dev/api/#PUT_api_widget_{widget_id} for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public WidgetCalendar Update(string widgetId, WidgetCalendar widgetCalendar, string subreddit = null)
        {
            return Update<WidgetCalendar>(widgetId, JsonConvert.SerializeObject(widgetCalendar), subreddit);
        }

        /// <summary>
        /// Update and return the data of a widget.
        /// Accepts a JSON payload representing the widget data to be saved. Valid payloads differ in shape based on the "kind" attribute passed on the root object, which must be a valid widget kind.
        /// </summary>
        /// <param name="widgetId">a valid widget id</param>
        /// <param name="widgetCommunityList">See https://www.reddit.com/dev/api/#PUT_api_widget_{widget_id} for expected format</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The result payload.</returns>
        public WidgetCommunityList Update(string widgetId, WidgetCommunityList widgetCommunityList, string subreddit = null)
        {
            return Update<WidgetCommunityList>(widgetId, JsonConvert.SerializeObject(widgetCommunityList), subreddit);
        }

        // TODO - Add support for other widget input types.  --Kris

        // TODO - Needs testing.
        // TODO - Skipping the S3 image upload stuff until I can get it to work (see Emoji).  --Kris
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

        /// <summary>
        /// Update the order of widget_ids in the specified subreddit.
        /// </summary>
        /// <param name="section">one of (sidebar)</param>
        /// <param name="json">json data:
        /// [
        /// a string,
        /// ...
        /// ]</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        public void UpdateOrder(string section, string json, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/widget_order/" + section, Method.PATCH);

            restRequest.AddParameter("json", json);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Update the order of widget_ids in the specified subreddit.
        /// </summary>
        /// <param name="section">one of (sidebar)</param>
        /// <param name="widgetIds">a list of widget ids</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        public void UpdateOrder(string section, List<string> widgetIds, string subreddit = null)
        {
            UpdateOrder(section, JsonConvert.SerializeObject(widgetIds), subreddit);
        }

        /// <summary>
        /// Return all widgets for the given subreddit.
        /// </summary>
        /// <param name="progressiveImages">boolean value</param>
        /// <param name="subreddit">The subreddit with the widgets</param>
        /// <returns>The requested widgets.</returns>
        public WidgetResults Get(bool progressiveImages, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/widgets");

            restRequest.AddParameter("progressive_images", progressiveImages);

            return JsonConvert.DeserializeObject<WidgetResults>(ExecuteRequest(restRequest));
        }
    }
}
