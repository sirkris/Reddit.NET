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
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
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
        /// 
        /// </summary>
        /// <param name="expandSrs"></param>
        /// <returns></returns>
        public object Mine(bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/mine");

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
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
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="expandSrs"></param>
        /// <returns></returns>
        public object User(string username, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/user/" + username);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="expandSrs"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Delete(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterpath"></param>
        /// <param name="expandSrs"></param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteFilter(string filterpath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="expandSrs"></param>
        /// <returns></returns>
        public object Get(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterpath"></param>
        /// <param name="expandSrs"></param>
        /// <returns>(TODO - Untested)</returns>
        public object GetFilter(string filterpath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="model"></param>
        /// <param name="expandSrs"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Create(string multipath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.POST);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterpath"></param>
        /// <param name="model"></param>
        /// <param name="expandSrs"></param>
        /// <returns>(TODO - Untested)</returns>
        public object CreateFilter(string filterpath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="model"></param>
        /// <param name="expandSrs"></param>
        /// <returns>(TODO - Untested)</returns>
        public object Update(string multipath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.PUT);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterpath"></param>
        /// <param name="model"></param>
        /// <param name="expandSrs"></param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdateFilter(string filterpath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <returns></returns>
        public object GetDescription(string multipath)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/description"));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="model"></param>
        /// <returns>(TODO - Untested)</returns>
        public object UpdateDescription(string multipath, string model)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath + "/description", Method.PUT);

            restRequest.AddParameter("model", model);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="srName"></param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteMultiSub(string multipath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName, Method.DELETE));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterpath"></param>
        /// <param name="srName"></param>
        /// <returns>(TODO - Untested)</returns>
        public object DeleteFilterSub(string filterpath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName, Method.DELETE));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="srName"></param>
        /// <returns></returns>
        public object GetMultiSub(string multipath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterpath"></param>
        /// <param name="srName"></param>
        /// <returns>(TODO - Untested)</returns>
        public object GetFilterSub(string filterpath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipath"></param>
        /// <param name="srName"></param>
        /// <param name="model"></param>
        /// <returns>(TODO - Untested)</returns>
        public object AddMultiSub(string multipath, string srName, string model)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName, Method.PUT));
        }

        // TODO - Needs testing.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterpath"></param>
        /// <param name="srName"></param>
        /// <param name="model"></param>
        /// <returns>(TODO - Untested)</returns>
        public object AddFilterSub(string filterpath, string srName, string model)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName, Method.PUT));
        }
    }
}
