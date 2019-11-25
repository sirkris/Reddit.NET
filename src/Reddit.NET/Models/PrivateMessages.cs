using Newtonsoft.Json;
using Reddit.Inputs.PrivateMessages;
using Reddit.Things;
using RestSharp;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class PrivateMessages : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public PrivateMessages(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        // Tested this one manually because there's no way to unblock a user via the API and having one test user blocking the other will cause tests to fail.  --Kris
        /// <summary>
        /// For blocking the author of a thing via inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void Block(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/block", id));
        }

        /// <summary>
        /// For asynchronously blocking the author of a thing via inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public async Task BlockAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/block", id));
        }

        /// <summary>
        /// Collapse a message.
        /// See also: /api/uncollapse_message
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void CollapseMessage(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/collapse_message", id));
        }

        /// <summary>
        /// Collapse a message asynchronously.
        /// See also: /api/uncollapse_message
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public async Task CollapseMessageAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/collapse_message", id));
        }

        /// <summary>
        /// Handles message composition under /message/compose.
        /// </summary>
        /// <param name="privateMessagesComposeInput">A valid PrivateMessagesComposeInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <returns>A generic response object containing any errors.</returns>
        public GenericContainer Compose(PrivateMessagesComposeInput privateMessagesComposeInput, string gRecaptchaResponse = "")
        {
            return SendRequest<GenericContainer>("api/compose", privateMessagesComposeInput, Method.POST);
        }

        /// <summary>
        /// Handles message composition asynchronously under /message/compose.
        /// </summary>
        /// <param name="privateMessagesComposeInput">A valid PrivateMessagesComposeInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <returns>A generic response object containing any errors.</returns>
        public async Task<GenericContainer> ComposeAsync(PrivateMessagesComposeInput privateMessagesComposeInput, string gRecaptchaResponse = "")
        {
            return await SendRequestAsync<GenericContainer>("api/compose", privateMessagesComposeInput, Method.POST);
        }

        /// <summary>
        /// Delete messages from the recipient's view of their inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void DelMsg(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/del_msg", id));
        }

        /// <summary>
        /// Delete messages from the recipient's view of their inbox asynchronously.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public async Task DelMsgAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/del_msg", id));
        }

        /// <summary>
        /// Queue up marking all messages for a user as read.
        /// This may take some time, and returns 202 to acknowledge acceptance of the request.
        /// </summary>
        /// <param name="filterTypes">A comma-separated list of items</param>
        public void ReadAllMessages(string filterTypes)
        {
            ExecuteRequest(PrepareReadAllMessages(filterTypes));
        }

        /// <summary>
        /// Asynchronously queue up marking all messages for a user as read.
        /// This may take some time, and returns 202 to acknowledge acceptance of the request.
        /// </summary>
        /// <param name="filterTypes">A comma-separated list of items</param>
        public async Task ReadAllMessagesAsync(string filterTypes)
        {
            await ExecuteRequestAsync(PrepareReadAllMessages(filterTypes));
        }

        private RestRequest PrepareReadAllMessages(string filterTypes)
        {
            RestRequest restRequest = PrepareRequest("api/read_all_messages", Method.POST);

            restRequest.AddParameter("filter_types", filterTypes);

            return restRequest;
        }

        /// <summary>
        /// Mark a message as read.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void ReadMessage(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/read_message", id));
        }

        /// <summary>
        /// Mark a message as read asynchronously.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public async Task ReadMessageAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/read_message", id));
        }

        // TODO - Reddit API returns 500 server error.  No idea why.
        /// <summary>
        /// Unblock a subreddit.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnblockSubreddit(string id)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(PrepareIDRequest("api/unblock_subreddit", id)));
        }

        /// <summary>
        /// Uncollapse a message.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void UncollapseMessage(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/uncollapse_message", id));
        }

        /// <summary>
        /// Uncollapse a message asynchronously.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public async Task UncollapseMessageAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/uncollapse_message", id));
        }

        /// <summary>
        /// Mark a message as unread.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void UnreadMessage(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/unread_message", id));
        }

        /// <summary>
        /// Mark a message as unread asynchronously.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public async Task UnreadMessageAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/unread_message", id));
        }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (inbox, unread, sent)</param>
        /// <param name="privateMessagesGetMessagesInput">A valid PrivateMessagesGetMessagesInput instance</param>
        /// <returns>A list of messages.</returns>
        public MessageContainer GetMessages(string where, PrivateMessagesGetMessagesInput privateMessagesGetMessagesInput)
        {
            return SendRequest<MessageContainer>("message/" + where, privateMessagesGetMessagesInput);
        }
    }
}
