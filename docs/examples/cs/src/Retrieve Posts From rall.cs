using Reddit;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");
			
			IDictionary<string, IList<Post>> Posts = new Dictionary<string, IList<Post>>();
			foreach (Post post in reddit.Subreddit("all").Posts.New)
			{
				if (!Posts.ContainsKey(post.Subreddit))
				{
					Posts.Add(post.Subreddit, new List<Post>());
				}
				Posts[post.Subreddit].Add(post);
			}
        }
    }
}
