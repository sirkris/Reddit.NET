using EmbedIO;
using EmbedIO.Actions;
using Newtonsoft.Json;
using RestSharp;
using Swan.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.AuthTokenRetriever
{
    public class AuthTokenRetrieverServer : IDisposable
    {
        private readonly string _appId;
        private readonly string _appSecret;
        private readonly int _port;
        private string _baseUrl
        {
            get
            {
                return $"http://localhost:{_port}/";
            }
        }
        private string _redirectUrl
        {
            get
            {
                return $"{_baseUrl}Reddit.NET/oauthRedirect";
            }
        }
        private Action<OAuthToken> _completedAuthCallback;
        private readonly string _state = Guid.NewGuid().ToString("N");

        /// <summary>
        /// A space or comma separated list of scopes the credentials can access
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// The URL the user should go to to authorise the application
        /// </summary>
        public string AuthorisationUrl
        {
            get
            {
                return $"https://www.reddit.com/api/v1/authorize?client_id={_appId}&response_type=code&" +
                    $"state={_state}&redirect_uri={_redirectUrl}&duration=permanent&scope={Scope}";
            }
        }

        private WebServer _webServer;
        private MemoryStream _memoryStream;
        private TextWriter _textWriter;
        private static readonly HttpClient _httpClient = new HttpClient();

        public OAuthToken Credentials { get; private set; }
        public string CredentialsJsonPath { get; private set; }

        /// <summary>
        /// Create a new instance of the Reddit.NET OAuth Token Retriever library.
        /// </summary>
        /// <param name="appId">Your Reddit App ID</param>
        /// <param name="appSecret">Your Reddit App Secret (leave empty for installed apps)</param>
        /// <param name="port">The port to listen on for the callback (default: 8080)</param>
        /// <param name="scope">A space or comma separated list of scopes the credentials can access (default: all scopes)</param>
        /// <param name="completedAuthCallback">The method to be called when the user successfully authenticates and obtains their tokens</param>
        public AuthTokenRetrieverServer(string appId = null, string appSecret = null,
            int port = 8080, string scope = "creddits%20modcontributors%20modmail%20modconfig%20subscribe%20structuredstyles%20vote%20wikiedit%20mysubreddits%20submit%20modlog%20modposts%20modflair%20save%20modothers%20read%20privatemessages%20report%20identity%20livemanage%20account%20modtraffic%20wikiread%20edit%20modwiki%20modself%20history%20flair",
            Action<OAuthToken> completedAuthCallback = null)
        {
            _appId = appId;
            _appSecret = appSecret;
            _port = port;
            Scope = scope;
            _completedAuthCallback = completedAuthCallback;
            Logger.NoLogging();
            _webServer = CreateWebServer();
            _webServer.RunAsync();
        }

        private WebServer CreateWebServer()
        {
            _memoryStream = new MemoryStream();
            _textWriter = new StreamWriter(_memoryStream);
            Action<TextWriter> htmlWriter = delegate (TextWriter textWriter)
            {
                textWriter.WriteLine("<h1>Token retrieval completed successfully!</h1>");
                textWriter.WriteLine($"<p><b>Access token:</b> {Credentials.AccessToken}</p>");
                textWriter.WriteLine($"<p><b>Refresh token:</b> {Credentials.RefreshToken}</p>");
                textWriter.WriteLine($"<p><b>Tokens saved to:</b> <a href=\"file://{CredentialsJsonPath}\">{CredentialsJsonPath}</a></p>");
            };
            return new WebServer(o => o
                    .WithUrlPrefix(_baseUrl)
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithLocalSessionManager()
                .WithModule(new ActionModule("/Reddit.NET/oauthRedirect", HttpVerbs.Any, ctx =>
                {
                    Credentials = RetrieveToken(ctx.GetRequestQueryData()).Result;
                    CredentialsJsonPath = WriteCredentialsToJson(Credentials);
                    _completedAuthCallback?.Invoke(Credentials);
                    return ctx.SendStandardHtmlAsync(200, htmlWriter);
                }));
        }

        private async Task<OAuthToken> RetrieveToken(NameValueCollection queryData)
        {
            if (!string.IsNullOrWhiteSpace(queryData["error"]))
            {
                throw new Exception($"Reddit returned error regarding authorisation. Error value: {queryData["error"]}");
            }

            if (queryData["state"] != _state)
            {
                throw new Exception($"State returned by Reddit does not match state sent.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_appId}:{_appSecret}")));
            string code = queryData["code"];
            var tokenRequestData = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", _redirectUrl }
            };
            HttpResponseMessage tokenResponse = await _httpClient.PostAsync("https://www.reddit.com/api/v1/access_token", new FormUrlEncodedContent(tokenRequestData));
            if (!tokenResponse.IsSuccessStatusCode)
            {
                throw new Exception("Reddit returned non-success status code when getting access token.");
            }
            string tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();
            if (tokenResponseContent.Contains("error"))
            {
                throw new Exception($"Reddit returned error when getting access token. JSON response: {tokenResponseContent}");
            }
            var credentials = JsonConvert.DeserializeObject<OAuthToken>(tokenResponseContent);
            return credentials;
        }

        private string WriteCredentialsToJson(OAuthToken oAuthToken)
        {
            string fileExt = "." + _appId + "." + (!string.IsNullOrWhiteSpace(_appSecret) ? _appSecret + "." : "") + "json";

            string tokenPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar
                + "RDNOauthToken_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileExt;

            File.WriteAllText(tokenPath, JsonConvert.SerializeObject(oAuthToken));
            return tokenPath;
        }

        public void Dispose()
        {
            _webServer.Dispose();
            _textWriter.Dispose();
            _memoryStream.Dispose();
        }
    }

    [Obsolete("This class has been deprecated in favour of " + nameof(AuthTokenRetrieverServer) + ".")]
    public class AuthTokenRetrieverLib
    {
        /// <summary>
        /// Your Reddit App ID
        /// </summary>
        internal string AppId
        {
            get;
        }

        /// <summary>
        /// Your Reddit App Secret (leave empty for installed apps)
        /// </summary>
        internal string AppSecret
        {
            get;
        }

        /// <summary>
        /// The port to listen on for the callback (default: 8080)
        /// </summary>
        internal int Port
        {
            get;
        }

        internal AuthTokenRetrieverServer AuthServer
        {
            get;
            private set;
        }

        public string AccessToken
        {
            get
            {
                return AuthServer.Credentials.AccessToken;
            }
        }

        public string RefreshToken
        {
            get
            {
                return AuthServer.Credentials.RefreshToken;
            }
        }

        /// <summary>
        /// Create a new instance of the Reddit.NET OAuth Token Retriever library.
        /// </summary>
        /// <param name="appId">Your Reddit App ID</param>
        /// <param name="appSecret">Your Reddit App Secret (leave empty for installed apps)</param>
        /// <param name="port">The port to listen on for the callback (default: 8080)</param>
        /// <param name="browserPath">The path to your local web browser</param>
        public AuthTokenRetrieverLib(string appId = null, string appSecret = null, int port = 8080)
        {
            AppId = appId;
            AppSecret = appSecret;
            Port = port;
        }

        public void AwaitCallback()
        {
            AuthServer = new AuthTokenRetrieverServer(AppId, AppSecret, Port);
        }

        public void StopListening()
        {
            AuthServer.Dispose();
        }

        public string AuthURL(string scope = "creddits%20modcontributors%20modmail%20modconfig%20subscribe%20structuredstyles%20vote%20wikiedit%20mysubreddits%20submit%20modlog%20modposts%20modflair%20save%20modothers%20read%20privatemessages%20report%20identity%20livemanage%20account%20modtraffic%20wikiread%20edit%20modwiki%20modself%20history%20flair")
        {
            AuthServer.Scope = scope;
            return AuthServer.AuthorisationUrl;
        }

        public string ExecuteRequest(RestRequest restRequest)
        {
            IRestResponse res = new RestClient("https://www.reddit.com").Execute(restRequest);
            if (res != null && res.IsSuccessful)
            {
                return res.Content;
            }
            else
            {
                Exception ex = new Exception("API returned non-success response.");

                ex.Data.Add("res", res);

                throw ex;
            }
        }
    }
}
