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

                File.WriteAllText("Emoji.All.json", JsonConvert.SerializeObject(reddit.Models.Emoji.All("WayOfTheBern")));

                File.WriteAllText("Flair.UserFlair.json", JsonConvert.SerializeObject(reddit.Models.Flair.UserFlair("SandersForPresident")));
                File.WriteAllText("Flair.UserFlairV2.json", JsonConvert.SerializeObject(reddit.Models.Flair.UserFlairV2("SandersForPresident")));

                File.WriteAllText("Listings.Best.json", JsonConvert.SerializeObject(reddit.Models.Listings.Best(null, null, true)));
                File.WriteAllText("Listings.BestNoCats.json", JsonConvert.SerializeObject(reddit.Models.Listings.Best(null, null, false)));
                File.WriteAllText("Listings.BestWithSrDetail.json", JsonConvert.SerializeObject(reddit.Models.Listings.Best(null, null, true, 0, 25, "all", true)));
                File.WriteAllText("Listings.GetByNames.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetByNames("StillSandersForPres,WayOfTheBern")));
                File.WriteAllText("Listings.GetComments.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetComments("9gaze5", 0, false, false, "top", true, 0)));
                File.WriteAllText("Listings.GetCommentsWithEditsAndMoreAndTruncate.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetComments("8gmf99", 0, true, true, "top", true, 50)));
                File.WriteAllText("Listings.GetCommentsWithContext.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetComments("8gmf99", 8, true, true, "top", true, 0, 
                    "FloridaMan", "dyd2vtc")));
                File.WriteAllText("Listings.Hot.json", JsonConvert.SerializeObject(reddit.Models.Listings.Hot("GLOBAL", null, null, true)));
                File.WriteAllText("Listings.New.json", JsonConvert.SerializeObject(reddit.Models.Listings.New(null, null, true, "StillSandersForPres")));
                File.WriteAllText("Listings.Random.json", JsonConvert.SerializeObject(reddit.Models.Listings.Random("Facepalm")));
                File.WriteAllText("Listings.RandomNoSub.json", JsonConvert.SerializeObject(reddit.Models.Listings.Random()));
                File.WriteAllText("Listings.Rising.json", JsonConvert.SerializeObject(reddit.Models.Listings.Rising(null, null, true)));
                File.WriteAllText("Listings.Top.json", JsonConvert.SerializeObject(reddit.Models.Listings.Top("all", null, null, true)));
                File.WriteAllText("Listings.TopDay.json", JsonConvert.SerializeObject(reddit.Models.Listings.Top("day", null, null, true)));
                File.WriteAllText("Listings.Controversial.json", JsonConvert.SerializeObject(reddit.Models.Listings.Controversial("all", null, null, true)));

            }
        }
    }
}
