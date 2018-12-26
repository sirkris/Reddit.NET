using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserFeatures
    {
        [JsonProperty("search_subreddit_traffic")]
        public bool SearchSubredditTraffic;

        [JsonProperty("chat_subreddit")]
        public bool ChatSubreddit;

        [JsonProperty("geopopular_mobile_holdout")]
        public JObject GeopopularMobileHoldout;

        [JsonProperty("five_follower_send_message")]
        public bool FiveFollowerSendMessage;

        [JsonProperty("oc_discovery")]
        public bool OcDiscovery;

        [JsonProperty("chat_subreddit_notification_ftux")]
        public bool ChatSubredditNotificationFtux;

        [JsonProperty("whitelisted_pms")]
        public bool WhitelistedPMs;

        [JsonProperty("reddit_premium_gold_award_only")]
        public bool RedditPremiumGoldAwardOnly;

        [JsonProperty("drafts_sharing")]
        public bool DraftsSharing;

        [JsonProperty("ad_moderation")]
        public bool AdModeration;

        [JsonProperty("legacy_search_pref")]
        public bool LegacySearchPref;

        [JsonProperty("creator_notif_redis")]
        public bool CreatorNotifRedis;

        [JsonProperty("pause_ads")]
        public bool PauseAds;

        [JsonProperty("activity_service_write")]
        public bool ActivityServiceWrite;

        [JsonProperty("mweb_xpromo_ad_loading_ios")]
        public JObject MwebXpromoAdLoadingIOS;

        [JsonProperty("do_not_track")]
        public bool DoNotTrack;

        [JsonProperty("outbound_clicktracking")]
        public bool OutboundClicktracking;

        [JsonProperty("ios_profile_edit")]
        public bool IOSProfileEdit;

        [JsonProperty("screenview_events")]
        public bool ScreenviewEvents;

        [JsonProperty("native_ad_server")]
        public bool NativeAdServer;

        [JsonProperty("redesign_ABvr2_loggedin")]
        public JObject RedesignABvr2Loggedin;

        [JsonProperty("mweb_xpromo_interstitial_comments_android")]
        public bool MwebXpromoInterstitialCommentsAndroid;

        [JsonProperty("post_embed")]
        public bool PostEmbed;

        [JsonProperty("logistic_regression_v13")]
        public JObject LogisticRegressionV13;

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_android")]
        public bool MwebXpromoModalListingClickDailyDismissibleAndroid;

        [JsonProperty("activity_service_read")]
        public bool ActivityServiceRead;

        [JsonProperty("chat_menu_notification")]
        public bool ChatMenuNotification;

        [JsonProperty("redesign_crosspost_creation")]
        public bool RedesignCrosspostCreation;

        [JsonProperty("profile_redesign_pinning")]
        public bool ProfileRedesignPinning;

        [JsonProperty("live_happening_now")]
        public bool LiveHappeningNow;

        [JsonProperty("geopopular_in_holdout")]
        public JObject GeopopularInHoldout;

        [JsonProperty("popular_re_sort_v3")]
        public JObject PopularReSortV3;

        [JsonProperty("block_user_by_report")]
        public bool BlockUserByReport;

        [JsonProperty("orangereds_as_emails")]
        public bool OrangeRedsAsEmails;

        [JsonProperty("external_accounts")]
        public bool ExternalAccounts;

        [JsonProperty("programmatic_ads")]
        public bool ProgrammaticAds;

        [JsonProperty("bypass_provider_preferences")]
        public bool BypassProviderPreferences;

        [JsonProperty("show_user_sr_name")]
        public bool ShowUserSrName;

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_link")]
        public bool MwebXpromoModalListingClickDailyDismissibleLink;

        [JsonProperty("android_promoted_posts")]
        public bool AndroidPromotedPosts;

        [JsonProperty("geopopular")]
        public bool Geopopular;

        [JsonProperty("news")]
        public bool News;

        [JsonProperty("subreddit_rules")]
        public bool SubredditRules;

        [JsonProperty("user_otp")]
        public bool UserOtp;

        [JsonProperty("new_overview")]
        public bool NewOverview;

        [JsonProperty("chat_group_rollout")]
        public bool ChatGroupRollout;

        [JsonProperty("redesign_beta")]
        public bool RedesignBeta;

        [JsonProperty("adblock_test")]
        public bool AdblockTest;

        [JsonProperty("reddit_request_sr_processing")]
        public bool RedditRequestSrProcessing;

        [JsonProperty("mweb_xpromo_revamp")]
        public JObject MwebXpromoRevamp;

        [JsonProperty("loadtest_sendbird_me")]
        public bool LoadtestSendbirdMe;

        [JsonProperty("email_digest_header_prefix")]
        public JObject EmailDigestHeaderPrefix;

        [JsonProperty("show_amp_link")]
        public bool ShowAmpLink;

        [JsonProperty("reddit_premium")]
        public bool RedditPremium;

        [JsonProperty("listing_service_rampup")]
        public bool ListingServiceRampup;

        [JsonProperty("default_srs_holdout")]
        public JObject DefaultSrsHoldout;

        [JsonProperty("profile_redesign_settings")]
        public bool ProfileRedesignSettings;

        [JsonProperty("upgrade_cookies")]
        public bool UpgradeCookies;

        [JsonProperty("interest_targeting")]
        public bool InterestTargeting;

        [JsonProperty("ads_auction")]
        public bool AdsAuction;

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_ios")]
        public bool MwebXpromoModalListingClickDailyDismissibleIOS;

        [JsonProperty("expando_events")]
        public bool ExpandoEvents;

        [JsonProperty("force_https")]
        public bool ForceHttps;

        [JsonProperty("inbox_push")]
        public bool InboxPush;

        [JsonProperty("oc_checkboxes")]
        public bool OcCheckboxes;

        [JsonProperty("post_to_profile_beta")]
        public bool PostToProfileBeta;

        [JsonProperty("crossposting_ga")]
        public bool CrosspostingGa;

        [JsonProperty("https_redirect")]
        public bool HttpsRedirect;

        [JsonProperty("top_content_email_digest_v2")]
        public JObject TopContentEmailDigestV2;

        [JsonProperty("original_content")]
        public bool OriginalContent;

        [JsonProperty("profile_redesign_posts")]
        public bool ProfileRedesignPosts;

        [JsonProperty("mobile_native_banner")]
        public bool MobileNativeBanner;

        [JsonProperty("ads_auto_extend")]
        public bool AdsAutoExtend;

        [JsonProperty("mobile_ad_image")]
        public bool MobileAdImage;

        [JsonProperty("new_profile_layout")]
        public bool NewProfileLayout;

        [JsonProperty("profile_redesign")]
        public bool ProfileRedesign;

        [JsonProperty("adserver_reporting")]
        public bool AdserverReporting;

        [JsonProperty("geopopular_gb_holdout")]
        public JObject GeopopularGbHoldout;

        [JsonProperty("chat_rollout")]
        public bool ChatRollout;

        [JsonProperty("chat")]
        public bool Chat;

        [JsonProperty("mobile_web_targeting")]
        public bool MobileWebTargeting;

        [JsonProperty("rte_video")]
        public bool RteVideo;

        [JsonProperty("users_listing")]
        public bool UsersListing;

        [JsonProperty("personalization_prefs")]
        public bool PersonalizationPrefs;

        [JsonProperty("ads_auto_refund")]
        public bool AdsAutoRefund;

        [JsonProperty("crossposting_recent")]
        public bool CrosspostingRecent;

        [JsonProperty("heartbeat_events")]
        public bool HeartbeatEvents;

        [JsonProperty("eu_cookie_policy")]
        public bool EUCookiePolicy;

        [JsonProperty("oc_discovery_filtering")]
        public bool OcDiscoveryFiltering;

        [JsonProperty("oc_creation")]
        public bool OcCreation;

        [JsonProperty("new_loggedin_cache_policy")]
        public bool NewLoggedinCachePolicy;

        [JsonProperty("give_hsts_grants")]
        public bool GiveHstsGrants;

        [JsonProperty("ios_promoted_posts")]
        public bool IOSPromotedPosts;

        [JsonProperty("mweb_xpromo_interstitial_comments_ios")]
        public bool MwebXpromoInterstitialCommentsIOS;

        [JsonProperty("moat_tracking")]
        public bool MoatTracking;

        [JsonProperty("drafts")]
        public bool Drafts;

        [JsonProperty("profile_redesign_comments")]
        public bool ProfileRedesignComments;

        [JsonProperty("scroll_events")]
        public bool ScrollEvents;
    }
}
