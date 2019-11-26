using Reddit;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            reddit.Subreddit("MySub").Flairs.CreateUserFlair("KrisCraig", "Fucking Genius");
        }
    }
}
