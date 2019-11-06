using Reddit;
using Reddit.Controllers;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			(new PostsHelper("YourRedditAppID", "YourBotUserRefreshToken")).ListPosts("movies");
		}
	}
	
	public class PostsHelper
	{
		private RedditAPI Reddit { get; set; }
		
		public PostsHelper(string appId, string refreshToken)
		{
			Reddit = new RedditAPI(appId, refreshToken);
		}
		
		// Display the title of each post followed by the link URL (if it's a link post) or the Body (if it's a self post).  --Kris
		public void ListPosts(string subreddit)
		{
			foreach (Post post in Reddit.Subreddit(subreddit).Posts.Hot)
			{
				Console.WriteLine("Title: " + post.Title);
				
				// Both LinkPost and SelfPost derive from the Post class.  --Kris
				Console.WriteLine(post.Listing.IsSelf
					? "Body: " + ((SelfPost)post).SelfText
					: "URL: " + ((LinkPost)post).URL);
			}
		}
	}
}
