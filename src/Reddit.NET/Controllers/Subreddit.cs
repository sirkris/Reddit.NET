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

        public ModelStructures.Subreddit SubredditData;

        private readonly Dispatch Dispatch;

        public Subreddit(Dispatch dispatch, ModelStructures.Subreddit subreddit)
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
            
            this.SubredditData = subreddit;
            
            this.Dispatch = dispatch;
        }

        public Subreddit(Dispatch dispatch, string name, string title, string description, string sidebar,
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

            this.SubredditData = new ModelStructures.Subreddit(this);

            this.Dispatch = dispatch;
        }

        public Subreddit(Dispatch dispatch)
        {
            this.Dispatch = dispatch;
        }

        // TODO - Methods.  --Kris
    }
}
