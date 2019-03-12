# Authorize New User (Token Retrieval)

## Author

[Kris Craig](../../../docs/contributors/Kris%20Craig.md)

## Required libraries

### [Reddit.NET Auth Token Retriever](https://www.nuget.org/packages/Reddit.AuthTokenRetrieverLib)

## Overview

Authorize a new Reddit user for your app and return the refresh token.

## Library Installation

In the NuGet Package Manager console:

    Install-Package Reddit.AuthTokenRetrieverLib

## The Code

```c#
using Reddit.AuthTokenRetriever;

...

public static string AuthorizeUser(string appId, string appSecret = null, int port = 8080)
{
	// Create a new instance of the auth token retrieval library.  --Kris
	AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, appSecret, port);
	
	// Start the callback listener.  --Kris
	// Note - Ignore the logging exception message if you see it.  You can use Console.Clear() after this call to get rid of it if you're running a console app.
	authTokenRetrieverLib.AwaitCallback();
	
	// Open the browser to the Reddit authentication page.  Once the user clicks "accept", Reddit will redirect the browser to localhost:8080, where AwaitCallback will take over.  --Kris
	authTokenRetrieverLib.OpenBrowser();
	
	// Replace this with whatever you want the app to do while it waits for the user to load the auth page and click Accept.  --Kris
	while (true) { }
	
	// Cleanup.  --Kris
	authTokenRetrieverLib.StopListening();
	
	return authTokenRetrieverLib.RefreshToken;
}
```
