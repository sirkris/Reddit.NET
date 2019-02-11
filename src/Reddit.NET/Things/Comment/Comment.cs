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
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime ApprovedAtUTC;

        [JsonProperty("subreddit")]
        public string Subreddit;

        [JsonProperty("user_reports")]
        public object UserReports;

        [JsonProperty("saved")]
        public bool Saved;

        [JsonProperty("mod_reason_title")]
        public string ModReasonTitle;

        // TODO - Assuming this is supposed to be boolean.  Not sure what else the int value could be for (it's either gilded or it's not, right?).  --Kris
        [JsonProperty("gilded")]
        [JsonConverter(typeof(IntBoolConvert))]
        public bool Gilded;

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed;

        [JsonProperty("downs")]
        public int Downs;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("author_flair_background_color")]
        public string AuthorFlairBackgroundColor;

        [JsonProperty("subreddit_type")]
        public string SubredditType;

        [JsonProperty("ups")]
        public int Ups;

        [JsonProperty("author_flair_template_id")]
        public string AuthorFlairTemplateId;

        [JsonProperty("author_fullname")]
        public string AuthorFullname;

        [JsonProperty("can_mod_post")]
        public bool CanModPost;

        [JsonProperty("score")]
        public int Score;

        [JsonProperty("approved_by")]
        public string ApprovedBy;

        [JsonProperty("ignore_reports")]
        public bool IgnoreReports;

        [JsonProperty("edited")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Edited;

        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCSSClass;

        [JsonProperty("previous_visits")]
        //[JsonConverter(typeof(List<TimestampConvert>))]
        public object PreviousVisits;

        // TODO - Is this a list or a string or what?  --Kris
        [JsonProperty("author_flair_richtext")]
        public List<object> AuthorFlairRichtext;

        [JsonProperty("gildings")]
        public Dictionary<string, int> Gildings;

        [JsonProperty("mod_note")]
        public string ModNote;

        [JsonProperty("created")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Created;

        [JsonProperty("banned_by")]
        public string BannedBy;

        [JsonProperty("author_flair_type")]
        public string AuthorFlairType;

        [JsonProperty("likes")]
        public bool? Likes;

        [JsonProperty("banned_at_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime BannedAtUTC;

        [JsonProperty("archived")]
        public bool Archived;

        [JsonProperty("no_follow")]
        public bool NoFollow;

        [JsonProperty("spam")]
        public bool Spam;

        [JsonProperty("can_gild")]
        public bool CanGild;

        [JsonProperty("removed")]
        public bool Removed;

        [JsonProperty("author_flair_text")]
        public string AuthorFlairText;

        [JsonProperty("rte_mode")]
        public string RteMode;

        [JsonProperty("num_reports")]
        public int? NumReports;

        [JsonProperty("distinguished")]
        public string Distinguished;

        [JsonProperty("subreddit_id")]
        public string SubredditId;

        [JsonProperty("mod_reason_by")]
        public string ModReasonBy;

        [JsonProperty("removal_reason")]
        public string RemovalReason;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("report_reasons")]
        public List<string> ReportReasons;

        [JsonProperty("author")]
        public string Author;

        [JsonProperty("send_replies")]
        public bool SendReplies;

        [JsonProperty("approved")]
        public bool Approved;

        [JsonProperty("author_flair_text_color")]
        public string AuthorFlairTextColor;

        [JsonProperty("permalink")]
        public string Permalink;

        [JsonProperty("stickied")]
        public bool Stickied;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime CreatedUTC;

        [JsonProperty("mod_reports")]
        public List<List<string>> ModReports;

        [JsonProperty("replies")]
        public CommentContainer Replies;

        [JsonProperty("body_html")]
        public string BodyHTML;

        [JsonProperty("parent_id")]
        public string ParentId;

        [JsonProperty("body")]
        public string Body;

        [JsonProperty("collapsed")]
        public bool Collapsed;

        [JsonProperty("is_submitter")]
        public bool IsSubmitter;

        [JsonProperty("collapsed_reason")]
        public string CollapsedReason;

        [JsonProperty("score_hidden")]
        public bool ScoreHidden;

        [JsonProperty("controversiality")]
        public int Controversiality;

        [JsonProperty("depth")]
        public int Depth;

        [JsonProperty("sr_detail")]
        public Subreddit SrDetail;

        [JsonProperty("link_id")]
        public string LinkId;

        public Comment(Controllers.Comment comment)
        {
            ImportFromComment(comment);
        }

        public Comment() { }

        private void ImportFromComment(Controllers.Comment comment)
        {
            Subreddit = comment.Subreddit;
            Author = comment.Author;
            Id = comment.Id;
            Name = comment.Fullname;
            Permalink = comment.Permalink;
            Created = comment.Created;
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

            Replies = null;
            if (comment.Replies != null && comment.Replies.Count > 0)
            {
                Replies = new CommentContainer
                {
                    Data = new CommentData
                    {
                        Children = new List<CommentChild>()
                    }
                };

                foreach (Controllers.Comment commentReply in comment.Replies)
                {
                    Replies.Data.Children.Add(new CommentChild { Data = new Comment(commentReply) });
                }
            }
        }
    }
}
