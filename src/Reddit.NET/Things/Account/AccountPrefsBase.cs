using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public abstract class AccountPrefsBase
    {
        [JsonProperty("threaded_messages")]
        public bool ThreadedMessages { get; set; }

        [JsonProperty("hide_downs")]
        public bool HideDowns { get; set; }

        [JsonProperty("label_nsfw")]
        public bool LabelNSFW { get; set; }

        [JsonProperty("activity_relevant_ads")]
        public bool ActivityRelevantAds { get; set; }

        [JsonProperty("email_messages")]
        public bool EmailMessages { get; set; }

        [JsonProperty("profile_opt_out")]
        public bool ProfileOptOut { get; set; }

        [JsonProperty("video_autoplay")]
        public bool VideoAutoplay { get; set; }

        [JsonProperty("accept_pms")]
        public string AcceptPms { get; set; }

        [JsonProperty("third_party_site_data_personalized_content")]
        public bool ThirdPartySiteDataPersonalizedContent { get; set; }

        [JsonProperty("show_link_flair")]
        public bool ShowLinkFlair { get; set; }

        [JsonProperty("creddit_auto_renew")]
        public bool CredditAutoRenew { get; set; }

        [JsonProperty("show_trending")]
        public bool ShowTrending { get; set; }

        [JsonProperty("private_feeds")]
        public bool PrivateFeeds { get; set; }

        [JsonProperty("monitor_mentions")]
        public bool MonitorMentions { get; set; }

        [JsonProperty("research")]
        public bool Research { get; set; }

        [JsonProperty("ignore_suggested_sort")]
        public bool IgnoreSuggestedSort { get; set; }

        [JsonProperty("email_digests")]
        public bool EmailDigests { get; set; }

        [JsonProperty("media")]
        public string Media { get; set; }

        [JsonProperty("clickgadget")]
        public bool ClickGadget { get; set; }

        [JsonProperty("use_global_defaults")]
        public bool UseGlobalDefaults { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("show_stylesheets")]
        public bool ShowStylesheets { get; set; }

        [JsonProperty("live_orangereds")]
        public bool LiveOrangeReds { get; set; }

        [JsonProperty("enable_default_themes")]
        public bool EnableDefaultThemes { get; set; }

        [JsonProperty("domain_details")]
        public bool DomainDetails { get; set; }

        [JsonProperty("collapse_left_bar")]
        public bool CollapseLeftBar { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("hide_ups")]
        public bool HideUps { get; set; }

        [JsonProperty("third_party_data_personalized_ads")]
        public bool ThirdPartyDataPersonalizedAds { get; set; }

        [JsonProperty("allow_clicktracking")]
        public bool AllowClickTracking { get; set; }

        [JsonProperty("hide_from_robots")]
        public bool HideFromRobots { get; set; }

        [JsonProperty("show_twitter")]
        public bool ShowTwitter { get; set; }

        [JsonProperty("compress")]
        public bool Compress { get; set; }

        [JsonProperty("store_visits")]
        public bool StoreVisits { get; set; }

        [JsonProperty("threaded_modmail")]
        public bool ThreadedModmail { get; set; }

        [JsonProperty("min_link_score")]
        public int MinLinkScore { get; set; }

        [JsonProperty("media_preview")]
        public string MediaPreview { get; set; }

        [JsonProperty("nightmode")]
        public bool NightMode { get; set; }

        [JsonProperty("highlight_controversial")]
        public bool HighlightControversial { get; set; }

        [JsonProperty("third_party_site_data_personalized_ads")]
        public bool ThirdPartySiteDataPersonalizedAds { get; set; }

        [JsonProperty("show_promote")]
        public bool? ShowPromote { get; set; }

        [JsonProperty("min_comment_score")]
        public int MinCommentScore { get; set; }

        [JsonProperty("public_votes")]
        public bool PublicVotes { get; set; }

        [JsonProperty("no_video_autoplay")]
        public bool NoVideoAutoplay { get; set; }

        [JsonProperty("organic")]
        public bool Organic { get; set; }

        [JsonProperty("collapse_read_messages")]
        public bool CollapseReadMessages { get; set; }

        [JsonProperty("show_flair")]
        public bool ShowFlair { get; set; }

        [JsonProperty("mark_messages_read")]
        public bool MarkMessagesRead { get; set; }

        [JsonProperty("search_include_over_18")]
        public bool SearchIncludeOver18 { get; set; }

        [JsonProperty("no_profanity")]
        public bool NoProfanity { get; set; }

        [JsonProperty("hide_ads")]
        public bool HideAds { get; set; }

        [JsonProperty("beta")]
        public bool Beta { get; set; }

        [JsonProperty("top_karma_subreddits")]
        public bool TopKarmaSubreddits { get; set; }

        [JsonProperty("newwindow")]
        public bool NewWindow { get; set; }

        [JsonProperty("numsites")]
        public int NumSites { get; set; }

        [JsonProperty("legacy_search")]
        public bool LegacySearch { get; set; }

        [JsonProperty("num_comments")]
        public int NumComments { get; set; }

        [JsonProperty("show_gold_expiration")]
        public bool ShowGoldExpiration { get; set; }

        [JsonProperty("highlight_new_comments")]
        public bool HighlightNewComments { get; set; }

        [JsonProperty("email_unsubscribe_all")]
        public bool EmailUnsubscribeAll { get; set; }

        [JsonProperty("default_comment_sort")]
        public string DefaultCommentSort { get; set; }

        [JsonProperty("hide_locationbar")]
        public bool HideLocationBar { get; set; }

        [JsonProperty("autoplay")]
        public bool Autoplay { get; set; }

        public AccountPrefsBase(bool threadedMessages, bool hideDowns, bool labelNsfw, bool activityRelevantAds, bool emailMessages, bool profileOptOut, bool videoAutoplay,
            string acceptPms, bool thirdPartySiteDataPersonalizedContent, bool showLinkFlair, bool credditAutoRenew, bool showTrending, bool privateFeeds,
            bool monitorMentions, bool research, bool ignoreSuggestedSort, bool emailDigests, string media, bool clickGadget, bool useGlobalDefaults,
            bool over18, bool showStylesheets, bool liveOrangeReds, bool enableDefaultThemes, bool domainDetails, bool collapseLeftBar, string lang,
            bool hideUps, bool thirdPartyDataPersonalizedAds, bool allowClickTracking, bool hideFromRobots, bool showTwitter, bool compress, bool storeVisits, bool threadedModmail,
            int minLinkScore, string mediaPreview, bool nightMode, bool highlightControversial, bool thirdPartySiteDataPersonalizedAds, bool? showPromote,
            int minCommentScore, bool publicVotes, bool noVideoAutoplay, bool organic, bool collapseReadMessages, bool showFlair, bool markMessagesRead,
            bool searchIncludeOver18, bool noProfanity, bool hideAds, bool beta, bool topKarmaSubreddits, bool newWindow, int numSites, bool legacySearch,
            int numComments, bool showGoldExpiration, bool highlightNewComments, bool emailUnsubscribeAll, string defaultCommentSort, bool hideLocationBar,
            bool autoplay)
        {
            Import(threadedMessages, hideDowns, labelNsfw, activityRelevantAds, emailMessages, profileOptOut, videoAutoplay,
            acceptPms, thirdPartySiteDataPersonalizedContent, showLinkFlair, credditAutoRenew, showTrending, privateFeeds,
            monitorMentions, research, ignoreSuggestedSort, emailDigests, media, clickGadget, useGlobalDefaults,
            over18, showStylesheets, liveOrangeReds, enableDefaultThemes, domainDetails, collapseLeftBar, lang,
            hideUps, thirdPartyDataPersonalizedAds, allowClickTracking, hideFromRobots, showTwitter, compress, storeVisits, threadedModmail,
            minLinkScore, mediaPreview, nightMode, highlightControversial, thirdPartySiteDataPersonalizedAds, showPromote,
            minCommentScore, publicVotes, noVideoAutoplay, organic, collapseReadMessages, showFlair, markMessagesRead,
            searchIncludeOver18, noProfanity, hideAds, beta, topKarmaSubreddits, newWindow, numSites, legacySearch,
            numComments, showGoldExpiration, highlightNewComments, emailUnsubscribeAll, defaultCommentSort, hideLocationBar,
            autoplay);
        }

        private void Import(bool threadedMessages, bool hideDowns, bool labelNsfw, bool activityRelevantAds, bool emailMessages, bool profileOptOut, bool videoAutoplay, 
            string acceptPms, bool thirdPartySiteDataPersonalizedContent, bool showLinkFlair, bool credditAutoRenew, bool showTrending, bool privateFeeds, 
            bool monitorMentions, bool research, bool ignoreSuggestedSort, bool emailDigests, string media, bool clickGadget, bool useGlobalDefaults, 
            bool over18, bool showStylesheets, bool liveOrangeReds, bool enableDefaultThemes, bool domainDetails, bool collapseLeftBar, string lang, 
            bool hideUps, bool thirdPartyDataPersonalizedAds, bool allowClickTracking, bool hideFromRobots, bool showTwitter, bool compress, bool storeVisits, bool threadedModmail, 
            int minLinkScore, string mediaPreview, bool nightMode, bool highlightControversial, bool thirdPartySiteDataPersonalizedAds, bool? showPromote, 
            int minCommentScore, bool publicVotes, bool noVideoAutoplay, bool organic, bool collapseReadMessages, bool showFlair, bool markMessagesRead, 
            bool searchIncludeOver18, bool noProfanity, bool hideAds, bool beta, bool topKarmaSubreddits, bool newWindow, int numSites, bool legacySearch, 
            int numComments, bool showGoldExpiration, bool highlightNewComments, bool emailUnsubscribeAll, string defaultCommentSort, bool hideLocationBar, 
            bool autoplay)
        {
            ThreadedMessages = threadedMessages;
            HideDowns = hideDowns;
            LabelNSFW = labelNsfw;
            ActivityRelevantAds = activityRelevantAds;
            EmailMessages = emailMessages;
            ProfileOptOut = profileOptOut;
            VideoAutoplay = videoAutoplay;
            AcceptPms = acceptPms;
            ThirdPartySiteDataPersonalizedContent = thirdPartySiteDataPersonalizedContent;
            ShowLinkFlair = showLinkFlair;
            CredditAutoRenew = credditAutoRenew;
            ShowTrending = showTrending;
            PrivateFeeds = privateFeeds;
            MonitorMentions = monitorMentions;
            Research = research;
            IgnoreSuggestedSort = ignoreSuggestedSort;
            EmailDigests = emailDigests;
            Media = media;
            ClickGadget = clickGadget;
            UseGlobalDefaults = useGlobalDefaults;
            Over18 = over18;
            ShowStylesheets = showStylesheets;
            LiveOrangeReds = liveOrangeReds;
            EnableDefaultThemes = enableDefaultThemes;
            DomainDetails = domainDetails;
            CollapseLeftBar = collapseLeftBar;
            Lang = lang;
            HideUps = hideUps;
            ThirdPartyDataPersonalizedAds = thirdPartyDataPersonalizedAds;
            AllowClickTracking = allowClickTracking;
            HideFromRobots = hideFromRobots;
            ShowTwitter = showTwitter;
            Compress = compress;
            StoreVisits = storeVisits;
            ThreadedModmail = threadedModmail;
            MinLinkScore = minLinkScore;
            MediaPreview = mediaPreview;
            NightMode = nightMode;
            HighlightControversial = highlightControversial;
            ThirdPartySiteDataPersonalizedAds = thirdPartySiteDataPersonalizedAds;
            ShowPromote = (showPromote ?? false);
            MinCommentScore = minCommentScore;
            PublicVotes = publicVotes;
            NoVideoAutoplay = noVideoAutoplay;
            Organic = organic;
            CollapseReadMessages = collapseReadMessages;
            ShowFlair = showFlair;
            MarkMessagesRead = markMessagesRead;
            SearchIncludeOver18 = searchIncludeOver18;
            NoProfanity = noProfanity;
            HideAds = hideAds;
            Beta = beta;
            TopKarmaSubreddits = topKarmaSubreddits;
            NewWindow = newWindow;
            NumSites = numSites;
            LegacySearch = legacySearch;
            NumComments = numComments;
            ShowGoldExpiration = showGoldExpiration;
            HighlightNewComments = highlightNewComments;
            EmailUnsubscribeAll = emailUnsubscribeAll;
            DefaultCommentSort = defaultCommentSort;
            HideLocationBar = hideLocationBar;
            Autoplay = autoplay;
        }
    }
}
