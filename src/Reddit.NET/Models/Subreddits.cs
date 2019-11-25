using Newtonsoft.Json;
using Reddit.Inputs;
using Reddit.Inputs.Subreddits;
using Reddit.Things;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class Subreddits : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Subreddits(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (banned, muted, wikibanned, contributors, wikicontributors, moderators)</param>
        /// <param name="subredditsAboutInput">A valid SubredditsAboutInput instance</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object containing the requested data.</returns>
        public DynamicShortListingContainer About(string where, SubredditsAboutInput subredditsAboutInput, string subreddit = null)
        {
            return SendRequest<DynamicShortListingContainer>(Sr(subreddit) + "about/" + where, subredditsAboutInput);
        }

        /// <summary>
        /// Return information about the subreddit.
        /// Data includes the subscriber count, description, and header image.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>A subreddit listing.</returns>
        public SubredditChild About(string subreddit)
        {
            return JsonConvert.DeserializeObject<SubredditChild>(ExecuteRequest("r/" + subreddit + "/about"));
        }

        /// <summary>
        /// Remove the subreddit's custom mobile banner.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer DeleteSrBanner(string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/delete_sr_banner", new APITypeInput(), Method.POST);
        }

        /// <summary>
        /// Remove the subreddit's custom mobile banner asynchronously.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> DeleteSrBannerAsync(string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/delete_sr_banner", new APITypeInput(), Method.POST);
        }

        /// <summary>
        /// Remove the subreddit's custom header image.
        /// The sitewide-default header image will be shown again after this call.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer DeleteSrHeader(string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/delete_sr_header", new APITypeInput(), Method.POST);
        }

        /// <summary>
        /// Remove the subreddit's custom header image asynchronously.
        /// The sitewide-default header image will be shown again after this call.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> DeleteSrHeaderAsync(string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/delete_sr_header", new APITypeInput(), Method.POST);
        }

        /// <summary>
        /// Remove the subreddit's custom mobile icon.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer DeleteSrIcon(string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/delete_sr_icon", new APITypeInput(), Method.POST);
        }

        /// <summary>
        /// Remove the subreddit's custom mobile icon asynchronously.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> DeleteSrIconAsync(string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/delete_sr_icon", new APITypeInput(), Method.POST);
        }

        /// <summary>
        /// Remove an image from the subreddit's custom image set.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subredditsDeleteSrImgInput">A valid SubredditsDeleteSrImgInput instance</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer DeleteSrImg(SubredditsDeleteSrImgInput subredditsDeleteSrImgInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/delete_sr_img", subredditsDeleteSrImgInput, Method.POST);
        }

        /// <summary>
        /// Remove an image from the subreddit's custom image set asynchronously.
        /// The image will no longer count against the subreddit's image limit. However, the actual image data may still be accessible for an unspecified amount of time. 
        /// If the image is currently referenced by the subreddit's stylesheet, that stylesheet will no longer validate and won't be editable until the image reference is removed.
        /// See also: /api/upload_sr_img.
        /// </summary>
        /// <param name="subredditsDeleteSrImgInput">A valid SubredditsDeleteSrImgInput instance</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> DeleteSrImgAsync(SubredditsDeleteSrImgInput subredditsDeleteSrImgInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/delete_sr_img", subredditsDeleteSrImgInput, Method.POST);
        }

        /// <summary>
        /// Return subreddits recommended for the given subreddit(s).
        /// Gets a list of subreddits recommended for srnames, filtering out any that appear in the optional omit param.
        /// </summary>
        /// <param name="srNames">comma-delimited list of subreddit names</param>
        /// <param name="omit">comma-delimited list of subreddit names</param>
        /// <param name="over18">boolean value</param>
        /// <returns>A list of subreddits.</returns>
        public IEnumerable<SubredditRecommendations> Recommended(string srNames, SubredditsRecommendInput subredditsRecommendInput)
        {
            return SendRequest<IEnumerable<SubredditRecommendations>>("api/recommend/sr/" + srNames, subredditsRecommendInput);
        }

        /// <summary>
        /// List subreddit names that begin with a query string.
        /// </summary>
        /// <param name="subredditsSearchInput">A valid SubredditsSearchInput instance</param>
        /// <returns>A list of subreddit names.</returns>
        public SubredditNames SearchRedditNames(SubredditsSearchNamesInput subredditsSearchInput)
        {
            return SendRequest<SubredditNames>("api/search_reddit_names", subredditsSearchInput);
        }

        /// <summary>
        /// List subreddits that begin with a query string.
        /// </summary>
        /// <param name="subredditsSearchInput">A valid SubredditsSearchInput instance</param>
        /// <returns>A list of subreddit listings.</returns>
        public SubSearch SearchSubreddits(SubredditsSearchNamesInput subredditsSearchInput)
        {
            return SendRequest<SubSearch>("api/search_subreddits", subredditsSearchInput, Method.POST);
        }

        /// <summary>
        /// Create or configure a subreddit.  Null values are ignored.
        /// If sr is specified, the request will attempt to modify the specified subreddit. If not, a subreddit with name name will be created.
        /// This endpoint expects all values to be supplied on every request. If modifying a subset of options, it may be useful to get the current settings from /about/edit.json first.
        /// For backwards compatibility, description is the sidebar text and public_description is the publicly visible subreddit description.
        /// Most of the parameters for this endpoint are identical to options visible in the user interface and their meanings are best explained there.
        /// See also: /about/edit.json.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle">a string no longer than 500 characters</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer SiteAdmin(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = null, string headerTitle = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(PrepareSiteAdmin(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle)));
        }

        /// <summary>
        /// Create or configure a subreddit asynchronously.  Null values are ignored.
        /// If sr is specified, the request will attempt to modify the specified subreddit. If not, a subreddit with name name will be created.
        /// This endpoint expects all values to be supplied on every request. If modifying a subset of options, it may be useful to get the current settings from /about/edit.json first.
        /// For backwards compatibility, description is the sidebar text and public_description is the publicly visible subreddit description.
        /// Most of the parameters for this endpoint are identical to options visible in the user interface and their meanings are best explained there.
        /// See also: /about/edit.json.
        /// </summary>
        /// <param name="subredditsSiteAdminInput">A valid SubredditsSiteAdminInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="headerTitle">a string no longer than 500 characters</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> SiteAdminAsync(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = null, string headerTitle = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(await ExecuteRequestAsync(PrepareSiteAdmin(subredditsSiteAdminInput, gRecaptchaResponse, headerTitle)));
        }

        private RestRequest PrepareSiteAdmin(SubredditsSiteAdminInput subredditsSiteAdminInput, string gRecaptchaResponse = null, string headerTitle = null)
        {
            RestRequest restRequest = PrepareRequest("api/site_admin", Method.POST);

            restRequest.AddObject(subredditsSiteAdminInput);
            AddParamIfNotNull("g-recaptcha-response", gRecaptchaResponse, ref restRequest);
            AddParamIfNotNull("header-title", headerTitle, ref restRequest);

            return restRequest;
        }

        /// <summary>
        /// Create or configure a subreddit.
        /// If sr is specified, the request will attempt to modify the specified subreddit. If not, a subreddit with name name will be created.
        /// This endpoint expects all values to be supplied on every request. If modifying a subset of options, it may be useful to get the current settings from /about/edit.json first.
        /// For backwards compatibility, description is the sidebar text and public_description is the publicly visible subreddit description.
        /// Most of the parameters for this endpoint are identical to options visible in the user interface and their meanings are best explained there.
        /// See also: /about/edit.json.
        /// </summary>
        /// <param name="subreddit">A valid subreddit object.</param>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="sr">fullname of a thing</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer SiteAdmin(Subreddit subreddit, bool? allowPostCrossposts = null, bool? allowTop = null, bool? excludeBannedModqueue = null,
            bool? freeFormReports = null, string gRecaptchaResponse = null, string linkType = null, string spamComments = null, string spamLinks = null,
            string spamSelfPosts = null, string sr = null, string themeSr = null, bool? themeSrUpdate = null, string wikiMode = null, int? wikiEditAge = null,
            int? wikiEditKarma = null)
        {
            return SiteAdmin(new SubredditsSiteAdminInput(subreddit.AllOriginalContent, subreddit.AllowDiscovery, subreddit.AllowImages, allowPostCrossposts,
                allowTop, subreddit.AllowVideos, subreddit.CollapseDeletedComments, subreddit.Description, excludeBannedModqueue,
                freeFormReports, subreddit.HideAds, subreddit.KeyColor, subreddit.Lang, linkType,
                subreddit.DisplayName, subreddit.OriginalContentTagEnabled, subreddit.Over18, subreddit.PublicDescription, subreddit.ShowMedia,
                subreddit.ShowMediaPreview, spamComments, spamLinks, spamSelfPosts, subreddit.SpoilersEnabled, sr, subreddit.SubmitLinkLabel,
                subreddit.SubmitText, subreddit.SubmitTextLabel, subreddit.SuggestedCommentSort, themeSr, themeSrUpdate, subreddit.Title,
                subreddit.SubredditType, wikiMode, subreddit.CommentScoreHideMins, wikiEditAge, wikiEditKarma), gRecaptchaResponse, subreddit.HeaderTitle);
        }

        /// <summary>
        /// Create or configure a subreddit asynchronously.
        /// If sr is specified, the request will attempt to modify the specified subreddit. If not, a subreddit with name name will be created.
        /// This endpoint expects all values to be supplied on every request. If modifying a subset of options, it may be useful to get the current settings from /about/edit.json first.
        /// For backwards compatibility, description is the sidebar text and public_description is the publicly visible subreddit description.
        /// Most of the parameters for this endpoint are identical to options visible in the user interface and their meanings are best explained there.
        /// See also: /about/edit.json.
        /// </summary>
        /// <param name="subreddit">A valid subreddit object.</param>
        /// <param name="allowPostCrossposts">boolean value</param>
        /// <param name="allowTop">boolean value</param>
        /// <param name="excludeBannedModqueue">boolean value</param>
        /// <param name="freeFormReports">boolean value</param>
        /// <param name="gRecaptchaResponse"></param>
        /// <param name="linkType">one of (any, link, self)</param>
        /// <param name="spamComments">one of (low, high, all)</param>
        /// <param name="spamLinks">one of (low, high, all)</param>
        /// <param name="spamSelfPosts">one of (low, high, all)</param>
        /// <param name="sr">fullname of a thing</param>
        /// <param name="themeSr">subreddit name</param>
        /// <param name="themeSrUpdate">boolean value</param>
        /// <param name="wikiMode">one of (disabled, modonly, anyone)</param>
        /// <param name="wikiEditAge">an integer between 0 and 36600 (default: 0)</param>
        /// <param name="wikiEditKarma">an integer between 0 and 1000000000 (default: 0)</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> SiteAdminAsync(Subreddit subreddit, bool? allowPostCrossposts = null, bool? allowTop = null, bool? excludeBannedModqueue = null,
            bool? freeFormReports = null, string gRecaptchaResponse = null, string linkType = null, string spamComments = null, string spamLinks = null,
            string spamSelfPosts = null, string sr = null, string themeSr = null, bool? themeSrUpdate = null, string wikiMode = null, int? wikiEditAge = null,
            int? wikiEditKarma = null)
        {
            return await SiteAdminAsync(new SubredditsSiteAdminInput(subreddit.AllOriginalContent, subreddit.AllowDiscovery, subreddit.AllowImages, allowPostCrossposts,
                allowTop, subreddit.AllowVideos, subreddit.CollapseDeletedComments, subreddit.Description, excludeBannedModqueue,
                freeFormReports, subreddit.HideAds, subreddit.KeyColor, subreddit.Lang, linkType,
                subreddit.DisplayName, subreddit.OriginalContentTagEnabled, subreddit.Over18, subreddit.PublicDescription, subreddit.ShowMedia,
                subreddit.ShowMediaPreview, spamComments, spamLinks, spamSelfPosts, subreddit.SpoilersEnabled, sr, subreddit.SubmitLinkLabel,
                subreddit.SubmitText, subreddit.SubmitTextLabel, subreddit.SuggestedCommentSort, themeSr, themeSrUpdate, subreddit.Title,
                subreddit.SubredditType, wikiMode, subreddit.CommentScoreHideMins, wikiEditAge, wikiEditKarma), gRecaptchaResponse, subreddit.HeaderTitle);
        }

        /// <summary>
        /// Get the submission text for the subreddit.
        /// This text is set by the subreddit moderators and intended to be displayed on the submission form.
        /// See also: /api/site_admin.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object containing submission text.</returns>
        public SubredditSubmitText SubmitText(string subreddit = null)
        {
            return JsonConvert.DeserializeObject<SubredditSubmitText>(ExecuteRequest(Sr(subreddit) + "api/submit_text"));
        }

        /// <summary>
        /// Return a list of subreddits and data for subreddits whose names start with 'query'.
        /// Uses typeahead endpoint to recieve the list of subreddits names. 
        /// Typeahead provides exact matches, typo correction, fuzzy matching and boosts subreddits to the top that the user is subscribed to.
        /// </summary>
        /// <param name="subredditsAutocompleteInput">A valid SubredditsAutocompleteInput instance</param>
        /// <returns>Matching subreddits.</returns>
        public SubredditAutocompleteResultContainer SubredditAutocomplete(SubredditsAutocompleteInput subredditsAutocompleteInput)
        {
            return SendRequest<SubredditAutocompleteResultContainer>("api/subreddit_autocomplete", subredditsAutocompleteInput);
        }

        /// <summary>
        /// Version 2 of SubredditAutocomplete.
        /// </summary>
        /// <param name="subredditsAutocompleteV2Input">A valid SubredditsAutocompleteV2Input instance</param>
        /// <returns>Matching subreddits.</returns>
        public SubredditContainer SubredditAutocompleteV2(SubredditsAutocompleteV2Input subredditsAutocompleteV2Input)
        {
            return SendRequest<SubredditContainer>("api/subreddit_autocomplete_v2", subredditsAutocompleteV2Input);
        }

        /// <summary>
        /// Update a subreddit's stylesheet.
        /// op should be save to update the contents of the stylesheet.
        /// </summary>
        /// <param name="subredditsSubredditStylesheetInput">A valid SubredditsSubredditStylesheetInput instance</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public GenericContainer SubredditStylesheet(SubredditsSubredditStylesheetInput subredditsSubredditStylesheetInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/subreddit_stylesheet", subredditsSubredditStylesheetInput, Method.POST);
        }

        /// <summary>
        /// Update a subreddit's stylesheet asynchronously.
        /// op should be save to update the contents of the stylesheet.
        /// </summary>
        /// <param name="subredditsSubredditStylesheetInput">A valid SubredditsSubredditStylesheetInput instance</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object indicating any errors.</returns>
        public async Task<GenericContainer> SubredditStylesheetAsync(SubredditsSubredditStylesheetInput subredditsSubredditStylesheetInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/subreddit_stylesheet", subredditsSubredditStylesheetInput, Method.POST);
        }

        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit.
        /// To subscribe, action should be sub.  To unsubscribe, action should be unsub.The user must have access to the subreddit to be able to subscribe to it.
        /// The skip_initial_defaults param can be set to True to prevent automatically subscribing the user to the current set of defaults when they take their first subscription action.
        /// Attempting to set it for an unsubscribe action will result in an error.
        /// See also: /subreddits/mine/.
        /// </summary>
        /// <param name="subredditsSubByFullnameInput">A valid SubredditsSubByFullnameInput instance</param>
        public void SubscribeByFullname(SubredditsSubByFullnameInput subredditsSubByFullnameInput)
        {
            SendRequest<object>("api/subscribe", subredditsSubByFullnameInput, Method.POST);
        }

        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit asynchronously.
        /// To subscribe, action should be sub.  To unsubscribe, action should be unsub.The user must have access to the subreddit to be able to subscribe to it.
        /// The skip_initial_defaults param can be set to True to prevent automatically subscribing the user to the current set of defaults when they take their first subscription action.
        /// Attempting to set it for an unsubscribe action will result in an error.
        /// See also: /subreddits/mine/.
        /// </summary>
        /// <param name="subredditsSubByFullnameInput">A valid SubredditsSubByFullnameInput instance</param>
        public async Task SubscribeByFullnameAsync(SubredditsSubByFullnameInput subredditsSubByFullnameInput)
        {
            await SendRequestAsync<object>("api/subscribe", subredditsSubByFullnameInput, Method.POST);
        }

        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit.
        /// To subscribe, action should be sub.  To unsubscribe, action should be unsub.The user must have access to the subreddit to be able to subscribe to it.
        /// The skip_initial_defaults param can be set to True to prevent automatically subscribing the user to the current set of defaults when they take their first subscription action.
        /// Attempting to set it for an unsubscribe action will result in an error.
        /// See also: /subreddits/mine/.
        /// </summary>
        /// <param name="subredditsSubByNameInput">A valid SubredditsSubByNameInput instance</param>
        public void Subscribe(SubredditsSubByNameInput subredditsSubByNameInput)
        {
            SendRequest<object>("api/subscribe", subredditsSubByNameInput, Method.POST);
        }

        /// <summary>
        /// Subscribe to or unsubscribe from a subreddit asynchronously.
        /// To subscribe, action should be sub.  To unsubscribe, action should be unsub.The user must have access to the subreddit to be able to subscribe to it.
        /// The skip_initial_defaults param can be set to True to prevent automatically subscribing the user to the current set of defaults when they take their first subscription action.
        /// Attempting to set it for an unsubscribe action will result in an error.
        /// See also: /subreddits/mine/.
        /// </summary>
        /// <param name="subredditsSubByNameInput">A valid SubredditsSubByNameInput instance</param>
        public async Task SubscribeAsync(SubredditsSubByNameInput subredditsSubByNameInput)
        {
            await SendRequestAsync<object>("api/subscribe", subredditsSubByNameInput, Method.POST);
        }

        /// <summary>
        /// Add or replace a subreddit image, custom header logo, custom mobile icon, or custom mobile banner.
        /// If the upload_type value is img, an image for use in the subreddit stylesheet is uploaded with the name specified in name.
        /// If the upload_type value is header then the image uploaded will be the subreddit's new logo and name will be ignored.
        /// If the upload_type value is icon then the image uploaded will be the subreddit's new mobile icon and name will be ignored.
        /// If the upload_type value is banner then the image uploaded will be the subreddit's new mobile banner and name will be ignored.
        /// For backwards compatibility, if upload_type is not specified, the header field will be used instead:
        /// If the header field has value 0, then upload_type is img.
        /// If the header field has value 1, then upload_type is header.
        /// The img_type field specifies whether to store the uploaded image as a PNG or JPEG.
        /// Subreddits have a limited number of images that can be in use at any given time. If no image with the specified name already exists, one of the slots will be consumed
        /// If an image with the specified name already exists, it will be replaced. This does not affect the stylesheet immediately, but will take effect the next time the stylesheet is saved.
        /// See also: /api/delete_sr_img, /api/delete_sr_header, /api/delete_sr_icon, and /api/delete_sr_banner.
        /// </summary>
        /// <param name="subredditsUploadSrImgInput">A valid SubredditsUploadSrImgInput instance</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public ImageUploadResult UploadSrImg(SubredditsUploadSrImgInput subredditsUploadSrImgInput, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<ImageUploadResult>(ExecuteRequest(PrepareUploadSrImg(subredditsUploadSrImgInput, subreddit)));
        }

        /// <summary>
        /// Add or replace a subreddit image, custom header logo, custom mobile icon, or custom mobile banner asynchronously.
        /// If the upload_type value is img, an image for use in the subreddit stylesheet is uploaded with the name specified in name.
        /// If the upload_type value is header then the image uploaded will be the subreddit's new logo and name will be ignored.
        /// If the upload_type value is icon then the image uploaded will be the subreddit's new mobile icon and name will be ignored.
        /// If the upload_type value is banner then the image uploaded will be the subreddit's new mobile banner and name will be ignored.
        /// For backwards compatibility, if upload_type is not specified, the header field will be used instead:
        /// If the header field has value 0, then upload_type is img.
        /// If the header field has value 1, then upload_type is header.
        /// The img_type field specifies whether to store the uploaded image as a PNG or JPEG.
        /// Subreddits have a limited number of images that can be in use at any given time. If no image with the specified name already exists, one of the slots will be consumed
        /// If an image with the specified name already exists, it will be replaced. This does not affect the stylesheet immediately, but will take effect the next time the stylesheet is saved.
        /// See also: /api/delete_sr_img, /api/delete_sr_header, /api/delete_sr_icon, and /api/delete_sr_banner.
        /// </summary>
        /// <param name="subredditsUploadSrImgInput">A valid SubredditsUploadSrImgInput instance</param>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>An object containing the resulting image URL and any errors.</returns>
        public async Task<ImageUploadResult> UploadSrImgAsync(SubredditsUploadSrImgInput subredditsUploadSrImgInput, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<ImageUploadResult>(await ExecuteRequestAsync(PrepareUploadSrImg(subredditsUploadSrImgInput, subreddit)));
        }

        private RestRequest PrepareUploadSrImg(SubredditsUploadSrImgInput subredditsUploadSrImgInput, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/upload_sr_img", Method.POST, "multipart/form-data");

            restRequest.AddFileBytes("file", subredditsUploadSrImgInput.file,
                (!string.IsNullOrWhiteSpace(subredditsUploadSrImgInput.name) ? subredditsUploadSrImgInput.name : "image") + "." + subredditsUploadSrImgInput.img_type);
            restRequest.AddObject(subredditsUploadSrImgInput);

            return restRequest;
        }

        // TODO - API returns 400 (after/before == "", q == "KrisCraig" or "t2_6vsit", sort == "relevance").  No idea why.  --Kris
        /// <summary>
        /// Search user profiles by title and description.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="subredditsSearchProfilesInput">A valid SubredditsSearchProfilesInput instance</param>
        /// <returns>(TODO - Untested)</returns>
        public object SearchProfiles(SubredditsSearchProfilesInput subredditsSearchProfilesInput)
        {
            return SendRequest<object>("profiles/search", subredditsSearchProfilesInput);
        }

        /// <summary>
        /// Get the current settings of a subreddit.
        /// This returns the current settings of the subreddit as used by /api/site_admin.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <param name="subredditsEditInput">A valid SubredditsEditInput instance</param>
        /// <returns>Settings for the requested subreddit.</returns>
        public SubredditSettingsContainer Edit(string subreddit, SubredditsEditInput subredditsEditInput)
        {
            return SendRequest<SubredditSettingsContainer>("r/" + subreddit + "/about/edit", subredditsEditInput);
        }

        /// <summary>
        /// Get the rules for the current subreddit.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>Subreddit rules.</returns>
        public RulesContainer Rules(string subreddit)
        {
            return JsonConvert.DeserializeObject<RulesContainer>(ExecuteRequest("r/" + subreddit + "/about/rules"));
        }

        /// <summary>
        /// Get subreddit traffic.
        /// </summary>
        /// <param name="subreddit">The subreddit being queried</param>
        /// <returns>Subreddit traffic data.</returns>
        public Traffic Traffic(string subreddit)
        {
            return JsonConvert.DeserializeObject<Traffic>(ExecuteRequest("r/" + subreddit + "/about/traffic"));
        }

        /*
         * Note - The API docs show the wrong URL for this endpoint.
         * According to multiple sources, this endpoint is incompatible with OAuth.  It just returns empty JSON.
         * 
         * --Kris
         */
        public object Sidebar(string subreddit = null)
        {
            throw new NotImplementedException("This endpoint does not work correctly with OAuth.");
            //return JsonConvert.DeserializeObject(ExecuteRequest(Sr(subreddit) + "about/sidebar"));
        }

        /* Note - The API docs show the wrong URL for this endpoint (I think).
         * TODO - This endpoint returns 403, with the content saying, "Request forbidden by administrative rules."
         * The response object does contain the URL of the stickied post, though, interestingly enough.
         * 
         * --Kris
         */
        public object Sticky(string subreddit = null, int num = 1)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "about/sticky");

            restRequest.AddParameter("num", num);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get subreddits the user has a relationship with.
        /// The where parameter chooses which subreddits are returned as follows:
        /// subscriber - subreddits the user is subscribed to
        /// contributor - subreddits the user is an approved submitter in
        /// moderator - subreddits the user is a moderator of
        /// streams - subscribed to subreddits that contain hosted video links
        /// See also: /api/subscribe, /api/friend, and /api/accept_moderator_invite.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (subscriber, contributor, moderator, streams)</param>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <returns>List of subreddit objects.</returns>
        public SubredditContainer Mine(string where, CategorizedSrListingInput categorizedSrListingInput)
        {
            return SendRequest<SubredditContainer>("subreddits/mine/" + where, categorizedSrListingInput);
        }

        /// <summary>
        /// Search subreddits by title and description.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="subredditsSearchInput">A valid SubredditsSearchInput instance</param>
        /// <returns>List of subreddit objects.</returns>
        public SubredditContainer Search(SubredditsSearchInput subredditsSearchInput)
        {
            return SendRequest<SubredditContainer>("subreddits/search", subredditsSearchInput);
        }

        /// <summary>
        /// Search subreddits by title and description.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="subredditsSearchInput">A valid SubredditsSearchInput instance</param>
        /// <returns>Results of the specified type.</returns>
        public T Search<T>(SubredditsSearchInput subredditsSearchInput)
        {
            return SendRequest<T>("subreddits/search", subredditsSearchInput);
        }

        /// <summary>
        /// Get all subreddits.
        /// The where parameter chooses the order in which the subreddits are displayed.
        /// popular sorts on the activity of the subreddit and the position of the subreddits can shift around.
        /// new sorts the subreddits based on their creation date, newest first.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (popular, new, gold, default)</param>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <returns>List of subreddit objects.</returns>
        public SubredditContainer Get(string where, CategorizedSrListingInput categorizedSrListingInput)
        {
            return SendRequest<SubredditContainer>("subreddits/" + where, categorizedSrListingInput);
        }

        /// <summary>
        /// Get all user subreddits.
        /// The where parameter chooses the order in which the subreddits are displayed.
        /// popular sorts on the activity of the subreddit and the position of the subreddits can shift around.
        /// new sorts the user subreddits based on their creation date, newest first.
        /// This endpoint is a listing.
        /// </summary>
        /// <param name="where">One of (popular, new)</param>
        /// <param name="categorizedSrListingInput">A valid CategorizedSrListingInput instance</param>
        /// <returns>List of subreddit objects.</returns>
        public SubredditContainer GetUserSubreddits(string where, CategorizedSrListingInput categorizedSrListingInput)
        {
            return SendRequest<SubredditContainer>("users/" + where, categorizedSrListingInput);
        }
    }
}
