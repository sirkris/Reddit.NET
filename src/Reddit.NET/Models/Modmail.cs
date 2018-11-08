using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;

namespace Reddit.NET.Models
{
    public class Modmail : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Modmail(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - This endpoint keeps returning 404.  No idea why.  Tried with both DisplayName and fullname.  --Kris
        /// <summary>
        /// Marks all conversations read for a particular conversation state within the passed list of subreddits.
        /// </summary>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <returns>(TODO - Untested)</returns>
        public object BulkRead(string entity, string state)
        {
            RestRequest restRequest = PrepareRequest("api/mod/bulk_read", Method.POST);

            restRequest.AddParameter("entity", entity);
            restRequest.AddParameter("state", state);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Get conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="sort">one of (recent, mod, user, unread)</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetConversations(string after, string entity, string sort, string state, int limit = 25)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("entity", entity);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("state", state);
            restRequest.AddParameter("limit", limit);

            return JsonConvert.DeserializeObject<ConversationContainer>(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        // TODO - Requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Creates a new conversation for a particular SR.
        /// This endpoint will create a ModmailConversation object as well as the first ModmailMessage within the ModmailConversation object.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="srName">subreddit name</param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="to">Modmail conversation recipient fullname</param>
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
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Returns all messages, mod actions and conversation metadata for a given conversation id.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="markRead">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetConversation(string conversationId, bool markRead)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/" + conversationId);

            restRequest.AddParameter("markRead", markRead);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Creates a new message for a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="body">raw markdown text</param>
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
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Marks a conversation as archived.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object ArchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/archive", Method.POST));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Removes a highlight from a conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object RemoveHighlight(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.DELETE));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Marks a conversation as highlighted.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkHighlighted(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.POST));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Mutes the non mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object Mute(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/mute", Method.POST));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Marks conversation as unarchived.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnarchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/unarchive", Method.POST));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Unmutes the non mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnMute(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/unmute", Method.POST));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Returns recent posts, comments and modmail conversations for a given user.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object User(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/user"));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Marks a conversations as read for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkRead(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/read", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Returns a list of srs that the user moderates that are also enrolled in the new modmail.
        /// </summary>
        /// <returns>A list of subreddits.</returns>
        public ModmailSubredditContainer Subreddits()
        {
            return JsonConvert.DeserializeObject<ModmailSubredditContainer>(ExecuteRequest("api/mod/conversations/subreddits"));
        }

        // TODO - Needs testing.
        // TODO - Requires endpoint that requires subreddit fullname retrieval so put this in ControllerTests only.  --Kris
        /// <summary>
        /// Marks conversations as unread for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkUnread(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/unread", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Endpoint to retrieve the unread conversation count by conversation state.
        /// </summary>
        /// <returns>An object with the int properties: highlighted, notifications, archived, new, inprogress, and mod.</returns>
        public ModmailUnreadCount UnreadCount()
        {
            return JsonConvert.DeserializeObject<ModmailUnreadCount>(ExecuteRequest("api/mod/conversations/unread/count"));
        }
    }
}
