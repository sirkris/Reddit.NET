using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Modmail : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Modmail(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        /// <returns>(TODO - Untested)</returns>
        public object BulkRead(string entity, string state)
        {
            RestRequest restRequest = PrepareRequest("api/mod/bulk_read", Method.POST);

            restRequest.AddParameter("entity", entity);
            restRequest.AddParameter("state", state);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="entity"></param>
        /// <param name="sort"></param>
        /// <param name="state"></param>
        /// <param name="limit"></param>
        /// <returns>(TODO - Untested)</returns>
        public object GetConversations(string after, string entity, string sort, string state, int limit = 25)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("entity", entity);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("state", state);
            restRequest.AddParameter("limit", limit);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="srName"></param>
        /// <param name="subject"></param>
        /// <param name="to"></param>
        /// <returns>(TODO - Untested)</returns>
        public object NewConversation(string body, bool isAuthorHidden, string srName, string subject, string to)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations", Method.POST);

            restRequest.AddParameter("body", body);
            restRequest.AddParameter("isAuthorHidden", isAuthorHidden);
            restRequest.AddParameter("srName", srName);
            restRequest.AddParameter("subject", subject);
            restRequest.AddParameter("to", to);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="markRead">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetConversation(string conversationId, bool markRead)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/" + conversationId);

            restRequest.AddParameter("markRead", markRead);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="body"></param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="isInternal">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object NewMessage(string conversationId, string body, bool isAuthorHidden, bool isInternal)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/" + conversationId, Method.POST);

            restRequest.AddParameter("body", body);
            restRequest.AddParameter("isAuthorHidden", isAuthorHidden);
            restRequest.AddParameter("isInternal", isInternal);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object ArchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/archive", Method.POST));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object RemoveHighlight(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.DELETE));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkHighlighted(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.POST));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Mute(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/mute", Method.POST));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object UnarchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/unarchive", Method.POST));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object UnMute(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/unmute", Method.POST));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object User(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/user"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationIds"></param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkRead(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/read", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Subreddits()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/subreddits"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationIds"></param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkUnread(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/unread", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object UnreadCount()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/unread/count"));
        }
    }
}
