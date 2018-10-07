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

        public object Copy(string displayName, string from, string to)
        {
            RestRequest restRequest = PrepareRequest("api/multi/copy", Method.POST);

            restRequest.AddParameter("display_name", displayName);
            restRequest.AddParameter("from", from);
            restRequest.AddParameter("to", to);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Mine(bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/mine");

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Rename(string displayName, string from, string to)
        {
            RestRequest restRequest = PrepareRequest("api/multi/rename", Method.POST);

            restRequest.AddParameter("display_name", displayName);
            restRequest.AddParameter("from", from);
            restRequest.AddParameter("to", to);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object User(string username, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/user/" + username);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Delete(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object DeleteFilter(string filterpath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Get(string multipath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object GetFilter(string filterpath, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Create(string multipath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.POST);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object CreateFilter(string filterpath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object Update(string multipath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath, Method.PUT);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object UpdateFilter(string filterpath, string model, bool expandSrs)
        {
            RestRequest restRequest = PrepareRequest("api/filter/" + filterpath, Method.DELETE);

            restRequest.AddParameter("expand_srs", expandSrs);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object GetDescription(string multipath)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/description"));
        }

        public object UpdateDescription(string multipath, string model)
        {
            RestRequest restRequest = PrepareRequest("api/multi/" + multipath + "/description", Method.PUT);

            restRequest.AddParameter("model", model);

            return JsonConvert.DeserializeObject(ExecuteRequest(restRequest));
        }

        public object DeleteMultiSub(string multipath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName, Method.DELETE));
        }

        public object DeleteFilterSub(string filterpath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName, Method.DELETE));
        }

        public object GetMultiSub(string multipath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName));
        }

        public object GetFilterSub(string filterpath, string srName)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName));
        }

        public object AddMultiSub(string multipath, string srName, string model)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/multi/" + multipath + "/r/" + srName, Method.PUT));
        }

        public object AddFilterSub(string filterpath, string srName, string model)
        {
            return JsonConvert.DeserializeObject(ExecuteRequest("api/filter/" + filterpath + "/r/" + srName, Method.PUT));
        }
    }
}
