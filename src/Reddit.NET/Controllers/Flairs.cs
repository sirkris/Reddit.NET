using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    /// <summary>
    /// Controller class for flairs.
    /// </summary>
    public class Flairs : BaseController
    {
        internal override ref Models.Internal.Monitor MonitorModel => ref MonitorNull;
        internal override ref MonitoringSnapshot Monitoring => ref MonitoringSnapshotNull;

        /// <summary>
        /// List of flairs.
        /// </summary>
        public List<RedditThings.FlairListResult> FlairList
        {
            get
            {
                return (FlairListLastUpdated.HasValue
                    && FlairListLastUpdated.Value.AddHours(1) > DateTime.Now ? flairList : GetFlairList());
            }
            set
            {
                flairList = value;
            }
        }
        private List<RedditThings.FlairListResult> flairList;
        private DateTime? FlairListLastUpdated;

        /// <summary>
        /// List of link flairs.
        /// </summary>
        public List<RedditThings.Flair> LinkFlair
        {
            get
            {
                return (LinkFlairLastUpdated.HasValue
                    && LinkFlairLastUpdated.Value.AddHours(1) > DateTime.Now ? linkFlair : GetLinkFlair());
            }
            set
            {
                linkFlair = value;
            }
        }
        private List<RedditThings.Flair> linkFlair;
        private DateTime? LinkFlairLastUpdated;

        /// <summary>
        /// List of link flairs.
        /// </summary>
        public List<RedditThings.FlairV2> LinkFlairV2
        {
            get
            {
                return (LinkFlairLastUpdatedV2.HasValue
                    && LinkFlairLastUpdatedV2.Value.AddHours(1) > DateTime.Now ? linkFlairV2 : GetLinkFlairV2());
            }
            set
            {
                linkFlairV2 = value;
            }
        }
        private List<RedditThings.FlairV2> linkFlairV2;
        private DateTime? LinkFlairLastUpdatedV2;

        /// <summary>
        /// List of user flairs.
        /// </summary>
        public List<RedditThings.Flair> UserFlair
        {
            get
            {
                return (UserFlairLastUpdated.HasValue
                    && UserFlairLastUpdated.Value.AddHours(1) > DateTime.Now ? userFlair : GetUserFlair());
            }
            set
            {
                userFlair = value;
            }
        }
        private List<RedditThings.Flair> userFlair;
        private DateTime? UserFlairLastUpdated;

        /// <summary>
        /// List of user flairs.
        /// </summary>
        public List<RedditThings.FlairV2> UserFlairV2
        {
            get
            {
                return (UserFlairLastUpdatedV2.HasValue
                    && UserFlairLastUpdatedV2.Value.AddHours(1) > DateTime.Now ? userFlairV2 : GetUserFlairV2());
            }
            set
            {
                userFlairV2 = value;
            }
        }
        private List<RedditThings.FlairV2> userFlairV2;
        private DateTime? UserFlairLastUpdatedV2;
        
        private readonly Subreddit Subreddit;
        private readonly Dispatch Dispatch;

        /// <summary>
        /// Create a new instance of the flairs controller.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit with the flairs</param>
        /// <param name="dispatch"></param>
        public Flairs(Subreddit subreddit, ref Dispatch dispatch) : base()
        {
            Subreddit = subreddit;
            Dispatch = dispatch;
        }

        /// <summary>
        /// Clear link flair templates.
        /// </summary>
        public void ClearLinkFlairTemplates()
        {
            Validate(Dispatch.Flair.ClearFlairTemplates("LINK_FLAIR", Subreddit.Name));
        }

        /// <summary>
        /// Clear link flair templates asynchronously.
        /// </summary>
        public async Task ClearLinkFlairTemplatesAsync()
        {
            await Task.Run(() =>
            {
                ClearLinkFlairTemplates();
            });
        }

        /// <summary>
        /// Clear user flair templates.
        /// </summary>
        public void ClearUserFlairTemplates()
        {
            Validate(Dispatch.Flair.ClearFlairTemplates("USER_FLAIR", Subreddit.Name));
        }

        /// <summary>
        /// Clear user flair templates asynchronously.
        /// </summary>
        public async Task ClearUserFlairTemplatesAsync()
        {
            await Task.Run(() =>
            {
                ClearUserFlairTemplates();
            });
        }

        /// <summary>
        /// Delete flair.
        /// </summary>
        /// <param name="username">The user whose flair we're removing</param>
        public void DeleteFlair(string username)
        {
            Validate(Dispatch.Flair.DeleteFlair(username, Subreddit.Name));
        }

        /// <summary>
        /// Delete flair asynchronously.
        /// </summary>
        /// <param name="username">The user whose flair we're removing</param>
        public async Task DeleteFlairAsync(string username)
        {
            await Task.Run(() =>
            {
                DeleteFlair(username);
            });
        }

        /// <summary>
        /// Delete flair template.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being deleted (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        public void DeleteFlairTemplate(string flairTemplateId)
        {
            Validate(Dispatch.Flair.DeleteFlairTemplate(flairTemplateId, Subreddit.Name));
        }

        /// <summary>
        /// Delete flair template asynchronously.
        /// <param name="flairTemplateId">The ID of the flair template being deleted (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// </summary>
        public async Task DeleteFlairTemplateAsync(string flairTemplateId)
        {
            await Task.Run(() =>
            {
                DeleteFlairTemplate(flairTemplateId);
            });
        }

        /// <summary>
        /// Create a new user flair.
        /// </summary>
        /// <param name="username">The user who's getting the new flair</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateFlair(string username, string text, string cssClass = "")
        {
            Validate(Dispatch.Flair.Create(cssClass, "", username, text, Subreddit.Name));
        }

        /// <summary>
        /// Create a new user flair asynchronously.
        /// </summary>
        /// <param name="username">The user who's getting the new flair</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateFlairAsync(string username, string text, string cssClass = "")
        {
            await Task.Run(() =>
            {
                CreateFlair(username, text, cssClass);
            });
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
            Validate(Dispatch.Flair.FlairConfig(flairEnabled, flairPosition, flairSelfAssignEnabled, linkFlairPosition, linkFlairSelfAssignEnabled, Subreddit.Name));
        }

        /// <summary>
        /// Update the flair configuration settings for this subreddit asynchronously.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        /// <param name="flairPosition">one of (left, right)</param>
        /// <param name="flairSelfAssignEnabled">boolean value</param>
        /// <param name="linkFlairPosition">one of (left, right)</param>
        /// <param name="linkFlairSelfAssignEnabled">boolean value</param>
        public async Task FlairConfigAsync(bool flairEnabled, string flairPosition, bool flairSelfAssignEnabled, string linkFlairPosition, bool linkFlairSelfAssignEnabled)
        {
            await Task.Run(() =>
            {
                FlairConfig(flairEnabled, flairPosition, flairSelfAssignEnabled, linkFlairPosition, linkFlairSelfAssignEnabled);
            });
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
            return Validate(Dispatch.Flair.FlairCSV(flairCsv, Subreddit.Name));
        }

        /// <summary>
        /// Asynchronously change the flair of multiple users in the same subreddit with a single API call.
        /// Requires a string 'flair_csv' which has up to 100 lines of the form 'user,flairtext,cssclass' (Lines beyond the 100th are ignored).
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">comma-seperated flair information</param>
        public async Task FlairCSVAsync(string flairCsv)
        {
            await Task.Run(() =>
            {
                FlairCSV(flairCsv);
            });
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
        /// Asynchronously change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A valid FlairListResultContainer object</param>
        public async Task FlairCSVAsync(RedditThings.FlairListResultContainer flairCsv)
        {
            await Task.Run(() =>
            {
                FlairCSV(flairCsv);
            });
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
        /// Asynchronously change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A list of valid FlairListResult objects</param>
        public async Task FlairCSVAsync(List<RedditThings.FlairListResult> flairCsv)
        {
            await Task.Run(() =>
            {
                FlairCSV(flairCsv);
            });
        }

        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="username">a user by name</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 1000)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <returns>Flair list results.</returns>
        public List<RedditThings.FlairListResult> GetFlairList(string username = "", int limit = 25, string after = "", string before = "", int count = 0,
            string show = "all", bool srDetail = false)
        {
            FlairList = Validate(Dispatch.Flair.FlairList(after, before, username, Subreddit.Name, count, limit, show, srDetail)).Users;
            FlairListLastUpdated = DateTime.Now;
            return FlairList;
        }

        /// <summary>
        /// Return information about a users's flair options.
        /// </summary>
        /// <param name="username">A valid Reddit username</param>
        /// <returns>Flair results.</returns>
        public RedditThings.FlairSelectorResultContainer FlairSelector(string username)
        {
            return Validate(Dispatch.Flair.FlairSelector(username, Subreddit.Name));
        }

        /// <summary>
        /// Create a new link flair template.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateLinkFlairTemplate(string text, bool textEditable = false, string cssClass = "")
        {
            Validate(Dispatch.Flair.FlairTemplate(cssClass, "", "LINK_FLAIR", text, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Create a new link flair template asynchronously.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateLinkFlairTemplateAsync(string text, bool textEditable = false, string cssClass = "")
        {
            await Task.Run(() =>
            {
                CreateLinkFlairTemplate(text, textEditable, cssClass);
            });
        }

        /// <summary>
        /// Create a new user flair template.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateUserFlairTemplate(string text, bool textEditable = false, string cssClass = "")
        {
            Validate(Dispatch.Flair.FlairTemplate(cssClass, "", "USER_FLAIR", text, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Create a new user flair template asynchronously.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateUserFlairTemplateAsync(string text, bool textEditable = false, string cssClass = "")
        {
            await Task.Run(() =>
            {
                CreateUserFlairTemplate(text, textEditable, cssClass);
            });
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
            Validate(Dispatch.Flair.FlairTemplate(cssClass, flairTemplateId, "LINK_FLAIR", text, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Update an existing link flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task UpdateLinkFlairTemplateAsync(string flairTemplateId, string text, bool textEditable = false, string cssClass = "")
        {
            await Task.Run(() =>
            {
                UpdateLinkFlairTemplate(flairTemplateId, text, textEditable, cssClass);
            });
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
            Validate(Dispatch.Flair.FlairTemplate(cssClass, flairTemplateId, "USER_FLAIR", text, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Update an existing user flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task UpdateUserFlairTemplateAsync(string flairTemplateId, string text, bool textEditable = false, string cssClass = "")
        {
            await Task.Run(() =>
            {
                UpdateUserFlairTemplate(flairTemplateId, text, textEditable, cssClass);
            });
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
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, "", "LINK_FLAIR", modOnly, text, textColor, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Create a new link flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        public async Task CreateLinkFlairTemplateV2Async(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            await Task.Run(() =>
            {
                CreateLinkFlairTemplateV2(text, textEditable, textColor, backgroundColor, modOnly);
            });
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
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, "", "USER_FLAIR", modOnly, text, textColor, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Create a new user flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        public async Task CreateUserFlairTemplateV2Async(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            await Task.Run(() =>
            {
                CreateUserFlairTemplateV2(text, textEditable, textColor, backgroundColor, modOnly);
            });
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
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, flairTemplateId, "LINK_FLAIR", modOnly, text, textColor, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Update an existing link flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        public async Task UpdateLinkFlairTemplateV2Async(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            await Task.Run(() =>
            {
                UpdateLinkFlairTemplateV2(flairTemplateId, text, textEditable, textColor, backgroundColor, modOnly);
            });
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
            return Validate(Dispatch.Flair.FlairTemplateV2(backgroundColor, flairTemplateId, "USER_FLAIR", modOnly, text, textColor, textEditable, Subreddit.Name));
        }

        /// <summary>
        /// Update an existing user flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being updated (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="textColor">one of (light, dark)</param>
        /// <param name="backgroundColor">a 6-digit rgb hex color, e.g. #AABBCC</param>
        /// <param name="modOnly">boolean value</param>
        public async Task UpdateUserFlairTemplateV2Async(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            await Task.Run(() =>
            {
                UpdateUserFlairTemplateV2(flairTemplateId, text, textEditable, textColor, backgroundColor, modOnly);
            });
        }

        /// <summary>
        /// Set flair enabled.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        public void SetFlairEnabled(bool flairEnabled = true)
        {
            Validate(Dispatch.Flair.SetFlairEnabled(flairEnabled, Subreddit.Name));
        }

        /// <summary>
        /// Set flair enabled asynchronously.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        public async Task SetFlairEnabledAsync(bool flairEnabled = true)
        {
            await Task.Run(() =>
            {
                SetFlairEnabled(flairEnabled);
            });
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available link flairs.</returns>
        public List<RedditThings.Flair> GetLinkFlair()
        {
            LinkFlair = Validate(Dispatch.Flair.LinkFlair(Subreddit.Name));
            LinkFlairLastUpdated = DateTime.Now;

            return LinkFlair;
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available link flairs.</returns>
        public List<RedditThings.FlairV2> GetLinkFlairV2()
        {
            LinkFlairV2 = Validate(Dispatch.Flair.LinkFlairV2(Subreddit.Name));
            LinkFlairLastUpdatedV2 = DateTime.Now;

            return LinkFlairV2;
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available user flairs.</returns>
        public List<RedditThings.Flair> GetUserFlair()
        {
            UserFlair = Validate(Dispatch.Flair.UserFlair(Subreddit.Name));
            UserFlairLastUpdated = DateTime.Now;

            return UserFlair;
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available user flairs.</returns>
        public List<RedditThings.FlairV2> GetUserFlairV2()
        {
            UserFlairV2 = Validate(Dispatch.Flair.UserFlairV2(Subreddit.Name));
            UserFlairLastUpdatedV2 = DateTime.Now;

            return UserFlairV2;
        }

    }
}
