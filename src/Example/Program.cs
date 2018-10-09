using Newtonsoft.Json;
using Reddit.NET;
using Reddit.NET.Controllers;
using System;
using System.IO;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Example.exe <Reddit App ID> <Reddit Refresh Token> [Reddit Access Token]");
            }
            else
            {
                string appId = args[0];
                string refreshToken = args[1];
                string accessToken = (args.Length > 2 ? args[2] : null);

                RedditAPI reddit = new RedditAPI(appId, refreshToken, accessToken);

                User me = reddit.User().Me();

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                // Temporary code - Verify I've got all the models right and catalogue their returns.  Will then proceed to writing unit tests.  --Kris
                File.WriteAllText("Account.Prefs.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs()));
                File.WriteAllText("Account.Trophies.json", JsonConvert.SerializeObject(reddit.Models.Account.Trophies()));
                File.WriteAllText("Account.PrefsFriends.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("friends")));
                File.WriteAllText("Account.PrefsBlocked.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("blocked")));
                File.WriteAllText("Account.PrefsMessaging.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("messaging")));
                File.WriteAllText("Account.PrefsTrusted.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("trusted")));

                var blah = reddit.Models.Emoji.All("WayOfTheBern");
                int i = 0;
            }
        }
    }
}
