using Newtonsoft.Json;
using Reddit.Inputs.Multis;
using Reddit.Things;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.Models
{
    public class Multis : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Multis(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }

        /// <summary>
        /// Copy a multi.
        /// Responds with 409 Conflict if the target already exists.
        /// A "copied from ..." line will automatically be appended to the description.
        /// </summary>
        /// <param name="multiURLInput">A valid MultiURLInput instance</param>
        /// <returns>An object containing the multireddit data.</returns>
        public LabeledMultiContainer Copy(MultiURLInput multiURLInput)
        {
            return SendRequest<LabeledMultiContainer>("api/multi/copy", multiURLInput, Method.POST);
        }

        /// <summary>
        /// Fetch a list of multis belonging to the current user.
        /// </summary>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A list of multis.</returns>
        public List<LabeledMultiContainer> Mine(bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/mine");

            restRequest.AddParameter("expand_srs", expandSrs);
            
            return JsonConvert.DeserializeObject<List<LabeledMultiContainer>>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Fetch a list of public multis belonging to username.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A list of multis.</returns>
        public List<LabeledMultiContainer> User(string username, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/user/" + username);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject<List<LabeledMultiContainer>>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Delete a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="expandSrs">boolean value</param>
        public void Delete(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);
            
            ExecuteRequest(restRequest);
        }

        // TODO - Needs testing.
        // TODO - Figure out what these filters are supposed to be.  --Kris
        /// <summary>
        /// Delete a filter.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteFilter(string filterpath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Fetch a multi's data and subreddit list by name.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A LabeledMultiContainer.</returns>
        public LabeledMultiContainer Get(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject<LabeledMultiContainer>(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        // TODO - Figure out what these filters are supposed to be.  --Kris
        /// <summary>
        /// Get a filter.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetFilter(string filterpath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Create a multi. Responds with 409 Conflict if it already exists.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="model">json data:
        /// {
        /// "description_md": raw markdown text,
        /// "display_name": a string no longer than 50 characters,
        /// "icon_name": one of (`art and design`, `ask`, `books`, `business`, `cars`, `comics`, `cute animals`, `diy`, `entertainment`, `food and drink`, `funny`, `games`, `grooming`, `health`, 
        /// `life advice`, `military`, `models pinup`, `music`, `news`, `philosophy`, `pictures and gifs`, `science`, `shopping`, `sports`, `style`, `tech`, `travel`, `unusual stories`, `video`, ``, 
        /// `None`),
        /// "key_color": a 6-digit rgb hex color, e.g. `#AABBCC`,
        /// "subreddits": [
        /// {
        /// "name": subreddit name,
        /// },
        /// ...
        /// ],
        /// "visibility": one of (`private`, `public`, `hidden`),
        /// "weighting_scheme": one of(`classic`, `fresh`),
        /// }</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>An object containing the multireddit data.</returns>
        public LabeledMultiContainer Create(string multipath, LabeledMultiSubmit model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.POST);

            restRequest.AddParameter("model", JsonConvert.SerializeObject(model));
            restRequest.AddParameter("expand_srs", expandSrs);
            
            return JsonConvert.DeserializeObject<LabeledMultiContainer>(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        // TODO - Figure out what these filters are supposed to be.  --Kris
        /// <summary>
        /// Create a filter. Responds with 409 Conflict if it already exists.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="model">json data:
        /// {
        /// "description_md": raw markdown text,
        /// "display_name": a string no longer than 50 characters,
        /// "icon_name": one of (`art and design`, `ask`, `books`, `business`, `cars`, `comics`, `cute animals`, `diy`, `entertainment`, `food and drink`, `funny`, `games`, `grooming`, `health`, 
        /// `life advice`, `military`, `models pinup`, `music`, `news`, `philosophy`, `pictures and gifs`, `science`, `shopping`, `sports`, `style`, `tech`, `travel`, `unusual stories`, `video`, ``, 
        /// `None`),
        /// "key_color": a 6-digit rgb hex color, e.g. `#AABBCC`,
        /// "subreddits": [
        /// {
        /// "name": subreddit name,
        /// },
        /// ...
        /// ],
        /// "visibility": one of (`private`, `public`, `hidden`),
        /// "weighting_scheme": one of(`classic`, `fresh`),
        /// }</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object CreateFilter(string filterpath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.POST);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Create or update a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="model">json data:
        /// {
        /// "description_md": raw markdown text,
        /// "display_name": a string no longer than 50 characters,
        /// "icon_name": one of (`art and design`, `ask`, `books`, `business`, `cars`, `comics`, `cute animals`, `diy`, `entertainment`, `food and drink`, `funny`, `games`, `grooming`, `health`, 
        /// `life advice`, `military`, `models pinup`, `music`, `news`, `philosophy`, `pictures and gifs`, `science`, `shopping`, `sports`, `style`, `tech`, `travel`, `unusual stories`, `video`, ``, 
        /// `None`),
        /// "key_color": a 6-digit rgb hex color, e.g. `#AABBCC`,
        /// "subreddits": [
        /// {
        /// "name": subreddit name,
        /// },
        /// ...
        /// ],
        /// "visibility": one of (`private`, `public`, `hidden`),
        /// "weighting_scheme": one of(`classic`, `fresh`),
        /// }</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>An object containing the multireddit data.</returns>
        public LabeledMultiContainer Update(string multipath, LabeledMultiSubmit model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.PUT);

            restRequest.AddParameter("model", JsonConvert.SerializeObject(model));
            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject<LabeledMultiContainer>(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        // TODO - Figure out what these filters are supposed to be.  --Kris
        /// <summary>
        /// Create or update a filter.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="model">json data:
        /// {
        /// "description_md": raw markdown text,
        /// "display_name": a string no longer than 50 characters,
        /// "icon_name": one of (`art and design`, `ask`, `books`, `business`, `cars`, `comics`, `cute animals`, `diy`, `entertainment`, `food and drink`, `funny`, `games`, `grooming`, `health`, 
        /// `life advice`, `military`, `models pinup`, `music`, `news`, `philosophy`, `pictures and gifs`, `science`, `shopping`, `sports`, `style`, `tech`, `travel`, `unusual stories`, `video`, ``, 
        /// `None`),
        /// "key_color": a 6-digit rgb hex color, e.g. `#AABBCC`,
        /// "subreddits": [
        /// {
        /// "name": subreddit name,
        /// },
        /// ...
        /// ],
        /// "visibility": one of (`private`, `public`, `hidden`),
        /// "weighting_scheme": one of(`classic`, `fresh`),
        /// }</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdateFilter(string filterpath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.PUT);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get a multi's description.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <returns>An object containing a description.</returns>
        public LabeledMultiDescriptionContainer GetDescription(string multipath)
        {
            return JsonConvert.DeserializeObject<LabeledMultiDescriptionContainer>(ExecuteRequest("api/multi/" + multipath + "/description"));
        }

        /// <summary>
        /// Change a multi's markdown description.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="description">The new description markdown text</param>
        /// <returns>An object containing a description.</returns>
        public LabeledMultiDescriptionContainer UpdateDescription(string multipath, string description)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath + "/description", Method.PUT);

            restRequest.AddParameter("model", "{\"body_md\": \"" + description + "\"}");
            
            return JsonConvert.DeserializeObject<LabeledMultiDescriptionContainer>(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Remove a subreddit from a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="srName">subreddit name</param>
        public void DeleteMultiSub(string multipath, string srName)
        {
            ExecuteRequest("api/multi/" + multipath + "/r/" + srName, Method.DELETE);
        }

        // TODO - Needs testing.
        // TODO - Figure out what these filters are supposed to be.  --Kris
        /// <summary>
        /// Remove a subreddit from a filter.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="srName">subreddit name</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteFilterSub(string filterpath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName, Method.DELETE));
        }

        /// <summary>
        /// Get data about a subreddit in a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="srName">subreddit name</param>
        /// <returns>An object containing the subreddit name.</returns>
        public NamedObj GetMultiSub(string multipath, string srName)
        {
            return JsonConvert.DeserializeObject<NamedObj>(ExecuteRequest("api/multi/" + multipath + "/r/" + srName));
        }

        // TODO - Needs testing.
        // TODO - Figure out what these filters are supposed to be.  --Kris
        /// <summary>
        /// Get data about a subreddit in a filter.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="srName">subreddit name</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetFilterSub(string filterpath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName));
        }

        /// <summary>
        /// Add a subreddit to a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="srName">subreddit name</param>
        /// <returns>An object containing the name of the added subreddit.</returns>
        public NamedObj AddMultiSub(string multipath, string srName)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath + "/r/" + srName, Method.PUT);

            restRequest.AddParameter("model", "{\"name\": \"" + srName + "\"}");

            return JsonConvert.DeserializeObject<NamedObj>(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        // TODO - Figure out what these filters are supposed to be.  --Kris
        /// <summary>
        /// Add a subreddit to a filter.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="srName">subreddit name</param>
        /// <param name="model">json data:
        /// {
        /// "name": subreddit name,
        /// }</param>
        /// <returns>(TODO - Untested)</returns>
        public object AddFilterSub(string filterpath, string srName, string model)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName, Method.PUT));
        }
    }
}
