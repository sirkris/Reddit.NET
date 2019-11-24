using Newtonsoft.Json;
using Reddit.Inputs.Flair;
using Reddit.Things;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Models
{
    public class Flair : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Flair(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// Clear flair templates.
        /// </summary>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer ClearFlairTemplates(string flairType, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(PrepareClearFlairTemplates(flairType, subreddit)));
        }

        /// <summary>
        /// Clear flair templates asynchronously.
        /// </summary>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> ClearFlairTemplatesAsync(string flairType, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(await ExecuteRequestAsync(PrepareClearFlairTemplates(flairType, subreddit)));
        }

        private RestRequest PrepareClearFlairTemplates(string flairType, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/clearflairtemplates", Method.POST);

            restRequest.AddParameter("flair_type", flairType);
            restRequest.AddParameter("api_type", "json");

            return restRequest;
        }

        /// <summary>
        /// Delete flair.
        /// </summary>
        /// <param name="name">a user by name</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer DeleteFlair(string name, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(PrepareDeleteFlair(name, subreddit)));
        }

        /// <summary>
        /// Delete flair asynchronously.
        /// </summary>
        /// <param name="name">a user by name</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> DeleteFlairAsync(string name, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(await ExecuteRequestAsync(PrepareDeleteFlair(name, subreddit)));
        }

        private RestRequest PrepareDeleteFlair(string name, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflair", Method.POST);

            restRequest.AddParameter("name", name);
            restRequest.AddParameter("api_type", "json");

            return restRequest;
        }

        /// <summary>
        /// Delete flair template.
        /// </summary>
        /// <param name="flairTemplateId"></param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer DeleteFlairTemplate(string flairTemplateId, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(PrepareDeleteFlairTemplate(flairTemplateId, subreddit)));
        }

        /// <summary>
        /// Delete flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateId"></param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> DeleteFlairTemplateAsync(string flairTemplateId, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(await ExecuteRequestAsync(PrepareDeleteFlairTemplate(flairTemplateId, subreddit)));
        }

        private RestRequest PrepareDeleteFlairTemplate(string flairTemplateId, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/deleteflairtemplate", Method.POST);

            restRequest.AddParameter("flair_template_id", flairTemplateId);
            restRequest.AddParameter("api_type", "json");

            return restRequest;
        }

        // In the controllers, link flair can be created from Post.  User flair can be created from Subreddit or User (no practical difference).  --Kris
        /// <summary>
        /// Create a new flair.
        /// </summary>
        /// <param name="flairCreateInput">a valid FlairCreateInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer Create(FlairCreateInput flairCreateInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/flair", flairCreateInput, Method.POST);
        }

        /// <summary>
        /// Create a new flair asynchronously.
        /// </summary>
        /// <param name="flairCreateInput">a valid FlairCreateInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> CreateAsync(FlairCreateInput flairCreateInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/flair", flairCreateInput, Method.POST);
        }

        // TODO - Docs neglect to mention exactly *how* to send the flair IDs.  All my guesses came up 400.  Skipped for now.  --Kris
        /// <summary>
        /// Update the order of flair templates in the specified subreddit.
        /// Order should contain every single flair id for that flair type; omitting any id will result in a loss of data.
        /// </summary>
        /// <param name="flairType">one of (USER_FLAIR, LINK_FLAIR)</param>
        /// <param name="subreddit">subreddit name</param>
        /// <returns>(TODO - Untested)</returns>
        public object FlairTemplateOrder(string flairType, List<Things.Flair> flairs, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flair_template_order", Method.PATCH);

            restRequest.AddParameter("flair_type", flairType);

            List<string> flairIds = new List<string>();
            foreach (Things.Flair flair in flairs)
            {
                flairIds.Add(flair.Id);
            }

            restRequest.AddBody(JsonConvert.SerializeObject(flairIds));  // ?

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Flair config.
        /// </summary>
        /// <param name="flairConfigInput">A valid FlairConfigInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer FlairConfig(FlairConfigInput flairConfigInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/flairconfig", flairConfigInput, Method.POST);
        }

        /// <summary>
        /// Asynchronous flair config.
        /// </summary>
        /// <param name="flairConfigInput">A valid FlairConfigInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> FlairConfigAsync(FlairConfigInput flairConfigInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/flairconfig", flairConfigInput, Method.POST);
        }

        /// <summary>
        /// Change the flair of multiple users in the same subreddit with a single API call.
        /// Requires a string 'flair_csv' which has up to 100 lines of the form 'user,flairtext,cssclass' (Lines beyond the 100th are ignored).
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">comma-seperated flair information</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>Action results.</returns>
        public List<ActionResult> FlairCSV(string flairCsv, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<List<ActionResult>>(ExecuteRequest(PrepareFlairCSV(flairCsv, subreddit)));
        }

        /// <summary>
        /// Asynchronously change the flair of multiple users in the same subreddit with a single API call.
        /// Requires a string 'flair_csv' which has up to 100 lines of the form 'user,flairtext,cssclass' (Lines beyond the 100th are ignored).
        /// If both cssclass and flairtext are the empty string for a given user, instead clears that user's flair.
        /// Returns an array of objects indicating if each flair setting was applied, or a reason for the failure.
        /// </summary>
        /// <param name="flairCsv">comma-seperated flair information</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>Action results.</returns>
        public async Task<List<ActionResult>> FlairCSVAsync(string flairCsv, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<List<ActionResult>>(await ExecuteRequestAsync(PrepareFlairCSV(flairCsv, subreddit)));
        }

        private RestRequest PrepareFlairCSV(string flairCsv, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/flaircsv", Method.POST);

            restRequest.AddParameter("flair_csv", flairCsv);

            return restRequest;
        }

        /// <summary>
        /// List of flairs.
        /// </summary>
        /// <param name="flairNameListingInput">A valid FlairNameListingInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>Flair list results.</returns>
        public FlairListResultContainer FlairList(FlairNameListingInput flairNameListingInput, string subreddit = null)
        {
            return SendRequest<FlairListResultContainer>(Sr(subreddit) + "api/flairlist", flairNameListingInput);
        }

        // In the controllers, link can be specified from Post.  Otherwise, call from Subreddit or User (no practical difference).  --Kris
        /// <summary>
        /// Return information about a users's flair options.
        /// If link is given, return link flair options. Otherwise, return user flair options for this subreddit.
        /// The logged in user's flair is also returned. Subreddit moderators may give a user by name to instead retrieve that user's flair.
        /// </summary>
        /// <param name="flairLinkInput">A valid FlairLinkInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>Flair results.</returns>
        public FlairSelectorResultContainer FlairSelector(FlairLinkInput flairLinkInput, string subreddit = null)
        {
            return SendRequest<FlairSelectorResultContainer>(Sr(subreddit) + "api/flairselector", flairLinkInput, Method.POST);
        }

        /// <summary>
        /// Create or update a flair template.
        /// </summary>
        /// <param name="flairTemplateInput">a valid FlairTemplateInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer FlairTemplate(FlairTemplateInput flairTemplateInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/flairtemplate", flairTemplateInput, Method.POST);
        }

        /// <summary>
        /// Create or update a flair template asynchronously.
        /// </summary>
        /// <param name="flairTemplateInput">a valid FlairTemplateInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> FlairTemplateAsync(FlairTemplateInput flairTemplateInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/flairtemplate", flairTemplateInput, Method.POST);
        }

        /// <summary>
        /// Create or update a flair template asynchronously.  Null values are ignored.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">a valid FlairTemplateV2Input instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>The created flair object.</returns>
        public FlairV2 FlairTemplateV2(FlairTemplateV2Input flairTemplateV2Input, string subreddit = null)
        {
            return SendRequest<FlairV2>(Sr(subreddit) + "api/flairtemplate_v2", flairTemplateV2Input, Method.POST);
        }

        /// <summary>
        /// Create or update a flair template.  Null values are ignored.
        /// This new endpoint is primarily used for the redesign.
        /// </summary>
        /// <param name="flairTemplateV2Input">a valid FlairTemplateV2Input instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>The created flair object.</returns>
        public async Task<FlairV2> FlairTemplateV2Async(FlairTemplateV2Input flairTemplateV2Input, string subreddit = null)
        {
            return await SendRequestAsync<FlairV2>(Sr(subreddit) + "api/flairtemplate_v2", flairTemplateV2Input, Method.POST);
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A list of flairs.</returns>
        public List<Things.Flair> LinkFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject<List<Things.Flair>>(ExecuteRequest(Sr(subreddit) + "api/link_flair"));
        }

        /// <summary>
        /// Return list of available link flair for the current subreddit.
        /// Will not return flair if the user cannot set their own link flair and they are not a moderator that can set flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A list of flairs.</returns>
        public List<FlairV2> LinkFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject<List<FlairV2>>(ExecuteRequest(Sr(subreddit) + "api/link_flair_v2"));
        }

        /// <summary>
        /// Sets a link flair.
        /// </summary>
        /// <param name="flairSelectFlairInput">a valid FlairSelectFlairInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer SelectFlair(FlairSelectFlairInput flairSelectFlairInput, string subreddit = null)
        {
            return SendRequest<GenericContainer>(Sr(subreddit) + "api/selectflair", flairSelectFlairInput, Method.POST);
        }

        /// <summary>
        /// Sets a link flair asynchronously.
        /// </summary>
        /// <param name="flairSelectFlairInput">a valid FlairSelectFlairInput instance</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> SelectFlairAsync(FlairSelectFlairInput flairSelectFlairInput, string subreddit = null)
        {
            return await SendRequestAsync<GenericContainer>(Sr(subreddit) + "api/selectflair", flairSelectFlairInput, Method.POST);
        }

        /// <summary>
        /// Set flair enabled.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public GenericContainer SetFlairEnabled(bool flairEnabled, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(ExecuteRequest(PrepareSetFlairEnabled(flairEnabled, subreddit)));
        }

        /// <summary>
        /// Set flair enabled asynchronously.
        /// </summary>
        /// <param name="flairEnabled">boolean value</param>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>A generic response object indicating any errors.</returns>
        public async Task<GenericContainer> SetFlairEnabledAsync(bool flairEnabled, string subreddit = null)
        {
            return JsonConvert.DeserializeObject<GenericContainer>(await ExecuteRequestAsync(PrepareSetFlairEnabled(flairEnabled, subreddit)));
        }

        private RestRequest PrepareSetFlairEnabled(bool flairEnabled, string subreddit = null)
        {
            RestRequest restRequest = PrepareRequest(Sr(subreddit) + "api/setflairenabled", Method.POST);

            restRequest.AddParameter("flair_enabled", flairEnabled);
            restRequest.AddParameter("api_type", "json");

            return restRequest;
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// Will not return flair if flair is disabled on the subreddit, the user cannot set their own flair, or they are not a moderator that can set flair.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>List of available user flairs.</returns>
        public List<Things.Flair> UserFlair(string subreddit = null)
        {
            return JsonConvert.DeserializeObject<List<Things.Flair>>(ExecuteRequest(Sr(subreddit) + "api/user_flair"));
        }

        /// <summary>
        /// Return list of available user flair for the current subreddit.
        /// If user is not a mod of the subreddit, this endpoint filters out mod_only templates.
        /// </summary>
        /// <param name="subreddit">The subreddit with the flairs</param>
        /// <returns>List of available user flairs.</returns>
        public List<FlairV2> UserFlairV2(string subreddit = null)
        {
            return JsonConvert.DeserializeObject<List<FlairV2>>(ExecuteRequest(Sr(subreddit) + "api/user_flair_v2"));
        }
    }
}
