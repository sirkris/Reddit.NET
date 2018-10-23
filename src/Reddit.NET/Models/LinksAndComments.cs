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
        /// Submit a new comment or reply to a message.
        /// parent is the fullname of the thing being replied to.Its value changes the kind of object created by this request:
        /// the fullname of a Link: a top-level comment in that Link's thread. (requires submit scope)
        /// the fullname of a Comment: a comment reply to that comment. (requires submit scope)
        /// the fullname of a Message: a message reply to that message. (requires privatemessages scope)
        /// text should be the raw markdown body of the comment or message.
        /// To start a new message thread, use /api/compose.
        /// </summary>
        /// <param name="returnRtjson">boolean value</param>
        /// <param name="richtextJson">JSON data</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="thingId">fullname of parent thing</param>
        /// <returns>A Reddit comment.</returns>
        public GenericContainer Comment(bool returnRtjson, string richtextJson, string text, string thingId)
        {
            RestRequest restRequest = PrepareRequest("api/comment", Method.POST);

            restRequest.AddParameter("return_rtjson", returnRtjson);
            restRequest.AddParameter("richtext_json", richtextJson);
            restRequest.AddParameter("text", text);
            restRequest.AddParameter("thing_id", thingId);
            restRequest.AddParameter("api_type", "json");

            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Delete a Link or Comment.
        /// </summary>
        /// <param name="id">fullname of a thing created by the user</param>
        /// <returns>(TODO - Untested)</returns>
        public object Delete(string id)
        {
            RestRequest restRequest = PrepareRequest("api/del", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Edit the body text of a comment or self-post.
        /// </summary>
        /// <param name="returnRtjson">boolean value</param>
        /// <param name="richtextJson">JSON data</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="thingId">fullname of a thing</param>
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
        /// Hide a link.
        /// This removes it from the user's default view of subreddit listings.
        /// See also: /api/unhide.
        /// </summary>
        /// <param name="id">A comma-separated list of link fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object Hide(string id)
        {
            RestRequest restRequest = PrepareRequest("api/hide", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Return a listing of things specified by their fullnames.
        /// Only Links, Comments, and Subreddits are allowed.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        /// <param name="subreddit">The subreddit with the listing.</param>
        /// <returns>The requested listings.</returns>
        public List<(List<Post>, List<Comment>, List<Subreddit>)> Info(string id, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/info");

            restRequest.AddParameter("id", id);

            DynamicListingContainer res = JsonConvert.DeserializeObject<DynamicListingContainer>(ExecuteRequest(restRequest));

            List<Post> posts = new List<Post>();
            List<Comment> comments = new List<Comment>();
            List<Subreddit> subreddits = new List<Subreddit>();

            foreach (DynamicListingChild child in res.Data.Children)
            {
                switch (child.Kind)
                {
                    case "t3":
                        posts.Add(JsonConvert.DeserializeObject<Post>(JsonConvert.SerializeObject(child.Data)));
                        break;
                    case "t1":
                        comments.Add(JsonConvert.DeserializeObject<Comment>(JsonConvert.SerializeObject(child.Data)));
                        break;
                    case "t5":
                        subreddits.Add(JsonConvert.DeserializeObject<Subreddit>(JsonConvert.SerializeObject(child.Data)));
                        break;
                }
            }

            return new List<(List<Post>, List<Comment>, List<Subreddit>)>
            {
                (posts, comments, subreddits)
            };
        }

        // TODO - Needs testing.
        /// <summary>
        /// Lock a link.
        /// Prevents a post from receiving new comments.
        /// See also: /api/unlock.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        /// <returns>(TODO - Untested)</returns>
        public object Lock(string id)
        {
            RestRequest restRequest = PrepareRequest("api/lock", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Mark a link NSFW.
        /// See also: /api/unmarknsfw.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object MarkNSFW(string id)
        {
            RestRequest restRequest = PrepareRequest("api/marknsfw", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Retrieve additional comments omitted from a base comment tree.
        /// When a comment tree is rendered, the most relevant comments are selected for display first.
        /// Remaining comments are stubbed out with "MoreComments" links. This API call is used to retrieve the additional comments represented by those stubs, up to 100 at a time.
        /// The two core parameters required are link and children. link is the fullname of the link whose comments are being fetched. children is a comma-delimited list of comment ID36s that need to be fetched.
        /// If id is passed, it should be the ID of the MoreComments object this call is replacing. This is needed only for the HTML UI's purposes and is optional otherwise.
        /// NOTE: you may only make one request at a time to this API endpoint. Higher concurrency will result in an error being returned.
        /// If limit_children is True, only return the children requested.
        /// </summary>
        /// <param name="children">a comma-delimited list of comment ID36s</param>
        /// <param name="limitChildren">boolean value</param>
        /// <param name="linkId">fullname of a link</param>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live)</param>
        /// <param name="id">(optional) id of the associated MoreChildren object</param>
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
        /// Report a link, comment or message.
        /// Reporting a thing brings it to the attention of the subreddit's moderators.
        /// Reporting a message sends it to a system for admin review.
        /// For links and comments, the thing is implicitly hidden as well (see /api/hide for details).
        /// </summary>
        /// <param name="additionalInfo">a string no longer than 2000 characters</param>
        /// <param name="banEvadingAccountsNames">a string no longer than 1000 characters</param>
        /// <param name="customText">a string no longer than 250 characters</param>
        /// <param name="fromHelpCenter">boolean value</param>
        /// <param name="otherReason">a string no longer than 100 characters</param>
        /// <param name="reason">a string no longer than 100 characters</param>
        /// <param name="ruleReason">a string no longer than 100 characters</param>
        /// <param name="siteReason">a string no longer than 100 characters</param>
        /// <param name="srName">a string no longer than 1000 characters</param>
        /// <param name="thingId">fullname of a thing</param>
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
        /// Save a link or comment.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// See also: /api/unsave.
        /// </summary>
        /// <param name="category">a category name</param>
        /// <param name="id">fullname of a thing</param>
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
        /// Get a list of categories in which things are currently saved.
        /// See also: /api/save.
        /// </summary>
        /// <returns>(TODO - Untested)</returns>
        public object SavedCategories()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/saved_categories"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Enable or disable inbox replies for a link or comment.
        /// state is a boolean that indicates whether you are enabling or disabling inbox replies - true to enable, false to disable.
        /// </summary>
        /// <param name="id">fullname of a thing created by the user</param>
        /// <param name="state">boolean value</param>
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
        /// Set or unset "contest mode" for a link's comments.
        /// state is a boolean that indicates whether you are enabling or disabling contest mode - true to enable, false to disable.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state">boolean value</param>
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
        /// Set or unset a Link as the sticky in its subreddit.
        /// state is a boolean that indicates whether to sticky or unsticky this post - true to sticky, false to unsticky.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="num">an integer between 1 and 4</param>
        /// <param name="state">boolean value</param>
        /// <param name="toProfile">boolean value</param>
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
        /// Set a suggested sort for a link.
        /// Suggested sorts are useful to display comments in a certain preferred way for posts.
        /// For example, casual conversation may be better sorted by new by default, or AMAs may be sorted by Q&A.A sort of an empty string clears the default sort.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort">one of (confidence, top, new, controversial, old, random, qa, live, blank)</param>
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
        /// Spoiler.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        /// <returns>(TODO - Untested)</returns>
        public object Spoiler(string id)
        {
            RestRequest restRequest = PrepareRequest("api/spoiler", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// *Requires a subscription to [reddit premium]
        /// </summary>
        /// <param name="links">A comma-separated list of link fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object StoreVisits(string links)
        {
            RestRequest restRequest = PrepareRequest("api/store_visits", Method.POST);

            restRequest.AddParameter("links", links);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Submit a link to a subreddit.
        /// Submit will create a link or self-post in the subreddit sr with the title title.
        /// If kind is "link", then url is expected to be a valid URL to link to.
        /// Otherwise, text, if present, will be the body of the self-post unless richtext_json is present, in which case it will be converted into the body of the self-post.
        /// An error is thrown if both text and richtext_json are present.
        /// If a link with the same URL has already been submitted to the specified subreddit an error will be returned unless resubmit is true.
        /// extension is used for determining which view-type (e.g.json, compact etc.) to use for the redirect that is generated if the resubmit error occurs.
        /// </summary>
        /// <param name="ad">boolean value</param>
        /// <param name="app"></param>
        /// <param name="extension">extension used for redirects</param>
        /// <param name="flairId">a string no longer than 36 characters</param>
        /// <param name="flairText">a string no longer than 64 characters</param>
        /// <param name="gRecaptchaResopnse"></param>
        /// <param name="kind">one of (link, self, image, video, videogif)</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resubmit">boolean value</param>
        /// <param name="richtextJson">JSON data</param>
        /// <param name="sendReplies">boolean value</param>
        /// <param name="spoiler">boolean value</param>
        /// <param name="sr">name of a subreddit</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="title">title of the submission. up to 300 characters long</param>
        /// <param name="url">a valid URL</param>
        /// <param name="videoPosterUrl">a valid URL</param>
        /// <returns>An object containing the id, name, and URL of the newly created post.</returns>
        public GenericContainer Submit(bool ad, string app, string extension, string flairId, string flairText, string gRecaptchaResopnse,
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

            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Unhide a link.
        /// </summary>
        /// <param name="id">A comma-separated list of link fullnames</param>
        /// <returns>(TODO - Untested)</returns>
        public object Unhide(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unhide", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Unlock a link.
        /// Allow a post to receive new comments.
        /// See also: /api/lock.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        /// <returns>(TODO - Untested)</returns>
        public object Unlock(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unlock", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove the NSFW marking from a link.
        /// See also: /api/marknsfw.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnmarkNSFW(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unmarknsfw", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Unsave a link or comment.
        /// This removes the thing from the user's saved listings as well.
        /// See also: /api/save.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object Unsave(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unsave", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove spoiler.
        /// </summary>
        /// <param name="id">fullname of a link</param>
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
        /// Cast a vote on a thing.
        /// id should be the fullname of the Link or Comment to vote on.
        /// dir indicates the direction of the vote. Voting 1 is an upvote, -1 is a downvote, and 0 is equivalent to "un-voting" by clicking again on a highlighted arrow.
        /// Note: votes must be cast by humans.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        /// <param name="dir">vote direction. one of (1, 0, -1)</param>
        /// <param name="id">fullname of a thing</param>
        /// <param name="rank">an integer greater than 1</param>
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
