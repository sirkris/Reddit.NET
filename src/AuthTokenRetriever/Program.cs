using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;
using uhttpsharp.Listeners;
using uhttpsharp.RequestProviders;

namespace AuthTokenRetriever
{
    class Program
    {
        // Change this to the path to your local web browser.  --Kris
        public const string BROWSER_PATH = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";

        static void Main(string[] args)
        {
            int port = 8080;
            if (args.Length > 0)
            {
                if (!int.TryParse(args[0], out port))
                {
                    Console.WriteLine("Reddit.NET OAuth Token Retriever");
                    Console.WriteLine("Created by Kris Craig");

                    Console.WriteLine();

                    Console.WriteLine("Usage:  AuthTokenRetriever [port] [App ID [App Secret]]");

                    Environment.Exit(Environment.ExitCode);
                }
            }

            string appId = (args.Length >= 2 ? args[1] : null);
            string appSecret = (args.Length >= 3 ? args[2] : null);

            // If appId and appSecret are unspecified, use guided mode.  --Kris
            if (string.IsNullOrWhiteSpace(appId) && string.IsNullOrWhiteSpace(appSecret))
            {
                if (string.IsNullOrWhiteSpace(appId))
                {
                    Console.Write("App ID: ");
                    appId = Console.ReadLine();
                }

                Console.Write("App Secret (leave blank for 'installed'-type apps): ");
                appSecret = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("** IMPORTANT:  Before you proceed any further, make sure you are logged into Reddit as the user you wish to authenticate! **");
                Console.WriteLine();

                Console.WriteLine("In the next step, a browser window will open and you'll be taken to Reddit's app authentication page.  Press any key to continue....");
                Console.ReadKey();
            }

            // Start the callback listener.  --Kris
            AwaitCallback(port);

            // Open the browser to the Reddit authentication page.  Once the user clicks "accept", Reddit will redirect the browser to localhost:8080, where AwaitCallback will take over.  --Kris
            // Tip - If you want to reduce the scopes, simply remove them from the URL string of the auth page and reload before clicking Accept.  --Kris
            string authUrl = "https://www.reddit.com/api/v1/authorize?client_id=" + appId + "&response_type=code"
                + "&state=" + appId + ":" + appSecret 
                + "&redirect_uri=http://localhost:8080/Reddit.NET/oauthRedirect&duration=permanent" 
                + "&scope=creddits%20modcontributors%20modmail%20modconfig%20subscribe%20structuredstyles%20vote%20wikiedit%20mysubreddits%20submit%20modlog%20modposts%20modflair%20save%20modothers%20read%20privatemessages%20report%20identity%20livemanage%20account%20modtraffic%20wikiread%20edit%20modwiki%20modself%20history%20flair";

            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(authUrl);
                Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // This typically occurs if the runtime doesn't know where your browser is.  Use BROWSER_PATH for when this happens.  --Kris
                ProcessStartInfo processStartInfo = new ProcessStartInfo(BROWSER_PATH)
                {
                    Arguments = authUrl
                };
                Process.Start(processStartInfo);
            }

            Console.ReadKey();  // Hit any key to exit.  --Kris

            Console.WriteLine("Token retrieval utility terminated.");
        }

        private static void AwaitCallback(int port)
        {
            using (HttpServer httpServer = new HttpServer(new HttpRequestProvider()))
            {
                httpServer.Use(new TcpListenerAdapter(new TcpListener(IPAddress.Loopback, port)));
                
                httpServer.Use((context, next) =>
                {
                    string code = null;
                    string state = null;
                    try
                    {
                        code = context.Request.QueryString.GetByName("code");
                        state = context.Request.QueryString.GetByName("state");  // This app formats state as:  AppId + ":" [+ AppSecret]
                    }
                    catch (KeyNotFoundException)
                    {
                        context.Response = new uhttpsharp.HttpResponse(HttpResponseCode.Ok, Encoding.UTF8.GetBytes("<b>ERROR:  No code and/or state received!</b>"), false);
                        Console.WriteLine("ERROR:  Request received without code and/or state!");
                    }

                    Console.Clear();  // Gets rid of that annoying logging exception message generated by the uHttpSharp library.  --Kris

                    if (!string.IsNullOrWhiteSpace(code)
                        && !string.IsNullOrWhiteSpace(state))
                    {
                        // Send request with code and JSON-decode the return for token retrieval.  --Kris
                        RestRequest restRequest = new RestRequest("/api/v1/access_token", Method.POST);

                        restRequest.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(state)));
                        restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                        restRequest.AddParameter("grant_type", "authorization_code");
                        restRequest.AddParameter("code", code);
                        restRequest.AddParameter("redirect_uri",
                            "http://localhost:" + port.ToString() + "/Reddit.NET/oauthRedirect");  // This must be an EXACT match in the app settings on Reddit!  --Kris

                        OAuthToken oAuthToken = JsonConvert.DeserializeObject<OAuthToken>(ExecuteRequest(restRequest));

                        Console.WriteLine("Access Token:  " + oAuthToken.AccessToken);
                        Console.WriteLine("Refresh Token: " + oAuthToken.RefreshToken);

                        string[] sArr = state.Split(":");
                        if (sArr == null || sArr.Length == 0)
                        {
                            throw new Exception("State must consist of 'appId:appSecret'!");
                        }

                        string appId = sArr[0];
                        string appSecret = (sArr.Length >= 2 ? sArr[1] : null);

                        string fileExt = "." + appId + "." + (!string.IsNullOrWhiteSpace(appSecret) ? appSecret + "." : "") + "json";

                        string tokenPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar
                            + "RDNOauthToken_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileExt;

                        File.WriteAllText(tokenPath, JsonConvert.SerializeObject(oAuthToken));

                        string html;
                        using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AuthTokenRetriever.Templates.Success.html"))
                        {
                            using (StreamReader streamReader = new StreamReader(stream))
                            {
                                html = streamReader.ReadToEnd();
                            }
                        }

                        html = html.Replace("REDDIT_OAUTH_ACCESS_TOKEN", oAuthToken.AccessToken);
                        html = html.Replace("REDDIT_OAUTH_REFRESH_TOKEN", oAuthToken.RefreshToken);
                        html = html.Replace("LOCAL_TOKEN_PATH", tokenPath);

                        context.Response = new uhttpsharp.HttpResponse(HttpResponseCode.Ok, Encoding.UTF8.GetBytes(html), false);

                        Console.WriteLine("Press any key to exit....");
                    }
                    else
                    {
                        Console.WriteLine("Unable to retrieve tokens.");
                    }

                    return Task.Factory.GetCompleted();
                });

                httpServer.Start();
                Console.Clear();  // Gets rid of that annoying logging exception message generated by the uHttpSharp library.  --Kris
            }
        }

        private static string ExecuteRequest(RestRequest restRequest)
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
