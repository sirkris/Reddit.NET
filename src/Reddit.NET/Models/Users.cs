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

        public object BlockUser(string accountId, string name)
        {
            RestRequest restRequest = PrepareRequest("api/block_user", Method.POST);

            restRequest.AddParameter("account_id", accountId);
            restRequest.AddParameter("name", name);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

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

        public object ReportUser(string details, string reason, string user)
        {
            RestRequest restRequest = PrepareRequest("api/report_user", Method.POST);

            restRequest.AddParameter("details", details);
            restRequest.AddParameter("reason", reason);
            restRequest.AddParameter("user", user);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object SetPermissions(string name, string permissions, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/setpermissions", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("permissions", permissions);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Unfriend(string container, string id, string name, string type, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/unfriend", Method.POST);

            restRequest.AddParameter("container", container);
            restRequest.AddParameter("id", id);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("type", type);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object UserDataByAccountIds(string ids)
        {
            RestRequest restRequest = PrepareRequest("api/user_data_by_account_ids");

            restRequest.AddParameter("ids", ids);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object UsernameAvailable(string user)
        {
            RestRequest restRequest = PrepareRequest("api/username_available");

            restRequest.AddParameter("user", user);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object DeleteFriend(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/friends/" + username, Method.DELETE));
        }

        public object GetFriend(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/me/friends/" + username));
        }

        public object UpdateFriend(string username, string json)
        {
            RestRequest restRequest = PrepareRequest("api/v1/me/friends/" + username, Method.PUT);

            restRequest.AddBody(json);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Trophies(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/v1/user/" + username + "/trophies"));
        }

        public object About(string username)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("user/" + username + "/about"));
        }

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
