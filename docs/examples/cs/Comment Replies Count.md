# Comment Replies Count

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Starting in 1.4, it is possible to get the number of direct replies to a comment without making a separate API call.  Here's how.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

// Get the number of direct replies to the top comment on the top post of r/AskReddit.  --Kris
int? numReplies = reddit.Subreddit("AskReddit").Posts.Top[0].Comments.Top[0].NumReplies;
```

## Source File

[Comment Replies Count.cs](src/Comment%20Replies%20Count.cs)
