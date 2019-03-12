# Set Link Flair

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Search a given subreddit for the phrase, "Bernie Sanders".  If no results are found, try searching all subreddits.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;
using Reddit.Controllers;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

List<Post> posts = reddit.Subreddit("MySub").Search("Bernie Sanders");  // Search r/MySub
if (posts.Count == 0)
{
	posts = reddit.Search("Bernie Sanders");  // Search r/all
}
```
