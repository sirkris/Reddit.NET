using Newtonsoft.Json.Linq;
using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using Reddit.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    /// <summary>
    /// Controller class for subreddits.
    /// </summary>
    public class Subreddit : BaseController
    {
        // Subreddit data pertaining to the logged-in user can be found in SubredditData.  --Kris
        public string BannerImg;
        public string BannerBackgroundColor;
        public string BannerBackgroundImage;
        public string SubredditType;
        public string CommunityIcon;
        public string HeaderTitle;
        public bool WikiEnabled;
        public bool Over18;
        public string Sidebar;
        public string Name;
        public object HeaderImg;
        public string Title;
        public bool CollapseDeletedComments;
        public string Id;
        public bool EmojisEnabled;
        public bool ShowMedia;
        public bool AllowVideos;
        public bool CanAssignUserFlair;
        public bool SpoilersEnabled;
        public string PrimaryColor;
        public string SuggestedCommentSort;
        public int? ActiveUserCount;
        public object IconImg;
        public bool CanAssignLinkFlair;
        public bool AllowVideoGifs;
        public int Subscribers;
        public string SubmitTextLabel;
        public string KeyColor;
        public string Lang;
        public string Fullname;
        public DateTime Created;
        public string URL;
        public string SubmitLinkLabel;
        public bool AllowDiscovery;
        public string Description;
        public bool LinkFlairEnabled;
        public bool AllowImages;
        public int CommentScoreHideMins;
        public bool ShowMediaPreview;
        public string SubmissionType;

        internal override ref Models.Internal.Monitor MonitorModel => ref MonitorNull;
        internal override ref MonitoringSnapshot Monitoring => ref MonitoringSnapshotNull;

        /// <summary>
        /// Full subreddit data retrieved from the API.
        /// </summary>
        public RedditThings.Subreddit SubredditData
        {
            get;
            private set;
        }

        /// <summary>
        /// Posts belonging to this subreddit.
        /// </summary>
        public SubredditPosts Posts;

        /// <summary>
        /// Flairs belonging to this subreddit.
        /// </summary>
        public Flairs Flairs;

        /// <summary>
        /// The subreddit wiki controller.
        /// </summary>
        public Wiki Wiki;

        internal Dispatch Dispatch;

        /// <summary>
        /// Get the submission text for the subreddit.
        /// This text is set by the subreddit moderators and intended to be displayed on the submission form.
        /// </summary>
        public RedditThings.SubredditSubmitText SubmitText
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
        internal RedditThings.SubredditSubmitText submitText;
        private DateTime? SubmitTextLastUpdated;

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
        private DateTime? ModeratorsLastUpdated;

        /// <summary>
        /// Create a new subreddit controller instance populated from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit"></param>
        public Subreddit(ref Dispatch dispatch, RedditThings.Subreddit subreddit)
            : base()
        {
            Dispatch = dispatch;
            ImportFromModel(subreddit);

            SubredditData = subreddit;
            Posts = new SubredditPosts(ref Dispatch, Name);
            Flairs = new Flairs(this, ref Dispatch);
            Wiki = new Wiki(ref Dispatch, Name);
        }

        /// <summary>
        /// Create a new subreddit controller instance populated from API return data.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subredditChild"></param>
        public Subreddit(ref Dispatch dispatch, RedditThings.SubredditChild subredditChild)
            : base()
        {
            Dispatch = dispatch;
            ImportFromModel(subredditChild.Data);

            SubredditData = subredditChild.Data;
            Posts = new SubredditPosts(ref Dispatch, Name);
            Flairs = new Flairs(this, ref Dispatch);
            Wiki = new Wiki(ref Dispatch, Name);
        }

        /// <summary>
        /// Copy another subreddit controller instance onto this one.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="subreddit">A valid subreddit controller instance</param>
        public Subreddit(ref Dispatch dispatch, Subreddit subreddit)
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
        public Subreddit(ref Dispatch dispatch, string name, string title = "", string description = "", string sidebar = "",
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

            UpdateSubredditData();
            Posts = new SubredditPosts(ref Dispatch, Name);
            Flairs = new Flairs(this, ref Dispatch);
            Wiki = new Wiki(ref Dispatch, Name);
        }

        /// <summary>
        /// Create an empty subreddit controller instance.
        /// </summary>
        /// <param name="dispatch"></param>
        public Subreddit(ref Dispatch dispatch)
            : base()
        {
            Dispatch = dispatch;
            Posts = new SubredditPosts(ref Dispatch, Name);
            Flairs = new Flairs(this, ref Dispatch);
            Wiki = new Wiki(ref Dispatch, Name);
        }

        private void ImportFromModel(RedditThings.Subreddit subreddit)
        {
            BannerImg = subreddit.BannerImg;
            BannerBackgroundColor = subreddit.BannerBackgroundColor;
            BannerBackgroundImage = subreddit.BannerBackgroundImage;
            SubredditType = subreddit.SubredditType;
            CommunityIcon = subreddit.CommunityIcon;
            HeaderTitle = subreddit.HeaderTitle;
            WikiEnabled = (subreddit.WikiEnabled.HasValue ? subreddit.WikiEnabled.Value : false);
            Over18 = subreddit.Over18;
            Sidebar = subreddit.Description;
            Name = subreddit.DisplayName;
            HeaderImg = subreddit.HeaderImg;
            Title = subreddit.Title;
            CollapseDeletedComments = subreddit.CollapseDeletedComments;
            Id = subreddit.Id;
            EmojisEnabled = subreddit.EmojisEnabled;
            ShowMedia = subreddit.ShowMedia;
            AllowVideos = subreddit.AllowVideos;
            CanAssignUserFlair = subreddit.CanAssignUserFlair;
            SpoilersEnabled = subreddit.SpoilersEnabled;
            PrimaryColor = subreddit.PrimaryColor;
            SuggestedCommentSort = subreddit.SuggestedCommentSort;
            ActiveUserCount = subreddit.ActiveUserCount;
            IconImg = subreddit.IconImg;
            CanAssignLinkFlair = subreddit.CanAssignLinkFlair;
            SubmitText = new RedditThings.SubredditSubmitText(subreddit.SubmitText);
            AllowVideoGifs = subreddit.AllowVideoGifs;
            Subscribers = subreddit.Subscribers;
            SubmitTextLabel = subreddit.SubmitTextLabel;
            KeyColor = subreddit.KeyColor;
            Lang = subreddit.Lang;
            Fullname = subreddit.Name;
            Created = subreddit.Created;
            URL = subreddit.URL;
            SubmitLinkLabel = subreddit.SubmitLinkLabel;
            AllowDiscovery = subreddit.AllowDiscovery;
            Description = subreddit.PublicDescription;
            LinkFlairEnabled = subreddit.LinkFlairEnabled;
            AllowImages = subreddit.AllowImages;
            CommentScoreHideMins = subreddit.CommentScoreHideMins;
            ShowMediaPreview = subreddit.ShowMediaPreview;
            SubmissionType = subreddit.SubmissionType;
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
            string fullname = null)
        {
            Name = name;
            Title = title;
            Description = description;
            Sidebar = sidebar;
            SubmitText = new RedditThings.SubredditSubmitText(submissionText);
            Lang = lang;
            SubredditType = subredditType;
            SubmissionType = submissionType;
            SubmitLinkLabel = submitLinkLabel;
            SubmitTextLabel = submitTextLabel;
            WikiEnabled = wikiEnabled;
            Over18 = over18;
            AllowDiscovery = allowDiscovery;
            SpoilersEnabled = allowSpoilers;
            ShowMedia = showMedia;
            ShowMediaPreview = showMediaPreview;
            AllowImages = allowImages;
            AllowVideos = allowVideos;
            CollapseDeletedComments = collapseDeletedComments;
            SuggestedCommentSort = suggestedCommentSort;
            CommentScoreHideMins = commentScoreHideMins;
            HeaderImg = headerImage;
            IconImg = iconImage;
            PrimaryColor = primaryColor;
            KeyColor = keyColor;
            Fullname = fullname;
        }

        /// <summary>
        /// Sync the subreddit model data to this and return the result.
        /// </summary>
        /// <returns>Updated subreddit model instance.</returns>
        private RedditThings.Subreddit UpdateSubredditData()
        {
            SubredditData = new RedditThings.Subreddit(this);

            return SubredditData;
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
            return new LinkPost(ref Dispatch, this, title, url, author, thumbnail, thumbnailHeight, thumbnailWidth, preview,
                id, fullname, permalink, created, edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Create a new LinkPost object with the specified fullname attached to this subreddit.
        /// </summary>
        /// <param name="fullname">The fullname of an existing LinkPost.</param>
        /// <returns>A new LinkPost object attached to this subreddit.</returns>
        public LinkPost LinkPost(string fullname)
        {
            return new LinkPost(ref Dispatch, fullname, this);
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
            return new SelfPost(ref Dispatch, this, title, selfText, selfTextHtml, author, id, fullname, permalink, created, 
                edited, score, upVotes, downVotes, removed, spam);
        }

        /// <summary>
        /// Create a new SelfPost object with the specified fullname attached to this subreddit.
        /// </summary>
        /// <param name="fullname">The fullname of an existing SelfPost.</param>
        /// <returns>A new SelfPost object attached to this subreddit.</returns>
        public SelfPost SelfPost(string fullname)
        {
            return new SelfPost(ref Dispatch, fullname, this);
        }

        /// <summary>
        /// Create a new generic Post object attached to this subreddit.
        /// </summary>
        /// <returns>A new generic Post object attached to this subreddit.</returns>
        public Post Post()
        {
            return new Post(ref Dispatch, this);
        }

        /// <summary>
        /// Create a new generic Post object with the specified fullname attached to this subreddit.
        /// </summary>
        /// <param name="fullname">The fullname of an existing Post.</param>
        /// <returns>A new generic Post object attached to this subreddit.</returns>
        public Post Post(string fullname)
        {
            return new Post(ref Dispatch, fullname, this);
        }

        // Example:  Subreddit sub = reddit.Subreddit("facepalm").About();
        // Equivalent to:  Subreddit sub = reddit.Subreddit(reddit.Models.Subreddits.About("facepalm"));
        /// <summary>
        /// Return information about the current subreddit instance.
        /// </summary>
        /// <returns>An instance of this class populated with the retrieved data.</returns>
        public Subreddit About()
        {
            return new Subreddit(ref Dispatch, Dispatch.Subreddits.About(Name));
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
        /// Abdicate moderator status in a subreddit.
        /// </summary>
        public void LeaveModerator()
        {
            Dispatch.Moderation.LeaveModerator(Fullname);
        }

        /// <summary>
        /// Abdicate approved submitter status in a subreddit.
        /// </summary>
        public void LeaveContributor()
        {
            Dispatch.Moderation.LeaveContributor(Fullname);
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
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("moderators", after, before, user, includeCategories, Name, count, limit, 
                show, srDetail);

            Validate(res);

            Moderators = Listings.GetAboutChildren<Moderator>(res);
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
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("contributors", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return Listings.GetAboutChildren<SubredditUser>(res);
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
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("muted", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return Listings.GetAboutChildren<SubredditUser>(res);
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
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("banned", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return Listings.GetAboutChildren<BannedUser>(res);
        }

        /// <summary>
        /// Get the submission text for the subreddit.
        /// This text is set by the subreddit moderators and intended to be displayed on the submission form.
        /// </summary>
        /// <returns>An object containing submission text.</returns>
        public RedditThings.SubredditSubmitText GetSubmitText()
        {
            RedditThings.SubredditSubmitText res = Dispatch.Subreddits.SubmitText(Name);

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
            Validate(Dispatch.Subreddits.SubredditStylesheet("save", reason, stylesheetContents, Name));
        }

        /// <summary>
        /// Update a subreddit's stylesheet asynchronously.
        /// </summary>
        /// <param name="reason">a string up to 256 characters long, consisting of printable characters</param>
        /// <param name="stylesheetContents">the new stylesheet content</param>
        public async Task UpdateStylesheetAsync(string reason, string stylesheetContents)
        {
            await Task.Run(() =>
            {
                UpdateStylesheet(reason, stylesheetContents);
            });
        }
        
        /// <summary>
        /// Subscribe to a subreddit.
        /// </summary>
        /// <param name="skipInitialDefaults">boolean value</param>
        public void Subscribe(bool skipInitialDefaults = false)
        {
            Dispatch.Subreddits.Subscribe("sub", skipInitialDefaults, Name);
        }

        /// <summary>
        /// Subscribe to a subreddit asynchronously.
        /// </summary>
        /// <param name="skipInitialDefaults">boolean value</param>
        public async Task SubscribeAsync(bool skipInitialDefaults = false)
        {
            await Task.Run(() =>
            {
                Subscribe(skipInitialDefaults);
            });
        }

        /// <summary>
        /// Unsubscribe from a subreddit.
        /// </summary>
        public void Unsubscribe()
        {
            Dispatch.Subreddits.Subscribe("unsub", false, Name);
        }

        /// <summary>
        /// Unsubscribe from a subreddit asynchronously.
        /// </summary>
        public async Task UnsubscribeAsync()
        {
            await Task.Run(() =>
            {
                Unsubscribe();
            });
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
            await Task.Run(() =>
            {
                DeleteBanner();
            });
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
            await Task.Run(() =>
            {
                DeleteHeader();
            });
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
            await Task.Run(() =>
            {
                DeleteIcon();
            });
        }

        /// <summary>
        /// Remove an image from the subreddit's custom image set.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// </summary>
        /// <param name="imgName">a valid subreddit image name</param>
        public void DeleteImg(string imgName)
        {
            Validate(Dispatch.Subreddits.DeleteSrImg(imgName, Name));
        }

        /// <summary>
        /// Remove an image from the subreddit's custom image set asynchronously.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// </summary>
        /// <param name="imgName">a valid subreddit image name</param>
        public async Task DeleteImgAsync(string imgName)
        {
            await Task.Run(() =>
            {
                DeleteImg(imgName);
            });
        }

        /// <summary>
        /// Add or replace a subreddit stylesheet image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgName">a valid subreddit image name</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public RedditThings.ImageUploadResult UploadImg(byte[] imgData, string imgName, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(imgData, 0, imgName, "img", Name, imgType));
        }

        /// <summary>
        /// Add or replace a subreddit stylesheet image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgName">a valid subreddit image name</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task UploadImgAsync(byte[] imgData, string imgName, string imgType = "png")
        {
            await Task.Run(() =>
            {
                UploadImg(imgData, imgName, imgType);
            });
        }

        /// <summary>
        /// Add or replace the subreddit logo image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public RedditThings.ImageUploadResult UploadHeader(byte[] imgData, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(imgData, 1, null, "header", Name, imgType));
        }

        /// <summary>
        /// Add or replace the subreddit logo image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task UploadHeaderAsync(byte[] imgData, string imgType = "png")
        {
            await Task.Run(() =>
            {
                UploadHeader(imgData, imgType);
            });
        }

        /// <summary>
        /// Add or replace a subreddit mobile icon image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public RedditThings.ImageUploadResult UploadIcon(byte[] imgData, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(imgData, 0, null, "icon", Name, imgType));
        }

        /// <summary>
        /// Add or replace a subreddit mobile icon image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task UploadIconAsync(byte[] imgData, string imgType = "png")
        {
            await Task.Run(() =>
            {
                UploadIcon(imgData, imgType);
            });
        }

        /// <summary>
        /// Add or replace a subreddit mobile banner image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public RedditThings.ImageUploadResult UploadBanner(byte[] imgData, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(imgData, 0, null, "banner", Name, imgType));
        }

        /// <summary>
        /// Add or replace a subreddit mobile banner image asynchronously.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        public async Task UploadBannerAsync(byte[] imgData, string imgType = "png")
        {
            await Task.Run(() =>
            {
                UploadBanner(imgData, imgType);
            });
        }

        // TODO - Figure out what created and location are for.  --Kris
        /// <summary>
        /// Get the current settings of a subreddit.
        /// </summary>
        /// <param name="created">one of (true, false)</param>
        /// <param name="location"></param>
        /// <returns>Settings for the requested subreddit.</returns>
        public RedditThings.SubredditSettingsContainer GetSettings(bool created = false, string location = "")
        {
            return Validate(Dispatch.Subreddits.Edit(Name, created, location));
        }

        /// <summary>
        /// Get the rules for the current subreddit.
        /// </summary>
        /// <returns>Subreddit rules.</returns>
        public RedditThings.RulesContainer GetRules()
        {
            return Validate(Dispatch.Subreddits.Rules(Name));
        }

        /// <summary>
        /// Get the traffic for the current subreddit.
        /// </summary>
        /// <returns>Subreddit traffic.</returns>
        public RedditThings.Traffic GetTraffic()
        {
            return Validate(Dispatch.Subreddits.Traffic(Name));
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
        public RedditThings.ModActionContainer GetLog(string type = null, int limit = 25, string after = "", string before = "", string mod = null,
            string show = "all", bool srDetail = false, int count = 0)
        {
            return Validate(Dispatch.Moderation.GetLog(after, before, Name, count, limit, mod, show, srDetail, type));
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
        public void ModeratorInvite(string username, string permissions)
        {
            Validate(Dispatch.Users.Friend(null, null, null, null, 999, username, permissions, "moderator_invite", Name));
        }

        /// <summary>
        /// Asynchronously invite a user to become a moderator of this subreddit.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        public async Task ModeratorInviteAsync(string username, string permissions)
        {
            await Task.Run(() =>
            {
                ModeratorInvite(username, permissions);
            });
        }

        /// <summary>
        /// Set permissions.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public void SetUserPermissions(string username, string permissions, string type)
        {
            Validate(Dispatch.Users.SetPermissions(username, permissions, type, Name));
        }

        /// <summary>
        /// Set permissions asynchronously.
        /// </summary>
        /// <param name="username">the name of an existing user</param>
        /// <param name="permissions">A string representing the permissions being set (e.g. "+wiki")</param>
        /// <param name="type">A string representing the type (e.g. "moderator_invite")</param>
        public async Task SetUserPermissionsAsync(string username, string permissions, string type)
        {
            await Task.Run(() =>
            {
                SetUserPermissions(username, permissions, type);
            });
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
            RedditThings.GenericContainer res = Dispatch.Subreddits.SiteAdmin(UpdateSubredditData(), allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports, gRecaptchaResponse,
                linkType, spamComments, spamLinks, spamSelfPosts, "", themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);

            Validate(res);

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
            try
            {
                return Create(allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports, gRecaptchaResponse, linkType, spamComments,
                    spamLinks, spamSelfPosts, themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);
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
            RedditThings.GenericContainer res;
            if (!manualUpdate)
            {
                res = Dispatch.Subreddits.SiteAdmin(UpdateSubredditData(), allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports,
                    gRecaptchaResponse, linkType, spamComments, spamLinks, spamSelfPosts, Fullname, themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);
            }
            else
            {
                res = Dispatch.Subreddits.SiteAdmin(allOriginalContent, allowDiscovery, allowImages, allowPostCrossposts, allowTop,
                    allowVideos, collapseDeletedComments, description, excludeBannedModqueue, freeFormReports,
                    gRecaptchaResponse, headerTitle, hideAds, keyColor, lang, linkType, name, originalContentTagEnabled,
                    over18, publicDescription, showMedia, showMediaPreview, spamComments, spamLinks, spamSelfPosts,
                    spoilersEnabled, sr, submitLinkLabel, submitText, submitTextLabel, suggestedCommentSort,
                    themeSr, themeSrUpdate, title, type, wikiMode, commentScoreHideMins, wikiEditAge,
                    wikiEditKarma);
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
        public async Task UpdateAsync(bool manualUpdate = false, bool? allOriginalContent = null, bool? allowDiscovery = null, bool? allowImages = null, bool? allowPostCrossposts = null,
            bool? allowTop = null, bool? allowVideos = null, bool? collapseDeletedComments = null, string description = null, bool? excludeBannedModqueue = null,
            bool? freeFormReports = null, string gRecaptchaResponse = null, string headerTitle = null, bool? hideAds = null, string keyColor = null, string lang = null,
            string linkType = null, string name = null, bool? originalContentTagEnabled = null, bool? over18 = null, string publicDescription = null, bool? showMedia = null,
            bool? showMediaPreview = null, string spamComments = null, string spamLinks = null, string spamSelfPosts = null, bool? spoilersEnabled = null, string sr = null,
            string submitLinkLabel = null, string submitText = null, string submitTextLabel = null, string suggestedCommentSort = null, string themeSr = null,
            bool? themeSrUpdate = null, string title = null, string type = null, string wikiMode = null, int? commentScoreHideMins = null, int? wikiEditAge = null,
            int? wikiEditKarma = null)
        {
            await Task.Run(() =>
            {
                Update(manualUpdate, allOriginalContent, allowDiscovery, allowImages, allowPostCrossposts, allowTop, allowVideos, collapseDeletedComments, description,
                    excludeBannedModqueue, freeFormReports, gRecaptchaResponse, headerTitle, hideAds, keyColor, lang, linkType, name, originalContentTagEnabled,
                    over18, publicDescription, showMedia, showMediaPreview, spamComments, spamLinks, spamSelfPosts, spoilersEnabled, sr, submitLinkLabel, submitText,
                    submitTextLabel, suggestedCommentSort, themeSr, themeSrUpdate, title, type, wikiMode, commentScoreHideMins, wikiEditAge, wikiEditKarma);
            });
        }
    }
}
