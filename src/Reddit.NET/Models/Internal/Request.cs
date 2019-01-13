using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reddit.Exceptions;
using Reddit.Models.EventArgs;
using Reddit.Things;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.Models.Internal
{
    public abstract class Request
    {
        internal abstract RestClient RestClient { get; set; }

        public event EventHandler<TokenUpdateEventArgs> TokenUpdated;
        public event EventHandler<RequestsUpdateEventArgs> RequestsUpdated;

        internal abstract string AppId { get; set; }
        internal abstract string AccessToken { get; set; }
        internal abstract string RefreshToken { get; set; }
        internal abstract string DeviceId { get; set; }

        internal abstract List<DateTime> Requests { get; set; }

        public Request(string appId, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null)
        {
            AppId = appId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            RestClient = restClient;
            DeviceId = deviceId;

            Requests = new List<DateTime>();
        }

        public T SendRequest<T>(string url, Method method = Method.GET, string contentType = "application/x-www-form-urlencoded")
        {
            return JsonConvert.DeserializeObject<T>(ExecuteRequest(PrepareRequest(url, method, contentType)));
        }

        public T SendRequest<T>(string url, dynamic parameters, Method method = Method.GET, string contentType = "application/x-www-form-urlencoded")
        {
            RestRequest restRequest = PrepareRequest(url, method, contentType);

            restRequest.AddObject(parameters);
            
            return JsonConvert.DeserializeObject<T>(ExecuteRequest(restRequest));
        }
        public async void SendRequestAsync(string url, dynamic parameters, Method method = Method.GET, string contentType = "application/x-www-form-urlencoded")
        {
            RestRequest restRequest = null;
            await Task.Run(() =>
            {
                restRequest = PrepareRequest(url, method, contentType);
                restRequest.AddObject(parameters);
            });

            Task<string> nobodyCares = ExecuteRequestAsync(restRequest);
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

        public string GetVersion()
        {
            string res = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return (string.IsNullOrWhiteSpace(res) || !res.Contains(".") ? res : res.Substring(0, res.LastIndexOf(".")) + (res.EndsWith(".1") ? "-develop" : ""));
        }

        public string ExecuteRequest(string url, Method method = Method.GET)
        {
            return ExecuteRequest(PrepareRequest(url, method));
        }

        public string ExecuteRequest(RestRequest restRequest)
        {
            return Task.Run(() => ExecuteRequestAsync(restRequest, true)).Result;
        }

        public async Task<string> ExecuteRequestAsync(RestRequest restRequest, bool awaitReturn = false)
        {
            // If we've reached the speed limit, hold until we're clear to proceed.  --Kris
            while (!RequestReady()) { }

            // Add to recent request history (used for ratelimiting purposes).  --Kris
            AddRequest();

            restRequest.AddHeader("User-Agent", "Reddit.NET v" + GetVersion());

            bool ratelimited;
            int ratelimitRetry = 100;
            IRestResponse res;
            do
            {
                if (awaitReturn)
                {
                    res = await RestClient.ExecuteTaskAsync(restRequest);
                }
                else
                {
                    Task<IRestResponse> nobodyCares = RestClient.ExecuteTaskAsync(restRequest);
                    return null;
                }

                // If we're not authenticated or the API doesn't respond, grab a new access token and retry.  --Kris
                int retry = 5;
                while ((res == null || !res.IsSuccessful)
                    && (RefreshToken != null || DeviceId != null)
                    && (res.StatusCode == HttpStatusCode.Unauthorized  // This is returned if the access token needs to be refreshed or wasn't provided.  --Kris
                        || res.StatusCode == HttpStatusCode.InternalServerError  // On rare occasion, a valid request will return a status code of 500, particularly if under heavy load.  --Kris
                        || res.StatusCode == 0)  // On rare occasion, a valid request will return a status code of 0, particularly if under heavy load.  --Kris
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

                // If we're using app-only authentication, handle any responses prompting for user login.  --Kris
                if (!string.IsNullOrWhiteSpace(res.Content)
                    && res.Content.Contains("Please log in to do that.")
                    && res.Content.Contains("\"errors\":")
                    && res.Content.Contains("USER_REQUIRED"))
                {
                    GenericContainer resObj = GetGenericResponse(res.Content);
                    if (resObj != null
                        && resObj.JSON != null
                        && resObj.JSON.Errors != null
                        && resObj.JSON.Errors.Count > 0)
                    {
                        throw new RedditUserRequiredException("This endpoint requires an authenticated user.");
                    }
                }

                // If we hit a ratelimit of less than a minute, wait the specified time then retry.  --Kris
                ratelimited = false;
                if (!string.IsNullOrWhiteSpace(res.Content)
                    && res.Content.Contains("you are doing that too much. try again in ")
                    && res.Content.Contains("\"errors\":")
                    && res.Content.Contains("\"ratelimit\":")
                    && (res.Content.Contains("seconds.") || res.Content.Contains("second.")))
                {
                    // Confirm the errors JSON and extract the wait time.  --Kris
                    Thread.Sleep(GetRateLimit(res.Content));

                    ratelimited = true;
                    ratelimitRetry--;
                }
            } while (ratelimited && ratelimitRetry > 0);

            if (res == null)
            {
                throw new RedditException("Reddit API returned null response.");
            }
            else if (!res.IsSuccessful)
            {
                switch (res.StatusCode)
                {
                    default:
                        throw (RedditException)BuildException(new RedditException("Reddit API returned non-success (" + res.StatusCode.ToString() + ") response."), res);
                    case 0:
                        throw (RedditException)BuildException(new RedditException("Reddit API failed to return a response."), res);
                    case HttpStatusCode.BadRequest:
                        throw (RedditBadRequestException)BuildException(new RedditBadRequestException("Reddit API returned Bad Request (400) response."), res);
                    case HttpStatusCode.Unauthorized:
                        throw (RedditUnauthorizedException)BuildException(new RedditUnauthorizedException("Reddit API returned Unauthorized (401) response."), res);
                    case HttpStatusCode.Forbidden:
                        throw (RedditForbiddenException)BuildException(new RedditForbiddenException("Reddit API returned Forbidden (403) response."), res);
                    case HttpStatusCode.NotFound:
                        throw (RedditNotFoundException)BuildException(new RedditNotFoundException("Reddit API returned Not Found (404) response."), res);
                    case HttpStatusCode.Conflict:
                        throw (RedditConflictException)BuildException(new RedditConflictException("Reddit API returned Conflict (409) response."), res);
                    case (HttpStatusCode)422:
                        throw (RedditUnprocessableEntityException)BuildException(new RedditUnprocessableEntityException("Reddit API returned Unprocessable Entity (422) response."), res);
                    case HttpStatusCode.InternalServerError:
                        throw (RedditInternalServerErrorException)BuildException(
                            new RedditInternalServerErrorException("Reddit API returned Internal Server Error (500) response."), res);
                    case HttpStatusCode.BadGateway:
                        throw (RedditBadGatewayException)BuildException(new RedditBadGatewayException("Reddit API returned Bad Gateway (502) response."), res);
                    case HttpStatusCode.ServiceUnavailable:
                        throw (RedditServiceUnavailableException)BuildException(
                            new RedditServiceUnavailableException("Reddit API returned Service Unavailable (503) response."), res);
                    case HttpStatusCode.GatewayTimeout:
                        throw (RedditGatewayTimeoutException)BuildException(new RedditGatewayTimeoutException("Reddit API returned Gateway Timeout (504) response."), res);
                }
            }
            else
            {
                return res.Content;
            }
        }

        private int GetRateLimit(string content)
        {
            int? res = null;

            try
            {
                res = Convert.ToInt32(JsonConvert.DeserializeObject<GenericContainer>(content).JSON.Ratelimit * 1000);
            }
            catch (Exception) { }

            return res ?? 60000;
        }

        private GenericContainer GetGenericResponse(string content)
        {
            GenericContainer res = null;

            try
            {
                res = JsonConvert.DeserializeObject<GenericContainer>(content);
            }
            catch (Exception) { }

            return res;
        }

        private Exception BuildException(Exception ex, IRestResponse res)
        {
            ex.Data.Add("StatusCode", res.StatusCode);
            ex.Data.Add("StatusDescription", res.StatusDescription);
            ex.Data.Add("res", res);

            return ex;
        }

        protected virtual void OnTokenUpdated(TokenUpdateEventArgs e)
        {
            TokenUpdated?.Invoke(this, e);
        }

        protected virtual void OnRequestsUpdated(RequestsUpdateEventArgs e)
        {
            RequestsUpdated?.Invoke(this, e);
        }

        public void UpdateAccessToken(string accessToken)
        {
            AccessToken = accessToken;
        }

        public void UpdateRequests(List<DateTime> requests)
        {
            Requests = requests;
        }

        internal bool RequestReady(int maxRequests = 60)
        {
            if (Requests.Count < maxRequests)
            {
                return true;
            }
            else
            {
                while (Requests.Count > 0)
                {
                    // As I understand, the general rule is 60 requests per minute.  --Kris
                    if (Requests[0].AddMinutes(1) < DateTime.Now)
                    {
                        Requests.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                RequestsUpdateEventArgs args = new RequestsUpdateEventArgs
                {
                    Requests = Requests
                };
                OnRequestsUpdated(args);

                return (Requests.Count < maxRequests);
            }
        }

        private void AddRequest()
        {
            Requests.Add(DateTime.Now);

            RequestsUpdateEventArgs args = new RequestsUpdateEventArgs
            {
                Requests = Requests
            };
            OnRequestsUpdated(args);
        }

        private RestRequest RefreshAccessToken(RestRequest restRequest)
        {
            RestClient keyCli = new RestClient("https://www.reddit.com");
            RestRequest keyReq = new RestRequest("/api/v1/access_token", Method.POST);

            keyReq.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(AppId + ":")));
            keyReq.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            if (!string.IsNullOrEmpty(RefreshToken))
            {
                keyReq.AddParameter("grant_type", "refresh_token");
                keyReq.AddParameter("refresh_token", RefreshToken);
            }
            else if (!string.IsNullOrEmpty(DeviceId))
            {
                keyReq.AddParameter("grant_type", "https://oauth.reddit.com/grants/installed_client");
                keyReq.AddParameter("device_id", DeviceId);
            }
            else
            {
                throw new RedditException("Either a refresh token or device ID is required for authentication.");
            }

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

        public void AddParamIfNotNull(string name, bool? value, ref RestRequest restRequest)
        {
            if (value.HasValue)
            {
                restRequest.AddParameter(name, value.Value);
            }
        }

        public void AddParamIfNotNull(string name, int? value, ref RestRequest restRequest)
        {
            if (value.HasValue)
            {
                restRequest.AddParameter(name, value.Value);
            }
        }

        public void AddParamIfNotNull(string name, object value, ref RestRequest restRequest)
        {
            if (value != null)
            {
                restRequest.AddParameter(name, value);
            }
        }
    }
}
