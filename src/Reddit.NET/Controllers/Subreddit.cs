using Newtonsoft.Json;
using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using Reddit.NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reddit.NET.Controllers
{
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

        public RedditThings.Subreddit SubredditData
        {
            get;
            private set;
        }

        public SubredditPosts Posts;

        internal readonly Dispatch Dispatch;

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

        public Subreddit(Dispatch dispatch, RedditThings.Subreddit subreddit)
            : base()
        {
            ImportFromModel(subreddit);

            Dispatch = dispatch;
            SubredditData = subreddit;
            Posts = new SubredditPosts(this);
        }

        public Subreddit(Dispatch dispatch, RedditThings.SubredditChild subredditChild)
            : base()
        {
            ImportFromModel(subredditChild.Data);

            Dispatch = dispatch;
            SubredditData = subredditChild.Data;
            Posts = new SubredditPosts(this);
        }

        public Subreddit(Dispatch dispatch, string name, string title, string description, string sidebar,
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null)
            : base()
        {
            SetValues(name, title, description, sidebar, submissionText, lang, subredditType, submissionType, submitLinkLabel, submitTextLabel,
                wikiEnabled, over18, allowDiscovery, allowSpoilers, showMedia, showMediaPreview, allowImages, allowVideos, collapseDeletedComments,
                suggestedCommentSort, commentScoreHideMins, headerImage, iconImage, primaryColor, keyColor);

            Dispatch = dispatch;
            UpdateSubredditData();
            Posts = new SubredditPosts(this);
        }

        public Subreddit(Dispatch dispatch, string name, string title = "", string description = "", string sidebar = "")
            : base()
        {
            SetValues(name, title, description, sidebar);

            Dispatch = dispatch;
            UpdateSubredditData();
            Posts = new SubredditPosts(this);
        }

        public Subreddit(Dispatch dispatch)
            : base()
        {
            Dispatch = dispatch;
            Posts = new SubredditPosts(this);
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
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null)
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

        public override void UpdateMonitoring(Dictionary<string, string> monitoring)
        {
            Monitoring = monitoring;
            if (Posts != null)
            {
                Posts.Subreddit.UpdateMonitoring(monitoring);
            }
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

        private List<T> GetAboutChildren<T>(RedditThings.DynamicShortListingContainer dynamicShortListingContainer)
        {
            List<T> res = new List<T>();
            if (dynamicShortListingContainer.Data.Children != null)
            {
                res = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dynamicShortListingContainer.Data.Children));
            }

            return res;
        }

        /// <summary>
        /// Get the moderators of this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of subreddit moderators.</returns>
        public List<Moderator> GetModerators(string after = "", string before = "", int limit = 100, string user = "", 
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("moderators", after, before, user, includeCategories, Name, count, limit, 
                show, srDetail);

            Validate(res);

            return GetAboutChildren<Moderator>(res);
        }

        /// <summary>
        /// Get the approved submitters of this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of subreddit contributors.</returns>
        public List<SubredditUser> GetContributors(string after = "", string before = "", int limit = 100, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("contributors", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return GetAboutChildren<SubredditUser>(res);
        }

        /// <summary>
        /// Get the approved submitters of this subreddit's wiki.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of subreddit contributors.</returns>
        public List<SubredditUser> GetWikiContributors(string after = "", string before = "", int limit = 100, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("wikicontributors", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return GetAboutChildren<SubredditUser>(res);
        }

        /// <summary>
        /// Get the muted users of this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of muted users.</returns>
        public List<SubredditUser> GetMutedUsers(string after = "", string before = "", int limit = 100, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("muted", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return GetAboutChildren<SubredditUser>(res);
        }

        /// <summary>
        /// Get a list of users who were banned from this subreddit.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of banned users.</returns>
        public List<BannedUser> GetBannedUsers(string after = "", string before = "", int limit = 100, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("banned", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return GetAboutChildren<BannedUser>(res);
        }

        /// <summary>
        /// Get a list of users who were banned from this subreddit's wiki.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="limit">the maximum number of items desired (maximum: 100)</param>
        /// <param name="user">A valid, existing reddit username</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>A list of banned users.</returns>
        public List<BannedUser> GetWikiBannedUsers(string after = "", string before = "", int limit = 100, string user = "",
            bool includeCategories = false, int count = 0, string show = "all", bool srDetail = false)
        {
            RedditThings.DynamicShortListingContainer res = Dispatch.Subreddits.About("wikibanned", after, before, user, includeCategories, Name, count, limit,
                show, srDetail);

            Validate(res);

            return GetAboutChildren<BannedUser>(res);
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
        /// Subscribe to a subreddit.
        /// </summary>
        /// <param name="skipInitialDefaults">boolean value</param>
        public void Subscribe(bool skipInitialDefaults = false)
        {
            Dispatch.Subreddits.Subscribe("sub", skipInitialDefaults, Name);
        }

        /// <summary>
        /// Unsubscribe from a subreddit.
        /// </summary>
        public void Unsubscribe()
        {
            Dispatch.Subreddits.Subscribe("unsub", false, Name);
        }

        /// <summary>
        /// Remove the subreddit's custom mobile banner.
        /// </summary>
        public void DeleteBanner()
        {
            Validate(Dispatch.Subreddits.DeleteSrBanner(Name));
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
        /// Remove the subreddit's custom mobile icon.
        /// </summary>
        public void DeleteIcon()
        {
            Validate(Dispatch.Subreddits.DeleteSrIcon(Name));
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
        /// Add or replace a subreddit mobile banner image.
        /// </summary>
        /// <param name="imgData">file upload with maximum size of 500 KiB</param>
        /// <param name="imgType">one of png or jpg (default: png)</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public RedditThings.ImageUploadResult UploadBanner(byte[] imgData, string imgType = "png")
        {
            return Validate(Dispatch.Subreddits.UploadSrImg(imgData, 0, null, "banner", Name, imgType));
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
        /// Clear link flair templates.
        /// </summary>
        public void ClearLinkFlairTemplates()
        {
            Validate(Dispatch.Flair.ClearFlairTemplates("LINK_FLAIR", Name));
        }

        /// <summary>
        /// Clear user flair templates.
        /// </summary>
        public void ClearUserFlairTemplates()
        {
            Validate(Dispatch.Flair.ClearFlairTemplates("USER_FLAIR", Name));
        }

        /// <summary>
        /// Delete flair.
        /// </summary>
        /// <param name="username">The user whose flair we're removing</param>
        public void DeleteFlair(string username)
        {
            Validate(Dispatch.Flair.DeleteFlair(username, Name));
        }

        /// <summary>
        /// Delete flair template.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being deleted (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        public void DeleteFlairTemplate(string flairTemplateId)
        {
            Validate(Dispatch.Flair.DeleteFlairTemplate(flairTemplateId, Name));
        }

        /// <summary>
        /// Create a new user flair.
        /// </summary>
        /// <param name="username">The user who's getting the new flair</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateFlair(string username, string text, string cssClass = "")
        {
            Validate(Dispatch.Flair.Create(cssClass, "", username, text, Name));
        }

        /// <summary>
        /// Update the flair configuration settings for this subreddit.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        /// <param name="flairPosition">one of (left, right)</param>
        /// <param name="flairSelfAssignEnabled">boolean value</param>
        /// <param name="linkFlairPosition">one of (left, right)</param>
        /// <param name="linkFlairSelfAssignEnabled">boolean value</param>
        public void FlairConfig(bool flairEnabled, string flairPosition, bool flairSelfAssignEnabled, string linkFlairPosition, bool linkFlairSelfAssignEnabled)
        {
            Validate(Dispatch.Flair.FlairConfig(flairEnabled, flairPosition, flairSelfAssignEnabled, linkFlairPosition, linkFlairSelfAssignEnabled, Name));
        }

        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// Requires a string 'flair_csv' which has up to 100 lines of the form 'user,flairtext,cssclass' (Lines beyond the 100th are ignored).
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">comma-seperated flair information</param>
        /// <returns>Action results.</returns>
        public List<RedditThings.ActionResult> FlairCSV(string flairCsv)
        {
            return Validate(Dispatch.Flair.FlairCSV(flairCsv, Name));
        }

        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A valid FlairListResultContainer object</param>
        /// <returns>Action results.</returns>
        public List<RedditThings.ActionResult> FlairCSV(RedditThings.FlairListResultContainer flairCsv)
        {
            return FlairCSV(flairCsv.Users);
        }

        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A list of valid FlairListResult objects</param>
        /// <returns>Action results.</returns>
        public List<RedditThings.ActionResult> FlairCSV(List<RedditThings.FlairListResult> flairCsv)
        {
            string arg = "";
            foreach (RedditThings.FlairListResult flairListResult in flairCsv)
            {
                arg += flairListResult.ToCSV();
            }

            return FlairCSV(arg);
        }

        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="username">a user by name</param>
        /// <param name="limit">the maximum number of items desired (maximum: 1000)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>Flair list results.</returns>
        public List<RedditThings.FlairListResult> FlairList(string username = "", int limit = 100, string after = "", string before = "", int count = 0,
            string show = "all", bool srDetail = false)
        {
            return Validate(Dispatch.Flair.FlairList(after, before, username, Name, count, limit, show, srDetail)).Users;
        }

        /// <summary>
        /// Return information about a users's flair options.
        /// </summary>
        /// <param name="username">A valid Reddit username</param>
        /// <returns>Flair results.</returns>
        public RedditThings.FlairSelectorResultContainer FlairSelector(string username)
        {
            return Validate(Dispatch.Flair.FlairSelector(username, Name));
        }

        /// <summary>
        /// Create a new link flair template.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateLinkFlairTemplate(string text, bool textEditable = false, string cssClass = "")
        {
            Validate(Dispatch.Flair.FlairTemplate(cssClass, "", "LINK_FLAIR", text, textEditable, Name));
        }

        /// <summary>
        /// Create a new user flair template.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateUserFlairTemplate(string text, bool textEditable = false, string cssClass = "")
        {
            Validate(Dispatch.Flair.FlairTemplate(cssClass, "", "USER_FLAIR", text, textEditable, Name));
        }

        /// <summary>
        /// Update an existing link flair template.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void UpdateLinkFlairTemplate(string flairTemplateId, string text = null, bool? textEditable = null, string cssClass = null)
        {
            Validate(Dispatch.Flair.FlairTemplate(cssClass, flairTemplateId, "LINK_FLAIR", text, textEditable, Name));
        }

        /// <summary>
        /// Update an existing user flair template.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void UpdateUserFlairTemplate(string flairTemplateId, string text = null, bool? textEditable = null, string cssClass = null)
        {
            Validate(Dispatch.Flair.FlairTemplate(cssClass, flairTemplateId, "USER_FLAIR", text, textEditable, Name));
        }

        /// <summary>
        /// Create a new link flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        /// <returns>The created flair object.</returns>
        public RedditThings.FlairV2 CreateLinkFlairTemplateV2(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, "", "LINK_FLAIR", modOnly, text, textColor, textEditable, Name));
        }

        /// <summary>
        /// Create a new user flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        /// <returns>The created flair object.</returns>
        public RedditThings.FlairV2 CreateUserFlairTemplateV2(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, "", "USER_FLAIR", modOnly, text, textColor, textEditable, Name));
        }

        /// <summary>
        /// Update an existing link flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        /// <returns>The updated flair object.</returns>
        public RedditThings.FlairV2 UpdateLinkFlairTemplateV2(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, flairTemplateId, "LINK_FLAIR", modOnly, text, textColor, textEditable, Name));
        }

        /// <summary>
        /// Update an existing user flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        /// <returns>The updated flair object.</returns>
        public RedditThings.FlairV2 UpdateUserFlairTemplateV2(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, flairTemplateId, "USER_FLAIR", modOnly, text, textColor, textEditable, Name));
        }

        /// <summary>
        /// Set flair enabled.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        public void SetFlairEnabled(bool flairEnabled)
        {
            Validate(Dispatch.Flair.SetFlairEnabled(flairEnabled, Name));
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available link flairs.</returns>
        public List<RedditThings.Flair> LinkFlair()
        {
            return Validate(Dispatch.Flair.LinkFlair(Name));
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available link flairs.</returns>
        public List<RedditThings.FlairV2> LinkFlairV2()
        {
            return Validate(Dispatch.Flair.LinkFlairV2(Name));
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available user flairs.</returns>
        public List<RedditThings.Flair> UserFlair()
        {
            return Validate(Dispatch.Flair.UserFlair(Name));
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available user flairs.</returns>
        public List<RedditThings.FlairV2> UserFlairV2()
        {
            return Validate(Dispatch.Flair.UserFlairV2(Name));
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
        /// <param name="limit">the maximum number of items desired (maximum: 500)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="mod">(optional) a moderator filter</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A listing of recent moderation actions.</returns>
        public RedditThings.ModActionContainer GetLog(string type = null, int limit = 100, string after = "", string before = "", string mod = null,
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
    }
}
