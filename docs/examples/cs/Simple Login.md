# Simple Login

## Required libraries:

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

This demonstrates how to login to the Reddit API via OAuth.  This assumes you already have an access token and refresh token.  If not, you can checkout [this video](https://www.youtube.com/watch?v=xlWhLyVgN2s) or consult [Reddit's docs](https://github.com/reddit-archive/reddit/wiki/OAuth2) for assistance.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

Console.WriteLine("Username: " + reddit.Account.Me.Name);

Console.WriteLine("Cake Day: " + reddit.Account.Me.Created.ToString("D"));
```
