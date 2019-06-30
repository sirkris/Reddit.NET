# Create a Link Post that Points to a Self Post

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Posts a link to the week's top post on r/AskReddit.  Note that all posts on that sub are self posts.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

// Retrieve the SelfPost we want and link to it on r/MySub.  The host will automatically be replaced with np.reddit.com and r/AskReddit will be credited in the title.  --Kris
var newLinkPost = reddit.Subreddit("AskReddit").Posts.GetTop(t: "week")[0].XPostToAsLink("MySub");
```

## Source File

[Link to SelfPost.cs](src/Link%20to%20SelfPost.cs)
