using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class User
    {
        [JsonProperty("is_employee")]
        public bool IsEmployee;

        [JsonProperty("has_visited_new_profile")]
        public bool HasVisitedNewProfile;

        [JsonProperty("is_friend")]
        public bool IsFriend;

        [JsonProperty("pref_no_profanity")]
        public bool PrefNoProfanity;

        [JsonProperty("is_suspended")]
        public bool IsSuspended;

        [JsonProperty("pref_geopopular")]
        public string PrefGeopopular;

        [JsonProperty("pref_show_trending")]
        public bool PrefShowTrending;

        [JsonProperty("subreddit")]
        public UserSubreddit Subreddit;

        [JsonProperty("is_sponsor")]
        public bool IsSponsor;

        [JsonProperty("gold_expiration")]
        public string GoldExpiration;

        [JsonProperty("has_gold_subscription")]
        public bool HasGoldSubscription;

        [JsonProperty("num_friends")]
        public int NumFriends;

        [JsonProperty("features")]
        public UserFeatures Features;

        [JsonProperty("has_stripe_subscription")]
        public bool HasStripeSubscription;

        [JsonProperty("verified")]
        public bool Verified;

        [JsonProperty("new_modmail_exists")]
        public bool? NewModmailExists;

        [JsonProperty("pref_autoplay")]
        public bool PrefAutoplay;

        [JsonProperty("coins")]
        public int coins;

        [JsonProperty("has_paypal_subscription")]
        public bool HasPayPalSubscription;

        [JsonProperty("has_subscribed_to_premium")]
        public bool HasSubscribedToPremium;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("over_18")]
        public bool Over18;

        [JsonProperty("is_gold")]
        public bool IsGold;

        [JsonProperty("is_mod")]
        public bool IsMod;

        [JsonProperty("suspension_expiration_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime SuspensionExpirationUTC;

        [JsonProperty("has_verified_email")]
        public bool HasVerifiedEmail;

        [JsonProperty("has_external_account")]
        public bool HasExternalAccount;

        [JsonProperty("pref_video_autoplay")]
        public bool PrefVideoAutoplay;

        [JsonProperty("in_chat")]
        public bool InChat;

        [JsonProperty("in_redesign_beta")]
        public bool InRedesignBeta;

        [JsonProperty("icon_img")]
        public string IconImg;

        [JsonProperty("has_mod_mail")]
        public bool HasModMail;

        [JsonProperty("pref_nightmode")]
        public bool PrefNightmode;

        [JsonProperty("hide_from_robots")]
        public bool HideFromRobots;

        [JsonProperty("modhash")]
        public string Modhash;

        [JsonProperty("link_karma")]
        public int LinkKarma;

        [JsonProperty("force_password_reset")]
        public bool ForcePasswordReset;

        [JsonProperty("inbox_count")]
        public int InboxCount;

        [JsonProperty("pref_top_karma_subreddits")]
        public bool? PrefTopKarmaSubreddits;

        [JsonProperty("has_mail")]
        public bool HasMail;

        [JsonProperty("pref_show_snoovatar")]
        public bool PrefShowSnoovatar;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("pref_clickgadget")]
        public int PrefClickgadget;

        [JsonProperty("created")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime Created;

        [JsonProperty("gold_credits")]
        public int GoldCredits;

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(TimestampConvert))]
        public DateTime CreatedUTC;

        [JsonProperty("pref_show_twitter")]
        public bool PrefShowTwitter;

        [JsonProperty("in_beta")]
        public bool InBeta;

        [JsonProperty("comment_karma")]
        public int CommentKarma;

        [JsonProperty("has_subscribed")]
        public bool HasSubscribed;

        public User(Controllers.User user)
        {
            IsFriend = user.IsFriend;
            PrefNoProfanity = user.ProfanityFilter;
            IsSuspended = user.IsSuspended;
            HasGoldSubscription = user.HasGoldSubscription;
            NumFriends = user.NumFriends;
            Verified = user.IsVerified;
            NewModmailExists = user.HasNewModmail;
            Id = user.Id;
            Over18 = user.Over18;
            IsGold = user.IsGold;
            IsMod = user.IsMod;
            HasVerifiedEmail = user.HasVerifiedEmail;
            IconImg = user.IconImg;
            HasModMail = user.HasModmail;
            LinkKarma = user.LinkKarma;
            InboxCount = user.InboxCount;
            HasMail = user.HasMail;
            Name = user.Name;
            Created = user.Created;
            CommentKarma = user.CommentKarma;
            HasSubscribed = user.HasSubscribed;
        }

        public User() { }
    }
}
