# Simple Crosspost

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Cross-posts the day's top post on r/news to r/MySub.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

// Since we only need the posts, there's no need to call .About() on this one.  --Kris
var news = reddit.Subreddit("news");

/*
 * If you call GetTop() directly, the last "t" value you passed will be used for the .Top property.
 * Remember that the .Top property is automatically cached with the results of GetTop(), so we're 
 * only doing one API query for the posts retrieval part.
 * 
 * --Kris
 */
if (news.Posts.GetTop("day")[0].Listing.IsSelf)
{
	var newSelfPost = news.SelfPost(news.Posts.Top[0].Fullname).About().XPostTo("MySub");
}
else
{
	var newLinkPost = news.LinkPost(news.Posts.Top[0].Fullname).About().XPostTo("MySub");
}
```
