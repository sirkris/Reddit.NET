using Reddit.Inputs.Flair;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for flairs.
    /// </summary>
    public class Flairs : BaseController
    {
        /// <summary>
        /// List of flairs.
        /// </summary>
        public List<FlairListResult> FlairList
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
        private List<FlairListResult> flairList;
        private DateTime? FlairListLastUpdated { get; set; }

        /// <summary>
        /// Fullname of the next page of flair list results.
        /// </summary>
        public string FlairListNext
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Fullname of the previous page of flair list results.
        /// </summary>
        public string FlairListPrev
        {
            get;
            private set;
        }

        /// <summary>
        /// List of link flairs.
        /// </summary>
        public List<Flair> LinkFlair
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
        private List<Flair> linkFlair;
        private DateTime? LinkFlairLastUpdated { get; set; }

        /// <summary>
        /// List of link flairs.
        /// </summary>
        public List<FlairV2> LinkFlairV2
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
        private List<FlairV2> linkFlairV2;
        private DateTime? LinkFlairLastUpdatedV2 { get; set; }

        /// <summary>
        /// List of user flairs.
        /// </summary>
        public List<Flair> UserFlair
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
        private List<Flair> userFlair;
        private DateTime? UserFlairLastUpdated { get; set; }

        /// <summary>
        /// List of user flairs.
        /// </summary>
        public List<FlairV2> UserFlairV2
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
        private List<FlairV2> userFlairV2;
        private DateTime? UserFlairLastUpdatedV2 { get; set; }

        private string Subreddit { get; set; }

        private readonly Dispatch Dispatch;

        /// <summary>
        /// Create a new instance of the flairs controller.
        /// </summary>
        /// <param name="subreddit">The name of the subreddit with the flairs</param>
        /// <param name="dispatch"></param>
        public Flairs(Dispatch dispatch, string subreddit) : base()
        {
            Dispatch = dispatch;
            Subreddit = subreddit;
        }

        /// <summary>
        /// Clear link flair templates.
        /// </summary>
        public void ClearLinkFlairTemplates()
        {
            Validate(Dispatch.Flair.ClearFlairTemplates("LINK_FLAIR", Subreddit));
        }

        /// <summary>
        /// Clear link flair templates asynchronously.
        /// </summary>
        public async Task ClearLinkFlairTemplatesAsync()
        {
            Validate(await Dispatch.Flair.ClearFlairTemplatesAsync("LINK_FLAIR", Subreddit));
        }

        /// <summary>
        /// Clear user flair templates.
        /// </summary>
        public void ClearUserFlairTemplates()
        {
            Validate(Dispatch.Flair.ClearFlairTemplates("USER_FLAIR", Subreddit));
        }

        /// <summary>
        /// Clear user flair templates asynchronously.
        /// </summary>
        public async Task ClearUserFlairTemplatesAsync()
        {
            Validate(await Dispatch.Flair.ClearFlairTemplatesAsync("USER_FLAIR", Subreddit));
        }

        /// <summary>
        /// Delete flair.
        /// </summary>
        /// <param name="username">The user whose flair we're removing</param>
        public void DeleteFlair(string username)
        {
            Validate(Dispatch.Flair.DeleteFlair(username, Subreddit));
        }

        /// <summary>
        /// Delete flair asynchronously.
        /// </summary>
        /// <param name="username">The user whose flair we're removing</param>
        public async Task DeleteFlairAsync(string username)
        {
            Validate(await Dispatch.Flair.DeleteFlairAsync(username, Subreddit));
        }

        /// <summary>
        /// Delete flair template.
        /// </summary>
        /// <param name="flairTemplateId">The ID of the flair template being deleted (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        public void DeleteFlairTemplate(string flairTemplateId)
        {
            Validate(Dispatch.Flair.DeleteFlairTemplate(flairTemplateId, Subreddit));
        }

        /// <summary>
        /// Delete flair template asynchronously.
        /// <param name="flairTemplateId">The ID of the flair template being deleted (e.g. "0778d5ec-db43-11e8-9258-0e3a02270976")</param>
        /// </summary>
        public async Task DeleteFlairTemplateAsync(string flairTemplateId)
        {
            Validate(await Dispatch.Flair.DeleteFlairTemplateAsync(flairTemplateId, Subreddit));
        }

        /// <summary>
        /// Create a new link flair.
        /// </summary>
        /// <param name="flairSelectFlairInput">a valid FlairSelectFlairInput instance</param>
        public void CreateLinkFlair(FlairSelectFlairInput flairSelectFlairInput)
        {
            Validate(Dispatch.Flair.SelectFlair(flairSelectFlairInput, Subreddit));
        }

        /// <summary>
        /// Create a new link flair asynchronously.
        /// </summary>
        /// <param name="flairSelectFlairInput">a valid FlairSelectFlairInput instance</param>
        public async Task CreateLinkFlairAsync(FlairSelectFlairInput flairSelectFlairInput)
        {
            Validate(await Dispatch.Flair.SelectFlairAsync(flairSelectFlairInput, Subreddit));
        }

        /// <summary>
        /// Create a new user flair.
        /// </summary>
        /// <param name="username">The user who's getting the new flair</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateUserFlair(string username, string text, string cssClass = "")
        {
            CreateUserFlair(new FlairCreateInput(text, "", username, cssClass));
        }

        /// <summary>
        /// Create a new user flair asynchronously.
        /// </summary>
        /// <param name="username">The user who's getting the new flair</param>
        /// <param name="text">The flair text</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateUserFlairAsync(string username, string text, string cssClass = "")
        {
            await CreateUserFlairAsync(new FlairCreateInput(text, "", username, cssClass));
        }

        /// <summary>
        /// Create a new user flair.
        /// </summary>
        /// <param name="flairCreateInput">A valid FlairCreateInput instance</param>
        public void CreateUserFlair(FlairCreateInput flairCreateInput)
        {
            Validate(Dispatch.Flair.Create(flairCreateInput, Subreddit));
        }

        /// <summary>
        /// Create a new user flair asynchronously.
        /// </summary>
        /// <param name="flairCreateInput">A valid FlairCreateInput instance</param>
        public async Task CreateUserFlairAsync(FlairCreateInput flairCreateInput)
        {
            Validate(await Dispatch.Flair.CreateAsync(flairCreateInput, Subreddit));
        }

        /// <summary>
        /// Update the flair configuration settings for this subreddit.
        /// </summary>
        /// <param name="flairConfigInput">A valid FlairConfigInput instance</param>
        public void FlairConfig(FlairConfigInput flairConfigInput)
        {
            Validate(Dispatch.Flair.FlairConfig(flairConfigInput, Subreddit));
        }

        /// <summary>
        /// Update the flair configuration settings for this subreddit asynchronously.
        /// </summary>
        /// <param name="flairConfigInput">A valid FlairConfigInput instance</param>
        public async Task FlairConfigAsync(FlairConfigInput flairConfigInput)
        {
            Validate(await Dispatch.Flair.FlairConfigAsync(flairConfigInput, Subreddit));
        }

        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// Requires a string 'flair_csv' which has up to 100 lines of the form 'user,flairtext,cssclass' (Lines beyond the 100th are ignored).
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">comma-seperated flair information</param>
        /// <returns>Action results.</returns>
        public List<ActionResult> FlairCSV(string flairCsv)
        {
            return Validate(Dispatch.Flair.FlairCSV(flairCsv, Subreddit));
        }

        /// <summary>
        /// Asynchronously change the flair of multiple users in the same subreddit with a single API call.
        /// Requires a string 'flair_csv' which has up to 100 lines of the form 'user,flairtext,cssclass' (Lines beyond the 100th are ignored).
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">comma-seperated flair information</param>
        public async Task<List<ActionResult>> FlairCSVAsync(string flairCsv)
        {
            return Validate(await Dispatch.Flair.FlairCSVAsync(flairCsv, Subreddit));
        }

        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A valid FlairListResultContainer object</param>
        /// <returns>Action results.</returns>
        public List<ActionResult> FlairCSV(FlairListResultContainer flairCsv)
        {
            return FlairCSV(flairCsv.Users);
        }

        /// <summary>
        /// Asynchronously change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A valid FlairListResultContainer object</param>
        public async Task<List<ActionResult>> FlairCSVAsync(FlairListResultContainer flairCsv)
        {
            return await FlairCSVAsync(flairCsv.Users);
        }

        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A list of valid FlairListResult objects</param>
        /// <returns>Action results.</returns>
        public List<ActionResult> FlairCSV(List<FlairListResult> flairCsv)
        {
            return FlairCSV(PrepareFlairCSV(flairCsv));
        }

        /// <summary>
        /// Asynchronously change the flair of multiple users in the same subreddit with a single API call.
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">A list of valid FlairListResult objects</param>
        public async Task<List<ActionResult>> FlairCSVAsync(List<FlairListResult> flairCsv)
        {
            return await FlairCSVAsync(PrepareFlairCSV(flairCsv));
        }

        private string PrepareFlairCSV(List<FlairListResult> flairCsv)
        {
            string arg = "";
            foreach (FlairListResult flairListResult in flairCsv)
            {
                arg += flairListResult.ToCSV();
            }

            return arg;
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
        public List<FlairListResult> GetFlairList(string username = "", int limit = 25, string after = "", string before = "", int count = 0,
            string show = "all", bool srDetail = false)
        {
            FlairList = GetFlairList(new FlairNameListingInput(username, after, before, limit, count, show, srDetail));
            return FlairList;
        }

        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="flairNameListingInput">A valid FlairNameListingInput instance</param>
        /// <returns>Flair list results.</returns>
        public List<FlairListResult> GetFlairList(FlairNameListingInput flairNameListingInput)
        {
            FlairListResultContainer res = Validate(Dispatch.Flair.FlairList(flairNameListingInput, Subreddit));

            FlairListNext = res.Next;
            FlairListPrev = res.Prev;

            FlairList = res.Users;
            FlairListLastUpdated = DateTime.Now;

            return FlairList;
        }

        /// <summary>
        /// Return information about a user's or link's flair options.
        /// </summary>
        /// <param name="username">A valid Reddit username</param>
        /// <returns>Flair results.</returns>
        public FlairSelectorResultContainer FlairSelector(string username = null, string link = null)
        {
            return Validate(Dispatch.Flair.FlairSelector(new FlairLinkInput(link: link, name: username), Subreddit));
        }

        /// <summary>
        /// Create a new link flair template.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateLinkFlairTemplate(string text, bool textEditable = false, string cssClass = "")
        {
            CreateLinkFlairTemplate(new FlairTemplateInput(text, "LINK_FLAIR", textEditable, cssClass));
        }

        /// <summary>
        /// Create a new link flair template asynchronously.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateLinkFlairTemplateAsync(string text, bool textEditable = false, string cssClass = "")
        {
            await CreateLinkFlairTemplateAsync(new FlairTemplateInput(text, "LINK_FLAIR", textEditable, cssClass));
        }

        /// <summary>
        /// Create a new link flair template.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public void CreateLinkFlairTemplate(FlairTemplateInput flairTemplateInput)
        {
            Validate(Dispatch.Flair.FlairTemplate(flairTemplateInput, Subreddit));
        }

        /// <summary>
        /// Create a new link flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public async Task CreateLinkFlairTemplateAsync(FlairTemplateInput flairTemplateInput)
        {
            Validate(await Dispatch.Flair.FlairTemplateAsync(flairTemplateInput, Subreddit));
        }

        /// <summary>
        /// Create a new user flair template.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public void CreateUserFlairTemplate(string text, bool textEditable = false, string cssClass = "")
        {
            CreateUserFlairTemplate(new FlairTemplateInput(text, "USER_FLAIR", textEditable, cssClass));
        }

        /// <summary>
        /// Create a new user flair template asynchronously.
        /// </summary>
        /// <param name="text">a string no longer than 64 characters</param>
        /// <param name="textEditable">boolean value</param>
        /// <param name="cssClass">a valid subreddit image name</param>
        public async Task CreateUserFlairTemplateAsync(string text, bool textEditable = false, string cssClass = "")
        {
            await CreateUserFlairTemplateAsync(new FlairTemplateInput(text, "USER_FLAIR", textEditable, cssClass));
        }

        /// <summary>
        /// Create a new user flair template.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public void CreateUserFlairTemplate(FlairTemplateInput flairTemplateInput)
        {
            Validate(Dispatch.Flair.FlairTemplate(flairTemplateInput, Subreddit));
        }

        /// <summary>
        /// Create a new user flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public async Task CreateUserFlairTemplateAsync(FlairTemplateInput flairTemplateInput)
        {
            Validate(await Dispatch.Flair.FlairTemplateAsync(flairTemplateInput, Subreddit));
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
            UpdateLinkFlairTemplate(new FlairTemplateInput(text, "LINK_FLAIR", textEditable, cssClass, flairTemplateId));
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
            await UpdateLinkFlairTemplateAsync(new FlairTemplateInput(text, "LINK_FLAIR", textEditable, cssClass, flairTemplateId));
        }

        /// <summary>
        /// Update an existing link flair template.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public void UpdateLinkFlairTemplate(FlairTemplateInput flairTemplateInput)
        {
            Validate(Dispatch.Flair.FlairTemplate(flairTemplateInput, Subreddit));
        }

        /// <summary>
        /// Update an existing link flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public async Task UpdateLinkFlairTemplateAsync(FlairTemplateInput flairTemplateInput)
        {
            Validate(await Dispatch.Flair.FlairTemplateAsync(flairTemplateInput, Subreddit));
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
            UpdateUserFlairTemplate(new FlairTemplateInput(text, "USER_FLAIR", textEditable, cssClass, flairTemplateId));
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
            await UpdateUserFlairTemplateAsync(new FlairTemplateInput(text, "USER_FLAIR", textEditable, cssClass, flairTemplateId));
        }

        /// <summary>
        /// Update an existing user flair template.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public void UpdateUserFlairTemplate(FlairTemplateInput flairTemplateInput)
        {
            Validate(Dispatch.Flair.FlairTemplate(flairTemplateInput, Subreddit));
        }

        /// <summary>
        /// Update an existing user flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateInput">A valid FlairTemplateInput instance</param>
        public async Task UpdateUserFlairTemplateAsync(FlairTemplateInput flairTemplateInput)
        {
            Validate(await Dispatch.Flair.FlairTemplateAsync(flairTemplateInput, Subreddit));
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
        public FlairV2 CreateLinkFlairTemplateV2(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            return CreateLinkFlairTemplateV2(new FlairTemplateV2Input(text, "LINK_FLAIR", textEditable, textColor, backgroundColor, "", modOnly));
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
        public async Task<FlairV2> CreateLinkFlairTemplateV2Async(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            return await CreateLinkFlairTemplateV2Async(new FlairTemplateV2Input(text, "LINK_FLAIR", textEditable, textColor, backgroundColor, "", modOnly));
        }

        /// <summary>
        /// Create a new link flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        /// <returns>The created flair object.</returns>
        public FlairV2 CreateLinkFlairTemplateV2(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(flairTemplateV2Input, Subreddit));
        }

        /// <summary>
        /// Create a new link flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        public async Task<FlairV2> CreateLinkFlairTemplateV2Async(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(await Dispatch.Flair.FlairTemplateV2Async(flairTemplateV2Input, Subreddit));
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
        public FlairV2 CreateUserFlairTemplateV2(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            return CreateUserFlairTemplateV2(new FlairTemplateV2Input(text, "USER_FLAIR", textEditable, textColor, backgroundColor, "", modOnly));
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
        public async Task<FlairV2> CreateUserFlairTemplateV2Async(string text, bool textEditable = false, string textColor = "dark",
            string backgroundColor = "#EEEEFF", bool modOnly = false)
        {
            return await CreateUserFlairTemplateV2Async(new FlairTemplateV2Input(text, "USER_FLAIR", textEditable, textColor, backgroundColor, "", modOnly));
        }

        /// <summary>
        /// Create a new user flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        /// <returns>The created flair object.</returns>
        public FlairV2 CreateUserFlairTemplateV2(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(flairTemplateV2Input, Subreddit));
        }

        /// <summary>
        /// Create a new user flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        public async Task<FlairV2> CreateUserFlairTemplateV2Async(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(await Dispatch.Flair.FlairTemplateV2Async(flairTemplateV2Input, Subreddit));
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
        public FlairV2 UpdateLinkFlairTemplateV2(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            return UpdateLinkFlairTemplateV2(new FlairTemplateV2Input(text, "LINK_FLAIR", textEditable, textColor, backgroundColor, flairTemplateId, modOnly));
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
        public async Task<FlairV2> UpdateLinkFlairTemplateV2Async(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            return await UpdateLinkFlairTemplateV2Async(new FlairTemplateV2Input(text, "LINK_FLAIR", textEditable, textColor, backgroundColor, flairTemplateId, modOnly));
        }

        /// <summary>
        /// Update an existing link flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        /// <returns>The updated flair object.</returns>
        public FlairV2 UpdateLinkFlairTemplateV2(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(flairTemplateV2Input, Subreddit));
        }

        /// <summary>
        /// Update an existing link flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        public async Task<FlairV2> UpdateLinkFlairTemplateV2Async(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(await Dispatch.Flair.FlairTemplateV2Async(flairTemplateV2Input, Subreddit));
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
        public FlairV2 UpdateUserFlairTemplateV2(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            return UpdateUserFlairTemplateV2(new FlairTemplateV2Input(text, "USER_FLAIR", textEditable, textColor, backgroundColor, flairTemplateId, modOnly));
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
        public async Task<FlairV2> UpdateUserFlairTemplateV2Async(string flairTemplateId, string text = null, bool? textEditable = null, string textColor = null,
            string backgroundColor = null, bool? modOnly = null)
        {
            return await UpdateUserFlairTemplateV2Async(new FlairTemplateV2Input(text, "USER_FLAIR", textEditable, textColor, backgroundColor, flairTemplateId, modOnly));
        }

        /// <summary>
        /// Update an existing user flair template.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        /// <returns>The updated flair object.</returns>
        public FlairV2 UpdateUserFlairTemplateV2(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(Dispatch.Flair.FlairTemplateV2(flairTemplateV2Input, Subreddit));
        }

        /// <summary>
        /// Update an existing user flair template asynchronously.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">A valid FlairTemplateV2Input instance</param>
        public async Task<FlairV2> UpdateUserFlairTemplateV2Async(FlairTemplateV2Input flairTemplateV2Input)
        {
            return Validate(await Dispatch.Flair.FlairTemplateV2Async(flairTemplateV2Input, Subreddit));
        }

        /// <summary>
        /// Set flair enabled.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        public void SetFlairEnabled(bool flairEnabled = true)
        {
            Validate(Dispatch.Flair.SetFlairEnabled(flairEnabled, Subreddit));
        }

        /// <summary>
        /// Set flair enabled asynchronously.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        public async Task SetFlairEnabledAsync(bool flairEnabled = true)
        {
            Validate(await Dispatch.Flair.SetFlairEnabledAsync(flairEnabled, Subreddit));
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available link flairs.</returns>
        public List<Flair> GetLinkFlair()
        {
            LinkFlair = Validate(Dispatch.Flair.LinkFlair(Subreddit));
            LinkFlairLastUpdated = DateTime.Now;

            return LinkFlair;
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available link flairs.</returns>
        public List<FlairV2> GetLinkFlairV2()
        {
            LinkFlairV2 = Validate(Dispatch.Flair.LinkFlairV2(Subreddit));
            LinkFlairLastUpdatedV2 = DateTime.Now;

            return LinkFlairV2;
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available user flairs.</returns>
        public List<Flair> GetUserFlair()
        {
            UserFlair = Validate(Dispatch.Flair.UserFlair(Subreddit));
            UserFlairLastUpdated = DateTime.Now;

            return UserFlair;
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <returns>List of available user flairs.</returns>
        public List<FlairV2> GetUserFlairV2()
        {
            UserFlairV2 = Validate(Dispatch.Flair.UserFlairV2(Subreddit));
            UserFlairLastUpdatedV2 = DateTime.Now;

            return UserFlairV2;
        }
    }
}
