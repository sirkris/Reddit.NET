# Mark a Post as Spam

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

How to mark a post as spam on a subreddit you moderate.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

// Marks the top post on r/MySubreddit as spam
reddit.Subreddit("MySubreddit").Posts.Top[0].Remove(spam: true);
```

## Source File

[Mark as Spam.cs](src/Mark%20as%20Spam.cs)
