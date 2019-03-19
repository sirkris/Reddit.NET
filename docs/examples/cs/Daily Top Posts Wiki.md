# Daily Top Posts Wiki

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Retrieves the top posts of the last 24 hours (up to 10) and posts them to the subreddit's wiki.

Here's the basic workflow:

1. Grab the top posts from the last 24 hours.  Don't include older posts that appear in the results.

2. Create a new wiki page for today and post links to the top posts, if there are any.

3. Update the Index wiki page with a link to the page we just created.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;
using Reddit.Controllers;
using Reddit.Inputs;
using System;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

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
```

## Source File

[Daily Top Posts Wiki.cs](src/Daily%20Top%20Posts%20Wiki.cs)
