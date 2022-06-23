using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            var profileLinkPost = reddit.Subreddit($"u_{reddit.Account.Me.Name}").LinkPost("https://xkcd.com/308/").Submit();
        }
    }
}
