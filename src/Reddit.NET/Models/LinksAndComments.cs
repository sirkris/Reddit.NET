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

        public object Delete(string id)
        {
            RestRequest restRequest = PrepareRequest("api/del", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

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

        public object Hide(string id)
        {
            RestRequest restRequest = PrepareRequest("api/hide", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Info(string id, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/info");

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Lock(string id)
        {
            RestRequest restRequest = PrepareRequest("api/lock", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object MarkNSFW(string id)
        {
            RestRequest restRequest = PrepareRequest("api/marknsfw", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

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

        public object Save(string category, string id)
        {
            RestRequest restRequest = PrepareRequest("api/store_visits", Method.POST);

            restRequest.AddParameter("category", category);
            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object SavedCategories()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/saved_categories"));
        }

        public object SendReplies(string id, bool state)
        {
            RestRequest restRequest = PrepareRequest("api/sendreplies", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("state", state);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object SetContestMode(string id, bool state)
        {
            RestRequest restRequest = PrepareRequest("api/set_contest_mode", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("state", state);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

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

        public object SetSuggestedSort(string id, string sort)
        {
            RestRequest restRequest = PrepareRequest("api/set_suggested_sort", Method.POST);

            restRequest.AddParameter("id", id);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Spoiler(string id)
        {
            RestRequest restRequest = PrepareRequest("api/spoiler", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object StoreVisits(string links)
        {
            RestRequest restRequest = PrepareRequest("api/store_visits", Method.POST);

            restRequest.AddParameter("links", links);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

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

        public object Unhide(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unhide", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Unlock(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unlock", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object UnmarkNSFW(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unmarknsfw", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Unsave(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unsave", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Unspoiler(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unspoiler", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // WARNING:  Automated bot-voting violates Reddit's rules.  --Kris
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
