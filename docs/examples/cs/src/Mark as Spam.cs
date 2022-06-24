using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            // Marks the top post on r/MySubreddit as spam
			reddit.Subreddit("MySubreddit").Posts.Top[0].Remove(spam: true);
        }
    }
}
