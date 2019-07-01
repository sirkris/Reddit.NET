using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class User
    {
        [JsonProperty("is_employee")]
        public bool IsEmployee { get; set; }

        [JsonProperty("has_visited_new_profile")]
        public bool HasVisitedNewProfile { get; set; }

        [JsonProperty("is_friend")]
        public bool IsFriend { get; set; }

        [JsonProperty("pref_no_profanity")]
        public bool PrefNoProfanity { get; set; }

        [JsonProperty("is_suspended")]
        public bool IsSuspended { get; set; }

        [JsonProperty("pref_geopopular")]
        public string PrefGeopopular { get; set; }

        [JsonProperty("pref_show_trending")]
        public bool PrefShowTrending { get; set; }

        [JsonProperty("subreddit")]
        public UserSubreddit Subreddit { get; set; }

        [JsonProperty("is_sponsor")]
        public bool IsSponsor { get; set; }

        [JsonProperty("gold_expiration")]
        public string GoldExpiration { get; set; }

        [JsonProperty("has_gold_subscription")]
        public bool HasGoldSubscription { get; set; }

        [JsonProperty("num_friends")]
        public int NumFriends { get; set; }

        [JsonProperty("features")]
        public UserFeatures Features { get; set; }

        [JsonProperty("has_stripe_subscription")]
        public bool HasStripeSubscription { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("new_modmail_exists")]
        public bool? NewModmailExists { get; set; }

        [JsonProperty("pref_autoplay")]
        public bool PrefAutoplay { get; set; }

        [JsonProperty("coins")]
        public int Coins { get; set; }

        [JsonProperty("has_paypal_subscription")]
        public bool HasPayPalSubscription { get; set; }

        [JsonProperty("has_subscribed_to_premium")]
        public bool HasSubscribedToPremium { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("is_gold")]
        public bool IsGold { get; set; }

        [JsonProperty("is_mod")]
        public bool IsMod { get; set; }

        [JsonProperty("suspension_expiration_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime SuspensionExpirationUTC { get; set; }

        [JsonProperty("has_verified_email")]
        public bool HasVerifiedEmail { get; set; }

        [JsonProperty("has_external_account")]
        public bool HasExternalAccount { get; set; }

        [JsonProperty("pref_video_autoplay")]
        public bool PrefVideoAutoplay { get; set; }

        [JsonProperty("in_chat")]
        public bool InChat { get; set; }

        [JsonProperty("in_redesign_beta")]
        public bool InRedesignBeta { get; set; }

        [JsonProperty("icon_img")]
        public string IconImg { get; set; }

        [JsonProperty("has_mod_mail")]
        public bool HasModMail { get; set; }

        [JsonProperty("pref_nightmode")]
        public bool PrefNightmode { get; set; }

        [JsonProperty("hide_from_robots")]
        public bool HideFromRobots { get; set; }

        [JsonProperty("modhash")]
        public string Modhash { get; set; }

        [JsonProperty("link_karma")]
        public int LinkKarma { get; set; }

        [JsonProperty("force_password_reset")]
        public bool ForcePasswordReset { get; set; }

        [JsonProperty("inbox_count")]
        public int InboxCount { get; set; }

        [JsonProperty("pref_top_karma_subreddits")]
        public bool? PrefTopKarmaSubreddits { get; set; }

        [JsonProperty("has_mail")]
        public bool HasMail { get; set; }

        [JsonProperty("pref_show_snoovatar")]
        public bool PrefShowSnoovatar { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pref_clickgadget")]
        public int PrefClickgadget { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(LocalTimestampConverter))]
        [Obsolete("Using this date can lead to unexpected results.  It is recommended that you use " + nameof(CreatedUTC) + " instead.")]
        public DateTime Created { get; set; }

        [JsonProperty("gold_credits")]
        public int GoldCredits { get; set; }

        [JsonProperty("created_utc")]
        [JsonConverter(typeof(UtcTimestampConverter))]
        public DateTime CreatedUTC { get; set; }

        [JsonProperty("pref_show_twitter")]
        public bool PrefShowTwitter { get; set; }

        [JsonProperty("in_beta")]
        public bool InBeta { get; set; }

        [JsonProperty("comment_karma")]
        public int CommentKarma { get; set; }

        [JsonProperty("has_subscribed")]
        public bool HasSubscribed { get; set; }

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
            CreatedUTC = user.Created;
            CommentKarma = user.CommentKarma;
            HasSubscribed = user.HasSubscribed;
        }

        public User() { }
    }
}
