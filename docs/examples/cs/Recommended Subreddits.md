# Retrieve Recommended Subreddits

## Authors

[jpsak09](../../../docs/contributors/jpsak09.md)

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Retrieves a list of recommended subreddits related to r/MySub.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;
using Reddit.Inputs.Subreddits;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

var recommended = reddit.Models.Subreddits.Recommended("MySub", new SubredditsRecommendInput());
```

## Source File

[Recommended Subreddits.cs](src/Recommended%20Subreddits.cs)
