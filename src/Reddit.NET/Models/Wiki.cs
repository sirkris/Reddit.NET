using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Wiki : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Wiki(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="username"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object AllowEditor(string page, string username, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/alloweditor/add", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("username", username);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="username"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object DenyEditor(string page, string username, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/alloweditor/del", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("username", username);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="page"></param>
        /// <param name="previous"></param>
        /// <param name="reason"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object Edit(string content, string page, string previous, string reason, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/edit", Method.POST);

            restRequest.AddParameter("content", content);
            restRequest.AddParameter("previous", previous);
            restRequest.AddParameter("reason", reason);

            return JsonConvert.DeserializeObject(ExecuteRequest(reason));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="revision"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object Hide(string page, string revision, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/hide", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("revision", revision);

            return JsonConvert.DeserializeObject(ExecuteRequest(revision));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="revision"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object Revert(string page, string revision, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/wiki/revert", Method.POST);

            restRequest.AddParameter("page", page);
            restRequest.AddParameter("revision", revision);

            return JsonConvert.DeserializeObject(ExecuteRequest(revision));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <param name="subreddit"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="show"></param>
        /// <param name="srDetail"></param>
        /// <returns></returns>
        public object Discussions(string page, string after, string before, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/discussions/" + page);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object Pages(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "wiki/pages"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <param name="subreddit"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="show"></param>
        /// <param name="srDetail"></param>
        /// <returns></returns>
        public object Revisions(string after, string before, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/revisions");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <param name="subreddit"></param>
        /// <param name="count"></param>
        /// <param name="limit"></param>
        /// <param name="show"></param>
        /// <param name="srDetail"></param>
        /// <returns></returns>
        public object PageRevisions(string page, string after, string before, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/revisions/" + page);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object GetPermissions(string page, string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "wiki/settings/" + page));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="listed"></param>
        /// <param name="permLevel"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object UpdatePermissions(string page, bool listed, int permLevel, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/settings/" + page, Method.POST);

            restRequest.AddParameter("listed", listed);
            restRequest.AddParameter("permlevel", permLevel);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="v"></param>
        /// <param name="v2"></param>
        /// <param name="subreddit"></param>
        /// <returns></returns>
        public object Page(string page, string v, string v2, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "wiki/" + page);

            restRequest.AddParameter("v", v);
            restRequest.AddParameter("v2", v2);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
