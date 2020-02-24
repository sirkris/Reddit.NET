using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            // Get the number of direct replies to the top comment on the top post of r/AskReddit.  --Kris
			int? numReplies = reddit.Subreddit("AskReddit").Posts.Top[0].Comments.Top[0].NumReplies;
        }
    }
}
