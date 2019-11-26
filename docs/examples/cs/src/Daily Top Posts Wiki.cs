using Reddit;
using Reddit.Controllers;
using Reddit.Inputs;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

            var subreddit = reddit.Subreddit("MySub");
            var today = DateTime.Today;

            string pageContent = "## Top Posts for " + today.ToString("D") + Environment.NewLine;

            // Get the top 10 posts from the last 24 hours.  --Kris
            var posts = subreddit.Posts.GetTop(new TimedCatSrListingInput(t: "day", limit: 10));
            if (posts.Count > 0)
            {
                foreach (Post post in posts)
                {
                    if (post.Created >= today && post.Created < today.AddDays(1))
                    {
                        pageContent += Environment.NewLine + "### [" + post.Title + "](" + post.Permalink + ")" + Environment.NewLine;
                    }
                }
            }
            else
            {
                pageContent += "*There were no new top posts today.*";
            }

            var pageUrl = "TopPosts/" + today.Year + "/" + today.Month + "/" + today.Day;

            // Create the wiki page.  Note that the first argument is the edit reason for the history and is required by the API.  --Kris
            var wikiPage = subreddit.Wiki.Page(pageUrl).CreateAndReturn("Created the page.", pageContent);

            // Retrieve the index.  Note that this page should already exist with a single revision even on a brand new subreddit.  --Kris
            var index = subreddit.Wiki.GetPage("index");

            // You'd probably want to break this up into multiple pages and whatnot, but you get the idea.  --Kris
            index.EditAndReturn("Added top posts for: " + today.ToString("D"),
                index.ContentMd + Environment.NewLine + "### [" + today.ToString("D") + "](" + pageUrl + ")" + Environment.NewLine,
                index.Revisions()[0].Id);  // FYI, the Revisions() are sorted by most-recent first.  --Kris
        }
    }
}
