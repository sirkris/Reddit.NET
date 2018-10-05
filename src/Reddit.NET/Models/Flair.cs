using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Flair : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Flair(string refreshToken, string accessToken, RestClient restClient) : base(refreshToken, accessToken, restClient) { }

        public object ClearFlairTemplates(string flairType, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/clearflairtemplates", Method.POST);

            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object DeleteFlair(string name, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflair", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object DeleteFlairTemplate(string flairTemplateId, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflairtemplate", Method.POST);

            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Create(string cssClass, string link, string name, string text, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flair", Method.POST);

            restRequest.AddParameter("css_class", cssClass);
            restRequest.AddParameter("link", link);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object FlairTemplateOrder(string flairType, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flair_template_order", Method.PATCH);

            restRequest.AddParameter("flair_type", flairType);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object FlairConfig(bool flairEnabled, string flairPosition, bool flairSelfAssignEnabled, string linkFlairPosition, bool linkFlairSelfAssignEnabled,
            string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairconfig", Method.POST);

            restRequest.AddParameter("flair_enabled", flairEnabled);
            restRequest.AddParameter("flair_position", flairPosition);
            restRequest.AddParameter("flair_self_assign_enabled", flairSelfAssignEnabled);
            restRequest.AddParameter("link_flair_position", linkFlairPosition);
            restRequest.AddParameter("link_flair_self_assign_enabled", linkFlairSelfAssignEnabled);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object FlairCSV(string flairCsv, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flaircsv", Method.POST);

            restRequest.AddParameter("flair_csv", flairCsv);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object FlairList(string after, string before, string name, string subreddit = null, int count = 0, int limit = 25, string show = "all", string srDetail = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairlist");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object FlairSelector(string name, string subreddit = null, string link = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairselector", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("link", link);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object FlairTemplate(string cssClass, string flairTemplateId, string flairType, string text, bool textEditable, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairtemplate", Method.POST);

            restRequest.AddParameter("css_class", cssClass);
            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("text_editable", textEditable);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object FlairTemplateV2(string backgroundColor, string flairTemplateId, string flairType, bool modOnly, string text, string textColor,
            bool textEditable, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairtemplate_v2", Method.POST);

            restRequest.AddParameter("background_color", backgroundColor);
            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("mod_only", modOnly);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("text_color", textColor);
            restRequest.AddParameter("text_editable", textEditable);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object LinkFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/link_flair"));
        }

        public object LinkFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/link_flair_v2"));
        }

        public object SelectFlair(string backgroundColor, string flairTemplateId, string link, string name, string returnRtson, string text,
            string textColor, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/selectflair", Method.POST);

            restRequest.AddParameter("background_color", backgroundColor);
            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("link", link);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("return_rtson", returnRtson);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("text_color", textColor);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object SetFlairEnabled(bool flairEnabled, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/setflairenabled", Method.POST);

            restRequest.AddParameter("flair_enabled", flairEnabled);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object UserFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/user_flair"));
        }

        public object UserFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/user_flair_v2"));
        }
    }
}
