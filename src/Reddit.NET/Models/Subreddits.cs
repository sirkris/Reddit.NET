using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Subreddits : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Subreddits(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (banned, muted, wikibanned, contributors, wikicontributors, moderators)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>An object containing the requested data.</returns>
        public object About(string where, string after, string before, string user, bool includeCategories, string subreddit = null, int count = 0,
            int limit = 25, string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "about/" + where);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("user", user);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return information about the subreddit.
        /// Data includes the subscriber count, description, and header image.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>A subreddit listing.</returns>
        public SubredditChild About(string subreddit)
        {
            return JsonConvert.DeserializeObject<SubredditChild>(ExecuteRequest("r/" + subreddit + "/about"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove the subreddit's custom mobile banner.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteSrBanner(string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/delete_sr_banner", Method.POST);

            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove the subreddit's custom header image.
        /// The sitewide-default header image will be shown again after this call.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteSrHeader(string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/delete_sr_header", Method.POST);

            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove the subreddit's custom mobile icon.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteSrIcon(string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/delete_sr_icon", Method.POST);

            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove an image from the subreddit's custom image set.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="imgName">a valid subreddit image name</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteSrImg(string imgName, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/delete_sr_img", Method.POST);

            restRequest.AddParameter("img_name", imgName);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return subreddits recommended for the given subreddit(s).
        /// Gets a list of subreddits recommended for srnames, filtering out any that appear in the optional omit param.
        /// </summary>
        /// <param name="srNames">comma-delimited list of subreddit names</param>
        /// <param name="omit">comma-delimited list of subreddit names</param>
        /// <param name="over18">boolean value</param>
        /// <returns>A list of subreddits.</returns>
        public object Recommend(string srNames, string omit, bool over18)
        {
            RestRequest restRequest = PrepareRequest("api/recommend/sr/" + srNames);

            restRequest.AddParameter("omit", omit);
            restRequest.AddParameter("over_18", over18);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// List subreddit names that begin with a query string.
        /// Subreddits whose names begin with query will be returned.
        /// If include_over_18 is false, subreddits with over-18 content restrictions will be filtered from the results.
        /// If include_unadvertisable is False, subreddits that have hide_ads set to True or are on the anti_ads_subreddits list will be filtered.
        /// If exact is true, only an exact match will be returned.Exact matches are inclusive of over_18 subreddits, but not hide_ad subreddits when include_unadvertisable is False.
        /// </summary>
        /// <param name="exact">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeUnadvertisable">boolean value</param>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <returns>A list of subreddit names.</returns>
        public object SearchRedditNames(bool exact, bool includeOver18, bool includeUnadvertisable, string query)
        {
            RestRequest restRequest = PrepareRequest("api/search_reddit_names");

            restRequest.AddParameter("exact", exact);
            restRequest.AddParameter("include_over_18", includeOver18);
            restRequest.AddParameter("include_unadvertisable", includeUnadvertisable);
            restRequest.AddParameter("query", query);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// List subreddits that begin with a query string.
        /// Subreddits whose names begin with query will be returned.
        /// If include_over_18 is false, subreddits with over-18 content restrictions will be filtered from the results.
        /// If include_unadvertisable is False, subreddits that have hide_ads set to True or are on the anti_ads_subreddits list will be filtered.
        /// If exact is true, only an exact match will be returned.Exact matches are inclusive of over_18 subreddits, but not hide_ad subreddits when include_unadvertisable is False.
        /// </summary>
        /// <param name="exact">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeUnadvertisable">boolean value</param>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <returns>A list of subreddit listings.</returns>
        public object SearchSubreddits(bool exact, bool includeOver18, bool includeUnadvertisable, string query)
        {
            RestRequest restRequest = PrepareRequest("api/search_subreddits", Method.POST);

            restRequest.AddParameter("exact", exact);
            restRequest.AddParameter("include_over_18", includeOver18);
            restRequest.AddParameter("include_unadvertisable", includeUnadvertisable);
            restRequest.AddParameter("query", query);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Create or configure a subreddit.
        /// If sr is specified, the request will attempt to modify the specified subreddit. If not, a subreddit with name name will be created.
        /// This endpoint expects all values to be supplied on every request. If modifying a subset of options, it may be useful to get the current settings from /about/edit.json first.
        /// For backwards compatibility, description is the sidebar text and public_description is the publicly visible subreddit description.
        /// Most of the parameters for this endpoint are identical to options visible in the user interface and their meanings are best explained there.
        /// See also: /about/edit.json.
        /// </summary>
        /// <param name="allOriginalContent">boolean value</param>
        /// <param name="allowDiscovery">boolean value</param>
        /// <param name="allowImages">boolean value</param>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="allowVideos">boolean value</param>
        /// <param name="collapseDeletedComments">boolean value</param>
        /// <param name="description">raw markdown text</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle">a string no longer than 500 characters</param>
        /// <param name="hideAds">boolean value</param>
        /// <param name="keyColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="lang">a valid IETF language tag (underscore separated)</param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="name">subreddit name</param>
        /// <param name="originalContentTagEnabled">boolean value</param>
        /// <param name="over18">boolean value</param>
        /// <param name="publicDescription">raw markdown text</param>
        /// <param name="showMedia">boolean value</param>
        /// <param name="showMediaPreview">boolean value</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="spoilersEnabled">boolean value</param>
        /// <param name="sr">fullname of a thing</param>
        /// <param name="submitLinkLabel">a string no longer than 60 characters</param>
        /// <param name="submitText">raw markdown text</param>
        /// <param name="submitTextLabel">a string no longer than 60 characters</param>
        /// <param name="suggestedCommentSort">one of (confidence, top, new, controversial, old, random, qa, live)</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="title">a string no longer than 100 characters</param>
        /// <param name="type">one of (gold_restricted, archived, restricted, employees_only, gold_only, private, user, public)</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="commentScoreHideMins">an integer between 0 and 1440 (default: 0)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An object indicating any errors.</returns>
        public object SiteAdmin(bool allOriginalContent, bool allowDiscovery, bool allowImages, bool allowPostCrossposts, bool allowTop,
            bool allowVideos, bool collapseDeletedComments, string description, bool excludeBannedModqueue, bool freeFormReports,
            string gRecaptchaResponse, string headerTitle, bool hideAds, string keyColor, string lang, string linkType, string name, bool originalContentTagEnabled,
            bool over18, string publicDescription, bool showMedia, bool showMediaPreview, string spamComments, string spamLinks, string spamSelfPosts,
            bool spoilersEnabled, string sr, string submitLinkLabel, string submitText, string submitTextLabel, string suggestedCommentSort,
            string themeSr, bool themeSrUpdate, string title, string type, string wikiMode, int commentScoreHideMins = 0, int wikiEditAge = 0,
            int wikiEditKarma = 0)
        {
            RestRequest restRequest = PrepareRequest("api/site_admin", Method.POST);

            restRequest.AddParameter("all_original_content", allOriginalContent);
            restRequest.AddParameter("allow_discovery", allowDiscovery);
            restRequest.AddParameter("allow_images", allowImages);
            restRequest.AddParameter("allow_post_crossposts", allowPostCrossposts);
            restRequest.AddParameter("allow_top", allowTop);
            restRequest.AddParameter("allow_videos", allowVideos);
            restRequest.AddParameter("collapse_deleted_comments", collapseDeletedComments);
            restRequest.AddParameter("comment_score_hide_mins", commentScoreHideMins);
            restRequest.AddParameter("description", description);
            restRequest.AddParameter("exclude_banned_modqueue", excludeBannedModqueue);
            restRequest.AddParameter("free_form_reports", freeFormReports);
            restRequest.AddParameter("g-recaptcha-response", gRecaptchaResponse);
            restRequest.AddParameter("header-title", headerTitle);
            restRequest.AddParameter("hide_ads", hideAds);
            restRequest.AddParameter("key_color", keyColor);
            restRequest.AddParameter("lang", lang);
            restRequest.AddParameter("link_type", linkType);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("original_content_tag_enabled", originalContentTagEnabled);
            restRequest.AddParameter("over_18", over18);
            restRequest.AddParameter("public_description", publicDescription);
            restRequest.AddParameter("show_media", showMedia);
            restRequest.AddParameter("show_media_preview", showMediaPreview);
            restRequest.AddParameter("spam_comments", spamComments);
            restRequest.AddParameter("spam_links", spamLinks);
            restRequest.AddParameter("spam_selfposts", spamSelfPosts);
            restRequest.AddParameter("spoilers_enabled", spoilersEnabled);
            restRequest.AddParameter("sr", sr);
            restRequest.AddParameter("submit_link_label", submitLinkLabel);
            restRequest.AddParameter("submit_text", submitText);
            restRequest.AddParameter("submit_text_label", submitTextLabel);
            restRequest.AddParameter("suggested_comment_sort", suggestedCommentSort);
            restRequest.AddParameter("theme_sr", themeSr);
            restRequest.AddParameter("theme_sr_update", themeSrUpdate);
            restRequest.AddParameter("title", title);
            restRequest.AddParameter("type", type);
            restRequest.AddParameter("wiki_edit_age", wikiEditAge);
            restRequest.AddParameter("wiki_edit_karma", wikiEditKarma);
            restRequest.AddParameter("wikimode", wikiMode);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get the submission text for the subreddit.
        /// This text is set by the subreddit moderators and intended to be displayed on the submission form.
        /// See also: /api/site_admin.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object containing submission text.</returns>
        public object SubmitText(string subreddit = null)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "api/submit_text"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Return a list of subreddits and data for subreddits whose names start with 'query'.
        /// Uses typeahead endpoint to recieve the list of subreddits names. Typeahead provides exact matches, typo correction, fuzzy matching and boosts subreddits to the top that the user is subscribed to.
        /// </summary>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeProfiles">boolean value</param>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <returns>(TODO - Untested)</returns>
        public object SubredditAutocomplete(bool includeOver18, bool includeProfiles, string query)
        {
            RestRequest restRequest = PrepareRequest("api/subreddit_autocomplete");

            restRequest.AddParameter("include_over_18", includeOver18);
            restRequest.AddParameter("include_profiles", includeProfiles);
            restRequest.AddParameter("query", query);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Version 2 of SubredditAutocomplete.
        /// </summary>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeProfiles">boolean value</param>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="limit">an integer between 1 and 10 (default: 5)</param>
        /// <returns>(TODO - Untested)</returns>
        public object SubredditAutocompleteV2(bool includeCategories, bool includeOver18, bool includeProfiles, string query, int limit = 5)
        {
            RestRequest restRequest = PrepareRequest("api/subreddit_autocomplete_v2");

            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("include_over_18", includeOver18);
            restRequest.AddParameter("include_profiles", includeProfiles);
            restRequest.AddParameter("query", query);
            restRequest.AddParameter("limit", limit);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Update a subreddit's stylesheet.
        /// op should be save to update the contents of the stylesheet.
        /// </summary>
        /// <param name="op">one of (save, preview)</param>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="stylesheetContents">the new stylesheet content</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>(TODO - Untested)</returns>
        public object SubredditStylesheet(string op, string reason, string stylesheetContents, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/subreddit_stylesheet", Method.POST);

            restRequest.AddParameter("op", op);
            restRequest.AddParameter("reason", reason);
            restRequest.AddParameter("stylesheet_contents", stylesheetContents);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit.
        /// To subscribe, action should be sub.To unsubscribe, action should be unsub.The user must have access to the subreddit to be able to subscribe to it.
        /// The skip_initial_defaults param can be set to True to prevent automatically subscribing the user to the current set of defaults when they take their first subscription action.
        /// Attempting to set it for an unsubscribe action will result in an error.
        /// See also: /subreddits/mine/.
        /// </summary>
        /// <param name="action">one of (sub, unsub)</param>
        /// <param name="skipInitialDefaults">boolean value</param>
        /// <param name="sr">A comma-separated list of subreddit fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object SubscribeByFullname(string action, bool skipInitialDefaults, string sr)
        {
            RestRequest restRequest = PrepareRequest("api/subscribe", Method.POST);

            restRequest.AddParameter("action", action);
            restRequest.AddParameter("skip_initial_defaults", skipInitialDefaults);
            restRequest.AddParameter("sr", sr);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit.
        /// To subscribe, action should be sub.To unsubscribe, action should be unsub.The user must have access to the subreddit to be able to subscribe to it.
        /// The skip_initial_defaults param can be set to True to prevent automatically subscribing the user to the current set of defaults when they take their first subscription action.
        /// Attempting to set it for an unsubscribe action will result in an error.
        /// See also: /subreddits/mine/.
        /// </summary>
        /// <param name="action">one of (sub, unsub)</param>
        /// <param name="skipInitialDefaults">boolean value</param>
        /// <param name="srName">A comma-separated list of subreddit names</param>
        /// <returns>(TODO - Untested)</returns>
        public object Subscribe(string action, bool skipInitialDefaults, string srName)
        {
            RestRequest restRequest = PrepareRequest("api/subscribe", Method.POST);

            restRequest.AddParameter("action", action);
            restRequest.AddParameter("skip_initial_defaults", skipInitialDefaults);
            restRequest.AddParameter("sr_name", srName);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Add or replace a subreddit image, custom header logo, custom mobile icon, or custom mobile banner.
        /// If the upload_type value is img, an image for use in the subreddit stylesheet is uploaded with the name specified in name.
        /// If the upload_type value is header then the image uploaded will be the subreddit's new logo and name will be ignored.
        /// If the upload_type value is icon then the image uploaded will be the subreddit's new mobile icon and name will be ignored.
        /// If the upload_type value is banner then the image uploaded will be the subreddit's new mobile banner and name will be ignored.
        /// For backwards compatibility, if upload_type is not specified, the header field will be used instead:
        /// If the header field has value 0, then upload_type is img.
        /// If the header field has value 1, then upload_type is header.
        /// The img_type field specifies whether to store the uploaded image as a PNG or JPEG.
        /// Subreddits have a limited number of images that can be in use at any given time. If no image with the specified name already exists, one of the slots will be consumed
        /// If an image with the specified name already exists, it will be replaced. This does not affect the stylesheet immediately, but will take effect the next time the stylesheet is saved.
        /// See also: /api/delete_sr_img, /api/delete_sr_header, /api/delete_sr_icon, and /api/delete_sr_banner.
        /// </summary>
        /// <param name="file">file upload with maximum size of 500 KiB</param>
        /// <param name="header">an integer between 0 and 1</param>
        /// <param name="name">a valid subreddit image name</param>
        /// <param name="uploadType"one of (img, header, icon, banner)></param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <param name="formId">(optional) can be ignored</param>
        /// <returns>(TODO - Untested)</returns>
        public object UploadSrImg(byte[] file, int header, string name, string uploadType, string subreddit = null, string imgType = "png",
            string formId = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/upload_sr_img", Method.POST);

            restRequest.AddParameter("file", file);
            restRequest.AddParameter("header", header);
            restRequest.AddParameter("name", name);
            restRequest.AddParameter("upload_type", uploadType);
            restRequest.AddParameter("img_type", imgType);
            restRequest.AddParameter("formid", formId);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Search user profiles by title and description.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="q">a search query</param>
        /// <param name="sort">one of (relevance, activity)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>(TODO - Untested)</returns>
        public object SearchProfiles(string after, string before, string q, string sort, int count = 0, int limit = 25, string show = "all",
            bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("profiles/search");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("q", q);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get the current settings of a subreddit.
        /// This returns the current settings of the subreddit as used by /api/site_admin.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <param name="created">one of (true, false)</param>
        /// <param name="location"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Edit(string subreddit, bool created, string location)
        {
            RestRequest restRequest = PrepareRequest("r/" + subreddit + "/about/edit");

            restRequest.AddParameter("created", created);
            restRequest.AddParameter("location", location);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get the rules for the current subreddit.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>Subreddit rules.</returns>
        public object Rules(string subreddit)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("r/" + subreddit + "/about/rules"));
        }

        /// <summary>
        /// Get subreddit traffic.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>Subreddit traffic data.</returns>
        public object Traffic(string subreddit)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("r/" + subreddit + "/about/traffic"));
        }

        /*
         * Note - The API docs show the wrong URL for this endpoint.
         * According to multiple sources, this endpoint is incompatible with OAuth.  It just returns empty JSON.
         * 
         * --Kris
         */
        public object Sidebar(string subreddit = null)
        {
            throw new NotImplementedException("This endpoint does not work correctly with OAuth.");
            //return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "about/sidebar"));
        }

        /* Note - The API docs show the wrong URL for this endpoint (I think).
         * This endpoint returns 403, with the content saying, "Request forbidden by administrative rules."
         * The response object does contain the URL of the stickied post, though, interestingly enough.
         * 
         * --Kris
         */
        public object Sticky(string subreddit = null, int num = 1)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "about/sticky");

            restRequest.AddParameter("num", num);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get subreddits the user has a relationship with.
        /// The where parameter chooses which subreddits are returned as follows:
        /// subscriber - subreddits the user is subscribed to
        /// contributor - subreddits the user is an approved submitter in
        /// moderator - subreddits the user is a moderator of
        /// streams - subscribed to subreddits that contain hosted video links
        /// See also: /api/subscribe, /api/friend, and /api/accept_moderator_invite.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (subscriber, contributor, moderator, streams)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>List of subreddit objects.</returns>
        public object Mine(string where, string after, string before, bool includeCategories, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("subreddits/mine/" + where);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Search subreddits by title and description.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="q">a search query</param>
        /// <param name="showUsers">boolean value</param>
        /// <param name="sort">one of (relevance, activity)</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>List of subreddit objects.</returns>
        public object Search(string after, string before, string q, bool showUsers, string sort, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("subreddits/search");

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("q", q);
            restRequest.AddParameter("show_users", showUsers);
            restRequest.AddParameter("sort", sort);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get all subreddits.
        /// The where parameter chooses the order in which the subreddits are displayed.
        /// popular sorts on the activity of the subreddit and the position of the subreddits can shift around.
        /// new sorts the subreddits based on their creation date, newest first.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (popular, new, gold, default)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>List of subreddit objects.</returns>
        public SubredditContainer Get(string where, string after, string before, bool includeCategories, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("subreddits/" + where);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject<SubredditContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get all user subreddits.
        /// The where parameter chooses the order in which the subreddits are displayed.
        /// popular sorts on the activity of the subreddit and the position of the subreddits can shift around.
        /// new sorts the user subreddits based on their creation date, newest first.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (popular, new)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>List of subreddit objects.</returns>
        public object GetUserSubreddits(string where, string after, string before, bool includeCategories, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            RestRequest restRequest = PrepareRequest("users/" + where);

            restRequest.AddParameter("after", after);
            restRequest.AddParameter("before", before);
            restRequest.AddParameter("include_categories", includeCategories);
            restRequest.AddParameter("count", count);
            restRequest.AddParameter("limit", limit);
            restRequest.AddParameter("show", show);
            restRequest.AddParameter("sr_detail", srDetail);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }
    }
}
