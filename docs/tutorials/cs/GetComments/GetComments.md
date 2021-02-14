# Tutorial: Retrieve Comments Tree

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

[Reddit.NET](https://github.com/sirkris/Reddit.NET)

| Table of Contents                                                                      |
|:---------------------------------------------------------------------------------------|
| [Overview](#overview)                                                                  |
| [Library Installation](#library-installation)                                          |
| [Create the Project](#create-the-project)                                              |
| [The Workflow Class](#the-workflow-class)                                              |
| [Initialize](#initialize)                                                              |
| [Main Loop](#main-loop)                                                                |
| [WARNING: Reddit Truncates Comment Results](#warning-reddit-truncates-comment-results) |
| [Display Comment Info](#display-comment-info)                                          |
| [Navigating Reply Trees](#navigating-reply-trees)                                      |
| [Handling MoreChildren](#handling-morechildren)                                        |
| [The Finished Solution](#the-finished-solution)                                        |

## Overview

In this tutorial, we will learn how to display a post's full comments tree up to the limit allowed by Reddit.

## Library Installation

In the NuGet Package Manager console:
    
    Install-Package Reddit

## Create the Project

Open Visual Studio and create a new .NET Core Console Application.  Let's call the project "GetRedditComments".

### Handle Command-Line Arguments

When running the console application, the user will need to provide 2 arguments:  An AppID and a Refresh Token.  Assuming you've already [created the app on Reddit](https://www.reddit.com/prefs/apps/) and [obtained a Refresh Token](https://github.com/sirkris/Reddit.NET/blob/master/docs/examples/cs/Authorize%20New%20User.md), that shouldn't be a problem for you.

### Program.cs

So let's start building our Program.cs file.  This is very straightforward, as all we'll be doing is parsing the command-line arguments and calling the workflow.  Here's how it should look:

```c#
using System;

namespace GetRedditComments
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: GetRedditComments <Reddit App ID> <Reddit Refresh Token> [Reddit Access Token]");
            }
            else
            {
                Workflow workflow = new Workflow(args);
                workflow.Run();
            }
        }
    }
}
```

## The Workflow Class

This will be where all the action takes place.  First, we'll need to add the necessary using statements:

```c#
using Reddit;
using Reddit.Controllers;
using Things = Reddit.Things;
using System;
using System.Collections.Generic;
```

Now let's build the wireframe:

```c#
namespace ELIZA
{
    public class Workflow
    {
        private RedditClient Reddit;
        private Post Post;
        private int NumComments = 0;
        private HashSet<string> CommentIds;

        public Workflow(string[] args)
        {
            
        }
    }
}
```

The Reddit property is where we'll store our RedditClient instance.  Post is the post we'll be retrieving comments on.  NumComments and CommentIds are used for keeping track of comments as we find them.

## Initialize

Now it's time to initialize our properties.  Put this code in your Workflow constructor:

```c#
string appId = args[0];
string refreshToken = args[1];
string accessToken = (args.Length > 2 ? args[2] : null);

Reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);
Post = Reddit.Subreddit("news").Post("t3_2lt3d0").About();
CommentIds = new HashSet<string>();
```

## Main Loop

In the Workflow class, create a new void method called Run with no arguments:

```c#
public void Run()
{
    Console.WriteLine(Post.Title);
    Console.WriteLine("There are " + Post.Listing.NumComments + " comments:");

    IterateComments(Post.Comments.GetNew(limit: 500));

    Console.WriteLine("Total Comments Iterated: " + NumComments);
    Console.WriteLine("Total Unique Comments: " + CommentIds.Count);
}
```

Fairly simple.  We start by displaying the post title and the number ofcomments (as reported in the post data sent by the Reddit API).  Then we call IterateComments, which will do the heavy lifting.  Then we end by displaying the counts.

## WARNING: Reddit Truncates Comment Results

I'm not exactly sure how this works, but if you're dealing with a thread that has a lot of comments, there's a good chance you're not going to be able to retrieve some or even most of them.  This is true both with the web UI and the API and there is no workaround.

The limit for most users is 500 comments.  That limit is reportedly increased to 1,500 for Reddit Gold subscribers.  Additionally, possibly due to removed comments, the actual number of comments retrieved is likely to be slightly below this limit.

This makes testing the confusing MoreChildren functionality a lot more difficult, since most posts either don't have enough top-level comments or the reply trees eat up the limit first.  If you notice any odd or unexpected behavior here, especially if the results don't match those returned by other libraries like PRAW for the same query, please don't hesitate to open an [Issue](https://github.com/sirkris/Reddit.NET/issues/new/choose) so we can investigate.

For more information, check out this link:

[Is there any way to see more than 500 comments in a thread?](https://www.reddit.com/r/help/comments/2blbdm/is_there_any_way_to_see_more_than_500_comments_in/)

## Display Comment Info

Let's whip together a method to handle displaying a comment:

```c#
private void ShowComment(Comment comment, int depth = 0)
{
    if (comment == null || string.IsNullOrWhiteSpace(comment.Author))
    {
        return;
    }

    NumComments++;
    if (!CommentIds.Contains(comment.Id))
    {
        CommentIds.Add(comment.Id);
    }

    if (depth.Equals(0))
    {
        Console.WriteLine("---------------------");
    }
    else
    {
        for (int i = 1; i <= depth; i++)
        {
            Console.Write("> ");
        }
    }

    Console.WriteLine("[" + comment.Author + "] " + comment.Body);
}
```

## Navigating Reply Trees

This was greatly simplified starting in 1.4.  Since Comment.Replies automatically initiates the necessary API call if the data isn't already present, retrieving a comments tree is just a simple matter of recursion:

```c#
private void IterateComments(IList<Comment> comments, int depth = 0)
{
    foreach (Comment comment in comments)
    {
        ShowComment(comment, depth);
        IterateComments(comment.Replies, (depth + 1));
        IterateComments(GetMoreChildren(comment), depth);
    }
}
```

## Handling MoreChildren

This is easily the most confusing aspect of retrieving comments through the Reddit API.  Basically, one of the top-level comments will contain a More object with a list of comment IDs that were excluded but can be retrieved with a MoreChildren query.  I did my best to simplify this in 1.4.

However, since the limit is almost always reached before all the top-level posts in the set have been reached, this usually won't yield any more comments.  So here's the code:

```c#
private IList<Comment> GetMoreChildren(Comment comment)
{
    List<Comment> res = new List<Comment>();
    if (comment.More == null)
    {
        return res;
    }

    foreach (Things.More more in comment.More)
    {
        foreach (string id in more.Children)
        {
            if (!CommentIds.Contains(id))
            {
                res.Add(Post.Comment("t1_" + id).About());
            }
        }
    }

    return res;
}
```

In here, we're gathering the "more" comment ids, making sure to exclude any we've already retrieved.  Once we have the comment ids, we pass them back to IterateComments.

## The Finished Solution

The complete source code can be found here:  **https://github.com/Reddit-NET/GetComments**
