using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class Comment
    {
        [JsonProperty("approved_at_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime ApprovedAtUTC { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("user_reports")]
        public object UserReports { get; set; }

        [JsonProperty("saved")]
        public bool Saved { get; set; }

        [JsonProperty("mod_reason_title")]
        public string ModReasonTitle { get; set; }

        [JsonProperty("gilded")]
        public int Gilded { get; set; }

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; set; }

        [JsonProperty("downs")]
        public int Downs { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author_flair_background_color")]
        public string AuthorFlairBackgroundColor { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("ups")]
        public int Ups { get; set; }

        [JsonProperty("author_flair_template_id")]
        public string AuthorFlairTemplateId { get; set; }

        [JsonProperty("author_fullname")]
        public string AuthorFullname { get; set; }

        [JsonProperty("can_mod_post")]
        public bool CanModPost { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("approved_by")]
        public string ApprovedBy { get; set; }

        [JsonProperty("ignore_reports")]
        public bool IgnoreReports { get; set; }

        [JsonProperty("edited")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime Edited { get; set; }

        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCSSClass { get; set; }

        [JsonProperty("previous_visits")]
        //[JsonConverter(typeof(List<TimestampConvert>))]
        public object PreviousVisits { get; set; }

        // TODO - Is this a list or a string or what?  --Kris
        [JsonProperty("author_flair_richtext")]
        public List<object> AuthorFlairRichtext { get; set; }

        [JsonProperty("gildings")]
        public Dictionary<string, int> Gildings { get; set; }

        [JsonProperty("mod_note")]
        public string ModNote { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("banned_by")]
        public string BannedBy { get; set; }

        [JsonProperty("author_flair_type")]
        public string AuthorFlairType { get; set; }

        [JsonProperty("likes")]
        public bool? Likes { get; set; }

        [JsonProperty("banned_at_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime BannedAtUTC { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("no_follow")]
        public bool NoFollow { get; set; }

        [JsonProperty("spam")]
        public bool Spam { get; set; }

        [JsonProperty("can_gild")]
        public bool CanGild { get; set; }

        [JsonProperty("removed")]
        public bool Removed { get; set; }

        [JsonProperty("author_flair_text")]
        public string AuthorFlairText { get; set; }

        [JsonProperty("rte_mode")]
        public string RteMode { get; set; }

        [JsonProperty("num_reports")]
        public int? NumReports { get; set; }

        [JsonProperty("distinguished")]
        public string Distinguished { get; set; }

        [JsonProperty("subreddit_id")]
        public string SubredditId { get; set; }

        [JsonProperty("mod_reason_by")]
        public string ModReasonBy { get; set; }

        [JsonProperty("removal_reason")]
        public string RemovalReason { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("report_reasons")]
        public List<string> ReportReasons { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("send_replies")]
        public bool SendReplies { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("author_flair_text_color")]
        public string AuthorFlairTextColor { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("stickied")]
        public bool Stickied { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("mod_reports")]
        public List<List<string>> ModReports { get; set; }

        // Either Replies.Comments or Replies.MoreData will be populated; never both.  --Kris
        [JsonProperty("replies")]
        [JsonConverter(typeof(CommentRepliesConverter))]
        public MoreChildren Replies { get; set; }

        [JsonProperty("body_html")]
        public string BodyHTML { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("collapsed")]
        public bool Collapsed { get; set; }

        [JsonProperty("is_submitter")]
        public bool IsSubmitter { get; set; }

        [JsonProperty("collapsed_reason")]
        public string CollapsedReason { get; set; }

        [JsonProperty("score_hidden")]
        public bool ScoreHidden { get; set; }

        [JsonProperty("controversiality")]
        public int Controversiality { get; set; }

        [JsonProperty("depth")]
        public int Depth { get; set; }

        [JsonProperty("sr_detail")]
        public Subreddit SrDetail { get; set; }

        [JsonProperty("link_id")]
        public string LinkId { get; set; }

        public Comment() { }

        private void ImportFromComment(Controllers.Comment comment)
        {
            Subreddit = comment.Subreddit;
            Author = comment.Author;
            Id = comment.Id;
            Name = comment.Fullname;
            Permalink = comment.Permalink;
            CreatedUTC = comment.Created;
            Edited = comment.Edited;
            Score = comment.Score;
            Ups = comment.UpVotes;
            Downs = comment.DownVotes;
            Removed = comment.Removed;
            Spam = comment.Spam;
            Body = comment.Body;
            BodyHTML = comment.BodyHTML;
            ParentId = comment.ParentFullname;
            CollapsedReason = comment.CollapsedReason;
            Collapsed = comment.Collapsed;
            IsSubmitter = comment.IsSubmitter;
            ScoreHidden = comment.ScoreHidden;
            Depth = comment.Depth;

            Replies = new MoreChildren();
            if (comment.replies != null && comment.replies.Count > 0)
            {
                foreach (Controllers.Comment commentReply in comment.replies)
                {
                    Replies.Comments.Add(commentReply.Listing);
                }
            }
        }
    }
}
