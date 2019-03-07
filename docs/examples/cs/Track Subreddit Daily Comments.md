# Track a Subreddit's Daily Comments

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries:

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

This app scans all of a subreddit's posts from the previous calendar day, adds up all the comments, then posts the total and updates the sidebar.  In order to update the sidebar, the bot user will have to be a moderator with the modconfig scope.  Note the use of the try/catch block to check for that.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;
using Reddit.Controllers;
using Reddit.Exceptions;
using Reddit.Inputs;
using System;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

// reddit.Subreddit("MySubreddit") loads an empty named subreddit instance, then About() queries Reddit and returns the data.  --Kris
var subreddit = reddit.Subreddit("MySubreddit").About();

/*
 * Gather all of yesterday's posts and add the total comments.  Note the use of "after" for pagination, as the API is limited to a maximum of 100 results per query.
 * Use the special "newForced" sort and the library will keep stickied posts (announcements) in chronological order.  The API always returns stickied posts at the top of "new"-sorted results.
 * The API sometimes returns posts slightly out of order (don't ask me why), so this will keep going until it gets 3 or more consecutive posts outside the date range.  It's an arbitrary number but should be sufficient.
 * Loop will timeout after 5 minutes, just to be safe.
 * 
 * --Kris
 */
int totalComments = 0;
int outdatedPosts = 0;
string after = "";
DateTime start = DateTime.Now;
do
{
	foreach (Post post in subreddit.Posts.GetNew(new CategorizedSrListingInput(after: after, limit: 100)))
	{
		// Keep going until we hit 3 posts in a row from the day before yesterday.  Today's posts are completely ignored.  --Kris
		if (post.Created >= DateTime.Today.AddDays(-1) && post.Created < DateTime.Today)
		{
			outdatedPosts = 0;
			comments += post.Listing.NumComments;
		}
		else if (post.Created < DateTime.Today.AddDays(-1))
		{
			outdatedPosts++;
		}
		
		after = post.Fullname;
	}
} while (outdatedPosts < 3 && start.AddMinutes(5) > DateTime.Now);

// Create a new self-post to report the result.  --Kris
var newPost = subreddit.SelfPost("Total Comments for " + DateTime.Today.AddDays(-1).ToString("D"), comments.ToString()).Submit();

// Update the sidebar to reflect yesterday's total.  --Kris
subreddit.Sidebar = "**Yesterday's Comments Total:** " + comments.ToString();
try
{
	subreddit.Update();  // Sends the subreddit data with the updated sidebar text back to the Reddit API to apply the change.  --Kris
}
catch (RedditControllerException) { }
```
