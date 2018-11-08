using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.NET.Exceptions;
using Reddit.NET.Models.EventHandlers;
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

        public event EventHandler<TokenUpdateEventArgs> TokenUpdated;

        public BaseModel(string appId, string refreshToken, string accessToken, RestClient restClient)
        {
            AppId = appId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            RestClient = restClient;
        }

        protected virtual void OnTokenUpdated(TokenUpdateEventArgs e)
        {
            TokenUpdated?.Invoke(this, e);
        }

        public void UpdateAccessToken(string accessToken)
        {
            AccessToken = accessToken;
        }

        public RestRequest PrepareRequest(string url, Method method = Method.GET, string contentType = "application/x-www-form-urlencoded")
        {
            RestRequest restRequest = new RestRequest(url, method);

            return PrepareRequest(restRequest, contentType);
        }

        public RestRequest PrepareRequest(string url, Method method, List<Parameter> parameters, List<FileParameter> files, 
            string contentType = "application/x-www-form-urlencoded")
        {
            RestRequest restRequest = PrepareRequest(url, method, contentType);

            foreach (Parameter param in parameters)
            {
                if (!param.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)
                    && !param.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    restRequest.AddParameter(param);
                }
            }
            
            foreach (FileParameter file in files)
            {
                restRequest.Files.Add(file);
            }

            return restRequest;
        }

        public RestRequest PrepareRequest(RestRequest restRequest, string contentType = "application/x-www-form-urlencoded")
        {
            restRequest.AddHeader("Authorization", "bearer " + AccessToken);

            if (restRequest.Method == Method.POST || restRequest.Method == Method.PUT)
            {
                restRequest.AddHeader("Content-Type", contentType);
            }

            return restRequest;
        }

        public string ExecuteRequest(string url, Method method = Method.GET)
        {
            return ExecuteRequest(PrepareRequest(url, method));
        }

        public string ExecuteRequest(RestRequest restRequest)
        {
            restRequest.AddHeader("User-Agent", "Reddit.NET");

            IRestResponse res = RestClient.Execute(restRequest);
            int retry = 3;
            while ((res == null || !res.IsSuccessful)
                && RefreshToken != null
                && res.StatusCode == HttpStatusCode.Unauthorized
                && retry > 0)
            {
                /*
                 * If it fails and we have a refresh token, request a new access token and retry.
                 * Note that this workflow will not work if you pass an empty access token, as the Reddit API will still return 200 on those requests.
                 * Therefore, if you just want to get a new access token from refresh, pass an arbitrary string value instead of null.
                 * 
                 * --Kris
                 */
                restRequest = RefreshAccessToken(restRequest);
                res = RestClient.Execute(restRequest);
                
                retry--;
            }

            if (res == null)
            {
                throw new RedditException("Reddit API returned null response.");
            }
            else if (!res.IsSuccessful)
            {
                switch (res.StatusCode)
                {
                    default:
                        throw (RedditException)BuildException(new RedditException("Reddit API returned non-success response."), res);
                    case HttpStatusCode.BadGateway:
                        throw (RedditBadGatewayException)BuildException(new RedditBadGatewayException("Reddit API returned Bad Gateway (502) response."), res);
                    case HttpStatusCode.BadRequest:
                        throw (RedditBadRequestException)BuildException(new RedditBadRequestException("Reddit API returned Bad Request (400) response."), res);
                    case HttpStatusCode.Conflict:
                        throw (RedditConflictException)BuildException(new RedditConflictException("Reddit API returned Conflict (409) response."), res);
                    case HttpStatusCode.Forbidden:
                        throw (RedditForbiddenException)BuildException(new RedditForbiddenException("Reddit API returned Forbidden (403) response."), res);
                    case HttpStatusCode.GatewayTimeout:
                        throw (RedditGatewayTimeoutException)BuildException(new RedditGatewayTimeoutException("Reddit API returned Gateway Timeout (504) response."), res);
                    case HttpStatusCode.InternalServerError:
                        throw (RedditInternalServerErrorException)BuildException(
                            new RedditInternalServerErrorException("Reddit API returned Internal Server Error (500) response."), res);
                    case HttpStatusCode.NotFound:
                        throw (RedditNotFoundException)BuildException(new RedditNotFoundException("Reddit API returned Not Found (404) response."), res);
                    case HttpStatusCode.ServiceUnavailable:
                        throw (RedditServiceUnavailableException)BuildException(
                            new RedditServiceUnavailableException("Reddit API returned Service Unavailable (503) response."), res);
                    case HttpStatusCode.Unauthorized:
                        throw (RedditUnauthorizedException)BuildException(new RedditUnauthorizedException("Reddit API returned Unauthorized (401) response."), res);
                }
            }
            else
            {
                return res.Content;
            }
        }

        private Exception BuildException(Exception ex, IRestResponse res)
        {
            ex.Data.Add("StatusCode", res.StatusCode);
            ex.Data.Add("StatusDescription", res.StatusDescription);
            ex.Data.Add("res", res);

            return ex;
        }

        private RestRequest RefreshAccessToken(RestRequest restRequest)
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

                TokenUpdateEventArgs args = new TokenUpdateEventArgs
                {
                    AccessToken = AccessToken
                };
                OnTokenUpdated(args);

                string contentType = "application/x-www-form-urlencoded";
                foreach (Parameter param in restRequest.Parameters)
                {
                    if (param.Name.Equals("content-type", StringComparison.OrdinalIgnoreCase)
                        || param.Name.Equals("contenttype", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = param.Value.ToString();
                        break;
                    }
                }
                
                return PrepareRequest(restRequest.Resource, restRequest.Method, restRequest.Parameters, restRequest.Files, contentType);
            }
            else
            {
                return restRequest;
            }
        }

        public string Sr(string subreddit)
        {
            return (!string.IsNullOrWhiteSpace(subreddit) ? "/r/" + subreddit + "/" : "");
        }
    }
}
