# Retrieve a LinkPost URL or SelfPost Body From a Reddit Post

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Retrieves the top posts from r/movies.  For each post, the title is output on a line by itself, followed by either the URL if it's a link post or the body if it's a self post.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;
using Reddit.Controllers;

...

class Program
{
	static void Main(string[] args)
	{
		(new PostsHelper("YourRedditAppID", "YourBotUserRefreshToken")).ListPosts("movies");
	}
}

public class PostsHelper
{
	private RedditAPI Reddit { get; set; }
	
    public PostsHelper(string appId, string refreshToken)
    {
        Reddit = new RedditAPI(appId, refreshToken);
    }
	
	// Display the title of each post followed by the link URL (if it's a link post) or the Body (if it's a self post).  --Kris
	public void ListPosts(string subreddit)
	{
		foreach (Post post in Reddit.Subreddit(subreddit).Posts.Hot)
		{
			Console.WriteLine("Title: " + post.Title);
			
			// Both LinkPost and SelfPost derive from the Post class.  --Kris
			Console.WriteLine(post.Listing.IsSelf
				? "Body: " + ((SelfPost)post).SelfText
				: "URL: " + ((LinkPost)post).URL);
		}
	}
}
```

## Source File

[Get LinkPost URL & SelfPost Body From Post.cs](src/Get%20LinkPost%20URL%20%26%20SelfPost%20Body%20From%20Post.cs)
