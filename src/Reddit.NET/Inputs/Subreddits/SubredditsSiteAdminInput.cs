using System;

namespace Reddit.Inputs.Subreddits
{
    [Serializable]
    public class SubredditsSiteAdminInput : APITypeInput
    {
        /// <summary>
        /// boolean value
        /// </summary>
        public bool? all_original_content { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? allow_discovery { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? allow_images { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? allow_post_crossposts { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? allow_top { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? allow_videos { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? collapse_deleted_comments { get; set; }

        /// <summary>
        /// an integer between 0 and 1440 (default: 0)
        /// </summary>
        public int? comment_score_hide_mins { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? exclude_banned_modqueue { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? free_form_reports { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? hide_ads { get; set; }

        /// <summary>
        /// a 6-digit rgb hex color, e.g. #AABBCC
        /// </summary>
        public string key_color { get; set; }

        /// <summary>
        /// a valid IETF language tag (underscore separated)
        /// </summary>
        public string lang { get; set; }

        /// <summary>
        /// >one of (any, link, self)
        /// </summary>
        public string link_type { get; set; }

        /// <summary>
        /// subreddit name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? original_content_tag_enabled { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? over_18 { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string public_description { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? show_media { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? show_media_preview { get; set; }

        /// <summary>
        /// one of (low, high, all)
        /// </summary>
        public string spam_comments { get; set; }

        /// <summary>
        /// one of (low, high, all)
        /// </summary>
        public string spam_links { get; set; }

        /// <summary>
        /// one of (low, high, all)
        /// </summary>
        public string spam_selfposts { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? spoilers_enabled { get; set; }

        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string sr { get; set; }

        /// <summary>
        /// a string no longer than 60 characters
        /// </summary>
        public string submit_link_label { get; set; }

        /// <summary>
        /// raw markdown text
        /// </summary>
        public string submit_text { get; set; }

        /// <summary>
        /// a string no longer than 60 characters
        /// </summary>
        public string submit_text_label { get; set; }

        /// <summary>
        /// one of (confidence, top, new, controversial, old, random, qa, live)
        /// </summary>
        public string suggested_comment_sort { get; set; }

        /// <summary>
        /// subreddit name
        /// </summary>
        public string theme_sr { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool? theme_sr_update { get; set; }

        /// <summary>
        /// a string no longer than 100 characters
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// one of (gold_restricted, archived, restricted, employees_only, gold_only, private, user, public)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// an integer between 0 and 36600 (default: 0)
        /// </summary>
        public int? wiki_edit_age { get; set; }

        /// <summary>
        /// an integer between 0 and 1000000000 (default: 0)
        /// </summary>
        public int? wiki_edit_karma { get; set; }

        /// <summary>
        /// one of (disabled, modonly, anyone)
        /// </summary>
        public string wikimode { get; set; }

        /// <summary>
        /// Create or configure a subreddit.  Null values are ignored.
        /// If sr is specified, the request will attempt to modify the specified subreddit. If not, a subreddit with name name will be created.
        /// This endpoint expects all values to be supplied on every request. If modifying a subset of options, it may be useful to get the current settings from /about/edit.json first.
        /// For backwards compatibility, description is the sidebar text and public_description is the publicly visible subreddit description.
        /// Most of the parameters for this endpoint are identical to options visible in the user interface and their meanings are best explained there.
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
        public SubredditsSiteAdminInput(bool? allOriginalContent = null, bool? allowDiscovery = null, bool? allowImages = null, bool? allowPostCrossposts = null,
            bool? allowTop = null, bool? allowVideos = null, bool? collapseDeletedComments = null, string description = null, bool? excludeBannedModqueue = null,
            bool? freeFormReports = null, bool? hideAds = null, string keyColor = null, string lang = null, string linkType = null, string name = null, 
            bool? originalContentTagEnabled = null, bool? over18 = null, string publicDescription = null, bool? showMedia = null, bool? showMediaPreview = null, 
            string spamComments = null, string spamLinks = null, string spamSelfPosts = null, bool? spoilersEnabled = null, string sr = null, string submitLinkLabel = null, 
            string submitText = null, string submitTextLabel = null, string suggestedCommentSort = null, string themeSr = null, bool? themeSrUpdate = null, string title = null, 
            string type = null, string wikiMode = null, int? commentScoreHideMins = null, int? wikiEditAge = null, int? wikiEditKarma = null)
            : base()
        {
            all_original_content = allOriginalContent;
            allow_discovery = allowDiscovery;
            allow_images = allowImages;
            allow_post_crossposts = allowPostCrossposts;
            allow_top = allowTop;
            allow_videos = allowVideos;
            collapse_deleted_comments = collapseDeletedComments;
            this.description = description;
            exclude_banned_modqueue = excludeBannedModqueue;
            free_form_reports = freeFormReports;
            hide_ads = hideAds;
            key_color = keyColor;
            this.lang = lang;
            link_type = linkType;
            this.name = name;
            original_content_tag_enabled = originalContentTagEnabled;
            over_18 = over18;
            public_description = publicDescription;
            show_media = showMedia;
            show_media_preview = showMediaPreview;
            spam_comments = spamComments;
            spam_links = spamLinks;
            spam_selfposts = spamSelfPosts;
            spoilers_enabled = spoilersEnabled;
            this.sr = sr;
            submit_link_label = submitLinkLabel;
            submit_text = submitText;
            submit_text_label = submitTextLabel;
            suggested_comment_sort = suggestedCommentSort;
            theme_sr = themeSr;
            theme_sr_update = themeSrUpdate;
            this.title = title;
            this.type = type;
            wikimode = wikiMode;
            comment_score_hide_mins = commentScoreHideMins;
            wiki_edit_age = wikiEditAge;
            wiki_edit_karma = wikiEditKarma;
        }
    }
}
