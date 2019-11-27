# Set User Flair

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Assigns the flair "Fucking Genius" to the user "KrisCraig" on a given subreddit.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

reddit.Subreddit("MySub").Flairs.CreateUserFlair("KrisCraig", "Fucking Genius");
```

## Source File

[User Flair.cs](src/Set%20User%20Flair.cs)
