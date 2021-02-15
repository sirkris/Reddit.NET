# Front Page Posts

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Retrieves a list containing the posts that the authenticated user would see on the Reddit front page.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

var frontPagePosts = reddit.FrontPage;
//var frontPagePosts = reddit.Subreddit().Posts.Best;  // Use this instead if you're using any version of Reddit.NET prior to 1.5.
```

## Source File

[Front Page Posts.cs](src/Front%20Page%20Posts.cs)
