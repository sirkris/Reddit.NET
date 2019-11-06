# Use a Permalink to Retrieve a Reddit Post

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Extracts the Post ID from a Permalink URL string, then uses that to retrieve the corresponding post from the API.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;
using Reddit.Controllers;
using System;
using System.Text.RegularExpressions;

...

public class GetPost
{
	private RedditAPI Reddit { get; set; }
	
    public GetPost(string appId, string refreshToken)
    {
        Reddit = new RedditAPI(appId, refreshToken);
    }
	
	public Post FromPermalink(string permalink)
	{
		// Get the ID from the permalink, then preface it with "t3_" to convert it to a Reddit fullname.  --Kris
		Match match = Regex.Match(permalink, @"\/comments\/([a-z0-9]+)\/");
		
		string postFullname = "t3_" + (match != null && match.Groups != null && match.Groups.Count >= 2 
			? match.Groups[1].Value 
			: "");
		if (postFullname.Equals("t3_"))
		{
			throw new Exception("Unable to extract ID from permalink.");
		}
		
		// Retrieve the post and return the result.  --Kris
		return Reddit.Post(Reddit.Models, postFullname).About();
	}
}
```

## Source File

[Get Post From Permalink.cs](src/Get%20Post%20From%20Permalink.cs)
