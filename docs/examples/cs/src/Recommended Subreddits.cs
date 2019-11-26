using Reddit;
using Reddit.Inputs.Subreddits;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");
			
			var recommended = reddit.Models.Subreddits.Recommended("MySub", new SubredditsRecommendInput());
		}
	}
}
