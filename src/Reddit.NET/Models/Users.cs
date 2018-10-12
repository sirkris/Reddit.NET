using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Users : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Users(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public object BlockUser(string accountId, string name)
        {
            RestRequest restRequest = PrepareRequest("api/block_user", Method.POST);

            restRequest.AddParameter("account_id", accountId);
            restRequest.AddParameter("name", name);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="banContext"></param>
        /// <param name="banMessage"></param>
        /// <param name="banReason"></param>
        /// <param name="container"></param>
        /// <param name="duration"></param>
        /// <param name="name"></param>
        /// <param name="permissions"></param>
        /// <param name="type"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object Friend(string banContext, string banMessage, string banReason, string container, int duration, string name,
            string permissions, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/friend", Method.POST);

            restRequest.AddParameter("ban_context", banContext);
            restRequest.AddParameter("ban_message", banMessage);
            restRequest.AddParameter("ban_reason", banReason);
            restRequest.AddParameter("container", container);
            restRequest.AddParameter("duration", duration);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("permissions", permissions);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="details"></param>
        /// <param name="reason"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public object ReportUser(string details, string reason, string user)
        {
            RestRequest restRequest = PrepareRequest("api/report_user", Method.POST);

            restRequest.AddParameter("details", details);
            restRequest.AddParameter("reason", reason);
            restRequest.AddParameter("user", user);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="permissions"></param>
        /// <param name="type"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object SetPermissions(string name, string permissions, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/setpermissions", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("permissions", permissions);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object Unfriend(string container, string id, string name, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/unfriend", Method.POST);

            restRequest.AddParameter("container", container);
            restRequest.AddParameter("id", id);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("type", type);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public object UserDataByAccountIds(string ids)
        {
            RestRequest restRequest = PrepareRequest("api/user_data_by_account_ids");

            restRequest.AddParameter("ids", ids);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object UsernameAvailable(string user)
        {
            RestRequest restRequest = PrepareRequest("api/username_available");

            restRequest.AddParameter("user", user);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public object DeleteFriend(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/friends/" + username, Method.DELETE));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public object GetFriend(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/friends/" + username));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public object UpdateFriend(string username, string json)
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/friends/" + username, Method.PUT);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public object Trophies(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/user/" + username + "/trophies"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public object About(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("user/" + username + "/about"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="where"></param>
        /// <param name="context"></param>
        /// <param name="show"></param>
        /// <param name="sort"></param>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <param name="includeCategories"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="srDetail"></param>
        /// <returns></returns>
        public ListingContainer History(string username, string where, int context, string show, string sort, string t, string type,
            string after, string before, bool includeCategories, int count = 0, int limit = 25, bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("user/" + username + "/" + where);

            restRequest.AddParameter("context", context);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("t", t);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("sr_detail", srDetail);
            
            return JsonConvert.DeserializeObject<ListingContainer>(ExecuteRequest(restRequest));
        }
    }
}
