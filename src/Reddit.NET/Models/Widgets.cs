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

        public object Add(string json)
        {
            RestRequest restRequest = PrepareRequest("api/widget", Method.POST);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Delete(string widgetId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/widget/" + widgetId, Method.DELETE));
        }

        public object Update(string widgetId, string json)
        {
            RestRequest restRequest = PrepareRequest("api/widget/" + widgetId, Method.PUT);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object WidgetImageUploadS3(string filePath, string mimeType)
        {
            RestRequest restRequest = PrepareRequest("api/widget_image_upload_s3", Method.POST);

            restRequest.AddParameter("filepath", filePath);
            restRequest.AddParameter("mimetype", mimeType);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object UpdateOrder(string section, string json)
        {
            RestRequest restRequest = PrepareRequest("api/widget_order/" + section, Method.PATCH);

            restRequest.AddParameter("json", json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Get(bool progressiveImages)
        {
            RestRequest restRequest = PrepareRequest("api/widgets");

            restRequest.AddParameter("progressive_images", progressiveImages);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
