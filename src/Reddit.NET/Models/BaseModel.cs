using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.NET.Models.Structures;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Reddit.NET.Models
{
    public abstract class BaseModel
    {
        private readonly string AppId;
        internal string AccessToken;
        private readonly string RefreshToken;

        internal abstract RestClient RestClient { get; set; }

        public BaseModel(string appId, string refreshToken, string accessToken, RestClient restClient)
        {
            this.AppId = appId;
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
            this.RestClient = restClient;
        }

        public RestRequest PrepareRequest(string url, Method method = Method.GET)
        {
            RestRequest restRequest = new RestRequest(url, method);

            return PrepareRequest(restRequest);
        }

        public RestRequest PrepareRequest(string url, Method method, List<Parameter> parameters)
        {
            RestRequest restRequest = PrepareRequest(url, method);

            foreach (Parameter param in parameters)
            {
                if (!param.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase))
                {
                    restRequest.AddParameter(param);
                }
            }
            
            return restRequest;
        }

        public RestRequest PrepareRequest(RestRequest restRequest)
        {
            restRequest.AddHeader("Authorization", "bearer " + AccessToken);
            //restRequest.AddHeader("User-Agent", "Reddit.NET");

            if (restRequest.Method == Method.POST || restRequest.Method == Method.PUT)
            {
                restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            }

            return restRequest;
        }

        public string ExecuteRequest(string url, Method method = Method.GET)
        {
            return ExecuteRequest(PrepareRequest(url, method));
        }

        public string ExecuteRequest(RestRequest restRequest)
        {
            IRestResponse res = RestClient.Execute(restRequest);
            int retry = 5;
            while ((res == null || !res.IsSuccessful)
                && retry > 0)
            {
                /*
                 * If it fails and we have a refresh token, request a new access token and retry.
                 * Note that this workflow will not work if you pass an empty access token, as the Reddit API will still return 200 on those requests.
                 * Therefore, if you just want to get a new access token from refresh, pass an arbitrary string value instead of null.
                 * 
                 * --Kris
                 */
                if (RefreshToken != null
                    && (res.StatusCode == System.Net.HttpStatusCode.BadRequest
                        || res.StatusCode == System.Net.HttpStatusCode.Unauthorized))
                {
                    RestClient keyCli = new RestClient("https://www.reddit.com");
                    RestRequest keyReq = new RestRequest("/api/v1/access_token", Method.POST);

                    keyReq.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(AppId + ":")));
                    keyReq.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    
                    keyReq.AddParameter("grant_type", "refresh_token");
                    keyReq.AddParameter("refresh_token", RefreshToken);

                    IRestResponse keyRes = keyCli.Execute(keyReq);
                    if (keyRes != null && keyRes.IsSuccessful)
                    {
                        AccessToken = JsonConvert.DeserializeObject<JObject>(keyRes.Content).GetValue("access_token").ToString();
                        restRequest = PrepareRequest(restRequest.Resource, restRequest.Method, restRequest.Parameters);
                    }
                }

                res = RestClient.Execute(restRequest);

                retry--;
            }

            if (res == null)
            {
                throw new WebException("Reddit API returned null response.");
            }
            else if (!res.IsSuccessful)
            {
                WebException ex = new WebException("Reddit API returned non-success response.");

                ex.Data.Add("StatusCode", res.StatusCode);
                ex.Data.Add("StatusDescription", res.StatusDescription);
                ex.Data.Add("res", res);

                throw ex;
            }
            else
            {
                return res.Content;
            }
        }

        public string Sr(string subreddit)
        {
            return (!string.IsNullOrWhiteSpace(subreddit) ? "/r/" + subreddit + "/" : "");
        }
    }
}
