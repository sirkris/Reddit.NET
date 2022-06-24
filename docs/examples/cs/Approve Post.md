# Approve a Post

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

How to approve a post on a subreddit you moderate.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

// Approves the top post on r/MySubreddit
reddit.Subreddit("MySubreddit").Posts.Top[0].Approve();
```

## Source File

[Approve Post.cs](src/Approve%20Post.cs)
