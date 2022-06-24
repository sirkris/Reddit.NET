using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            // Approves the top post on r/MySubreddit
			reddit.Subreddit("MySubreddit").Posts.Top[0].Approve();
        }
    }
}
