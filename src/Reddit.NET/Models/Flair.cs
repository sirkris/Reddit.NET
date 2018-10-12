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

        public Flair(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flairType"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object ClearFlairTemplates(string flairType, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/clearflairtemplates", Method.POST);

            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object DeleteFlair(string name, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflair", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flairTemplateId"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object DeleteFlairTemplate(string flairTemplateId, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflairtemplate", Method.POST);

            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cssClass"></param>
        /// <param name="link"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
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

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flairType"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object FlairTemplateOrder(string flairType, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flair_template_order", Method.PATCH);

            restRequest.AddParameter("flair_type", flairType);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flairEnabled"></param>
        /// <param name="flairPosition"></param>
        /// <param name="flairSelfAssignEnabled"></param>
        /// <param name="linkFlairPosition"></param>
        /// <param name="linkFlairSelfAssignEnabled"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
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

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flairCsv"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object FlairCSV(string flairCsv, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flaircsv", Method.POST);

            restRequest.AddParameter("flair_csv", flairCsv);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <param name="name"></param>
        /// <param name="subreddit"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="show"></param>
        /// <param name="srDetail"></param>
        /// <returns></returns>
        public object FlairList(string after, string before, string name, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
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

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subreddit"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public object FlairSelector(string name, string subreddit = null, string link = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairselector", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("link", link);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cssClass"></param>
        /// <param name="flairTemplateId"></param>
        /// <param name="flairType"></param>
        /// <param name="text"></param>
        /// <param name="textEditable"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
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

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="flairTemplateId"></param>
        /// <param name="flairType"></param>
        /// <param name="modOnly"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="textEditable"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
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

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object LinkFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/link_flair"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object LinkFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/link_flair_v2"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="flairTemplateId"></param>
        /// <param name="link"></param>
        /// <param name="name"></param>
        /// <param name="returnRtson"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
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

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flairEnabled"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object SetFlairEnabled(bool flairEnabled, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/setflairenabled", Method.POST);

            restRequest.AddParameter("flair_enabled", flairEnabled);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>List of available user flairs.</returns>
        public object UserFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/user_flair"));
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// If user is not a mod of the subreddit, this endpoint filters out mod_only templates.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>List of available user flairs.</returns>
        public object UserFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/user_flair_v2"));
        }
    }
}
