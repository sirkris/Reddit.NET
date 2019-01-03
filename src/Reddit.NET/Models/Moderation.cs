using Newtonsoft.Json;
using Reddit.Models.Inputs.Moderation;
using Reddit.Things;
using RestSharp;

namespace Reddit.Models
{
    public class Moderation : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Moderation(string appId, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
            : base(appId, refreshToken, accessToken, ref restClient, deviceId) { }

        /// <summary>
        /// Get a list of recent moderation actions.
        /// Moderator actions taken within a subreddit are logged. This listing is a view of that log with various filters to aid in analyzing the information.
        /// The optional mod parameter can be a comma-delimited list of moderator names to restrict the results to, or the string a to restrict the results to admin actions taken within the subreddit.
        /// The type parameter is optional and if sent limits the log entries returned to only those of the type specified.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="moderationGetLogInput">A valid ModerationGetLogInput instance</param>
        /// <param name="subreddit">The subreddit being moderated</param>
        /// <returns>A listing of recent moderation actions.</returns>
        public ModActionContainer GetLog(ModerationGetLogInput moderationGetLogInput, string subreddit = null)
        {
            return SendRequest<ModActionContainer>(Sr(subreddit) + "about/log", moderationGetLogInput);
        }

        // TODO - Split into two functions (only = links, only = comments).  Comments return not supported yet.  --Kris
        /// <summary>
        /// Return a listing of posts relevant to moderators.
        /// reports: Things that have been reported.
        /// spam: Things that have been marked as spam or otherwise removed.
        /// modqueue: Things requiring moderator review, such as reported things and items caught by the spam filter.
        /// unmoderated: Things that have yet to be approved/removed by a mod.
        /// edited: Things that have been edited recently.
        /// Requires the "posts" moderator permission for the subreddit.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="location">One of (reports, spam, modqueue, unmoderated, edited)</param>
        /// <param name="moderationModQueueInput">A valid ModerationModQueueInput instance</param>
        /// <param name="subreddit">The subreddit being moderated</param>
        /// <returns>A listing of posts relevant to moderators.</returns>
        public PostContainer ModQueue(ModerationModQueueInput moderationModQueueInput, string location = "modqueue", string subreddit = null)
        {
            return SendRequest<PostContainer>(Sr(subreddit) + "about/" + location, moderationModQueueInput);
        }

        /// <summary>
        /// Accept an invite to moderate the specified subreddit.
        /// The authenticated user must have been invited to moderate the subreddit by one of its current moderators.
        /// See also: /api/friend and /subreddits/mine.
        /// </summary>
        /// <param name="subreddit">The subreddit being moderated</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer AcceptModeratorInvite(string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/accept_moderator_invite", Method.POST);

            restRequest.AddParameter("api_type", "json");
            
            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Approve a link or comment.
        /// If the thing was removed, it will be re-inserted into appropriate listings.
        /// Any reports on the approved thing will be discarded.
        /// See also: /api/remove.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void Approve(string id)
        {
            RestRequest restRequest = PrepareRequest("api/approve", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Distinguish a thing's author with a sigil.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a link or comment of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish. depends on user.
        /// The first time a top-level comment is moderator distinguished, the author of the link the comment is in reply to will get a notification in their inbox.
        /// sticky is a boolean flag for comments, which will stick the distingushed comment to the top of all comments threads.
        /// If a comment is marked sticky, it will override any other stickied comment for that link (as only one comment may be stickied at a time). Only top-level comments may be stickied.
        /// </summary>
        /// <param name="moderationDistinguishInput">A valid ModerationDistinguishInput instance</param>
        /// <returns>The distinguished post or comment object.</returns>
        public T Distinguish<T>(ModerationDistinguishInput moderationDistinguishInput)
        {
            return SendRequest<T>("api/distinguish", moderationDistinguishInput, Method.POST);
        }

        /// <summary>
        /// Distinguish a post's author with a sigil.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a link of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish. depends on user.
        /// </summary>
        /// <param name="how">one of (yes, no, admin, special)</param>
        /// <param name="id">fullname of a thing</param>
        /// <returns>The distinguished post object.</returns>
        public PostResultContainer DistinguishPost(string how, string id)
        {
            return Distinguish<PostResultContainer>(new ModerationDistinguishInput(id, how));
        }

        /// <summary>
        /// Distinguish a comment's author with a sigil.
        /// This can be useful to draw attention to and confirm the identity of the user in the context of a comment of theirs.
        /// The options for distinguish are as follows:
        /// yes - add a moderator distinguish([M]). only if the user is a moderator of the subreddit the thing is in.
        /// no - remove any distinguishes.
        /// admin - add an admin distinguish([A]). admin accounts only.
        /// special - add a user-specific distinguish.depends on user.
        /// The first time a top-level comment is moderator distinguished, the author of the link the comment is in reply to will get a notification in their inbox.
        /// sticky is a boolean flag for comments, which will stick the distingushed comment to the top of all comments threads.
        /// If a comment is marked sticky, it will override any other stickied comment for that link (as only one comment may be stickied at a time). Only top-level comments may be stickied.
        /// </summary>
        /// <param name="how">one of (yes, no, admin, special)</param>
        /// <param name="id">fullname of a thing</param>
        /// <param name="sticky">boolean value</param>
        /// <returns>The distinguished comment object.</returns>
        public CommentResultContainer DistinguishComment(string how, string id, bool? sticky = null)
        {
            return Distinguish<CommentResultContainer>(new ModerationDistinguishInput(id, how, sticky));
        }

        /// <summary>
        /// Prevent future reports on a thing from causing notifications.
        /// Any reports made about a thing after this flag is set on it will not cause notifications or make the thing show up in the various moderation listings.
        /// See also: /api/unignore_reports.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void IgnoreReports(string id)
        {
            RestRequest restRequest = PrepareRequest("api/ignore_reports", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Abdicate approved submitter status in a subreddit.
        /// See also: /api/friend.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void LeaveContributor(string id)
        {
            RestRequest restRequest = PrepareRequest("api/leavecontributor", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        /// <summary>
        /// Abdicate moderator status in a subreddit.
        /// See also: /api/friend.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void LeaveModerator(string id)
        {
            RestRequest restRequest = PrepareRequest("api/leavemoderator", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        // TODO - Reddit API returns 500 server error when passing a user fullname (t2_2cclzaxt).  Maybe it expects a thread id, instead?  --Kris
        /// <summary>
        /// For muting user via modmail.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object MuteMessageAuthor(string id)
        {
            RestRequest restRequest = PrepareRequest("api/mute_message_author", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Test with Modmail.  --Kris
        /// <summary>
        /// Remove a link, comment, or modmail message.
        /// If the thing is a link, it will be removed from all subreddit listings. If the thing is a comment, it will be redacted and removed from all subreddit comment listings.
        /// See also: /api/approve.
        /// </summary>
        /// <param name="moderationRemoveInput">A valid ModerationRemoveInput instance</param>
        public void Remove(ModerationRemoveInput moderationRemoveInput)
        {
            SendRequest<object>("api/remove", moderationRemoveInput, Method.POST);
        }

        /// <summary>
        /// Allow future reports on a thing to cause notifications.
        /// See also: /api/ignore_reports.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void UnignoreReports(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unignore_reports", Method.POST);

            restRequest.AddParameter("id", id);

            ExecuteRequest(restRequest);
        }

        // TODO - Reddit API returns 500 server error when passing a user fullname (t2_2cclzaxt).  Maybe it expects a thread id, instead?  --Kris
        /// <summary>
        /// For unmuting user via modmail.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        /// <returns>(TODO - Untested)</returns>
        public object UnmuteMessageAuthor(string id)
        {
            RestRequest restRequest = PrepareRequest("api/unmute_message_author", Method.POST);

            restRequest.AddParameter("id", id);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Redirect to the subreddit's stylesheet if one exists.
        /// See also: /api/subreddit_stylesheet.
        /// </summary>
        /// <param name="subreddit">The subreddit being moderated</param>
        /// <returns>The subreddit's CSS.</returns>
        public string Stylesheet(string subreddit = null)
        {
            return ExecuteRequest(Sr(subreddit) + "stylesheet");
        }
    }
}
