using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class PrivateMessages : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public PrivateMessages(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Block(string id)
        {
            RestRequest restRequest = PrepareRequest("api/block", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object CollapseMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/collapse_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromSr"></param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="subject"></param>
        /// <param name="text"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public object Compose(string fromSr, string gRecaptchaResponse, string subject, string text, string to)
        {
            RestRequest restRequest = PrepareRequest("api/compose", Method.POST);

            restRequest.AddParameter("from_sr", fromSr);
            restRequest.AddParameter("g-recaptcha-response", gRecaptchaResponse);
            restRequest.AddParameter("subject", subject);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("to", to);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object DelMsg(string id)
        {
            RestRequest restRequest = PrepareRequest("api/del_msg", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterTypes"></param>
        /// <returns></returns>
        public object ReadAllMessages(string filterTypes)
        {
            RestRequest restRequest = PrepareRequest("api/read_all_messages", Method.POST);

            restRequest.AddParameter("filter_types", filterTypes);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object ReadMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/read_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UnblockSubreddit(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unblock_subreddit", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UncollapseMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/uncollapse_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object UnreadMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unread_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="mark"></param>
        /// <param name="mid"></param>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <param name="includeCategories"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="show"></param>
        /// <param name="srDetail"></param>
        /// <returns></returns>
        public object GetMessages(string where, bool mark, string mid, string after, string before, bool includeCategories, int count = 0,
            int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("message/" + where);

            restRequest.AddParameter("mark", mark);
            restRequest.AddParameter("mid", mid);
            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
