using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");
			
			var frontPagePosts = reddit.Subreddit().Posts.Best;
        }
    }
}
