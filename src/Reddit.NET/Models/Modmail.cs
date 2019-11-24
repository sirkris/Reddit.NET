using Newtonsoft.Json;
using Reddit.Inputs.Modmail;
using Reddit.Things;
using RestSharp;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class Modmail : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Modmail(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        // TODO - This endpoint keeps returning 404.  No idea why.  Tried with both DisplayName and fullname.  --Kris
        /// <summary>
        /// Marks all conversations read for a particular conversation state within the passed list of subreddits.
        /// </summary>
        /// <param name="modmailBulkReadInput">A valid ModmailBulkReadInput instance</param>
        /// <returns>(TODO - Untested)</returns>
        public object BulkRead(ModmailBulkReadInput modmailBulkReadInput)
        {
            return SendRequest<object>("api/mod/bulk_read", modmailBulkReadInput, Method.POST);
        }

        /// <summary>
        /// Get conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="modmailGetConversationsInput">A valid ModmailGetConversationsInput instance</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetConversations(ModmailGetConversationsInput modmailGetConversationsInput)
        {
            return SendRequest<ConversationContainer>("api/mod/conversations", modmailGetConversationsInput);
        }

        /// <summary>
        /// Creates a new conversation for a particular SR.
        /// This endpoint will create a ModmailConversation object as well as the first ModmailMessage within the ModmailConversation object.
        /// </summary>
        /// <param name="modmailNewConversationInput">A valid ModmailNewConversationInput instance</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer NewConversation(ModmailNewConversationInput modmailNewConversationInput)
        {
            return SendRequest<ModmailConversationContainer>("api/mod/conversations", modmailNewConversationInput, Method.POST);
        }

        /// <summary>
        /// Creates a new conversation for a particular SR asynchronously.
        /// This endpoint will create a ModmailConversation object as well as the first ModmailMessage within the ModmailConversation object.
        /// </summary>
        /// <param name="modmailNewConversationInput">A valid ModmailNewConversationInput instance</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> NewConversationAsync(ModmailNewConversationInput modmailNewConversationInput)
        {
            return await SendRequestAsync<ModmailConversationContainer>("api/mod/conversations", modmailNewConversationInput, Method.POST);
        }

        /// <summary>
        /// Returns all messages, mod actions and conversation metadata for a given conversation id.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="markRead">boolean value</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer GetConversation(string conversationId, bool markRead)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/" + conversationId);

            restRequest.AddParameter("markRead", markRead);
            
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Creates a new message for a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="modmailNewMessageInput">A valid ModmailNewMessageInput instance</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer NewMessage(string conversationId, ModmailNewMessageInput modmailNewMessageInput)
        {
            return SendRequest<ModmailConversationContainer>("api/mod/conversations/" + conversationId, modmailNewMessageInput, Method.POST);
        }

        /// <summary>
        /// Creates a new message for a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="modmailNewMessageInput">A valid ModmailNewMessageInput instance</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> NewMessageAsync(string conversationId, ModmailNewMessageInput modmailNewMessageInput)
        {
            return await SendRequestAsync<ModmailConversationContainer>("api/mod/conversations/" + conversationId, modmailNewMessageInput, Method.POST);
        }

        // TODO - Keeps returning 422, saying the conversation is not archivable without any indication as to why.  --Kris
        /// <summary>
        /// Marks a conversation as archived.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object ArchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/archive", Method.POST));
        }

        /// <summary>
        /// Removes a highlight from a conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer RemoveHighlight(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.DELETE));
        }

        /// <summary>
        /// Removes a highlight from a conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> RemoveHighlightAsync(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(await ExecuteRequestAsync("api/mod/conversations/" + conversationId + "/highlight", Method.DELETE));
        }

        /// <summary>
        /// Marks a conversation as highlighted.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer MarkHighlighted(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(ExecuteRequest("api/mod/conversations/" + conversationId + "/highlight", Method.POST));
        }

        /// <summary>
        /// Marks a conversation as highlighted asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> MarkHighlightedAsync(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(await ExecuteRequestAsync("api/mod/conversations/" + conversationId + "/highlight", Method.POST));
        }

        /// <summary>
        /// Mutes the non-mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer Mute(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(ExecuteRequest("api/mod/conversations/" + conversationId + "/mute", Method.POST));
        }

        /// <summary>
        /// Asynchronously mutes the non-mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> MuteAsync(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(await ExecuteRequestAsync("api/mod/conversations/" + conversationId + "/mute", Method.POST));
        }

        // TODO - Will test when I can get the archive endpoint to work.  --Kris
        /// <summary>
        /// Marks conversation as unarchived.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnarchiveConversation(string conversationId)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/mod/conversations/" + conversationId + "/unarchive", Method.POST));
        }

        /// <summary>
        /// Unmutes the non mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer UnMute(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(ExecuteRequest("api/mod/conversations/" + conversationId + "/unmute", Method.POST));
        }

        /// <summary>
        /// Asynchronously unmutes the non mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> UnMuteAsync(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailConversationContainer>(await ExecuteRequestAsync("api/mod/conversations/" + conversationId + "/unmute", Method.POST));
        }

        /// <summary>
        /// Returns recent posts, comments and modmail conversations for the user that started this conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the user data.</returns>
        public ModmailUser User(string conversationId)
        {
            return JsonConvert.DeserializeObject<ModmailUser>(ExecuteRequest("api/mod/conversations/" + conversationId + "/user"));
        }

        /// <summary>
        /// Marks conversations as read for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public void MarkRead(string conversationIds)
        {
            ExecuteRequest(PrepareMarkRead(conversationIds));
        }

        /// <summary>
        /// Asynchronously marks conversations as read for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public async Task MarkReadAsync(string conversationIds)
        {
            await ExecuteRequestAsync(PrepareMarkRead(conversationIds));
        }

        private RestRequest PrepareMarkRead(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/read", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return restRequest;
        }

        /// <summary>
        /// Returns a list of srs that the user moderates that are also enrolled in the new modmail.
        /// </summary>
        /// <returns>A list of subreddits.</returns>
        public ModmailSubredditContainer Subreddits()
        {
            return JsonConvert.DeserializeObject<ModmailSubredditContainer>(ExecuteRequest("api/mod/conversations/subreddits"));
        }

        /// <summary>
        /// Marks conversations as unread for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public void MarkUnread(string conversationIds)
        {
            ExecuteRequest(PrepareMarkUnread(conversationIds));
        }

        /// <summary>
        /// Asynchronously marks conversations as unread for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public async Task MarkUnreadAsync(string conversationIds)
        {
            await ExecuteRequestAsync(PrepareMarkUnread(conversationIds));
        }

        private RestRequest PrepareMarkUnread(string conversationIds)
        {
            RestRequest restRequest = PrepareRequest("api/mod/conversations/unread", Method.POST);

            restRequest.AddParameter("conversationIds", conversationIds);

            return restRequest;
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
