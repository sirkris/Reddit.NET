using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Moderation : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Moderation(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="subreddit"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="mod"></param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetLog(string after, string before, string subreddit = null, int count = 0, int limit = 25, string mod = null, string show = "all",
            bool srDetail = false, string type = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "about/log");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("mod", mod);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);
            restRequest.AddParameter("type", type);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="only"></param>
        /// <param name="subreddit"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns></returns>
        public object ModQueue(string location, string after, string before, string only, string subreddit = null, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "about/" + location);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("only", only);
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
        /// <param name="subreddit"></param>
        /// <returns>(TODO - Untested)</returns>
        public object AcceptModeratorInvite(string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/accept_moderator_invite", Method.POST);

            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns></returns>
        public object Approve(string id)
        {
            RestRequest restRequest = PrepareRequest("api/approve", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="how"></param>
        /// <param name="id">fullname of a thing</param>
        /// <param name="sticky">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object Distinguish(string how, string id, bool sticky)
        {
            RestRequest restRequest = PrepareRequest("api/distinguish", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("how", how);
            restRequest.AddParameter("sticky", sticky);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object IgnoreReports(string id)
        {
            RestRequest restRequest = PrepareRequest("api/ignore_reports", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object LeaveContributor(string id)
        {
            RestRequest restRequest = PrepareRequest("api/leavecontributor", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object LeaveModerator(string id)
        {
            RestRequest restRequest = PrepareRequest("api/leavemoderator", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object MuteMessageAuthor(string id)
        {
            RestRequest restRequest = PrepareRequest("api/mute_message_author", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <param name="spam">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object Remove(string id, bool spam)
        {
            RestRequest restRequest = PrepareRequest("api/remove", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("spam", spam);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnignoreReports(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unignore_reports", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnmuteMessageAuthor(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unmute_message_author", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subreddit"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Stylesheet(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/stylesheet"));
        }
    }
}
