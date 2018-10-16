using ModelStructures = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers
{
    public class Subreddit
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
        public byte[] HeaderImg;
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
        public byte[] IconImg;
        public bool CanAssignLinkFlair;
        public string SubmitText;
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

        public ModelStructures.Subreddit SubredditData
        {
            get;
            private set;
        }

        private readonly Dispatch Dispatch;

        public Subreddit(Dispatch dispatch, ModelStructures.Subreddit subreddit)
        {
            ImportFromModel(subreddit);

            this.SubredditData = subreddit;
            this.Dispatch = dispatch;
        }

        public Subreddit(Dispatch dispatch, ModelStructures.SubredditChild subredditChild)
        {
            ImportFromModel(subredditChild.Data);

            this.SubredditData = subredditChild.Data;
            this.Dispatch = dispatch;
        }

        public Subreddit(Dispatch dispatch, string name, string title, string description, string sidebar,
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null)
        {
            SetValues(name, title, description, sidebar, submissionText, lang, subredditType, submissionType, submitLinkLabel, submitTextLabel,
                wikiEnabled, over18, allowDiscovery, allowSpoilers, showMedia, showMediaPreview, allowImages, allowVideos, collapseDeletedComments,
                suggestedCommentSort, commentScoreHideMins, headerImage, iconImage, primaryColor, keyColor);

            UpdateSubredditData();
            this.Dispatch = dispatch;
        }

        public Subreddit(Dispatch dispatch, string name, string title = "", string description = "", string sidebar = "")
        {
            SetValues(name, title, description, sidebar);

            UpdateSubredditData();
            this.Dispatch = dispatch;
        }

        public Subreddit(Dispatch dispatch)
        {
            this.Dispatch = dispatch;
        }

        private void ImportFromModel(ModelStructures.Subreddit subreddit)
        {
            this.BannerImg = subreddit.BannerImg;
            this.BannerBackgroundColor = subreddit.BannerBackgroundColor;
            this.BannerBackgroundImage = subreddit.BannerBackgroundImage;
            this.SubredditType = subreddit.SubredditType;
            this.CommunityIcon = subreddit.CommunityIcon;
            this.HeaderTitle = subreddit.HeaderTitle;
            this.WikiEnabled = (subreddit.WikiEnabled.HasValue ? subreddit.WikiEnabled.Value : false);
            this.Over18 = subreddit.Over18;
            this.Sidebar = subreddit.Description;
            this.Name = subreddit.DisplayName;
            this.HeaderImg = subreddit.HeaderImg;
            this.Title = subreddit.Title;
            this.CollapseDeletedComments = subreddit.CollapseDeletedComments;
            this.Id = subreddit.Id;
            this.EmojisEnabled = subreddit.EmojisEnabled;
            this.ShowMedia = subreddit.ShowMedia;
            this.AllowVideos = subreddit.AllowVideos;
            this.CanAssignUserFlair = subreddit.CanAssignUserFlair;
            this.SpoilersEnabled = subreddit.SpoilersEnabled;
            this.PrimaryColor = subreddit.PrimaryColor;
            this.SuggestedCommentSort = subreddit.SuggestedCommentSort;
            this.ActiveUserCount = subreddit.ActiveUserCount;
            this.IconImg = subreddit.IconImg;
            this.CanAssignLinkFlair = subreddit.CanAssignLinkFlair;
            this.SubmitText = subreddit.SubmitText;
            this.AllowVideoGifs = subreddit.AllowVideoGifs;
            this.Subscribers = subreddit.Subscribers;
            this.SubmitTextLabel = subreddit.SubmitTextLabel;
            this.KeyColor = subreddit.KeyColor;
            this.Lang = subreddit.Lang;
            this.Fullname = subreddit.Name;
            this.Created = subreddit.Created;
            this.URL = subreddit.URL;
            this.SubmitLinkLabel = subreddit.SubmitLinkLabel;
            this.AllowDiscovery = subreddit.AllowDiscovery;
            this.Description = subreddit.PublicDescription;
            this.LinkFlairEnabled = subreddit.LinkFlairEnabled;
            this.AllowImages = subreddit.AllowImages;
            this.CommentScoreHideMins = subreddit.CommentScoreHideMins;
            this.ShowMediaPreview = subreddit.ShowMediaPreview;
            this.SubmissionType = subreddit.SubmissionType;
        }

        private void SetValues(string name, string title, string description, string sidebar,
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null)
        {
            this.Name = name;
            this.Title = title;
            this.Description = description;
            this.Sidebar = sidebar;
            this.SubmitText = submissionText;
            this.Lang = lang;
            this.SubredditType = subredditType;
            this.SubmissionType = submissionType;
            this.SubmitLinkLabel = submitLinkLabel;
            this.SubmitTextLabel = submitTextLabel;
            this.WikiEnabled = wikiEnabled;
            this.Over18 = over18;
            this.AllowDiscovery = allowDiscovery;
            this.SpoilersEnabled = allowSpoilers;
            this.ShowMedia = showMedia;
            this.ShowMediaPreview = showMediaPreview;
            this.AllowImages = allowImages;
            this.AllowVideos = allowVideos;
            this.CollapseDeletedComments = collapseDeletedComments;
            this.SuggestedCommentSort = suggestedCommentSort;
            this.CommentScoreHideMins = commentScoreHideMins;
            this.HeaderImg = headerImage;
            this.IconImg = iconImage;
            this.PrimaryColor = primaryColor;
            this.KeyColor = keyColor;
        }

        /// <summary>
        /// Sync the subreddit model data to this and return the result.
        /// </summary>
        /// <returns>Updated subreddit model instance.</returns>
        private ModelStructures.Subreddit UpdateSubredditData()
        {
            this.SubredditData = new ModelStructures.Subreddit(this);

            return SubredditData;
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
        /// Get the rules for the current subreddit.
        /// </summary>
        /// <returns>Subreddit rules.</returns>
        public ModelStructures.RulesContainer GetRules()
        {
            return Dispatch.Subreddits.Rules(Name);
        }

        // Example:  Subreddit sub = reddit.Subreddit("MyNewSubreddit", "My New Subreddit", "Some description.", "This is my sidebar!").Create();
        // Equivalent to:  reddit.Models.Subreddits.SiteAdmin(name:"MyNewSubreddit", title:"My New Subreddit", publicDescription:"Some description", description:"This is my sidebar!", ...);
        //                 Subreddit sub = reddit.Subreddit(reddit.Models.Subreddits.About("MyNewSubreddit"));
        /// <summary>
        /// Create a new subreddit and return the created result.
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
            object res = Dispatch.Subreddits.SiteAdmin(UpdateSubredditData(), allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports, gRecaptchaResponse,
                linkType, spamComments, spamLinks, spamSelfPosts, "", themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);

            // TODO - Check res for errors (or will API return non-200 on failure?).  --Kris

            return About();
        }

        // Example:  Subreddit sub = reddit.Subreddit("MyNewSubreddit").About();
        //           sub.Sidebar = "I've updated my sidebar and there's nothing you can do about it.";
        //           sub.Update();
        // Example:  Subreddit sub = reddit.Subreddit("MyNewSubreddit").About();
        //           sub.Update(wikiMode:"anyone");
        /// <summary>
        /// Update an existing subreddit.
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
        /// <returns>Whether the update was successful.</returns>
        public bool Update(bool allowPostCrossposts = true, bool allowTop = true, bool excludeBannedModqueue = false, bool freeFormReports = true,
            string gRecaptchaResponse = "", string linkType = "any", string spamComments = "low", string spamLinks = "high", string spamSelfPosts = "high",
            string themeSr = "", bool themeSrUpdate = true, string wikiMode = "disabled", int wikiEditAge = 0, int wikiEditKarma = 0)
        {
            object res = Dispatch.Subreddits.SiteAdmin(UpdateSubredditData(), allowPostCrossposts, allowTop, excludeBannedModqueue, freeFormReports, gRecaptchaResponse,
                linkType, spamComments, spamLinks, spamSelfPosts, Fullname, themeSr, themeSrUpdate, wikiMode, wikiEditAge, wikiEditKarma);

            // TODO - Check res for errors (or will API return non-200 on failure?).  --Kris

            return true;
        }
    }
}
