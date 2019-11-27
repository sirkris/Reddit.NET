using Reddit;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            reddit.Subreddit("MySub").LinkPost("Reddit.NET", "https://www.nuget.org/packages/Reddit").Submit().SetFlair("NuGet Package");
        }
    }
}
