using Reddit;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

			// Retrieve the SelfPost we want and link to it on r/MySub.  The host will automatically be replaced with np.reddit.com and r/AskReddit will be credited in the title.  --Kris
			var newLinkPost = reddit.Subreddit("AskReddit").Posts.GetTop(t: "week")[0].XPostToAsLink("MySub");
        }
    }
}
