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
        /// For blocking the author of a thing via inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object Block(string id)
        {
            RestRequest restRequest = PrepareRequest("api/block", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Collapse a message
        /// See also: /api/uncollapse_message
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object CollapseMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/collapse_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Handles message composition under /message/compose.
        /// </summary>
        /// <param name="fromSr">subreddit name</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="to">the name of an existing user</param>
        /// <returns>(TODO - Untested)</returns>
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
        /// Delete messages from the recipient's view of their inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object DelMsg(string id)
        {
            RestRequest restRequest = PrepareRequest("api/del_msg", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Queue up marking all messages for a user as read.
        /// This may take some time, and returns 202 to acknowledge acceptance of the request.
        /// </summary>
        /// <param name="filterTypes">A comma-separated list of items</param>
        /// <returns>(TODO - Untested)</returns>
        public object ReadAllMessages(string filterTypes)
        {
            RestRequest restRequest = PrepareRequest("api/read_all_messages", Method.POST);

            restRequest.AddParameter("filter_types", filterTypes);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Mark a message as read.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object ReadMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/read_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Unblock a subreddit.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnblockSubreddit(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unblock_subreddit", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Uncollapse a message.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object UncollapseMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/uncollapse_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Mark a message as unread.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnreadMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unread_message", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (inbox, unread, sent)</param>
        /// <param name="mark">one of (true, false)</param>
        /// <param name="mid"></param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>(TODO - Untested)</returns>
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
