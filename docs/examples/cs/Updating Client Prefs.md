# Updating Client Prefs

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET](https://github.com/sirkris/Reddit.NET)

## Overview

Updates the authenticated user's preferences to allow viewing of NSFW content.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit

## The Code

```c#
using Reddit;

...

var reddit = new RedditClient("YourRedditAppID", "YourBotUserRefreshToken");

// Retrieve the current account preferences.  --Kris
var prefs = reddit.Account.Prefs();

// Modify our local copy of the preferences by setting the Over18 property to true.  --Kris
prefs.Over18 = true;

// Send our modified preferences instance back to the Reddit API.  --Kris
reddit.Account.UpdatePrefs(new AccountPrefsSubmit(prefs, null, false, null));
```

## Source File

[Updating Client Prefs.cs](src/Updating%20Client%20Prefs.cs)
