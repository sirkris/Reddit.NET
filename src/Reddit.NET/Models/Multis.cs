using Newtonsoft.Json;
using Reddit.NET.Models.Structures;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models
{
    public class Multis : BaseModel
    {
        internal override RestClient RestClient { get; set; }

        public Multis(string appId, string refreshToken, string accessToken, RestClient restClient) : base(appId, refreshToken, accessToken, restClient) { }

        // TODO - Needs testing.
        /// <summary>
        /// Copy a multi.
        /// Responds with 409 Conflict if the target already exists.
        /// A "copied from ..." line will automatically be appended to the description.
        /// </summary>
        /// <param name="displayName">a string no longer than 50 characters</param>
        /// <param name="from">multireddit url path</param>
        /// <param name="to">destination multireddit url path</param>
        /// <returns>(TODO - Untested)</returns>
        public object Copy(string displayName, string from, string to)
        {
            RestRequest restRequest = PrepareRequest("api/multi/copy", Method.POST);

            restRequest.AddParameter("display_name", displayName);
            restRequest.AddParameter("from", from);
            restRequest.AddParameter("to", to);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Fetch a list of multis belonging to the current user.
        /// </summary>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A list of multis.</returns>
        public object Mine(bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/mine");

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Rename a multi.
        /// </summary>
        /// <param name="displayName">a string no longer than 50 characters</param>
        /// <param name="from">multireddit url path</param>
        /// <param name="to">destination multireddit url path</param>
        /// <returns>(TODO - Untested)</returns>
        public object Rename(string displayName, string from, string to)
        {
            RestRequest restRequest = PrepareRequest("api/multi/rename", Method.POST);

            restRequest.AddParameter("display_name", displayName);
            restRequest.AddParameter("from", from);
            restRequest.AddParameter("to", to);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Fetch a list of public multis belonging to username.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A list of multis.</returns>
        public object User(string username, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/user/" + username);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Delete a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object Delete(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
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
        /// <returns></returns>
        public object Get(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Get a filter.
        /// </summary>
        /// <param name="filterpath">filter url path</param>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>(TODO - Untested)</returns>
        public object GetFilter(string filterpath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
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
        /// <returns>(TODO - Untested)</returns>
        public object Create(string multipath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.POST);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
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
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
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
        /// <returns>(TODO - Untested)</returns>
        public object Update(string multipath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.PUT);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
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
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// Get a multi's description.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <returns>An object containing a description.</returns>
        public object GetDescription(string multipath)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/description"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Change a multi's markdown description.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="model">json data:
        /// {
        /// "body_md": raw markdown text,
        /// }</param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdateDescription(string multipath, string model)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath + "/description", Method.PUT);

            restRequest.AddParameter("model", model);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// Remove a subreddit from a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="srName">subreddit name</param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteMultiSub(string multipath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName, Method.DELETE));
        }

        // TODO - Needs testing.
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
        public object GetMultiSub(string multipath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName));
        }

        // TODO - Needs testing.
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

        // TODO - Needs testing.
        /// <summary>
        /// Add a subreddit to a multi.
        /// </summary>
        /// <param name="multipath">multireddit url path</param>
        /// <param name="srName">subreddit name</param>
        /// <param name="model">json data:
        /// {
        /// "name": subreddit name,
        /// }</param>
        /// <returns>(TODO - Untested)</returns>
        public object AddMultiSub(string multipath, string srName, string model)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName, Method.PUT));
        }

        // TODO - Needs testing.
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
