# Set Link Flair

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Creates a new link post and assigns a flair to it.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

reddit.Subreddit("MySub").LinkPost("Reddit.NET", "https://www.nuget.org/packages/Reddit").Submit().SetFlair("NuGet Package");
```
