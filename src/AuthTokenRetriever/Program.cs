using Reddit.AuthTokenRetriever;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

            Action<OAuthToken> tokenCallback = (OAuthToken token) =>
            {
                Console.Clear();
                Console.WriteLine($"Access token: {token.AccessToken}");
                Console.WriteLine($"Refresh token: {token.RefreshToken}");
                Console.WriteLine("Press any key to exit...");
            };

            using (var tokenRetriever = new AuthTokenRetrieverServer(appId, appSecret, port, completedAuthCallback: tokenCallback))
            {
                // Open the browser to the Reddit authentication page. Once the user clicks "accept", Reddit will redirect the browser to localhost:8080, where the tokenCallback delegate will be called.
                OpenBrowser(tokenRetriever.AuthorisationUrl);
                Console.WriteLine("Please open the following URL in your browser if it doesn't automatically open:");
                Console.WriteLine(tokenRetriever.AuthorisationUrl);

                Console.ReadKey(true);  // Hit any key to exit.  --Kris
            }

            Console.WriteLine("Token retrieval utility terminated.");
        }

        public static void OpenBrowser(string authUrl = "about:blank")
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(authUrl);
                Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                //For OSX run a separate command to open the web browser as found in https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", authUrl);
                }
                else
                {
                    // This typically occurs if the runtime doesn't know where your browser is.  Use BrowserPath for when this happens.  --Kris
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(BROWSER_PATH)
                    {
                        Arguments = authUrl
                    };
                    Process.Start(processStartInfo);
                }
            }
        }
    }
}
