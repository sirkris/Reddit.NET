# Monitor Subreddit Comments

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Monitors a subreddit for new comments (all posts).

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System.Collections.Generic;

namespace MonitorSubredditComments
{
	class Program
	{
		public List<Comment> NewComments;
		
		static void Main(string[] args)
		{
			var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");
			
			NewComments = new List<Comment>();

			// Start monitoring the subreddit for new comments and register the callback function.  --Kris
			var subreddit = reddit.Subreddit("AskReddit");
			
			subreddit.Comments.GetNew();  // This call prevents any existing "new"-sorted comments from triggering the update event.  --Kris
			subreddit.Comments.MonitorNew();
			subreddit.Comments.NewUpdated += C_NewCommentsUpdated;

			while(true) { } // Replace this with whatever you've got for a program loop.  The monitoring will run asynchronously in a separate thread.  --Kris

			// Stop monitoring and unregister the callback function.  --Kris
			subreddit.Comments.MonitorNew();
			reddit.Account.Modmail.NewUpdated -= C_NewCommentsUpdated;
		}
		
        private void C_NewCommentsUpdated(object sender, CommentsUpdateEventArgs e)
        {
            foreach (Comment comment in e.Added)
            {
                if (!NewComments.ContainsKey(comment.Fullname))
                {
                    NewComments.Add(comment.Fullname, comment);
                }
            }
        }
	}
}
```

## Source File

[Monitor Modmail.cs](src/Monitor%20Subreddit%20Comments.cs)
