using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public abstract class AccountPrefsBase
    {
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
        public string AcceptPms;

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

        [JsonProperty("over_18")]
        public bool Over18;

        [JsonProperty("show_stylesheets")]
        public bool ShowStylesheets;

        [JsonProperty("live_orangereds")]
        public bool LiveOrangeReds;

        [JsonProperty("enable_default_themes")]
        public bool EnableDefaultThemes;

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

        [JsonProperty("third_party_site_data_personalized_ads")]
        public bool ThirdPartySiteDataPersonalizedAds;

        [JsonProperty("show_promote")]
        public bool? ShowPromote;

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
