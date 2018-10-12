using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Flair : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Flair(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// Clear flair templates.
        /// </summary>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object ClearFlairTemplates(string flairType, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/clearflairtemplates", Method.POST);

            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Delete flair.
        /// </summary>
        /// <param name="name">a user by name</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteFlair(string name, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflair", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Delete flair template.
        /// </summary>
        /// <param name="flairTemplateId"></param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteFlairTemplate(string flairTemplateId, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflairtemplate", Method.POST);

            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Create a new flair.
        /// </summary>
        /// <param name="cssClass">a valid subreddit image name</param>
        /// <param name="link">a fullname of a link</param>
        /// <param name="name">a user by name</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object Create(string cssClass, string link, string name, string text, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flair", Method.POST);

            restRequest.AddParameter("css_class", cssClass);
            restRequest.AddParameter("link", link);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Update the order of flair templates in the specified subreddit.
        /// Order should contain every single flair id for that flair type; omitting any id will result in a loss of data.
        /// </summary>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="subreddit">subreddit name</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairTemplateOrder(string flairType, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flair_template_order", Method.PATCH);

            restRequest.AddParameter("flair_type", flairType);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Flair config.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        /// <param name="flairPosition">one of (left, right)</param>
        /// <param name="flairSelfAssignEnabled">boolean value</param>
        /// <param name="linkFlairPosition">one of (left, right)</param>
        /// <param name="linkFlairSelfAssignEnabled">boolean value</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairConfig(bool flairEnabled, string flairPosition, bool flairSelfAssignEnabled, string linkFlairPosition, bool linkFlairSelfAssignEnabled,
            string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairconfig", Method.POST);

            restRequest.AddParameter("flair_enabled", flairEnabled);
            restRequest.AddParameter("flair_position", flairPosition);
            restRequest.AddParameter("flair_self_assign_enabled", flairSelfAssignEnabled);
            restRequest.AddParameter("link_flair_position", linkFlairPosition);
            restRequest.AddParameter("link_flair_self_assign_enabled", linkFlairSelfAssignEnabled);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// Requires a string 'flair_csv' which has up to 100 lines of the form 'user,flairtext,cssclass' (Lines beyond the 100th are ignored).
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">comma-seperated flair information</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairCSV(string flairCsv, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flaircsv", Method.POST);

            restRequest.AddParameter("flair_csv", flairCsv);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="name">a user by name</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 1000)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairList(string after, string before, string name, string subreddit = null, int count = 0, int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairlist");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Return information about a users's flair options.
        /// If link is given, return link flair options.Otherwise, return user flair options for this subreddit.
        /// The logged in user's flair is also returned. Subreddit moderators may give a user by name to instead retrieve that user's flair.
        /// </summary>
        /// <param name="name">a user by name</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <param name="link">a fullname of a link</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairSelector(string name, string subreddit = null, string link = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairselector", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("link", link);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Flair template.
        /// </summary>
        /// <param name="cssClass">a valid subreddit image name</param>
        /// <param name="flairTemplateId"></param>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairTemplate(string cssClass, string flairTemplateId, string flairType, string text, bool textEditable, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairtemplate", Method.POST);

            restRequest.AddParameter("css_class", cssClass);
            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("text_editable", textEditable);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Create or update a flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="flairTemplateId"></param>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="modOnly">boolean value</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairTemplateV2(string backgroundColor, string flairTemplateId, string flairType, bool modOnly, string text, string textColor,
            bool textEditable, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flairtemplate_v2", Method.POST);

            restRequest.AddParameter("background_color", backgroundColor);
            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("mod_only", modOnly);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("text_color", textColor);
            restRequest.AddParameter("text_editable", textEditable);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object LinkFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/link_flair"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object LinkFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/link_flair_v2"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Select flair.
        /// </summary>
        /// <param name="backgroundColor">	
        /// <param name="flairTemplateId">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="link">a fullname of a link</param>
        /// <param name="name">a user by name</param>
        /// <param name="returnRtson">[all|only|none]: "all" saves attributes and returns rtjson, "only" only returns rtjson, and "none" only saves attributes</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object SelectFlair(string backgroundColor, string flairTemplateId, string link, string name, string returnRtson, string text,
            string textColor, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/selectflair", Method.POST);

            restRequest.AddParameter("background_color", backgroundColor);
            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("link", link);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("return_rtson", returnRtson);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("text_color", textColor);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Set flair enabled.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>(TODO - Untested)</returns>
        public object SetFlairEnabled(bool flairEnabled, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/setflairenabled", Method.POST);

            restRequest.AddParameter("flair_enabled", flairEnabled);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>List of available user flairs.</returns>
        public object UserFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/user_flair"));
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// If user is not a mod of the subreddit, this endpoint filters out mod_only templates.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>List of available user flairs.</returns>
        public object UserFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/user_flair_v2"));
        }
    }
}
