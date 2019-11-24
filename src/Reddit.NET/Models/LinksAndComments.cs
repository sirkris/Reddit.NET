using Newtonsoft.Json;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.Listings;
using Reddit.Models.Internal;
using Reddit.Things;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class LinksAndComments : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        private Common Common { get; set; }

        public LinksAndComments(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent)
        {
            Common = new Common(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
        }

        /// <summary>
        /// Submit a new comment or reply to a message.
        /// parent is the fullname of the thing being replied to. Its value changes the kind of object created by this request:
        /// the fullname of a Link: a top-level comment in that Link's thread. (requires submit scope)
        /// the fullname of a Comment: a comment reply to that comment. (requires submit scope)
        /// the fullname of a Message: a message reply to that message. (requires privatemessages scope)
        /// text should be the raw markdown body of the comment or message.
        /// To start a new message thread, use /api/compose.
        /// the thing_id is the fullname of the parent thing.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>A Reddit comment.</returns>
        public T Comment<T>(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return SendRequest<T>("api/comment", linksAndCommentsThingInput, Method.POST);
        }

        /// <summary>
        /// Asynchronously submit a new comment or reply to a message.
        /// parent is the fullname of the thing being replied to. Its value changes the kind of object created by this request:
        /// the fullname of a Link: a top-level comment in that Link's thread. (requires submit scope)
        /// the fullname of a Comment: a comment reply to that comment. (requires submit scope)
        /// the fullname of a Message: a message reply to that message. (requires privatemessages scope)
        /// text should be the raw markdown body of the comment or message.
        /// To start a new message thread, use /api/compose.
        /// the thing_id is the fullname of the parent thing.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>A Reddit comment.</returns>
        public async Task<T> CommentAsync<T>(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return await SendRequestAsync<T>("api/comment", linksAndCommentsThingInput, Method.POST);
        }

        /// <summary>
        /// Delete a Link or Comment.
        /// </summary>
        /// <param name="id">fullname of a thing created by the user</param>
        public void Delete(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/del", id));
        }

        /// <summary>
        /// Delete a Link or Comment asynchronously.
        /// </summary>
        /// <param name="id">fullname of a thing created by the user</param>
        public async Task DeleteAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/del", id));
        }

        /// <summary>
        /// Edit the body text of a self-post.
        /// the thing_id is the fullname of a self post.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>The modified post data.</returns>
        public PostResultContainer EditUserText(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return SendRequest<PostResultContainer>("api/editusertext", linksAndCommentsThingInput, Method.POST);
        }

        /// <summary>
        /// Edit the body text of a self-post asynchronously.
        /// the thing_id is the fullname of a self post.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>The modified post data.</returns>
        public async Task<PostResultContainer> EditUserTextAsync(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return await SendRequestAsync<PostResultContainer>("api/editusertext", linksAndCommentsThingInput, Method.POST);
        }

        /// <summary>
        /// Edit the body text of a comment.
        /// the thing_id is the fullname of a comment.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>The modified comment data.</returns>
        public CommentResultContainer EditUserTextComment(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return SendRequest<CommentResultContainer>("api/editusertext", linksAndCommentsThingInput, Method.POST);
        }

        /// <summary>
        /// Edit the body text of a comment asynchronously.
        /// the thing_id is the fullname of a comment.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>The modified comment data.</returns>
        public async Task<CommentResultContainer> EditUserTextCommentAsync(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return await SendRequestAsync<CommentResultContainer>("api/editusertext", linksAndCommentsThingInput, Method.POST);
        }

        /// <summary>
        /// Hide a link.
        /// This removes it from the user's default view of subreddit listings.
        /// See also: /api/unhide.
        /// </summary>
        /// <param name="id">A comma-separated list of link fullnames</param>
        public void Hide(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/hide", id));
        }

        /// <summary>
        /// Hide a link asynchronously.
        /// This removes it from the user's default view of subreddit listings.
        /// See also: /api/unhide.
        /// </summary>
        /// <param name="id">A comma-separated list of link fullnames</param>
        public async Task HideAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/hide", id));
        }

        // Regarding loadReplies:  https://www.reddit.com/r/redditdev/comments/dqz2jy/api_bug_the_apiinfo_endpoint_does_not_populate/
        /// <summary>
        /// Return a listing of things specified by their fullnames.
        /// Only Links, Comments, and Subreddits are allowed.
        /// </summary>
        /// <param name="id">A comma-separated list of thing fullnames</param>
        /// <param name="subreddit">The subreddit with the listing</param>
        /// <param name="loadReplies">The Info endpoint doesn't include replies; if loadReplies is true, an additional API call will be triggered to retrieve the replies (default: true)</param>
        /// <returns>The requested listings.</returns>
        public Info Info(string id, string subreddit = null, bool loadReplies = true)
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
                        Comment comment = JsonConvert.DeserializeObject<Comment>(JsonConvert.SerializeObject(child.Data));
                        if (loadReplies)
                        {
                            CommentContainer commentContainer = Common.GetComments(comment.ParentId.Substring(3), new ListingsGetCommentsInput(comment: comment.Id), comment.Subreddit);
                            if (commentContainer != null
                                && commentContainer.Data != null
                                && commentContainer.Data.Children != null
                                && !commentContainer.Data.Children.Count.Equals(0)
                                && commentContainer.Data.Children[0].Data != null)
                            {
                                comment.Replies = commentContainer.Data.Children[0].Data.Replies;
                            }
                        }

                        comments.Add(comment);
                        break;
                    case "t5":
                        subreddits.Add(JsonConvert.DeserializeObject<Subreddit>(JsonConvert.SerializeObject(child.Data)));
                        break;
                }
            }

            return new Info(posts, comments, subreddits);
        }

        /// <summary>
        /// Lock a link.
        /// Prevents a post from receiving new comments.
        /// See also: /api/unlock.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        public void Lock(string id)
        {
            RestRequest restRequest = PrepareIDRequest("api/lock", id);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Lock a link asynchronously.
        /// Prevents a post from receiving new comments.
        /// See also: /api/unlock.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        public async Task LockAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/lock", id));
        }

        /// <summary>
        /// Mark a link NSFW.
        /// See also: /api/unmarknsfw.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void MarkNSFW(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/marknsfw", id));
        }

        /// <summary>
        /// Mark a link NSFW asynchronously.
        /// See also: /api/unmarknsfw.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public async Task MarkNSFWAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/marknsfw", id));
        }

        /// <summary>
        /// Retrieve additional comments omitted from a base comment tree.
        /// When a comment tree is rendered, the most relevant comments are selected for display first.
        /// Remaining comments are stubbed out with "MoreComments" links. 
        /// This API call is used to retrieve the additional comments represented by those stubs, up to 100 at a time.
        /// The two core parameters required are link and children. link is the fullname of the link whose comments are being fetched. 
        /// children is a comma-delimited list of comment ID36s that need to be fetched.
        /// If id is passed, it should be the ID of the MoreComments object this call is replacing. This is needed only for the HTML UI's purposes and is optional otherwise.
        /// NOTE: you may only make one request at a time to this API endpoint. Higher concurrency will result in an error being returned.
        /// If limit_children is True, only return the children requested.
        /// </summary>
        /// <param name="linksAndCommentsMoreChildrenInput">A valid LinksAndCommentsMoreChildrenInput instance</param>
        /// <returns>The requested comments.</returns>
        public MoreChildren MoreChildren(LinksAndCommentsMoreChildrenInput linksAndCommentsMoreChildrenInput)
        {
            MultipleResponseContainer res = SendRequest<MultipleResponseContainer>("api/morechildren", linksAndCommentsMoreChildrenInput);

            MoreChildren moreChildren = new MoreChildren();
            foreach (DynamicListingChild child in res.JSON.Data.Things)
            {
                switch (child.Kind)
                {
                    case "t1":
                        moreChildren.Comments.Add(JsonConvert.DeserializeObject<Comment>(JsonConvert.SerializeObject(child.Data)));
                        break;
                    case "more":
                        moreChildren.MoreData.Add(JsonConvert.DeserializeObject<More>(JsonConvert.SerializeObject(child.Data)));
                        break;
                }
            }

            return moreChildren;
        }

        /// <summary>
        /// Report a link, comment or message.
        /// Reporting a thing brings it to the attention of the subreddit's moderators.
        /// Reporting a message sends it to a system for admin review.
        /// For links and comments, the thing is implicitly hidden as well (see /api/hide for details).
        /// </summary>
        /// <param name="linksAndCommentsReportInput">A valid LinksAndCommentsReportInput instance</param>
        /// <returns>A return object indicating success.</returns>
        public JQueryReturn Report(LinksAndCommentsReportInput linksAndCommentsReportInput)
        {
            return SendRequest<JQueryReturn>("api/report", linksAndCommentsReportInput, Method.POST);
        }

        /// <summary>
        /// Report a link, comment or message asynchronously.
        /// Reporting a thing brings it to the attention of the subreddit's moderators.
        /// Reporting a message sends it to a system for admin review.
        /// For links and comments, the thing is implicitly hidden as well (see /api/hide for details).
        /// </summary>
        /// <param name="linksAndCommentsReportInput">A valid LinksAndCommentsReportInput instance</param>
        /// <returns>A return object indicating success.</returns>
        public async Task<JQueryReturn> ReportAsync(LinksAndCommentsReportInput linksAndCommentsReportInput)
        {
            return await SendRequestAsync<JQueryReturn>("api/report", linksAndCommentsReportInput, Method.POST);
        }

        /// <summary>
        /// Save a link or comment.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// See also: /api/unsave.
        /// </summary>
        /// <param name="linksAndCommentsSaveInput">A valid LinksAndCommentsSaveInput instance</param>
        public void Save(LinksAndCommentsSaveInput linksAndCommentsSaveInput)
        {
            SendRequest<object>("api/save", linksAndCommentsSaveInput, Method.POST);
        }

        /// <summary>
        /// Save a link or comment asynchronously.
        /// Saved things are kept in the user's saved listing for later perusal.
        /// See also: /api/unsave.
        /// </summary>
        /// <param name="linksAndCommentsSaveInput">A valid LinksAndCommentsSaveInput instance</param>
        public async Task SaveAsync(LinksAndCommentsSaveInput linksAndCommentsSaveInput)
        {
            await SendRequestAsync<object>("api/save", linksAndCommentsSaveInput, Method.POST);
        }

        // TODO - API returns 403 whenever I try to hit this endpoint.  No idea why.  --Kris
        /// <summary>
        /// Get a list of categories in which things are currently saved.
        /// See also: /api/save.
        /// </summary>
        /// <returns>(TODO - Untested)</returns>
        public object SavedCategories()
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/saved_categories"));
        }

        /// <summary>
        /// Enable or disable inbox replies for a link or comment.
        /// state is a boolean that indicates whether you are enabling or disabling inbox replies - true to enable, false to disable.
        /// id is the fullname of a thing created by the user.
        /// </summary>
        /// <param name="linksAndCommentsStateInput">a valid LinksAndCommentsStateInput instance</param>
        public void SendReplies(LinksAndCommentsStateInput linksAndCommentsStateInput)
        {
            SendRequest<object>("api/sendreplies", linksAndCommentsStateInput, Method.POST);
        }

        /// <summary>
        /// Enable or disable inbox replies for a link or comment asynchronously.
        /// state is a boolean that indicates whether you are enabling or disabling inbox replies - true to enable, false to disable.
        /// id is the fullname of a thing created by the user.
        /// </summary>
        /// <param name="linksAndCommentsStateInput">a valid LinksAndCommentsStateInput instance</param>
        public async Task SendRepliesAsync(LinksAndCommentsStateInput linksAndCommentsStateInput)
        {
            await SendRequestAsync<object>("api/sendreplies", linksAndCommentsStateInput, Method.POST);
        }

        /// <summary>
        /// Set or unset "contest mode" for a link's comments.
        /// state is a boolean that indicates whether you are enabling or disabling contest mode - true to enable, false to disable.
        /// </summary>
        /// <param name="linksAndCommentsStateInput">a valid LinksAndCommentsStateInput instance</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer SetContestMode(LinksAndCommentsStateInput linksAndCommentsStateInput)
        {
            return SendRequest<GenericContainer>("api/set_contest_mode", linksAndCommentsStateInput, Method.POST);
        }

        /// <summary>
        /// Set or unset "contest mode" for a link's comments asynchronously.
        /// state is a boolean that indicates whether you are enabling or disabling contest mode - true to enable, false to disable.
        /// </summary>
        /// <param name="linksAndCommentsStateInput">a valid LinksAndCommentsStateInput instance</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> SetContestModeAsync(LinksAndCommentsStateInput linksAndCommentsStateInput)
        {
            return await SendRequestAsync<GenericContainer>("api/set_contest_mode", linksAndCommentsStateInput, Method.POST);
        }

        /// <summary>
        /// Set or unset a Link as the sticky in its subreddit.
        /// state is a boolean that indicates whether to sticky or unsticky this post - true to sticky, false to unsticky.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="linksAndCommentsStickyInput">A valid LinksAndCommentsStickyInput instance</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer SetSubredditSticky(LinksAndCommentsStickyInput linksAndCommentsStickyInput)
        {
            return SendRequest<GenericContainer>("api/set_subreddit_sticky", linksAndCommentsStickyInput, Method.POST);
        }

        /// <summary>
        /// Set or unset a Link as the sticky in its subreddit asynchronously.
        /// state is a boolean that indicates whether to sticky or unsticky this post - true to sticky, false to unsticky.
        /// The num argument is optional, and only used when stickying a post.
        /// It allows specifying a particular "slot" to sticky the post into, and if there is already a post stickied in that slot it will be replaced.
        /// If there is no post in the specified slot to replace, or num is None, the bottom-most slot will be used.
        /// </summary>
        /// <param name="linksAndCommentsStickyInput">A valid LinksAndCommentsStickyInput instance</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> SetSubredditStickyAsync(LinksAndCommentsStickyInput linksAndCommentsStickyInput)
        {
            return await SendRequestAsync<GenericContainer>("api/set_subreddit_sticky", linksAndCommentsStickyInput, Method.POST);
        }

        /// <summary>
        /// Set a suggested sort for a link.
        /// Suggested sorts are useful to display comments in a certain preferred way for posts.
        /// For example, casual conversation may be better sorted by new by default, or AMAs may be sorted by Q&A.
        /// A sort of an empty string clears the default sort.
        /// </summary>
        /// <param name="linksAndCommentsSuggestedSortInput">A valid LinksAndCommentsSuggestedSortInput instance</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer SetSuggestedSort(LinksAndCommentsSuggestedSortInput linksAndCommentsSuggestedSortInput)
        {
            return SendRequest<GenericContainer>("api/set_suggested_sort", linksAndCommentsSuggestedSortInput, Method.POST);
        }

        /// <summary>
        /// Set a suggested sort for a link asynchronously.
        /// Suggested sorts are useful to display comments in a certain preferred way for posts.
        /// For example, casual conversation may be better sorted by new by default, or AMAs may be sorted by Q&A.
        /// A sort of an empty string clears the default sort.
        /// </summary>
        /// <param name="linksAndCommentsSuggestedSortInput">A valid LinksAndCommentsSuggestedSortInput instance</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> SetSuggestedSortAsync(LinksAndCommentsSuggestedSortInput linksAndCommentsSuggestedSortInput)
        {
            return await SendRequestAsync<GenericContainer>("api/set_suggested_sort", linksAndCommentsSuggestedSortInput, Method.POST);
        }

        /// <summary>
        /// Spoiler.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        public void Spoiler(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/spoiler", id));
        }

        /// <summary>
        /// Asynchronous spoiler.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        public async Task SpoilerAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/spoiler", id));
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

        // TODO - Controller support for image, video, and videogif kinds.  --Kris
        /// <summary>
        /// Submit a link to a subreddit.
        /// Submit will create a link or self-post in the subreddit sr with the title title.
        /// If kind is "link", then url is expected to be a valid URL to link to.
        /// Otherwise, text, if present, will be the body of the self-post unless richtext_json is present, in which case it will be converted into the body of the self-post.
        /// An error is thrown if both text and richtext_json are present.
        /// If a link with the same URL has already been submitted to the specified subreddit an error will be returned unless resubmit is true.
        /// extension is used for determining which view-type (e.g.json, compact etc.) to use for the redirect that is generated if the resubmit error occurs.
        /// </summary>
        /// <param name="linksAndCommentsSubmitInput">A valid LinksAndCommentsSubmitInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <returns>An object containing the id, name, and URL of the newly created post.</returns>
        public PostResultShortContainer Submit(LinksAndCommentsSubmitInput linksAndCommentsSubmitInput, string gRecaptchaResponse = "")
        {
            return JsonConvert.DeserializeObject<PostResultShortContainer>(ExecuteRequest(PrepareSubmit(linksAndCommentsSubmitInput, gRecaptchaResponse)));
        }

        /// <summary>
        /// Submit a link to a subreddit asynchronously.
        /// Submit will create a link or self-post in the subreddit sr with the title title.
        /// If kind is "link", then url is expected to be a valid URL to link to.
        /// Otherwise, text, if present, will be the body of the self-post unless richtext_json is present, in which case it will be converted into the body of the self-post.
        /// An error is thrown if both text and richtext_json are present.
        /// If a link with the same URL has already been submitted to the specified subreddit an error will be returned unless resubmit is true.
        /// extension is used for determining which view-type (e.g.json, compact etc.) to use for the redirect that is generated if the resubmit error occurs.
        /// </summary>
        /// <param name="linksAndCommentsSubmitInput">A valid LinksAndCommentsSubmitInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <returns>An object containing the id, name, and URL of the newly created post.</returns>
        public async Task<PostResultShortContainer> SubmitAsync(LinksAndCommentsSubmitInput linksAndCommentsSubmitInput, string gRecaptchaResponse = "")
        {
            return JsonConvert.DeserializeObject<PostResultShortContainer>(await ExecuteRequestAsync(PrepareSubmit(linksAndCommentsSubmitInput, gRecaptchaResponse)));
        }

        private RestRequest PrepareSubmit(LinksAndCommentsSubmitInput linksAndCommentsSubmitInput, string gRecaptchaResponse = "")
        {
            RestRequest restRequest = PrepareRequest("api/submit", Method.POST);

            restRequest.AddObject(linksAndCommentsSubmitInput);
            restRequest.AddParameter("g-recaptcha-response", gRecaptchaResponse);

            return restRequest;
        }

        /// <summary>
        /// Unhide a link.
        /// </summary>
        /// <param name="id">A comma-separated list of link fullnames</param>
        public void Unhide(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/unhide", id));
        }

        /// <summary>
        /// Unhide a link asynchronously.
        /// </summary>
        /// <param name="id">A comma-separated list of link fullnames</param>
        public async Task UnhideAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/unhide", id));
        }

        /// <summary>
        /// Unlock a link.
        /// Allows a post to receive new comments.
        /// See also: /api/lock.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        public void Unlock(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/unlock", id));
        }

        /// <summary>
        /// Unlock a link asynchronously.
        /// Allows a post to receive new comments.
        /// See also: /api/lock.
        /// </summary>
        /// <param name="id">A comma-separated list of link fullnames</param>
        public async Task UnlockAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/unlock", id));
        }

        /// <summary>
        /// Remove the NSFW marking from a link.
        /// See also: /api/marknsfw.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void UnmarkNSFW(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/unmarknsfw", id));
        }

        /// <summary>
        /// Remove the NSFW marking from a link asynchronously.
        /// See also: /api/marknsfw.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public async Task UnmarkNSFWAsync(string id)
        {
           await ExecuteRequestAsync(PrepareIDRequest("api/unmarknsfw", id));
        }

        /// <summary>
        /// Unsave a link or comment.
        /// This removes the thing from the user's saved listings as well.
        /// See also: /api/save.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void Unsave(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/unsave", id));
        }

        /// <summary>
        /// Unsave a link or comment asynchronously.
        /// This removes the thing from the user's saved listings as well.
        /// See also: /api/save.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public async Task UnsaveAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/unsave", id));
        }

        /// <summary>
        /// Remove spoiler.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        public void Unspoiler(string id)
        {
            ExecuteRequest(PrepareIDRequest("api/unspoiler", id));
        }

        /// <summary>
        /// Remove spoiler asynchronously.
        /// </summary>
        /// <param name="id">fullname of a link</param>
        public async Task UnspoilerAsync(string id)
        {
            await ExecuteRequestAsync(PrepareIDRequest("api/unspoiler", id));
        }

        // WARNING:  Automated bot-voting violates Reddit's rules.  --Kris
        /// <summary>
        /// Cast a vote on a thing.
        /// id should be the fullname of the Link or Comment to vote on.
        /// dir indicates the direction of the vote. Voting 1 is an upvote, -1 is a downvote, and 0 is equivalent to "un-voting" by clicking again on a highlighted arrow.
        /// Note: votes must be cast by humans.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        /// <param name="linksAndCommentsVoteInput">A valid LinksAndCommentsVoteInput instance</param>
        public void Vote(LinksAndCommentsVoteInput linksAndCommentsVoteInput)
        {
            SendRequest<object>("api/vote", linksAndCommentsVoteInput, Method.POST);
        }

        // WARNING:  Automated bot-voting violates Reddit's rules.  --Kris
        /// <summary>
        /// Cast a vote on a thing asynchronously.
        /// id should be the fullname of the Link or Comment to vote on.
        /// dir indicates the direction of the vote. Voting 1 is an upvote, -1 is a downvote, and 0 is equivalent to "un-voting" by clicking again on a highlighted arrow.
        /// Note: votes must be cast by humans.
        /// That is, API clients proxying a human's action one-for-one are OK, but bots deciding how to vote on content or amplifying a human's vote are not.
        /// See the reddit rules for more details on what constitutes vote cheating.
        /// </summary>
        /// <param name="linksAndCommentsVoteInput">A valid LinksAndCommentsVoteInput instance</param>
        public async Task VoteAsync(LinksAndCommentsVoteInput linksAndCommentsVoteInput)
        {
            await SendRequestAsync<object>("api/vote", linksAndCommentsVoteInput, Method.POST);
        }
    }
}
