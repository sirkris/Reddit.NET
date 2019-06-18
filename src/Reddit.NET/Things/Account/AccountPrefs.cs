using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class AccountPrefs : AccountPrefsBase
    {
        [JsonProperty("default_theme_sr")]
        public string DefaultThemeSr { get; set; }

        [JsonProperty("public_server_seconds")]
        public bool PublicServerSeconds { get; set; }

        [JsonProperty("show_snoovatar")]
        public bool ShowSnoovatar { get; set; }

        [JsonProperty("force_https")]
        public bool ForceHTTPS { get; set; }

        [JsonProperty("geopopular")]
        public string Geopopular { get; set; }

        [JsonProperty("content_langs")]
        public List<string> ContentLangs { get; set; }

        public AccountPrefs(bool threadedMessages, bool hideDowns, bool labelNsfw, bool activityRelevantAds, bool emailMessages, bool profileOptOut, bool videoAutoplay,
            string acceptPms, bool thirdPartySiteDataPersonalizedContent, bool showLinkFlair, bool credditAutoRenew, bool showTrending, bool privateFeeds,
            bool monitorMentions, bool research, bool ignoreSuggestedSort, bool emailDigests, string media, bool clickGadget, bool useGlobalDefaults,
            bool over18, bool showStylesheets, bool liveOrangeReds, bool enableDefaultThemes, bool domainDetails, bool collapseLeftBar, string lang,
            bool hideUps, bool thirdPartyDataPersonalizedAds, bool allowClickTracking, bool hideFromRobots, bool showTwitter, bool compress, bool storeVisits, bool threadedModmail,
            int minLinkScore, string mediaPreview, bool nightMode, bool highlightControversial, bool thirdPartySiteDataPersonalizedAds, bool? showPromote,
            int minCommentScore, bool publicVotes, bool noVideoAutoplay, bool organic, bool collapseReadMessages, bool showFlair, bool markMessagesRead,
            bool searchIncludeOver18, bool noProfanity, bool hideAds, bool beta, bool topKarmaSubreddits, bool newWindow, int numSites, bool legacySearch,
            int numComments, bool showGoldExpiration, bool highlightNewComments, bool emailUnsubscribeAll, string defaultCommentSort, bool hideLocationBar,
            bool autoplay, string defaultThemeSr, bool publicServerSeconds, bool showSnoovatar, bool forceHttps, string geopopular, List<string> contentLangs)
                : base(threadedMessages, hideDowns, labelNsfw, activityRelevantAds, emailMessages, profileOptOut, videoAutoplay,
                acceptPms, thirdPartySiteDataPersonalizedContent, showLinkFlair, credditAutoRenew, showTrending, privateFeeds,
                monitorMentions, research, ignoreSuggestedSort, emailDigests, media, clickGadget, useGlobalDefaults,
                over18, showStylesheets, liveOrangeReds, enableDefaultThemes, domainDetails, collapseLeftBar, lang,
                hideUps, thirdPartyDataPersonalizedAds, allowClickTracking, hideFromRobots, showTwitter, compress, storeVisits, threadedModmail,
                minLinkScore, mediaPreview, nightMode, highlightControversial, thirdPartySiteDataPersonalizedAds, showPromote,
                minCommentScore, publicVotes, noVideoAutoplay, organic, collapseReadMessages, showFlair, markMessagesRead,
                searchIncludeOver18, noProfanity, hideAds, beta, topKarmaSubreddits, newWindow, numSites, legacySearch,
                numComments, showGoldExpiration, highlightNewComments, emailUnsubscribeAll, defaultCommentSort, hideLocationBar,
                autoplay)
        {
            Import(defaultThemeSr, publicServerSeconds, showSnoovatar, forceHttps, geopopular, contentLangs);
        }

        private void Import(string defaultThemeSr, bool publicServerSeconds, bool showSnoovatar, bool forceHttps, string geopopular, List<string> contentLangs)
        {
            DefaultThemeSr = defaultThemeSr;
            PublicServerSeconds = publicServerSeconds;
            ShowSnoovatar = showSnoovatar;
            ForceHTTPS = forceHttps;
            Geopopular = geopopular;
            ContentLangs = contentLangs;
        }
    }
}
