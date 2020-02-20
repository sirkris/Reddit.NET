using Newtonsoft.Json.Linq;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.Moderation;
using Reddit.Inputs.Search;
using Reddit.Inputs.Subreddits;
using Reddit.Inputs.Users;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for subreddits.
    /// </summary>
    public class Subreddit : BaseController
    {
        // Subreddit data pertaining to the logged-in user can be found in SubredditData.  --Kris
        /// <summary>
        /// The banner image URL.
        /// </summary>
        public string BannerImg
        {
            get
            {
                return SubredditData?.BannerImg;
            }
            set
            {
                ImportToExisting(bannerImg: value);
            }
        }

        /// <summary>
        /// The banner background color.
        /// </summary>
        public string BannerBackgroundColor
        {
            get
            {
                return SubredditData?.BannerBackgroundColor;
            }
            set
            {
                ImportToExisting(bannerBackgroundColor: value);
            }
        }

        /// <summary>
        /// The banner background image URL.
        /// </summary>
        public string BannerBackgroundImage
        {
            get
            {
                return SubredditData?.BannerBackgroundImage;
            }
            set
            {
                ImportToExisting(bannerBackgroundImage: value);
            }
        }

        /// <summary>
        /// The subreddit type (public, restricted, or private)
        /// </summary>
        public string SubredditType
        {
            get
            {
                return SubredditData?.SubredditType;
            }
            set
            {
                ImportToExisting(subredditType: value);
            }
        }

        /// <summary>
        /// The community icon URL.
        /// </summary>
        public string CommunityIcon
        {
            get
            {
                return SubredditData?.CommunityIcon;
            }
            set
            {
                ImportToExisting(communityIcon: value);
            }
        }

        /// <summary>
        /// The header title.
        /// </summary>
        public string HeaderTitle
        {
            get
            {
                return SubredditData?.HeaderTitle;
            }
            set
            {
                ImportToExisting(headerTitle: value);
            }
        }

        /// <summary>
        /// Whether the wiki is enabled for this subreddit.
        /// </summary>
        public bool WikiEnabled
        {
            get
            {
                return (SubredditData != null && SubredditData.WikiEnabled.HasValue ? SubredditData.WikiEnabled.Value : false);
            }
            set
            {
                ImportToExisting(wikiEnabled: value);
            }
        }

        /// <summary>
        /// Whether you have to be over 18 to view this subreddit.
        /// </summary>
        public bool? Over18
        {
            get
            {
                return SubredditData?.Over18;
            }
            set
            {
                ImportToExisting(over18: value);
            }
        }

        /// <summary>
        /// The sidebar text.
        /// </summary>
        public string Sidebar
        {
            get
            {
                return SubredditData?.Description;
            }
            set
            {
                ImportToExisting(sidebar: value);
            }
        }

        /// <summary>
        /// The subreddit name.
        /// </summary>
        public string Name
        {
            get
            {
                return SubredditData?.DisplayName;
            }
            set
            {
                ImportToExisting(name: value);
            }
        }

        /// <summary>
        /// The header image.
        /// </summary>
        public byte[] HeaderImg
        {
            get
            {
                return (byte[])SubredditData?.HeaderImg;
            }
            set
            {
                ImportToExisting(headerImage: value);
            }
        }

        /// <summary>
        /// The subreddit title.
        /// </summary>
        public string Title
        {
            get
            {
                return SubredditData?.Title;
            }
            set
            {
                ImportToExisting(title: value);
            }
        }

        /// <summary>
        /// Whether to collapse deleted comments.
        /// </summary>
        public bool? CollapseDeletedComments
        {
            get
            {
                return SubredditData?.CollapseDeletedComments;
            }
            set
            {
                ImportToExisting(collapseDeletedComments: value);
            }
        }

        /// <summary>
        /// The ID36 of this subreddit.
        /// </summary>
        public string Id
        {
            get
            {
                return SubredditData?.Id;
            }
            set
            {
                ImportToExisting(id: value);
            }
        }

        /// <summary>
        /// Whether emojis are enabled.
        /// </summary>
        public bool EmojisEnabled
        {
            get
            {
                return (SubredditData != null ? SubredditData.EmojisEnabled : false);
            }
            set
            {
                ImportToExisting(emojisEnabled: value);
            }
        }

        /// <summary>
        /// Whether to show media.
        /// </summary>
        public bool? ShowMedia
        {
            get
            {
                return SubredditData?.ShowMedia;
            }
            set
            {
                ImportToExisting(showMedia: value);
            }
        }

        /// <summary>
        /// Whether to allow videos.
        /// </summary>
        public bool AllowVideos
        {
            get
            {
                return (SubredditData != null ? SubredditData.AllowVideos : false);
            }
            set
            {
                ImportToExisting(allowVideos: value);
            }
        }

        /// <summary>
        /// Whether user flair can be assigned.
        /// </summary>
        public bool CanAssignUserFlair
        {
            get
            {
                return (SubredditData != null ? SubredditData.CanAssignUserFlair : false);
            }
            set
            {
                ImportToExisting(canAssignUserFlair: value);
            }
        }

        /// <summary>
        /// Whether spoilers are enabled.
        /// </summary>
        public bool? SpoilersEnabled
        {
            get
            {
                return SubredditData?.SpoilersEnabled;
            }
            set
            {
                ImportToExisting(allowSpoilers: value);
            }
        }

        /// <summary>
        /// The primary color.
        /// </summary>
        public string PrimaryColor
        {
            get
            {
                return SubredditData?.PrimaryColor;
            }
            set
            {
                ImportToExisting(primaryColor: value);
            }
        }

        /// <summary>
        /// The suggested comment sort for this subreddit.
        /// </summary>
        public string SuggestedCommentSort
        {
            get
            {
                return SubredditData?.SuggestedCommentSort;
            }
            set
            {
                ImportToExisting(suggestedCommentSort: value);
            }
        }

        /// <summary>
        /// The active user count.
        /// </summary>
        public int? ActiveUserCount
        {
            get
            {
                return SubredditData?.ActiveUserCount;
            }
            set
            {
                ImportToExisting(activeUserCount: value ?? 0);
            }
        }

        /// <summary>
        /// The icon image.
        /// </summary>
        public byte[] IconImg
        {
            get
            {
                return (byte[])SubredditData?.IconImg;
            }
            set
            {
                ImportToExisting(iconImage: value);
            }
        }

        /// <summary>
        /// Whether link flair can be assigned.
        /// </summary>
        public bool CanAssignLinkFlair
        {
            get
            {
                return (SubredditData != null ? SubredditData.CanAssignLinkFlair : false);
            }
            set
            {
                ImportToExisting(canAssignLinkFlair: value);
            }
        }

        /// <summary>
        /// Whether to allow video GIFs.
        /// </summary>
        public bool AllowVideoGifs
        {
            get
            {
                return (SubredditData != null ? SubredditData.AllowVideoGifs : false);
            }
            set
            {
                ImportToExisting(allowVideoGifs: value);
            }
        }

        /// <summary>
        /// The number of subscribers.
        /// </summary>
        public int? Subscribers
        {
            get
            {
                return SubredditData?.Subscribers;
            }
            set
            {
                ImportToExisting(subscribers: value);
            }
        }

        /// <summary>
        /// The submit text label.
        /// </summary>
        public string SubmitTextLabel
        {
            get
            {
                return SubredditData?.SubmitLinkLabel;
            }
            set
            {
                ImportToExisting(submitLinkLabel: value);
            }
        }

        /// <summary>
        /// The key color.
        /// </summary>
        public string KeyColor
        {
            get
            {
                return SubredditData?.KeyColor;
            }
            set
            {
                ImportToExisting(keyColor: value);
            }
        }

        /// <summary>
        /// The language.
        /// </summary>
        public string Lang
        {
            get
            {
                return SubredditData?.Lang;
            }
            set
            {
                ImportToExisting(lang: value);
            }
        }

        /// <summary>
        /// The subreddit fullname.
        /// </summary>
        public string Fullname
        {
            get
            {
                return SubredditData?.Name;
            }
            set
            {
                ImportToExisting(fullname: value);
            }
        }

        /// <summary>
        /// When the subreddit was created.
        /// </summary>
        public DateTime Created
        {
            get
            {
                return (SubredditData != null ? SubredditData.CreatedUTC : default(DateTime));
            }
            set
            {
                ImportToExisting(created: value);
            }
        }

        /// <summary>
        /// The URL.
        /// </summary>
        public string URL
        {
            get
            {
                return SubredditData?.URL;
            }
            set
            {
                ImportToExisting(url: value);
            }
        }

        /// <summary>
        /// The submit link label.
        /// </summary>
        public string SubmitLinkLabel
        {
            get
            {
                return SubredditData?.SubmitLinkLabel;
            }
            set
            {
                ImportToExisting(submitLinkLabel: value);
            }
        }

        /// <summary>
        /// Whether to allow discovery.
        /// </summary>
        public bool? AllowDiscovery
        {
            get
            {
                return SubredditData?.AllowDiscovery;
            }
            set
            {
                ImportToExisting(allowDiscovery: value);
            }
        }

        /// <summary>
        /// The subreddit description.
        /// </summary>
        public string Description
        {
            get
            {
                return SubredditData?.PublicDescription;
            }
            set
            {
                ImportToExisting(description: value);
            }
        }

        /// <summary>
        /// Whether link flair is enabled.
        /// </summary>
        public bool? LinkFlairEnabled
        {
            get
            {
                return SubredditData?.LinkFlairEnabled;
            }
            set
            {
                ImportToExisting(linkFlairEnabled: value);
            }
        }

        /// <summary>
        /// Whether to allow images.
        /// </summary>
        public bool? AllowImages
        {
            get
            {
                return SubredditData?.AllowImages;
            }
            set
            {
                ImportToExisting(allowImages: value);
            }
        }

        /// <summary>
        /// How many minutes to hide comment scores.
        /// </summary>
        public int? CommentScoreHideMins
        {
            get
            {
                return SubredditData?.CommentScoreHideMins;
            }
            set
            {
                ImportToExisting(commentScoreHideMins: value);
            }
        }

        /// <summary>
        /// Whether to show media previews.
        /// </summary>
        public bool? ShowMediaPreview
        {
            get
            {
                return SubredditData?.ShowMediaPreview;
            }
            set
            {
                ImportToExisting(showMediaPreview: value);
            }
        }

        /// <summary>
        /// The submission type.
        /// </summary>
        public string SubmissionType
        {
            get
            {
                return SubredditData?.SubmissionType;
            }
            set
            {
                ImportToExisting(submissionType: value);
            }
        }

        /// <summary>
        /// Full subreddit data retrieved from the API.
        /// </summary>
        public Things.Subreddit SubredditData
        {
            get;
            private set;
        }

        /// <summary>
        /// Posts belonging to this subreddit.
        /// </summary>
        public SubredditPosts Posts
        {
            get
            {
                if (posts == null)
                {
                    posts = new SubredditPosts(Dispatch, Name);
                }

                return posts;
            }
            set
            {
                posts = value;
            }
        }
        private SubredditPosts posts;

        /// <summary>
        /// Comments belonging to this subreddit.
        /// </summary>
        public Comments Comments
        {
            get
            {
                if (comments == null)
                {
                    comments = new Comments(Dispatch, subreddit: Name);
                }

                return comments;
            }
            set
            {
                comments = value;
            }
        }
        private Comments comments;

        /// <summary>
        /// Flairs belonging to this subreddit.
        /// </summary>
        public Flairs Flairs
        {
            get
            {
                if (flairs == null)
                {
                    flairs = new Flairs(Dispatch, Name);
                }

                return flairs;
            }
            set
            {
                flairs = value;
            }
        }
        private Flairs flairs;

        /// <summary>
        /// The subreddit wiki controller.
        /// </summary>
        public Wiki Wiki
        {
            get
            {
                if (wiki == null)
                {
                    wiki = new Wiki(Dispatch, Name);
                }

                return wiki;
            }
            set
            {
                wiki = value;
            }
        }
        private Wiki wiki;

        internal Dispatch Dispatch;

        /// <summary>
        /// Get the submission text for the subreddit.
        /// This text is set by the subreddit moderators and intended to be displayed on the submission form.
        /// </summary>
        public Things.SubredditSubmitText SubmitText
        {
            get
            {
                return (SubmitTextLastUpdated.HasValue
                    && SubmitTextLastUpdated.Value.AddHours(1) > DateTime.Now ? submitText : GetSubmitText());
            }
            set
            {
                submitText = value;
                SubmitTextLastUpdated = DateTime.Now;
            }
        }
        internal Things.SubredditSubmitText submitText;
        private DateTime? SubmitTextLastUpdated { get; set; }

        /// <summary>
        /// Get the moderators of this subreddit.
        /// </summary>
        public List<Moderator> Moderators
        {
            get
            {
                return (ModeratorsLastUpdated.HasValue
                    && ModeratorsLastUpdated.Value.AddMinutes(1) > DateTime.Now ? moderators : GetModerators());
            }
            set
            {
                moderators = value;
                ModeratorsLastUpdated = DateTime.Now;
            }
        }
        internal List<Moderator> moderators;
        private DateTime? ModeratorsLastUpdated { get; set; }

        /// <summary>
        /// Create a new subreddit controller instance populated from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit"></param>
        public Subreddit(Dispatch dispatch, Things.Subreddit subreddit)
            : base()
        {
            Dispatch = dispatch;
            ImportFromModel(subreddit);

            SubredditData = subreddit;
        }

        /// <summary>
        /// Create a new subreddit controller instance populated from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subredditChild"></param>
        public Subreddit(Dispatch dispatch, Things.SubredditChild subredditChild)
            : base()
        {
            Dispatch = dispatch;
            ImportFromModel(subredditChild.Data);

            SubredditData = subredditChild.Data;
        }

        /// <summary>
        /// Copy another subreddit controller instance onto this one.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">A valid subreddit controller instance</param>
        public Subreddit(Dispatch dispatch, Subreddit subreddit)
        {
            Dispatch = dispatch;

            Import(subreddit, true);

            SubredditData = subreddit.SubredditData;
        }

        /// <summary>
        /// Create a new subreddit controller instance, populated manually.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="sidebar"></param>
        /// <param name="submissionText"></param>
        /// <param name="lang"></param>
        /// <param name="subredditType"></param>
        /// <param name="submissionType"></param>
        /// <param name="submitLinkLabel"></param>
        /// <param name="submitTextLabel"></param>
        /// <param name="wikiEnabled"></param>
        /// <param name="over18"></param>
        /// <param name="allowDiscovery"></param>
        /// <param name="allowSpoilers"></param>
        /// <param name="showMedia"></param>
        /// <param name="showMediaPreview"></param>
        /// <param name="allowImages"></param>
        /// <param name="allowVideos"></param>
        /// <param name="collapseDeletedComments"></param>
        /// <param name="suggestedCommentSort"></param>
        /// <param name="commentScoreHideMins"></param>
        /// <param name="headerImage"></param>
        /// <param name="iconImage"></param>
        /// <param name="primaryColor"></param>
        /// <param name="keyColor"></param>
        /// <param name="fullname"></param>
        public Subreddit(Dispatch dispatch, string name, string title = "", string description = "", string sidebar = "",
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null,
            string fullname = null)
            : base()
        {
            Dispatch = dispatch;

            SetValues(name, title, description, sidebar, submissionText, lang, subredditType, submissionType, submitLinkLabel, submitTextLabel,
                wikiEnabled, over18, allowDiscovery, allowSpoilers, showMedia, showMediaPreview, allowImages, allowVideos, collapseDeletedComments,
                suggestedCommentSort, commentScoreHideMins, headerImage, iconImage, primaryColor, keyColor, fullname);
        }

        /// <summary>
        /// Create an empty subreddit controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public Subreddit(Dispatch dispatch)
            : base()
        {
            Dispatch = dispatch;
        }

        private void ImportFromModel(Things.Subreddit subreddit)
        {
            SubredditData = subreddit;
        }

        /// <summary>
        /// Copy all property values from another subreddit instance onto this one.
        /// </summary>
        /// <param name="subreddit">The subreddit instance being copied</param>
        /// <param name="overwrite">If true, any existing values are overwritten</param>
        private void Import(Subreddit subreddit, bool overwrite = true)
        {
            foreach (PropertyInfo property in typeof(Subreddit).GetProperties())
            {
                if (property.CanWrite
                    && (overwrite
                        || property.GetValue(this, null) == null))
                {
                    property.SetValue(this, property.GetValue(subreddit, null), null);
                }
            }
        }

        private void SetValues(string name, string title, string description, string sidebar,
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null,
            string fullname = null, string bannerImg = null, string bannerBackgroundColor = null, string bannerBackgroundImage = null, 
            string communityIcon = null, bool emojisEnabled = true, string headerTitle = null, string id = null, bool canAssignUserFlair = false, 
            bool canAssignLinkFlair = false, int activeUserCount = 0, bool allowVideoGifs = true, int subscribers = 0, DateTime created = default(DateTime), 
            string url = null, bool linkFlairEnabled = false)
        {
            SubredditData = new Things.Subreddit
            {
                DisplayName = name,
                Title = title,
                PublicDescription = description,
                Description = sidebar,
                SubmitText = submissionText,
                SubmitTextHTML = submissionText,
                Lang = lang,
                SubredditType = subredditType,
                SubmissionType = submissionType,
                SubmitLinkLabel = submitLinkLabel,
                SubmitTextLabel = submitTextLabel,
                WikiEnabled = wikiEnabled,
                Over18 = over18,
                AllowDiscovery = allowDiscovery,
                SpoilersEnabled = allowSpoilers,
                ShowMedia = showMedia,
                ShowMediaPreview = showMediaPreview,
                AllowImages = allowImages,
                AllowVideos = allowVideos,
                CollapseDeletedComments = collapseDeletedComments,
                SuggestedCommentSort = suggestedCommentSort,
                CommentScoreHideMins = commentScoreHideMins,
                HeaderImg = headerImage,
                IconImg = iconImage,
                PrimaryColor = primaryColor,
                KeyColor = keyColor,
                Name = fullname,
                BannerImg = bannerImg,
                BannerBackgroundColor = bannerBackgroundColor,
                BannerBackgroundImage = bannerBackgroundImage,
                CommunityIcon = communityIcon,
                EmojisEnabled = emojisEnabled,
                HeaderTitle = headerTitle,
                Id = id,
                CanAssignUserFlair = canAssignUserFlair,
                CanAssignLinkFlair = canAssignLinkFlair,
                ActiveUserCount = activeUserCount,
                AllowVideoGifs = allowVideoGifs,
                Subscribers = subscribers,
                CreatedUTC = created,
                URL = url,
                LinkFlairEnabled = linkFlairEnabled
            };
        }

        private void ImportToExisting(string name = null, string title = null, string description = null, string sidebar = null,
            string submissionText = null, string lang = null, string subredditType = null, string submissionType = null,
            string submitLinkLabel = null, string submitTextLabel = null, bool? wikiEnabled = null, bool? over18 = null,
            bool? allowDiscovery = null, bool? allowSpoilers = null, bool? showMedia = null, bool? showMediaPreview = null,
            bool? allowImages = null, bool? allowVideos = null, bool? collapseDeletedComments = null, string suggestedCommentSort = null,
            int? commentScoreHideMins = null, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null,
            string fullname = null, string bannerImg = null, string bannerBackgroundColor = null, string bannerBackgroundImage = null,
            string communityIcon = null, bool? emojisEnabled = null, string headerTitle = null, string id = null, bool? canAssignUserFlair = null,
            bool? canAssignLinkFlair = null, int? activeUserCount = null, bool? allowVideoGifs = null, int? subscribers = null, DateTime? created = null,
            string url = null, bool? linkFlairEnabled = null)
        {
            if (SubredditData == null)
            {
                SetValues(name, title, description, sidebar, submissionText, lang, subredditType, subredditType, submitLinkLabel, submitTextLabel, wikiEnabled ?? false,
                    over18 ?? false, allowDiscovery ?? true, allowSpoilers ?? true, showMedia ?? true, showMediaPreview ?? true, allowImages ?? true, allowVideos ?? true, 
                    collapseDeletedComments ?? false, suggestedCommentSort, commentScoreHideMins ?? 0, headerImage, iconImage, primaryColor, keyColor, fullname, 
                    bannerImg, bannerBackgroundColor, bannerBackgroundImage, communityIcon, emojisEnabled ?? true, headerTitle, id, canAssignUserFlair ?? false, 
                    canAssignLinkFlair ?? false, activeUserCount ?? 0, allowVideoGifs ?? true, subscribers ?? 0, created ?? default(DateTime), url, linkFlairEnabled ?? false);
            }
            else
            {
                SubredditData.DisplayName = (!string.IsNullOrEmpty(name) ? name : SubredditData.DisplayName);
                SubredditData.Title = (!string.IsNullOrEmpty(title) ? title : SubredditData.Title);
                SubredditData.PublicDescription = (!string.IsNullOrEmpty(description) ? description : SubredditData.PublicDescription);
                SubredditData.Description = (!string.IsNullOrEmpty(sidebar) ? sidebar : SubredditData.Description);
                SubredditData.SubmitText = (!string.IsNullOrEmpty(submissionText) ? submissionText : SubredditData.SubmitText);
                SubredditData.SubmitTextHTML = (!string.IsNullOrEmpty(submissionText) ? submissionText : SubredditData.SubmitTextHTML);
                SubredditData.Lang = (!string.IsNullOrEmpty(lang) ? lang : SubredditData.Lang);
                SubredditData.SubredditType = (!string.IsNullOrEmpty(subredditType) ? subredditType : SubredditData.SubredditType);
                SubredditData.SubmissionType = (!string.IsNullOrEmpty(submissionType) ? submissionType : SubredditData.SubmissionType);
                SubredditData.SubmitLinkLabel = (!string.IsNullOrEmpty(submitLinkLabel) ? submitLinkLabel : SubredditData.SubmitLinkLabel);
                SubredditData.SubmitTextLabel = (!string.IsNullOrEmpty(submitTextLabel) ? submitTextLabel : SubredditData.SubmitTextLabel);
                SubredditData.WikiEnabled = (wikiEnabled ?? SubredditData.WikiEnabled);
                SubredditData.Over18 = (over18 ?? SubredditData.Over18);
                SubredditData.AllowDiscovery = (allowDiscovery ?? SubredditData.AllowDiscovery);
                SubredditData.SpoilersEnabled = (allowSpoilers ?? SubredditData.SpoilersEnabled);
                SubredditData.ShowMedia = (showMedia ?? SubredditData.ShowMedia);
                SubredditData.ShowMediaPreview = (showMediaPreview ?? SubredditData.ShowMediaPreview);
                SubredditData.AllowImages = (allowImages ?? SubredditData.AllowImages);
                SubredditData.AllowVideos = (allowVideos ?? SubredditData.AllowVideos);
                SubredditData.CollapseDeletedComments = (collapseDeletedComments ?? SubredditData.CollapseDeletedComments);
                SubredditData.SuggestedCommentSort = (suggestedCommentSort ?? SubredditData.SuggestedCommentSort);
                SubredditData.CommentScoreHideMins = (commentScoreHideMins ?? SubredditData.CommentScoreHideMins);
                SubredditData.HeaderImg = (headerImage ?? SubredditData.HeaderImg);
                SubredditData.IconImg = (iconImage ?? SubredditData.IconImg);
                SubredditData.PrimaryColor = (!string.IsNullOrEmpty(primaryColor) ? primaryColor : SubredditData.PrimaryColor);
                SubredditData.KeyColor = (!string.IsNullOrEmpty(keyColor) ? keyColor : SubredditData.KeyColor);
                SubredditData.Name = (!string.IsNullOrEmpty(name) ? name : SubredditData.Name);
                SubredditData.BannerImg = (!string.IsNullOrEmpty(bannerImg) ? bannerImg : SubredditData.BannerImg);
                SubredditData.BannerBackgroundColor = (!string.IsNullOrEmpty(bannerBackgroundColor) ? bannerBackgroundColor : SubredditData.BannerBackgroundColor);
                SubredditData.BannerBackgroundImage = (!string.IsNullOrEmpty(bannerBackgroundImage) ? bannerBackgroundImage : SubredditData.BannerBackgroundImage);
                SubredditData.CommunityIcon = (!string.IsNullOrEmpty(communityIcon) ? communityIcon : SubredditData.CommunityIcon);
                SubredditData.EmojisEnabled = (emojisEnabled ?? SubredditData.EmojisEnabled);
                SubredditData.HeaderTitle = (!string.IsNullOrEmpty(headerTitle) ? headerTitle : SubredditData.HeaderTitle);
                SubredditData.Id = (!string.IsNullOrEmpty(id) ? id : SubredditData.Id);
                SubredditData.CanAssignUserFlair = (canAssignUserFlair ?? SubredditData.CanAssignUserFlair);
                SubredditData.CanAssignLinkFlair = (canAssignLinkFlair ?? SubredditData.CanAssignLinkFlair);
                SubredditData.ActiveUserCount = (activeUserCount ?? SubredditData.ActiveUserCount);
                SubredditData.AllowVideoGifs = (allowVideoGifs ?? SubredditData.AllowVideoGifs);
                SubredditData.Subscribers = (subscribers ?? SubredditData.Subscribers);
                SubredditData.CreatedUTC = (created ?? SubredditData.CreatedUTC);
                SubredditData.URL = (!string.IsNullOrEmpty(url) ? url : SubredditData.URL);
                SubredditData.LinkFlairEnabled = (linkFlairEnabled ?? SubredditData.LinkFlairEnabled);
            }
        }

        /// <summary>
        /// Create a new LinkPost object attached to this subreddit.
        /// </summary>
        /// <returns>A new LinkPost object attached to this subreddit.</returns>
        public LinkPost LinkPost(string title = null, string url = null, string author = null,
            string thumbnail = null, int? thumbnailHeight = null, int? thumbnailWidth = null, JObject preview = null,
            string id = null, string fullname = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
        {
            return new LinkPost(Dispatch, Name, title, url, author, thumbnail, thumbnailHeight, thumbnailWidth, preview,
                id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Create a new LinkPost object with the specified fullname attached to this subreddit.
        /// </summary>
        /// <param name="fullname">The fullname of an existing LinkPost.</param>
        /// <returns>A new LinkPost object attached to this subreddit.</returns>
        public LinkPost LinkPost(string fullname)
        {
            return new LinkPost(Dispatch, fullname, Name);
        }

        /// <summary>
        /// Create a new SelfPost object attached to this subreddit.
        /// </summary>
        /// <returns>A new SelfPost object attached to this subreddit.</returns>
        public SelfPost SelfPost(string title = null, string selfText = null, string selfTextHtml = null, string author = null,
            string id = null, string fullname = null, string permalink = null, DateTime created = default(DateTime),
            DateTime edited = default(DateTime), int score = 0, int upVotes = 0, int downVotes = 0,
            bool removed = false, bool spam = false)
        {
            return new SelfPost(Dispatch, Name, title, author, selfText, selfTextHtml, id, fullname, permalink, created,
                edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Create a new SelfPost object with the specified fullname attached to this subreddit.
        /// </summary>
        /// <param name="fullname">The fullname of an existing SelfPost.</param>
        /// <returns>A new SelfPost object attached to this subreddit.</returns>
        public SelfPost SelfPost(string fullname)
        {
            return new SelfPost(Dispatch, fullname, Name);
        }

        /// <summary>
        /// Create a new generic Post object attached to this subreddit.
        /// </summary>
        /// <returns>A new generic Post object attached to this subreddit.</returns>
        public Post Post()
        {
            return new Post(Dispatch, Name);
        }

        /// <summary>
        /// Create a new generic Post object with the specified fullname attached to this subreddit.
        /// </summary>
        /// <param name="fullname">The fullname of an existing Post.</param>
        /// <returns>A new generic Post object attached to this subreddit.</returns>
        public Post Post(string fullname)
        {
            return new Post(Dispatch, fullname, Name);
        }

        // Example:  Subreddit sub = reddit.Subreddit("facepalm").About();
        // Equivalent to:  Subreddit sub = reddit.Subreddit(reddit.Models.Subreddits.About("facepalm"));
        /// <summary>
        /// Return information about the current subreddit instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Subreddit About()
        {
            return new Subreddit(Dispatch, Dispatch.Subreddits.About(Name));
        }

        /// <summary>
        /// Accept an invite to moderate the specified subreddit.
        /// The authenticated user must have been invited to moderate the subreddit by one of its current moderators.
        /// </summary>
        public void AcceptModeratorInvite()
        {
            Validate(Dispatch.Moderation.AcceptModeratorInvite(Name));
        }

        /// <summary>
        /// Asynchronously accept an invite to moderate the specified subreddit.
        /// The authenticated user must have been invited to moderate the subreddit by one of its current moderators.
        /// </summary>
        public async Task AcceptModeratorInviteAsync()
        {
            Validate(await Dispatch.Moderation.AcceptModeratorInviteAsync(Name));
        }

        /// <summary>
        /// Abdicate moderator status in a subreddit.
        /// </summary>
        public void LeaveModerator()
        {
            Dispatch.Moderation.LeaveModerator("t2_" + Dispatch.Account.Me().Id, Name);
        }

        /// <summary>
        /// Abdicate moderator status in a subreddit asynchronously.
        /// </summary>
        public async Task LeaveModeratorAsync()
        {
            await Dispatch.Moderation.LeaveModeratorAsync("t2_" + Dispatch.Account.Me().Id, Name);
        }

        /// <summary>
        /// Abdicate approved submitter status in a subreddit.
        /// </summary>
        public void LeaveContributor()
        {
            Dispatch.Moderation.LeaveContributor(Fullname);
        }

        /// <summary>
        /// Abdicate approved submitter status in a subreddit asynchronously.
        /// </summary>
        public async Task LeaveContributorAsync()
        {
            await Dispatch.Moderation.LeaveContributorAsync(Fullname);
        }

        /// <summary>
        /// Get the moderators of this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of subreddit moderators.</returns>
        public List<Moderator> GetModerators(string after = "", string before = "", int limit = 25, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            return GetModerators(new SubredditsAboutInput(user, after, before, count, limit, show, srDetail, includeCategories));
        }

        /// <summary>
        /// Get the moderators of this subreddit.
        /// </summary>
        /// <param name="subredditsAboutInput">A valid SubredditsAboutInput instance</param>
        /// <returns>A list of subreddit moderators.</returns>
        public List<Moderator> GetModerators(SubredditsAboutInput subredditsAboutInput)
        {
            Things.DynamicShortListingContainer res = Dispatch.Subreddits.About("moderators", subredditsAboutInput, Name);

            Validate(res);

            Moderators = Lists.GetAboutChildren<Moderator>(res);
            return Moderators;
        }

        /// <summary>
        /// Get the approved submitters of this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of subreddit contributors.</returns>
        public List<SubredditUser> GetContributors(string after = "", string before = "", int limit = 25, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            return GetContributors(new SubredditsAboutInput(user, after, before, count, limit, show, srDetail, includeCategories));
        }

        /// <summary>
        /// Get the approved submitters of this subreddit.
        /// </summary>
        /// <param name="subredditsAboutInput">A valid SubredditsAboutInput instance</param>
        /// <returns>A list of subreddit contributors.</returns>
        public List<SubredditUser> GetContributors(SubredditsAboutInput subredditsAboutInput)
        {
            Things.DynamicShortListingContainer res = Dispatch.Subreddits.About("contributors", subredditsAboutInput, Name);

            Validate(res);

            return Lists.GetAboutChildren<SubredditUser>(res);
        }

        /// <summary>
        /// Get the muted users of this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of muted users.</returns>
        public List<SubredditUser> GetMutedUsers(string after = "", string before = "", int limit = 25, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            return GetMutedUsers(new SubredditsAboutInput(user, after, before, count, limit, show, srDetail, includeCategories));
        }

        /// <summary>
        /// Get the muted users of this subreddit.
        /// </summary>
        /// <param name="subredditsAboutInput">A valid SubredditsAboutInput instance</param>
        /// <returns>A list of muted users.</returns>
        public List<SubredditUser> GetMutedUsers(SubredditsAboutInput subredditsAboutInput)
        {
            Things.DynamicShortListingContainer res = Dispatch.Subreddits.About("muted", subredditsAboutInput, Name);

            Validate(res);

            return Lists.GetAboutChildren<SubredditUser>(res);
        }


        /// <summary>
        /// Get a list of users who were banned from this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of banned users.</returns>
        public List<BannedUser> GetBannedUsers(string after = "", string before = "", int limit = 25, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            return GetBannedUsers(new SubredditsAboutInput(user, after, before, count, limit, show, srDetail, includeCategories));
        }

        /// <summary>
        /// Get a list of users who were banned from this subreddit.
        /// </summary>
        /// <param name="subredditsAboutInput">A valid SubredditsAboutInput instance</param>
        /// <returns>A list of banned users.</returns>
        public List<BannedUser> GetBannedUsers(SubredditsAboutInput subredditsAboutInput)
        {
            Things.DynamicShortListingContainer res = Dispatch.Subreddits.About("banned", subredditsAboutInput, Name);

            Validate(res);

            return Lists.GetAboutChildren<BannedUser>(res);
        }

        /// <summary>
        /// Get the submission text for the subreddit.
        /// This text is set by the subreddit moderators and intended to be displayed on the submission form.
        /// </summary>
        /// <returns>An object containing submission text.</returns>
        public Things.SubredditSubmitText GetSubmitText()
        {
            Things.SubredditSubmitText res = Dispatch.Subreddits.SubmitText(Name);

            SubmitTextLastUpdated = DateTime.Now;

            SubmitText = res;
            return res;
        }

        /// <summary>
        /// Update a subreddit's stylesheet.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="stylesheetContents">the new stylesheet content</param>
        public void UpdateStylesheet(string reason, string stylesheetContents)
        {
            Validate(Dispatch.Subreddits.SubredditStylesheet(new SubredditsSubredditStylesheetInput(stylesheetContents, reason), Name));
        }

        /// <summary>
        /// Update a subreddit's stylesheet asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="stylesheetContents">the new stylesheet content</param>
        public async Task UpdateStylesheetAsync(string reason, string stylesheetContents)
        {
            Validate(await Dispatch.Subreddits.SubredditStylesheetAsync(new SubredditsSubredditStylesheetInput(stylesheetContents, reason), Name));
        }

        /// <summary>
        /// Subscribe to a subreddit.
        /// </summary>
        /// <param name="skipInitialDefaults">boolean value</param>
        public void Subscribe(bool skipInitialDefaults = false)
        {
            Dispatch.Subreddits.Subscribe(new SubredditsSubByNameInput(Name, skipInitialDefaults: skipInitialDefaults));
        }

        /// <summary>
        /// Subscribe to a subreddit asynchronously.
        /// </summary>
        /// <param name="skipInitialDefaults">boolean value</param>
        public async Task SubscribeAsync(bool skipInitialDefaults = false)
        {
            await Dispatch.Subreddits.SubscribeAsync(new SubredditsSubByNameInput(Name, skipInitialDefaults: skipInitialDefaults));
        }

        /// <summary>
        /// Unsubscribe from a subreddit.
        /// </summary>
        public void Unsubscribe()
        {
            Dispatch.Subreddits.Subscribe(new SubredditsSubByNameInput(Name, "unsub"));
        }

        /// <summary>
        /// Unsubscribe from a subreddit asynchronously.
        /// </summary>
        public async Task UnsubscribeAsync()
        {
            await Dispatch.Subreddits.SubscribeAsync(new SubredditsSubByNameInput(Name, "unsub"));
        }

        /// <summary>
        /// Remove the subreddit's custom mobile banner.
        /// </summary>
        public void DeleteBanner()
        {
            Validate(Dispatch.Subreddits.DeleteSrBanner(Name));
        }

        /// <summary>
        /// Remove the subreddit's custom mobile banner asynchronously.
        /// </summary>
        public async Task DeleteBannerAsync()
        {
            Validate(await Dispatch.Subreddits.DeleteSrBannerAsync(Name));
        }

        /// <summary>
        /// Remove the subreddit's custom header image.
        /// The sitewide-default header image will be shown again after this call.
        /// </summary>
        public void DeleteHeader()
        {
            Validate(Dispatch.Subreddits.DeleteSrHeader(Name));
        }

        /// <summary>
        /// Remove the subreddit's custom header image asynchronously.
        /// </summary>
        public async Task DeleteHeaderAsync()
        {
            Validate(await Dispatch.Subreddits.DeleteSrHeaderAsync(Name));
        }

        /// <summary>
        /// Remove the subreddit's custom mobile icon.
        /// </summary>
        public void DeleteIcon()
        {
            Validate(Dispatch.Subreddits.DeleteSrIcon(Name));
        }

        /// <summary>
        /// Remove the subreddit's custom mobile icon asynchronously.
        /// </summary>
        public async Task DeleteIconAsync()
        {
            Validate(await Dispatch.Subreddits.DeleteSrIconAsync(Name));
        }

        /// <summary>
        /// Remove an image from the subreddit's custom image set.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// </summary>
        /// <param name="imgName">a valid subreddit image name</param>
        public void DeleteImg(string imgName)
        {
            Validate(Dispatch.Subreddits.DeleteSrImg(new SubredditsDeleteSrImgInput(imgName), Name));
        }

        /// <summary>
        /// Remove an image from the subreddit's custom image set asynchronously.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// </summary>
        /// <param name="imgName">a valid subreddit image name</param>
        public async Task DeleteImgAsync(string imgName)
        {
            Validate(await Dispatch.Subreddits.DeleteSrImgAsync(new SubredditsDeleteSrImgInput(imgName), Name));
        }

        /// <summary>
        /// Add or replace a subreddit stylesheet image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgName">a valid subreddit image name</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public Things.ImageUploadResult UploadImg(byte[] imgData, string imgName, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imgData, 0, imgName, "img", imgType), Name));
        }

        /// <summary>
        /// Add or replace a subreddit stylesheet image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgName">a valid subreddit image name</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task<Things.ImageUploadResult> UploadImgAsync(byte[] imgData, string imgName, string imgType = "png")
        {
            return Validate(await Dispatch.Subreddits.UploadSrImgAsync(new SubredditsUploadSrImgInput(imgData, 0, imgName, "img", imgType), Name));
        }

        /// <summary>
        /// Add or replace the subreddit logo image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public Things.ImageUploadResult UploadHeader(byte[] imgData, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imgData, 1, null, "header", imgType), Name));
        }

        /// <summary>
        /// Add or replace the subreddit logo image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task<Things.ImageUploadResult> UploadHeaderAsync(byte[] imgData, string imgType = "png")
        {
            return Validate(await Dispatch.Subreddits.UploadSrImgAsync(new SubredditsUploadSrImgInput(imgData, 1, null, "header", imgType), Name));
        }

        /// <summary>
        /// Add or replace a subreddit mobile icon image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public Things.ImageUploadResult UploadIcon(byte[] imgData, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imgData, 0, null, "icon", imgType), Name));
        }

        /// <summary>
        /// Add or replace a subreddit mobile icon image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task<Things.ImageUploadResult> UploadIconAsync(byte[] imgData, string imgType = "png")
        {
            return Validate(await Dispatch.Subreddits.UploadSrImgAsync(new SubredditsUploadSrImgInput(imgData, 0, null, "icon", imgType), Name));
        }

        /// <summary>
        /// Add or replace a subreddit mobile banner image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public Things.ImageUploadResult UploadBanner(byte[] imgData, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imgData, 0, null, "banner", imgType), Name));
        }

        /// <summary>
        /// Add or replace a subreddit mobile banner image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task<Things.ImageUploadResult> UploadBannerAsync(byte[] imgData, string imgType = "png")
        {
            return Validate(await Dispatch.Subreddits.UploadSrImgAsync(new SubredditsUploadSrImgInput(imgData, 0, null, "banner", imgType), Name));
        }

        // TODO - Figure out what created and location are for.  --Kris
        /// <summary>
        /// Get the current settings of a subreddit.
        /// </summary>
        /// <param name="created">one of (true, false)</param>
        /// <param name="location"></param>
        /// <returns>Settings for the requested subreddit.</returns>
        public Things.SubredditSettingsContainer GetSettings(bool created = false, string location = "")
        {
            return Validate(Dispatch.Subreddits.Edit(Name, new SubredditsEditInput(created, location)));
        }

        /// <summary>
        /// Get the rules for the current subreddit.
        /// </summary>
        /// <returns>Subreddit rules.</returns>
        public Things.RulesContainer GetRules()
        {
            return Validate(Dispatch.Subreddits.Rules(Name));
        }

        /// <summary>
        /// Get the traffic for the current subreddit.
        /// </summary>
        /// <returns>Subreddit traffic.</returns>
        public Things.Traffic GetTraffic()
        {
            return Validate(Dispatch.Subreddits.Traffic(Name));
        }

        /// <summary>
        /// Search this subreddit for posts.
        /// To search across all subreddits, use RedditAPI.Search, instead.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <returns>A list of posts that match the search criteria.</returns>
        public List<Post> Search(SearchGetSearchInput searchGetSearchInput)
        {
            searchGetSearchInput.restrict_sr = true;
            return Lists.GetPosts(Validate(Dispatch.Search.SearchPosts(searchGetSearchInput, Name)), Dispatch);
        }

        /// <summary>
        /// Search this subreddit for posts.
        /// To search across all subreddits, use RedditAPI.Search, instead.
        /// </summary>
        /// <param name="q">A valid search query</param>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance (optional)</param>
        /// <returns>A list of posts that match the search criteria.</returns>
        public List<Post> Search(string q, SearchGetSearchInput searchGetSearchInput = null)
        {
            if (searchGetSearchInput == null)
            {
                searchGetSearchInput = new SearchGetSearchInput();
            }

            searchGetSearchInput.q = q;
            searchGetSearchInput.restrict_sr = true;

            return Search(searchGetSearchInput);
        }

        /// <summary>
        /// Search this subreddit for posts.
        /// </summary>
        /// <param name="q">a string no longer than 512 characters</param>
        /// <param name="restrictSr">boolean value</param>
        /// <param name="sort">one of (relevance, hot, top, new, comments)</param>
        /// <param name="category">a string no longer than 5 characters</param>
        /// <param name="includeFacets">boolean value</param>
        /// <param name="type">(optional) comma-delimited list of result types (sr, link, user)</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">boolean value</param>
        /// <returns>A list of posts that match the search criteria.</returns>
        public List<Post> Search(string q = "", bool restrictSr = true, string sort = "new", string category = "", bool includeFacets = false, string type = null,
            string t = "all", string after = null, string before = null, bool includeCategories = false, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            return Lists.GetPosts(Validate(Dispatch.Search.GetSearch<Things.PostContainer>(
                new SearchGetSearchInput(q, restrictSr, sort, category, includeFacets, type, t, after, before,
                    includeCategories, count, limit, show, srDetail),
                Name)), Dispatch);
        }

        /// <summary>
        /// Retrieve the advisory text about saving media for relevant media links.
        /// This endpoint returns a notice for display during the post submission process that is pertinent to media links.
        /// </summary>
        /// <param name="url">a valid URL</param>
        /// <returns>A Reddit notice message.</returns>
        public Dictionary<string, string> SavedMediaText(string url)
        {
            return Validate(Dispatch.Misc.SavedMediaText(url, Name));
        }

        /// <summary>
        /// Get a list of recent moderation actions.
        /// Moderator actions taken within a subreddit are logged. This listing is a view of that log with various filters to aid in analyzing the information.
        /// The optional mod parameter can be a comma-delimited list of moderator names to restrict the results to, or the string a to restrict the results to admin actions taken within the subreddit.
        /// The type parameter is optional and if sent limits the log entries returned to only those of the type specified.
        /// </summary>
        /// <param name="type">one of (banuser, unbanuser, spamlink, removelink, approvelink, spamcomment, removecomment, approvecomment, addmoderator, invitemoderator, uninvitemoderator, 
        /// acceptmoderatorinvite, removemoderator, addcontributor, removecontributor, editsettings, editflair, distinguish, marknsfw, wikibanned, wikicontributor, wikiunbanned, wikipagelisted, 
        /// removewikicontributor, wikirevise, wikipermlevel, ignorereports, unignorereports, setpermissions, setsuggestedsort, sticky, unsticky, setcontestmode, unsetcontestmode, lock, unlock, 
        /// muteuser, unmuteuser, createrule, editrule, deleterule, spoiler, unspoiler, modmail_enrollment, community_styling, community_widgets, markoriginalcontent)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 500)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="mod">(optional) a moderator filter</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A listing of recent moderation actions.</returns>
        public Things.ModActionContainer GetLog(string type = null, int limit = 25, string after = "", string before = "", string mod = null,
            string show = "all", bool srDetail = false, int count = 0)
        {
            return GetLog(new ModerationGetLogInput(type, mod, after, before, limit, count, srDetail, show));
        }

        /// <summary>
        /// Get a list of recent moderation actions.
        /// Moderator actions taken within a subreddit are logged. This listing is a view of that log with various filters to aid in analyzing the information.
        /// The optional mod parameter can be a comma-delimited list of moderator names to restrict the results to, or the string a to restrict the results to admin actions taken within the subreddit.
        /// The type parameter is optional and if sent limits the log entries returned to only those of the type specified.
        /// </summary>
        /// <param name="moderationGetLogInput">A valid ModerationGetLogInput instance</param>
        /// <returns>A listing of recent moderation actions.</returns>
        public Things.ModActionContainer GetLog(ModerationGetLogInput moderationGetLogInput)
        {
            return Validate(Dispatch.Moderation.GetLog(moderationGetLogInput, Name));
        }

        /// <summary>
        /// Redirect to the subreddit's stylesheet if one exists.
        /// </summary>
        /// <returns>The subreddit's CSS.</returns>
        public string Stylesheet()
        {
            return Dispatch.Moderation.Stylesheet(Name);
        }

        /// <summary>
        /// Invite a user to become a moderator of this subreddit.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        public void ModeratorInvite(string username, string permissions, int duration = 999)
        {
            ModeratorInvite(new UsersFriendInput(username, "moderator_invite", duration, permissions));
        }

        /// <summary>
        /// Asynchronously invite a user to become a moderator of this subreddit.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        public async Task ModeratorInviteAsync(string username, string permissions, int duration = 999)
        {
            await ModeratorInviteAsync(new UsersFriendInput(username, "moderator_invite", duration, permissions));
        }

        /// <summary>
        /// Invite a user to become a moderator of this subreddit.
        /// </summary>
        /// <param name="usersFriendInput">A valid UsersFriendInput instance</param>
        public void ModeratorInvite(UsersFriendInput usersFriendInput)
        {
            usersFriendInput.type = "moderator_invite";

            Validate(Dispatch.Users.Friend(usersFriendInput, Name));
        }

        /// <summary>
        /// Asynchronously invite a user to become a moderator of this subreddit.
        /// </summary>
        /// <param name="usersFriendInput">A valid UsersFriendInput instance</param>
        public async Task ModeratorInviteAsync(UsersFriendInput usersFriendInput)
        {
            usersFriendInput.type = "moderator_invite";

            Validate(await Dispatch.Users.FriendAsync(usersFriendInput, Name));
        }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public void SetUserPermissions(string username, string permissions, string type)
        {
            SetUserPermissions(new UsersSetPermissionsInput(username, permissions, type));
        }

        /// <summary>
        /// Set permissions asynchronously.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public async Task SetUserPermissionsAsync(string username, string permissions, string type)
        {
            await SetUserPermissionsAsync(new UsersSetPermissionsInput(username, permissions, type));
        }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="usersSetPermissionsInput">A valid UsersSetPermissionsInput instance</param>
        public void SetUserPermissions(UsersSetPermissionsInput usersSetPermissionsInput)
        {
            Validate(Dispatch.Users.SetPermissions(usersSetPermissionsInput, Name));
        }

        /// <summary>
        /// Set permissions asynchronously.
        /// </summary>
        /// <param name="usersSetPermissionsInput">A valid UsersSetPermissionsInput instance</param>
        public async Task SetUserPermissionsAsync(UsersSetPermissionsInput usersSetPermissionsInput)
        {
            Validate(await Dispatch.Users.SetPermissionsAsync(usersSetPermissionsInput, Name));
        }

        // TODO - Add Emoji and Widgets endpoints once the S3 image upload issue is solved.  --Kris

        // Example:  Subreddit sub = reddit.Subreddit("MyNewSubreddit", "My New Subreddit", "Some description.", "This is my sidebar!").Create();
        // Equivalent to:  reddit.Models.Subreddits.SiteAdmin(name:"MyNewSubreddit", title:"My New Subreddit", publicDescription:"Some description", description:"This is my sidebar!", ...);
        //                 Subreddit sub = reddit.Subreddit(reddit.Models.Subreddits.About("MyNewSubreddit"));
        /// <summary>
        /// Create a new subreddit and return the created result.
        /// If a subreddit by that name already exists, an exception is thrown.
        /// </summary>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An instance of this class populated with the newly created subreddit.</returns>
        public Subreddit Create(bool allowPostCrossposts = true, bool allowTop = true, bool excludeBannedModqueue = false, bool freeFormReports = true,
            string gRecaptchaResponse = "", string linkType = "any", string spamComments = "low", string spamLinks = "high", string spamSelfPosts = "high",
            string themeSr = "", bool themeSrUpdate = true, string wikiMode = "disabled", int wikiEditAge = 0, int wikiEditKarma = 0)
        {
            return Create(new SubredditsSiteAdminInput(allowPostCrossposts: allowPostCrossposts, allowTop: allowTop, excludeBannedModqueue: excludeBannedModqueue,
                freeFormReports: freeFormReports, linkType: linkType, spamComments: spamComments, spamLinks: spamLinks, spamSelfPosts: spamSelfPosts, themeSr: themeSr,
                themeSrUpdate: themeSrUpdate, wikiMode: wikiMode, wikiEditAge: wikiEditAge, wikiEditKarma: wikiEditKarma), gRecaptchaResponse);
        }

        /// <summary>
        /// Create a new subreddit asynchronously and return the created result.
        /// If a subreddit by that name already exists, an exception is thrown.
        /// </summary>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An instance of this class populated with the newly created subreddit.</returns>
        public async Task<Subreddit> CreateAsync(bool allowPostCrossposts = true, bool allowTop = true, bool excludeBannedModqueue = false, bool freeFormReports = true,
            string gRecaptchaResponse = "", string linkType = "any", string spamComments = "low", string spamLinks = "high", string spamSelfPosts = "high",
            string themeSr = "", bool themeSrUpdate = true, string wikiMode = "disabled", int wikiEditAge = 0, int wikiEditKarma = 0)
        {
            return await CreateAsync(new SubredditsSiteAdminInput(allowPostCrossposts: allowPostCrossposts, allowTop: allowTop, excludeBannedModqueue: excludeBannedModqueue,
                freeFormReports: freeFormReports, linkType: linkType, spamComments: spamComments, spamLinks: spamLinks, spamSelfPosts: spamSelfPosts, themeSr: themeSr,
                themeSrUpdate: themeSrUpdate, wikiMode: wikiMode, wikiEditAge: wikiEditAge, wikiEditKarma: wikiEditKarma), gRecaptchaResponse);
        }

        /// <summary>
        /// Create a new subreddit and return the created result.
        /// If a subreddit by that name already exists, an exception is thrown.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle"></param>
        /// <returns>An instance of this class populated with the newly created subreddit.</returns>
        public Subreddit Create(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            Validate(Dispatch.Subreddits.SiteAdmin(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle));

            return About();
        }

        /// <summary>
        /// Create a new subreddit asynchronously and return the created result.
        /// If a subreddit by that name already exists, an exception is thrown.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle"></param>
        /// <returns>An instance of this class populated with the newly created subreddit.</returns>
        public async Task<Subreddit> CreateAsync(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            Validate(await Dispatch.Subreddits.SiteAdminAsync(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle));

            return About();
        }

        /// <summary>
        /// Create a new subreddit and return the created result.
        /// If a subreddit by that name already exists, retrieve that existing subreddit and return the result.
        /// If the subreddit already exists, the parameters passed to this method will be ignored.
        /// </summary>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An instance of this class populated with the newly created or existing subreddit.</returns>
        public Subreddit CreateIfNotExists(bool allowPostCrossposts = true, bool allowTop = true, bool excludeBannedModqueue = false, bool freeFormReports = true,
            string gRecaptchaResponse = "", string linkType = "any", string spamComments = "low", string spamLinks = "high", string spamSelfPosts = "high",
            string themeSr = "", bool themeSrUpdate = true, string wikiMode = "disabled", int wikiEditAge = 0, int wikiEditKarma = 0)
        {
            return CreateIfNotExists(new SubredditsSiteAdminInput(allowPostCrossposts: allowPostCrossposts, allowTop: allowTop, excludeBannedModqueue: excludeBannedModqueue,
                freeFormReports: freeFormReports, linkType: linkType, spamComments: spamComments, spamLinks: spamLinks, spamSelfPosts: spamSelfPosts,
                themeSr: themeSr, themeSrUpdate: themeSrUpdate, wikiMode: wikiMode, wikiEditAge: wikiEditAge, wikiEditKarma: wikiEditKarma), gRecaptchaResponse);
        }

        /// <summary>
        /// Create a new subreddit asynchronously and return the created result.
        /// If a subreddit by that name already exists, retrieve that existing subreddit and return the result.
        /// If the subreddit already exists, the parameters passed to this method will be ignored.
        /// </summary>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An instance of this class populated with the newly created or existing subreddit.</returns>
        public async Task<Subreddit> CreateIfNotExistsAsync(bool allowPostCrossposts = true, bool allowTop = true, bool excludeBannedModqueue = false, bool freeFormReports = true,
            string gRecaptchaResponse = "", string linkType = "any", string spamComments = "low", string spamLinks = "high", string spamSelfPosts = "high",
            string themeSr = "", bool themeSrUpdate = true, string wikiMode = "disabled", int wikiEditAge = 0, int wikiEditKarma = 0)
        {
            return await CreateIfNotExistsAsync(new SubredditsSiteAdminInput(allowPostCrossposts: allowPostCrossposts, allowTop: allowTop, excludeBannedModqueue: excludeBannedModqueue,
                freeFormReports: freeFormReports, linkType: linkType, spamComments: spamComments, spamLinks: spamLinks, spamSelfPosts: spamSelfPosts,
                themeSr: themeSr, themeSrUpdate: themeSrUpdate, wikiMode: wikiMode, wikiEditAge: wikiEditAge, wikiEditKarma: wikiEditKarma), gRecaptchaResponse);
        }

        /// <summary>
        /// Create a new subreddit and return the created result.
        /// If a subreddit by that name already exists, retrieve that existing subreddit and return the result.
        /// If the subreddit already exists, the parameters passed to this method will be ignored.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <returns>An instance of this class populated with the newly created or existing subreddit.</returns>
        public Subreddit CreateIfNotExists(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            try
            {
                return Create(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle);
            }
            catch (RedditSubredditExistsException) { }

            return About();
        }

        /// <summary>
        /// Create a new subreddit asynchronously and return the created result.
        /// If a subreddit by that name already exists, retrieve that existing subreddit and return the result.
        /// If the subreddit already exists, the parameters passed to this method will be ignored.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <returns>An instance of this class populated with the newly created or existing subreddit.</returns>
        public async Task<Subreddit> CreateIfNotExistsAsync(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            try
            {
                return await CreateAsync(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle);
            }
            catch (RedditSubredditExistsException) { }

            return About();
        }

        /// <summary>
        /// Create a new subreddit and return the created result.
        /// If a subreddit by that name already exists, update that existing subreddit and return the result.
        /// </summary>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An instance of this class populated with the newly created or updated subreddit.</returns>
        public Subreddit CreateOrUpdate(bool allowPostCrossposts = true, bool allowTop = true, bool excludeBannedModqueue = false, bool freeFormReports = true,
            string gRecaptchaResponse = "", string linkType = "any", string spamComments = "low", string spamLinks = "high", string spamSelfPosts = "high",
            string themeSr = "", bool themeSrUpdate = true, string wikiMode = "disabled", int wikiEditAge = 0, int wikiEditKarma = 0)
        {
            try
            {
                return Create(allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports, gRecaptchaResponse, linkType, spamComments,
                    spamLinks, spamSelfPosts, themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);
            }
            catch (RedditSubredditExistsException) { }

            // If subreddit already exists, import its data to this instance so we can get the fullname.  --Kris
            if (string.IsNullOrWhiteSpace(Fullname))
            {
                Import(About(), false);
            }

            return Update(allowPostCrossposts: allowPostCrossposts, allowTop: allowTop, excludeBannedModqueue: excludeBannedModqueue,
                freeFormReports: freeFormReports, gRecaptchaResponse: gRecaptchaResponse, linkType: linkType, spamComments: spamComments,
                spamLinks: spamLinks, spamSelfPosts: spamSelfPosts, themeSr: themeSr, themeSrUpdate: themeSrUpdate, wikiMode: wikiMode,
                wikiEditAge: wikiEditAge, wikiEditKarma: wikiEditKarma);
        }

        /// <summary>
        /// Create a new subreddit asynchronously and return the created result.
        /// If a subreddit by that name already exists, update that existing subreddit and return the result.
        /// </summary>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An instance of this class populated with the newly created or updated subreddit.</returns>
        public async Task<Subreddit> CreateOrUpdateAsync(bool allowPostCrossposts = true, bool allowTop = true, bool excludeBannedModqueue = false, bool freeFormReports = true,
            string gRecaptchaResponse = "", string linkType = "any", string spamComments = "low", string spamLinks = "high", string spamSelfPosts = "high",
            string themeSr = "", bool themeSrUpdate = true, string wikiMode = "disabled", int wikiEditAge = 0, int wikiEditKarma = 0)
        {
            try
            {
                return await CreateAsync(allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports, gRecaptchaResponse, linkType, spamComments,
                    spamLinks, spamSelfPosts, themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);
            }
            catch (RedditSubredditExistsException) { }

            // If subreddit already exists, import its data to this instance so we can get the fullname.  --Kris
            if (string.IsNullOrWhiteSpace(Fullname))
            {
                Import(About(), false);
            }

            return await UpdateAsync(allowPostCrossposts: allowPostCrossposts, allowTop: allowTop, excludeBannedModqueue: excludeBannedModqueue,
                freeFormReports: freeFormReports, gRecaptchaResponse: gRecaptchaResponse, linkType: linkType, spamComments: spamComments,
                spamLinks: spamLinks, spamSelfPosts: spamSelfPosts, themeSr: themeSr, themeSrUpdate: themeSrUpdate, wikiMode: wikiMode,
                wikiEditAge: wikiEditAge, wikiEditKarma: wikiEditKarma);
        }

        /// <summary>
        /// Create a new subreddit and return the created result.
        /// If a subreddit by that name already exists, update that existing subreddit and return the result.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle"></param>
        /// <returns>An instance of this class populated with the newly created or updated subreddit.</returns>
        public Subreddit CreateOrUpdate(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            try
            {
                return Create(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle);
            }
            catch (RedditSubredditExistsException) { }

            // If subreddit already exists, import its data to this instance so we can get the fullname.  --Kris
            if (string.IsNullOrWhiteSpace(Fullname))
            {
                Import(About(), false);
            }

            return Update(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle);
        }

        /// <summary>
        /// Create a new subreddit asynchronously and return the created result.
        /// If a subreddit by that name already exists, update that existing subreddit and return the result.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle"></param>
        /// <returns>An instance of this class populated with the newly created or updated subreddit.</returns>
        public async Task<Subreddit> CreateOrUpdateAsync(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            try
            {
                return await CreateAsync(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle);
            }
            catch (RedditSubredditExistsException) { }

            // If subreddit already exists, import its data to this instance so we can get the fullname.  --Kris
            if (string.IsNullOrWhiteSpace(Fullname))
            {
                Import(About(), false);
            }

            return await UpdateAsync(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle);
        }

        // Example:  Subreddit sub = reddit.Subreddit("MyNewSubreddit").About();
        //           sub.Sidebar = "I've updated my sidebar and there's nothing you can do about it.";
        //           sub.Update();
        // Example:  Subreddit sub = reddit.Subreddit("MyNewSubreddit").About();
        //           sub.Update(wikiMode:"anyone");
        // Example:  reddit.Subreddit("MyNewSubreddit").Update(true, over18:true, commentScoreHideMins:5);
        /// <summary>
        /// Update an existing subreddit.
        /// </summary>
        /// <param name="manualUpdate">if true, only the values explicitly passed to this method will be updated (default: false)</param>
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
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle">a string no longer than 500 characters</param>
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
        /// <returns>An instance of this class populated with the newly created or updated subreddit.</returns>
        public Subreddit Update(bool manualUpdate = false, bool? allOriginalContent = null, bool? allowDiscovery = null, bool? allowImages = null, bool? allowPostCrossposts = null,
            bool? allowTop = null, bool? allowVideos = null, bool? collapseDeletedComments = null, string description = null, bool? excludeBannedModqueue = null,
            bool? freeFormReports = null, string gRecaptchaResponse = null, string headerTitle = null, bool? hideAds = null, string keyColor = null, string lang = null,
            string linkType = null, string name = null, bool? originalContentTagEnabled = null, bool? over18 = null, string publicDescription = null, bool? showMedia = null,
            bool? showMediaPreview = null, string spamComments = null, string spamLinks = null, string spamSelfPosts = null, bool? spoilersEnabled = null, string sr = null,
            string submitLinkLabel = null, string submitText = null, string submitTextLabel = null, string suggestedCommentSort = null, string themeSr = null,
            bool? themeSrUpdate = null, string title = null, string type = null, string wikiMode = null, int? commentScoreHideMins = null, int? wikiEditAge = null,
            int? wikiEditKarma = null)
        {
            Things.GenericContainer res;
            if (!manualUpdate)
            {
                res = Dispatch.Subreddits.SiteAdmin(SubredditData, allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports,
                    gRecaptchaResponse, linkType, spamComments, spamLinks, spamSelfPosts, Fullname, themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);
            }
            else
            {
                res = Dispatch.Subreddits.SiteAdmin(new SubredditsSiteAdminInput(allOriginalContent, allowDiscovery, allowImages, allowPostCrossposts, allowTop,
                    allowVideos, collapseDeletedComments, description, excludeBannedModqueue, freeFormReports, hideAds, keyColor, lang, linkType, name, originalContentTagEnabled,
                    over18, publicDescription, showMedia, showMediaPreview, spamComments, spamLinks, spamSelfPosts,
                    spoilersEnabled, sr, submitLinkLabel, submitText, submitTextLabel, suggestedCommentSort,
                    themeSr, themeSrUpdate, title, type, wikiMode, commentScoreHideMins, wikiEditAge,
                    wikiEditKarma), gRecaptchaResponse, headerTitle);
            }

            Validate(res);

            return About();
        }

        /// <summary>
        /// Update an existing subreddit asynchronously.
        /// </summary>
        /// <param name="manualUpdate">if true, only the values explicitly passed to this method will be updated (default: false)</param>
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
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle">a string no longer than 500 characters</param>
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
        public async Task<Subreddit> UpdateAsync(bool manualUpdate = false, bool? allOriginalContent = null, bool? allowDiscovery = null, bool? allowImages = null, bool? allowPostCrossposts = null,
            bool? allowTop = null, bool? allowVideos = null, bool? collapseDeletedComments = null, string description = null, bool? excludeBannedModqueue = null,
            bool? freeFormReports = null, string gRecaptchaResponse = null, string headerTitle = null, bool? hideAds = null, string keyColor = null, string lang = null,
            string linkType = null, string name = null, bool? originalContentTagEnabled = null, bool? over18 = null, string publicDescription = null, bool? showMedia = null,
            bool? showMediaPreview = null, string spamComments = null, string spamLinks = null, string spamSelfPosts = null, bool? spoilersEnabled = null, string sr = null,
            string submitLinkLabel = null, string submitText = null, string submitTextLabel = null, string suggestedCommentSort = null, string themeSr = null,
            bool? themeSrUpdate = null, string title = null, string type = null, string wikiMode = null, int? commentScoreHideMins = null, int? wikiEditAge = null,
            int? wikiEditKarma = null)
        {
            return await UpdateAsync(manualUpdate, allOriginalContent, allowDiscovery, allowImages, allowPostCrossposts, allowTop, allowVideos, collapseDeletedComments, description,
                    excludeBannedModqueue, freeFormReports, gRecaptchaResponse, headerTitle, hideAds, keyColor, lang, linkType, name, originalContentTagEnabled,
                    over18, publicDescription, showMedia, showMediaPreview, spamComments, spamLinks, spamSelfPosts, spoilersEnabled, sr, submitLinkLabel, submitText,
                    submitTextLabel, suggestedCommentSort, themeSr, themeSrUpdate, title, type, wikiMode, commentScoreHideMins, wikiEditAge, wikiEditKarma);
        }

        /// <summary>
        /// Update an existing subreddit.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle"></param>
        /// <returns>An instance of this class populated with the newly created or updated subreddit.</returns>
        public Subreddit Update(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            Validate(Dispatch.Subreddits.SiteAdmin(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle));

            return About();
        }

        /// <summary>
        /// Update an existing subreddit asynchronously.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle"></param>
        public async Task<Subreddit> UpdateAsync(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = "", string headerTitle = "")
        {
            Validate(await Dispatch.Subreddits.SiteAdminAsync(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle));

            return About();
        }
    }
}
