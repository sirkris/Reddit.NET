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

        public object BulkRead(string entity, string state)
        {
            RestRequest restRequest = PrepareRequest("api/mod/bulk_read", Method.POST);

            restRequest.AddParameter("entity", entity);
            restRequest.AddParameter("state", state);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

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

        public object GetConversation(string conversationId, bool markRead)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/" + conversationId);

            restRequest.AddParameter("markRead", markRead);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object NewMessage(string conversationId, string body, bool isAuthorHidden, bool isInternal)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/" + conversationId, Method.POST);

            restRequest.AddParameter("body", body);
            restRequest.AddParameter("isAuthorHidden", isAuthorHidden);
            restRequest.AddParameter("isInternal", isInternal);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object ArchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/archive", Method.POST));
        }

        public object RemoveHighlight(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.DELETE));
        }

        public object MarkHighlighted(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.POST));
        }

        public object Mute(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/mute", Method.POST));
        }

        public object UnarchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/unarchive", Method.POST));
        }

        public object UnMute(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/unmute", Method.POST));
        }

        public object User(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/user"));
        }

        public object MarkRead(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/read", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Subreddits()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/subreddits"));
        }

        public object MarkUnread(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/unread", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object UnreadCount()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/unread/count"));
        }
    }
}
