# Retrieve Posts From r/all

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Retrieves the latest posts from r/all and groups them by subreddit.

**Warning:** Never call the About() method on a Subreddit instance for a reserved subreddit like r/all or r/popular.  Doing so will throw an exception because they're not actual subreddits.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

IDictionary<string, IList<Post>> Posts = new Dictionary<string, IList<Post>>();
foreach (Post post in reddit.Subreddit("all").Posts.New)
{
	if (!Posts.ContainsKey(post.Subreddit))
	{
		Posts.Add(post.Subreddit, new List<Post>());
	}
	Posts[post.Subreddit].Add(post);
}
```

## Source File

[Retrieve Posts From rall.cs](src/Retrieve%20Posts%20From%20rall.cs)
