using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class UserFeatures
    {
        [JsonProperty("search_subreddit_traffic")]
        public bool SearchSubredditTraffic { get; set; }

        [JsonProperty("chat_subreddit")]
        public bool ChatSubreddit { get; set; }

        [JsonProperty("geopopular_mobile_holdout")]
        public JObject GeopopularMobileHoldout { get; set; }

        [JsonProperty("five_follower_send_message")]
        public bool FiveFollowerSendMessage { get; set; }

        [JsonProperty("oc_discovery")]
        public bool OcDiscovery { get; set; }

        [JsonProperty("chat_subreddit_notification_ftux")]
        public bool ChatSubredditNotificationFtux { get; set; }

        [JsonProperty("whitelisted_pms")]
        public bool WhitelistedPMs { get; set; }

        [JsonProperty("reddit_premium_gold_award_only")]
        public bool RedditPremiumGoldAwardOnly { get; set; }

        [JsonProperty("drafts_sharing")]
        public bool DraftsSharing { get; set; }

        [JsonProperty("ad_moderation")]
        public bool AdModeration { get; set; }

        [JsonProperty("legacy_search_pref")]
        public bool LegacySearchPref { get; set; }

        [JsonProperty("creator_notif_redis")]
        public bool CreatorNotifRedis { get; set; }

        [JsonProperty("pause_ads")]
        public bool PauseAds { get; set; }

        [JsonProperty("activity_service_write")]
        public bool ActivityServiceWrite { get; set; }

        [JsonProperty("mweb_xpromo_ad_loading_ios")]
        public JObject MwebXpromoAdLoadingIOS { get; set; }

        [JsonProperty("do_not_track")]
        public bool DoNotTrack { get; set; }

        [JsonProperty("outbound_clicktracking")]
        public bool OutboundClicktracking { get; set; }

        [JsonProperty("ios_profile_edit")]
        public bool IOSProfileEdit { get; set; }

        [JsonProperty("screenview_events")]
        public bool ScreenviewEvents { get; set; }

        [JsonProperty("native_ad_server")]
        public bool NativeAdServer { get; set; }

        [JsonProperty("redesign_ABvr2_loggedin")]
        public JObject RedesignABvr2Loggedin { get; set; }

        [JsonProperty("mweb_xpromo_interstitial_comments_android")]
        public bool MwebXpromoInterstitialCommentsAndroid { get; set; }

        [JsonProperty("post_embed")]
        public bool PostEmbed { get; set; }

        [JsonProperty("logistic_regression_v13")]
        public JObject LogisticRegressionV13 { get; set; }

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_android")]
        public bool MwebXpromoModalListingClickDailyDismissibleAndroid { get; set; }

        [JsonProperty("activity_service_read")]
        public bool ActivityServiceRead { get; set; }

        [JsonProperty("chat_menu_notification")]
        public bool ChatMenuNotification { get; set; }

        [JsonProperty("redesign_crosspost_creation")]
        public bool RedesignCrosspostCreation { get; set; }

        [JsonProperty("profile_redesign_pinning")]
        public bool ProfileRedesignPinning { get; set; }

        [JsonProperty("live_happening_now")]
        public bool LiveHappeningNow { get; set; }

        [JsonProperty("geopopular_in_holdout")]
        public JObject GeopopularInHoldout { get; set; }

        [JsonProperty("popular_re_sort_v3")]
        public JObject PopularReSortV3 { get; set; }

        [JsonProperty("block_user_by_report")]
        public bool BlockUserByReport { get; set; }

        [JsonProperty("orangereds_as_emails")]
        public bool OrangeRedsAsEmails { get; set; }

        [JsonProperty("external_accounts")]
        public bool ExternalAccounts { get; set; }

        [JsonProperty("programmatic_ads")]
        public bool ProgrammaticAds { get; set; }

        [JsonProperty("bypass_provider_preferences")]
        public bool BypassProviderPreferences { get; set; }

        [JsonProperty("show_user_sr_name")]
        public bool ShowUserSrName { get; set; }

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_link")]
        public bool MwebXpromoModalListingClickDailyDismissibleLink { get; set; }

        [JsonProperty("android_promoted_posts")]
        public bool AndroidPromotedPosts { get; set; }

        [JsonProperty("geopopular")]
        public bool Geopopular { get; set; }

        [JsonProperty("news")]
        public bool News { get; set; }

        [JsonProperty("subreddit_rules")]
        public bool SubredditRules { get; set; }

        [JsonProperty("user_otp")]
        public bool UserOtp { get; set; }

        [JsonProperty("new_overview")]
        public bool NewOverview { get; set; }

        [JsonProperty("chat_group_rollout")]
        public bool ChatGroupRollout { get; set; }

        [JsonProperty("redesign_beta")]
        public bool RedesignBeta { get; set; }

        [JsonProperty("adblock_test")]
        public bool AdblockTest { get; set; }

        [JsonProperty("reddit_request_sr_processing")]
        public bool RedditRequestSrProcessing { get; set; }

        [JsonProperty("mweb_xpromo_revamp")]
        public JObject MwebXpromoRevamp { get; set; }

        [JsonProperty("loadtest_sendbird_me")]
        public bool LoadtestSendbirdMe { get; set; }

        [JsonProperty("email_digest_header_prefix")]
        public JObject EmailDigestHeaderPrefix { get; set; }

        [JsonProperty("show_amp_link")]
        public bool ShowAmpLink { get; set; }

        [JsonProperty("reddit_premium")]
        public bool RedditPremium { get; set; }

        [JsonProperty("listing_service_rampup")]
        public bool ListingServiceRampup { get; set; }

        [JsonProperty("default_srs_holdout")]
        public JObject DefaultSrsHoldout { get; set; }

        [JsonProperty("profile_redesign_settings")]
        public bool ProfileRedesignSettings { get; set; }

        [JsonProperty("upgrade_cookies")]
        public bool UpgradeCookies { get; set; }

        [JsonProperty("interest_targeting")]
        public bool InterestTargeting { get; set; }

        [JsonProperty("ads_auction")]
        public bool AdsAuction { get; set; }

        [JsonProperty("mweb_xpromo_modal_listing_click_daily_dismissible_ios")]
        public bool MwebXpromoModalListingClickDailyDismissibleIOS { get; set; }

        [JsonProperty("expando_events")]
        public bool ExpandoEvents { get; set; }

        [JsonProperty("force_https")]
        public bool ForceHttps { get; set; }

        [JsonProperty("inbox_push")]
        public bool InboxPush { get; set; }

        [JsonProperty("oc_checkboxes")]
        public bool OcCheckboxes { get; set; }

        [JsonProperty("post_to_profile_beta")]
        public bool PostToProfileBeta { get; set; }

        [JsonProperty("crossposting_ga")]
        public bool CrosspostingGa { get; set; }

        [JsonProperty("https_redirect")]
        public bool HttpsRedirect { get; set; }

        [JsonProperty("top_content_email_digest_v2")]
        public JObject TopContentEmailDigestV2 { get; set; }

        [JsonProperty("original_content")]
        public bool OriginalContent { get; set; }

        [JsonProperty("profile_redesign_posts")]
        public bool ProfileRedesignPosts { get; set; }

        [JsonProperty("mobile_native_banner")]
        public bool MobileNativeBanner { get; set; }

        [JsonProperty("ads_auto_extend")]
        public bool AdsAutoExtend { get; set; }

        [JsonProperty("mobile_ad_image")]
        public bool MobileAdImage { get; set; }

        [JsonProperty("new_profile_layout")]
        public bool NewProfileLayout { get; set; }

        [JsonProperty("profile_redesign")]
        public bool ProfileRedesign { get; set; }

        [JsonProperty("adserver_reporting")]
        public bool AdserverReporting { get; set; }

        [JsonProperty("geopopular_gb_holdout")]
        public JObject GeopopularGbHoldout { get; set; }

        [JsonProperty("chat_rollout")]
        public bool ChatRollout { get; set; }

        [JsonProperty("chat")]
        public bool Chat { get; set; }

        [JsonProperty("mobile_web_targeting")]
        public bool MobileWebTargeting { get; set; }

        [JsonProperty("rte_video")]
        public bool RteVideo { get; set; }

        [JsonProperty("users_listing")]
        public bool UsersListing { get; set; }

        [JsonProperty("personalization_prefs")]
        public bool PersonalizationPrefs { get; set; }

        [JsonProperty("ads_auto_refund")]
        public bool AdsAutoRefund { get; set; }

        [JsonProperty("crossposting_recent")]
        public bool CrosspostingRecent { get; set; }

        [JsonProperty("heartbeat_events")]
        public bool HeartbeatEvents { get; set; }

        [JsonProperty("eu_cookie_policy")]
        public bool EUCookiePolicy { get; set; }

        [JsonProperty("oc_discovery_filtering")]
        public bool OcDiscoveryFiltering { get; set; }

        [JsonProperty("oc_creation")]
        public bool OcCreation { get; set; }

        [JsonProperty("new_loggedin_cache_policy")]
        public bool NewLoggedinCachePolicy { get; set; }

        [JsonProperty("give_hsts_grants")]
        public bool GiveHstsGrants { get; set; }

        [JsonProperty("ios_promoted_posts")]
        public bool IOSPromotedPosts { get; set; }

        [JsonProperty("mweb_xpromo_interstitial_comments_ios")]
        public bool MwebXpromoInterstitialCommentsIOS { get; set; }

        [JsonProperty("moat_tracking")]
        public bool MoatTracking { get; set; }

        [JsonProperty("drafts")]
        public bool Drafts { get; set; }

        [JsonProperty("profile_redesign_comments")]
        public bool ProfileRedesignComments { get; set; }

        [JsonProperty("scroll_events")]
        public bool ScrollEvents { get; set; }
    }
}
