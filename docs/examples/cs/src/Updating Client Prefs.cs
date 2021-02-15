using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");
			
			// Retrieve the current account preferences.  --Kris
			var prefs = reddit.Account.Prefs();

			// Modify our local copy of the preferences by setting the Over18 property to true.  --Kris
			prefs.Over18 = true;

			// Send our modified preferences instance back to the Reddit API.  --Kris
			reddit.Account.UpdatePrefs(new AccountPrefsSubmit(prefs, null, false, null));
        }
    }
}
