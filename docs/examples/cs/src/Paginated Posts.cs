using Reddit;
using Reddit.Controllers;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            // Since we only need the posts, there's no need to call .About() on this one.  --Kris
            var worldnews = reddit.Subreddit("worldnews");

            // Just keep going until we hit a post from before today.  Note that the API may sometimes return posts slightly out of order.  --Kris
            var posts = new List<Post>();
            string after = "";
            DateTime start = DateTime.Now;
            DateTime today = DateTime.Today;
            bool outdated = false;
            do
            {
                foreach (Post post in worldnews.Posts.GetNew(after: after))
                {
                    if (post.Created >= today)
                    {
                        posts.Add(post);
                    }
                    else
                    {
                        outdated = true;
                        break;
                    }

                    after = post.Fullname;
                }
            } while (!outdated
                && start.AddMinutes(5) > DateTime.Now
                && worldnews.Posts.New.Count > 0);  // This is automatically populated with the results of the last GetNew call.  --Kris
        }
    }
}
