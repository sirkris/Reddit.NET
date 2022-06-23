# Profile Post

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

This demonstrates how to add a new link post to your Reddit profile.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

var profileLinkPost = reddit.Subreddit($"u_{reddit.Account.Me.Name}").LinkPost("https://xkcd.com/308/").Submit();
```

## Source File

[Profile Post.cs](src/Profile%20Post.cs)
