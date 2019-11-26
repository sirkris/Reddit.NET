using Reddit;
using Reddit.Controllers;
using Reddit.Inputs.Search;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            List<Post> posts = reddit.Subreddit("MySub").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/MySub
            if (posts.Count == 0)
            {
                posts = reddit.Subreddit("all").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/all
            }
        }
    }
}
