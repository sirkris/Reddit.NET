using Newtonsoft.Json;
using Reddit.Inputs.PrivateMessages;
using Reddit.Things;
using RestSharp;

namespace Reddit.Models
{
    public class PrivateMessages : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public PrivateMessages(string appId, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, refreshToken, accessToken, ref restClient, deviceId) { }

        // Tested this one manually because there's no way to unblock a user via the API and having one test user blocking the other will cause tests to fail.  --Kris
        /// <summary>
        /// For blocking the author of a thing via inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void Block(string id)
        {
            RestRequest restRequest = PrepareRequest("api/block", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Collapse a message.
        /// See also: /api/uncollapse_message
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void CollapseMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/collapse_message", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
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
        /// Delete messages from the recipient's view of their inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void DelMsg(string id)
        {
            RestRequest restRequest = PrepareRequest("api/del_msg", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Queue up marking all messages for a user as read.
        /// This may take some time, and returns 202 to acknowledge acceptance of the request.
        /// </summary>
        /// <param name="filterTypes">A comma-separated list of items</param>
        public void ReadAllMessages(string filterTypes)
        {
            RestRequest restRequest = PrepareRequest("api/read_all_messages", Method.POST);

            restRequest.AddParameter("filter_types", filterTypes);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Mark a message as read.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void ReadMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/read_message", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        // TODO - Reddit API returns 500 server error.  No idea why.
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

        /// <summary>
        /// Uncollapse a message.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void UncollapseMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/uncollapse_message", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Mark a message as unread.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        public void UnreadMessage(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unread_message", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
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
