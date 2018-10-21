using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class AccountPrefs
    {
        [JsonProperty("default_theme_sr")]
        public string DefaultThemeSr;

        [JsonProperty("threaded_messages")]
        public bool ThreadedMessages;

        [JsonProperty("hide_downs")]
        public bool HideDowns;

        [JsonProperty("label_nsfw")]
        public bool LabelNSFW;

        [JsonProperty("activity_relevant_ads")]
        public bool ActivityRelevantAds;

        [JsonProperty("email_messages")]
        public bool EmailMessages;

        [JsonProperty("profile_opt_out")]
        public bool ProfileOptOut;

        [JsonProperty("video_autoplay")]
        public bool VideoAutoplay;

        [JsonProperty("accept_pms")]
        public object AcceptPms;  // TODO - Determine type.  --Kris

        [JsonProperty("third_party_site_data_personalized_content")]
        public bool ThirdPartySiteDataPersonalizedContent;

        [JsonProperty("show_link_flair")]
        public bool ShowLinkFlair;

        [JsonProperty("creddit_auto_renew")]
        public bool CredditAutoRenew;

        [JsonProperty("show_trending")]
        public bool ShowTrending;

        [JsonProperty("private_feeds")]
        public bool PrivateFeeds;

        [JsonProperty("monitor_mentions")]
        public bool MonitorMentions;

        [JsonProperty("public_server_seconds")]
        public bool PublicServerSeconds;

        [JsonProperty("research")]
        public bool Research;

        [JsonProperty("ignore_suggested_sort")]
        public bool IgnoreSuggestedSort;

        [JsonProperty("email_digests")]
        public bool EmailDigests;

        [JsonProperty("media")]
        public string Media;

        [JsonProperty("clickgadget")]
        public bool ClickGadget;

        [JsonProperty("use_global_defaults")]
        public bool UseGlobalDefaults;

        [JsonProperty("show_snoovatar")]
        public bool ShowSnoovatar;

        [JsonProperty("over_18")]
        public bool Over18;

        [JsonProperty("show_stylesheets")]
        public bool ShowStylesheets;

        [JsonProperty("live_orangereds")]
        public bool LiveOrangeReds;

        [JsonProperty("enable_default_themes")]
        public bool EnableDefaultThemes;

        [JsonProperty("force_https")]
        public bool ForceHTTPS;

        [JsonProperty("domain_details")]
        public bool DomainDetails;

        [JsonProperty("collapse_left_bar")]
        public bool CollapseLeftBar;

        [JsonProperty("lang")]
        public string Lang;

        [JsonProperty("hide_ups")]
        public bool HideUps;

        [JsonProperty("third_party_data_personalized_ads")]
        public bool ThirdPartyDataPersonalizedAds;

        [JsonProperty("allow_clicktracking")]
        public bool AllowClickTracking;

        [JsonProperty("hide_from_robots")]
        public bool HideFromRobots;

        [JsonProperty("show_twitter")]
        public bool ShowTwitter;

        [JsonProperty("compress")]
        public bool Compress;

        [JsonProperty("store_visits")]
        public bool StoreVisits;

        [JsonProperty("threaded_modmail")]
        public bool ThreadedModmail;

        [JsonProperty("min_link_score")]
        public int MinLinkScore;

        [JsonProperty("media_preview")]
        public string MediaPreview;

        [JsonProperty("nightmode")]
        public bool NightMode;

        [JsonProperty("highlight_controversial")]
        public bool HighlightControversial;

        [JsonProperty("geopopular")]
        public string Geopopular;

        [JsonProperty("third_party_site_data_personalized_ads")]
        public bool ThirdPartySiteDataPersonalizedAds;

        [JsonProperty("content_langs")]
        public List<string> ContentLangs;

        [JsonProperty("show_promote")]
        public object ShowPromote;  // TODO - Determine type.  --Kris

        [JsonProperty("min_comment_score")]
        public int MinCommentScore;

        [JsonProperty("public_votes")]
        public bool PublicVotes;

        [JsonProperty("no_video_autoplay")]
        public bool NoVideoAutoplay;

        [JsonProperty("organic")]
        public bool Organic;

        [JsonProperty("collapse_read_messages")]
        public bool CollapseReadMessages;

        [JsonProperty("show_flair")]
        public bool ShowFlair;

        [JsonProperty("mark_messages_read")]
        public bool MarkMessagesRead;

        [JsonProperty("search_include_over_18")]
        public bool SearchIncludeOver18;

        [JsonProperty("no_profanity")]
        public bool NoProfanity;

        [JsonProperty("hide_ads")]
        public bool HideAds;

        [JsonProperty("beta")]
        public bool Beta;

        [JsonProperty("top_karma_subreddits")]
        public bool TopKarmaSubreddits;

        [JsonProperty("newwindow")]
        public bool NewWindow;

        [JsonProperty("numsites")]
        public int NumSites;

        [JsonProperty("legacy_search")]
        public bool LegacySearch;

        [JsonProperty("num_comments")]
        public int NumComments;

        [JsonProperty("show_gold_expiration")]
        public bool ShowGoldExpiration;

        [JsonProperty("highlight_new_comments")]
        public bool HighlightNewComments;

        [JsonProperty("email_unsubscribe_all")]
        public bool EmailUnsubscribeAll;

        [JsonProperty("default_comment_sort")]
        public string DefaultCommentSort;

        [JsonProperty("hide_locationbar")]
        public bool HideLocationBar;

        [JsonProperty("autoplay")]
        public bool Autoplay;
    }
}
