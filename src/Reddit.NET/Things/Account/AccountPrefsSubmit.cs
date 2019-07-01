using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class AccountPrefsSubmit : AccountPrefsBase
    {
        [JsonProperty("g")]
        public string G { get; set; }

        [JsonProperty("in_redesign_beta")]
        public bool InRedesignBeta { get; set; }

        [JsonProperty("other_theme")]
        public string OtherTheme { get; set; }

        public AccountPrefsSubmit(AccountPrefs accountPrefs, string g, bool inRedesignBeta, string otherTheme)
            : base(accountPrefs.ThreadedMessages, accountPrefs.HideDowns, accountPrefs.LabelNSFW, accountPrefs.ActivityRelevantAds, accountPrefs.EmailMessages, 
                  accountPrefs.ProfileOptOut, accountPrefs.VideoAutoplay, accountPrefs.AcceptPms, accountPrefs.ThirdPartySiteDataPersonalizedContent, 
                  accountPrefs.ShowLinkFlair, accountPrefs.CredditAutoRenew, accountPrefs.ShowTrending, accountPrefs.PrivateFeeds, accountPrefs.MonitorMentions, 
                  accountPrefs.Research, accountPrefs.IgnoreSuggestedSort, accountPrefs.EmailDigests, accountPrefs.Media, accountPrefs.ClickGadget, 
                  accountPrefs.UseGlobalDefaults, accountPrefs.Over18, accountPrefs.ShowStylesheets, accountPrefs.LiveOrangeReds, accountPrefs.EnableDefaultThemes, 
                  accountPrefs.DomainDetails, accountPrefs.CollapseLeftBar, accountPrefs.Lang, accountPrefs.HideUps, accountPrefs.ThirdPartyDataPersonalizedAds, 
                  accountPrefs.AllowClickTracking, accountPrefs.HideFromRobots, accountPrefs.ShowTwitter, accountPrefs.Compress, accountPrefs.StoreVisits, 
                  accountPrefs.ThreadedModmail, accountPrefs.MinLinkScore, accountPrefs.MediaPreview, accountPrefs.NightMode, accountPrefs.HighlightControversial, 
                  accountPrefs.ThirdPartySiteDataPersonalizedAds, accountPrefs.ShowPromote, accountPrefs.MinCommentScore, accountPrefs.PublicVotes, 
                  accountPrefs.NoVideoAutoplay, accountPrefs.Organic, accountPrefs.CollapseReadMessages, accountPrefs.ShowFlair, accountPrefs.MarkMessagesRead, 
                  accountPrefs.SearchIncludeOver18, accountPrefs.NoProfanity, accountPrefs.HideAds, accountPrefs.Beta, accountPrefs.TopKarmaSubreddits, 
                  accountPrefs.NewWindow, accountPrefs.NumSites, accountPrefs.LegacySearch, accountPrefs.NumComments, accountPrefs.ShowGoldExpiration, 
                  accountPrefs.HighlightNewComments, accountPrefs.EmailUnsubscribeAll, accountPrefs.DefaultCommentSort, accountPrefs.HideLocationBar, accountPrefs.Autoplay)
        {
            Import(g, inRedesignBeta, otherTheme);
        }

        public AccountPrefsSubmit(bool threadedMessages, bool hideDowns, bool labelNsfw, bool activityRelevantAds, bool emailMessages, bool profileOptOut, bool videoAutoplay,
            string acceptPms, bool thirdPartySiteDataPersonalizedContent, bool showLinkFlair, bool credditAutoRenew, bool showTrending, bool privateFeeds,
            bool monitorMentions, bool research, bool ignoreSuggestedSort, bool emailDigests, string media, bool clickGadget, bool useGlobalDefaults,
            bool over18, bool showStylesheets, bool liveOrangeReds, bool enableDefaultThemes, bool domainDetails, bool collapseLeftBar, string lang,
            bool hideUps, bool thirdPartyDataPersonalizedAds, bool allowClickTracking, bool hideFromRobots, bool showTwitter, bool compress, bool storeVisits, bool threadedModmail,
            int minLinkScore, string mediaPreview, bool nightMode, bool highlightControversial, bool thirdPartySiteDataPersonalizedAds, bool? showPromote,
            int minCommentScore, bool publicVotes, bool noVideoAutoplay, bool organic, bool collapseReadMessages, bool showFlair, bool markMessagesRead,
            bool searchIncludeOver18, bool noProfanity, bool hideAds, bool beta, bool topKarmaSubreddits, bool newWindow, int numSites, bool legacySearch,
            int numComments, bool showGoldExpiration, bool highlightNewComments, bool emailUnsubscribeAll, string defaultCommentSort, bool hideLocationBar,
            bool autoplay, string g, bool inRedesignBeta, string otherTheme)
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
            Import(g, inRedesignBeta, otherTheme);
        }

        private void Import(string g, bool inRedesignBeta, string otherTheme)
        {
            G = g;
            InRedesignBeta = inRedesignBeta;
            OtherTheme = otherTheme;
        }
    }
}
