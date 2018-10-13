using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class LinksAndComments : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public LinksAndComments(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnRtjson"></param>
        /// <param name="richtextJson"></param>
        /// <param name="text"></param>
        /// <param name="thingId"></param>
        /// <returns></returns>
        public object Comment(bool returnRtjson, string richtextJson, string text, string thingId)
        {
            RestRequest restRequest = PrepareRequest("api/comment", Method.POST);

            restRequest.AddParameter("return_rtjson", returnRtjson);
            restRequest.AddParameter("richtext_json", richtextJson);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("thing_id", thingId);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Delete(string id)
        {
            RestRequest restRequest = PrepareRequest("api/del", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnRtjson"></param>
        /// <param name="richtextJson"></param>
        /// <param name="text"></param>
        /// <param name="thingId"></param>
        /// <returns>(TODO - Untested)</returns>
        public object EditUserText(bool returnRtjson, string richtextJson, string text, string thingId)
        {
            RestRequest restRequest = PrepareRequest("api/editusertext", Method.POST);

            restRequest.AddParameter("return_rtjson", returnRtjson);
            restRequest.AddParameter("richtext_json", richtextJson);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("thing_id", thingId);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Hide(string id)
        {
            RestRequest restRequest = PrepareRequest("api/hide", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subreddit"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Info(string id, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/info");

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Lock(string id)
        {
            RestRequest restRequest = PrepareRequest("api/lock", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkNSFW(string id)
        {
            RestRequest restRequest = PrepareRequest("api/marknsfw", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="children"></param>
        /// <param name="limitChildren"></param>
        /// <param name="linkId"></param>
        /// <param name="sort"></param>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object MoreChildren(string children, bool limitChildren, string linkId, string sort, string id = null)
        {
            RestRequest restRequest = PrepareRequest("api/morechildren");

            restRequest.AddParameter("children", children);
            restRequest.AddParameter("limit_children", limitChildren);
            restRequest.AddParameter("link_id", linkId);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("id", id);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="additionalInfo"></param>
        /// <param name="banEvadingAccountsNames"></param>
        /// <param name="customText"></param>
        /// <param name="fromHelpCenter"></param>
        /// <param name="otherReason"></param>
        /// <param name="reason"></param>
        /// <param name="ruleReason"></param>
        /// <param name="siteReason"></param>
        /// <param name="srName"></param>
        /// <param name="thingId"></param>
        /// <param name="violatorUsername"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Report(string additionalInfo, string banEvadingAccountsNames, string customText, bool fromHelpCenter,
            string otherReason, string reason, string ruleReason, string siteReason, string srName, string thingId,
            string violatorUsername)
        {
            RestRequest restRequest = PrepareRequest("api/report", Method.POST);

            restRequest.AddParameter("additional_info", additionalInfo);
            restRequest.AddParameter("ban_evading_accounts_names", banEvadingAccountsNames);
            restRequest.AddParameter("custom_text", customText);
            restRequest.AddParameter("from_help_center", fromHelpCenter);
            restRequest.AddParameter("other_reason", otherReason);
            restRequest.AddParameter("reason", reason);
            restRequest.AddParameter("rule_reason", ruleReason);
            restRequest.AddParameter("site_reason", siteReason);
            restRequest.AddParameter("sr_name", srName);
            restRequest.AddParameter("thing_id", thingId);
            restRequest.AddParameter("violator_username", violatorUsername);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Save(string category, string id)
        {
            RestRequest restRequest = PrepareRequest("api/store_visits", Method.POST);

            restRequest.AddParameter("category", category);
            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <returns>(TODO - Untested)</returns>
        public object SavedCategories()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/saved_categories"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns>(TODO - Untested)</returns>
        public object SendReplies(string id, bool state)
        {
            RestRequest restRequest = PrepareRequest("api/sendreplies", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("state", state);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns>(TODO - Untested)</returns>
        public object SetContestMode(string id, bool state)
        {
            RestRequest restRequest = PrepareRequest("api/set_contest_mode", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("state", state);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num"></param>
        /// <param name="state"></param>
        /// <param name="toProfile"></param>
        /// <returns>(TODO - Untested)</returns>
        public object SetSubredditSticky(string id, int num, bool state, bool toProfile)
        {
            RestRequest restRequest = PrepareRequest("api/set_subreddit_sticky", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("num", num);
            restRequest.AddParameter("state", state);
            restRequest.AddParameter("to_profile", toProfile);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort"></param>
        /// <returns>(TODO - Untested)</returns>
        public object SetSuggestedSort(string id, string sort)
        {
            RestRequest restRequest = PrepareRequest("api/set_suggested_sort", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Spoiler(string id)
        {
            RestRequest restRequest = PrepareRequest("api/spoiler", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="links"></param>
        /// <returns>(TODO - Untested)</returns>
        public object StoreVisits(string links)
        {
            RestRequest restRequest = PrepareRequest("api/store_visits", Method.POST);

            restRequest.AddParameter("links", links);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ad"></param>
        /// <param name="app"></param>
        /// <param name="extension"></param>
        /// <param name="flairId"></param>
        /// <param name="flairText"></param>
        /// <param name="gRecaptchaResopnse"></param>
        /// <param name="kind"></param>
        /// <param name="nsfw"></param>
        /// <param name="resubmit"></param>
        /// <param name="richtextJson"></param>
        /// <param name="sendReplies"></param>
        /// <param name="spoiler"></param>
        /// <param name="sr"></param>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="videoPosterUrl"></param>
        /// <returns></returns>
        public object Submit(bool ad, string app, string extension, string flairId, string flairText, string gRecaptchaResopnse,
            string kind, bool nsfw, bool resubmit, string richtextJson, bool sendReplies, bool spoiler, string sr, string text,
            string title, string url, string videoPosterUrl)
        {
            RestRequest restRequest = PrepareRequest("api/submit", Method.POST);

            restRequest.AddParameter("ad", ad);
            restRequest.AddParameter("app", app);
            restRequest.AddParameter("extension", extension);
            restRequest.AddParameter("flair_id", flairId);
            restRequest.AddParameter("flair_text", flairText);
            restRequest.AddParameter("g-recaptcha-response", gRecaptchaResopnse);
            restRequest.AddParameter("kind", kind);
            restRequest.AddParameter("nsfw", nsfw);
            restRequest.AddParameter("resubmit", resubmit);
            restRequest.AddParameter("richtext_json", richtextJson);
            restRequest.AddParameter("sendreplies", sendReplies);
            restRequest.AddParameter("spoiler", spoiler);
            restRequest.AddParameter("sr", sr);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("title", title);
            restRequest.AddParameter("url", url);
            restRequest.AddParameter("video_poster_url", videoPosterUrl);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Unhide(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unhide", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Unlock(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unlock", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object UnmarkNSFW(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unmarknsfw", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Unsave(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unsave", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Unspoiler(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unspoiler", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // WARNING:  Automated bot-voting violates Reddit's rules.  --Kris
        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="id"></param>
        /// <param name="rank"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Vote(int dir, string id, int rank)
        {
            RestRequest restRequest = PrepareRequest("api/vote", Method.POST);

            restRequest.AddParameter("dir", dir);
            restRequest.AddParameter("id", id);
            restRequest.AddParameter("rank", rank);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
