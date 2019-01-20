```
    ____             __     __ _  __      _   __ ______ ______
   / __ \ ___   ____/ /____/ /(_)/ /_    / | / // ____//_  __/
  / /_/ // _ \ / __  // __  // // __/   /  |/ // __/    / /   
 / _, _//  __// /_/ // /_/ // // /_ _  / /|  // /___   / /    
/_/ |_| \___/ \__,_/ \__,_//_/ \__/(_)/_/ |_//_____/  /_/     
```

Created by Kris Craig.

## Overview

Reddit.NET is a .NET Standard library that provides easy access to the Reddit API with virtually no boilerplate code required. Keep reading below for code examples.

Currently, the library supports 169 of the 205 endpoints currently listed in the API documentation. All of them (except voting and admin-reporting, for obvious reasons) are covered by integration tests and all 327 of the tests are currently passing. All of the most commonly used endpoints are supported.

Reddit.NET is FOSS (MIT license) and was written in C#. It will be available on NuGet once I'm ready to put out the first stable release, which I expect to be very soon. You can check it out now on Github at:
https://github.com/sirkris/Reddit.NET/tree/develop

### Contributors

[Kris Craig](https://www.linkedin.com/in/kriscraig/), [Andrew Hall](https://github.com/ryzngard), and the knowledgeable people over at [r/csharp](https://www.reddit.com/r/csharp/) and [r/redditdev](https://www.reddit.com/r/redditdev/).

### Beta Testers

Kris Craig

## Usage

Reddit.NET can be installed via NuGet.  You can find it at:  https://www.nuget.org/packages/Reddit.NET

To install via the Visual Studio NuGet Package Manager Console (in VS 2017, you'll find it under Tools->NuGet Package Manager->NuGet Package Manager Console):

```nuget
PM> Install-Package Reddit.NET
```

To create a new API instance bound to a specific user's refresh token:

```c#
using Reddit;

...

var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");
```

See below for more detailed usage examples.

## Basic Architecture
Reddit.NET follows a model-controller pattern, with each layer serving a distinct purpose. The model classes/methods (which can be accessed directly if for some reason you don't want to go through the controller) handle all the REST interactions and deserializations. The controller classes/methods organize these API features into a cleaner OO interface, with an emphasis on intuitive design and minimizing any need for messy boilerplate code.

### Models
You'll notice that each model class corresponds to a section in the API documentation. Each method represents one of those endpoints with their respective fields passed as method parameters.

Here's a list of the model classes:

* Account
* Captcha (unused, possibly deprecated; will probably remove it entirely before release)
* Emoji
* Flair
* LinksAndComments
* Listings
* LiveThreads
* Misc
* Moderation
* Modmail
* Multis
* PrivateMessages
* RedditGold (all untested so not currently supported)
* Search
* Subreddits
* Users
* Widgets
* Wiki

Ratelimit handling also occurs in the model layer. If it's less than a minute, the library will automatically wait the specified number of seconds then retry. This can be easily tested using the LiveThread workflow tests. If it's more than a minute, an exception will bubble up and it'll be up to the app developer to decide what to do with it.
Reddit.NET has a built-in limit of no more than 60 requests in any 1-minute period. This is a safety net designed to keep us from inadvertantly violating the API speed limit.

JSON return data is automatically deserialized to its appropriate type. All 170 of these custom types (and yes, it did take fucking forever to write them all) can be found in Models.Structures.

Many model methods also have async counterparts.

### Coordinators
These constitute our controllers and are the classes with which app developers will be doing all or most of their interactions. While the models are structured to closely mirror the API documentation, the coordinators are structured to create an intuitive, object-oriented interface with the API, so you'll notice I took a lot more liberties in this layer.

The coordinators also provide other features, like asynchronous monitoring and automatic caching of certain data sets. I'll get into that stuff in more detail below.

Each coordinator class corresponds to a Reddit object of some kind (subreddit, post, user, etc). Here's a list of the coordinator classes:

#### Account
Provides access to data and endpoints related to the authenticated user.

#### Comment
Represents a Reddit comment and provides access to comment-related data and endpoints.

#### Comments
Represents a set of comment replies to a post or comment. Provides access to all sorts and monitoring. Similar in purpose to SubredditPosts.

#### Dispatch
This is a special controller that provides direct access to the models and keeps them in sync.

#### Flairs
Provides access to data and endpoints related to a subreddit's flairs.

#### LinkPost
Represents a Reddit link post and provides access to related data and endpoints.

#### SelfPost
Represents a Reddit self post and provides access to related data and endpoints.

#### Post
Base class for LinkPost and SelfPost.

#### LiveThread
Represents a Reddit live event. It provides access to related data, endpoints, and monitoring.

#### Modmail
Provides access to data and endpoints related to the authenticated user's modmail.

#### PrivateMessages
Provides access to data and endpoints related to the authenticated user's private messages.

#### Subreddit
Represents a subreddit and provides access to related data and endpoints.

#### SubredditPosts
Represents a set of a subreddit's posts. Provides access to all sorts and monitoring. Similar in purpose to Comments.

#### User
Represents a Reddit user and provides access to related data and endpoints.

#### Wiki
Represents a subreddit's wiki and provides access to related data and endpoints.

#### WikiPage
Represents a wiki page and provides access to related data and endpoints.

Many controller methods also have async counterparts.

## Monitoring

Reddit.NET allows for asynchronous, event-based monitoring of various things. For example, if you're monitoring a subreddit for new posts, the monitoring thread will do its API query once every 1.5 seconds times the total number of current monitoring threads (more on that below). When there's a change in the return data, the library identifies any posts that were added or removed since the last query and includes them in the eventargs. The app developer can then write a custom callback function that will be called whenever the event fires, at which point the dev can do whatever they want with it from there.

Reddit.NET automatically scales the delay between each monitoring query depending on how many things are being monitored. This ensures that the library will average 1 monitoring query every 1.5 seconds, regardless of how many things are being monitored at once, leaving 25% of available bandwidth remaining for any non-monitoring queries you wish to run.
There is theoretically no limit to how many things can be monitored at once, hardware and other considerations notwithstanding. In one of the stress tests, I have it simultaneously montioring 60 posts for new comments. In this case, the delay between each monitoring thread's query is 90 seconds (actually, it's 91.5 because it's also monitoring a subreddit for new posts at the same time).

If you want to see how much load this can handle, check out the PoliceState() stress test. That one was especially fun to write.

Here's a list of things that can currently be monitored by Reddit.NET:

* Monitor a post for new comment replies (any sort).
* Monitor a comment for new comment replies (any sort).
* Monitor a live thread for new/removed updates.
* Monitor a live thread for new/removed contributors.
* Monitor a live thread for any configuration changes.
* Monitor the authenticated user's modmail for new messages (any sort).
* Monitor the authenticated user's modqueue for new items.
* Monitor the authenticated user's inbox for new messages.
* Monitor the authenticated user's unread queue for new messages.
* Monitor the authenticated user's sent messages for new messages.
* Monitor a subreddit for new posts (any sort).
* Monitor a subreddit's wiki for any added/removed pages.
* Monitor a wiki page for new revisions.
* Each monitoring session occurs in its own thread.

## Solution Projects

There are 3 projects in the Reddit.NET solution:

### Example

A simple example .NET Core console application that demonstrates some of Reddit.NET's functionality. If you have Visual Studio 2017, you can run it using debug. You'll need to set your application ID and refresh token in the debug arguments. Only passive operations are demonstrated in this example app; nothing is created or modified in any way.

### Reddit.NET

The main library. This is what the app dev includes in their project.

### Reddit.NETTests

This project contains unit, workflow, and stress tests using MSTest. There are currently 327 tests, all passing (at least, they all pass for me). All of the 169 supported endpoints are included in the tests, except for vote and admin-reporting endpoints.

## Running the Tests

Running the tests is easy. All you need is an app ID and two refresh tokens (the second is used for things like accepting invitations and replying to messages). The first refresh token should belong to a well-established account that wouldn't run into any special ratelimits or restrictions that might make certain endpoints unavailable. The second refresh token's account does not have any special requirements, as it's only used in a handful of workflow tests.
You will also need to specify a test subreddit. It should either be a non-existing subreddit (the tests will create it) or an existing subreddit in which the primary test user is a moderator with full privileges. If you're going with a non-existing subreddit, you'll need to run the test that creates it first; there's a special playlist just for that and obviously you'll only need to do it that first time. The same test subreddit should be reused on subsequent tests since there's no way to delete a subreddit once it's been created.
To set these values, simply edit the Reddit.NETTestsData.xml file. Here's what it looks like:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Rows>
    <Row>
        <AppId>Your_App_ID</AppId>
        <RefreshToken>Primary_Test_User's_Token</RefreshToken>
        <RefreshToken2>Secondary_Test_User's_Token</RefreshToken2>
        <Subreddit>Your_Test_Subreddit</Subreddit>
    </Row>
</Rows>
```

As you can see, it's pretty intuitive in terms of what goes where. Once these values are set and you've created the test subreddit (either via the corresponding unit test or manually with the primary test user having full mod privs), you can run all the tests in any order and as many times as you want after that.

Many tests take less than a second to complete. Others can take up to a few minutes, depending on what's being tested. The workflow tests tend to take longer than the unit tests and the stress tests take longer than the workflow tests. In fact, the stress tests take considerably longer; PoliceState() alone takes roughly 80 minutes to complete.

## Code Examples
```c#
using Reddit;
using Reddit.Coordinators;
using Reddit.Coordinators.EventArgs;
using System;

...

// Create a new Reddit.NET instance.
var r = new RedditAPI("MyAppID", "MyRefreshToken");

// Display the name and cake day of the authenticated user.
Console.WriteLine("Username: " + r.Account.Me.Name);
Console.WriteLine("Cake Day: " + r.Account.Me.Created.ToString("D"));

// Retrieve the authenticated user's recent post history.
// Change "new" to "newForced" if you don't want older stickied profile posts to appear first.
var postHistory = r.Account.Me.PostHistory(sort: "new");

// Retrieve the authenticated user's recent comment history.
var commentHistory = r.Account.Me.CommentHistory(sort: "new");

// Create a new subreddit.
var mySub = r.Subreddit("MyNewSubreddit", "My subreddit's title", "Description", "Sidebar").Create();

// Get info on another subreddit.
var askReddit = r.Subreddit("AskReddit").About();

// Get the top post from a subreddit.
var topPost = askReddit.Posts.Top[0];

// Create a new self post.
var mySelfPost = mySub.SelfPost("Self Post Title", "Self post text.").Submit();

// Create a new link post.
// Use .Submit(resubmit: true) instead to force resubmission of duplicate links.
var myLinkPost = mySub.LinkPost("Link Post Title", "http://www.google.com").Submit();  

// Comment on a post.
var myComment = myLinkPost.Reply("This is my comment.");

// Reply to a comment.
var myCommentReply = myComment.Reply("This is my comment reply.");

// Create a new subreddit, then create a new link post on said subreddit,
// then comment on said post, then reply to said comment, then delete said comment reply.
// Because I said so.
r.Subreddit("MySub", "Title", "Desc", "Sidebar")
.Create()
.SelfPost("MyPost")
.Submit()
.Reply("My comment.")
.Reply("This comment will be deleted.")
.Delete();

// Asynchronously monitor r/AskReddit for new posts.
askReddit.Posts.GetNew();
askReddit.Posts.NewUpdated += C_NewPostsUpdated;
askReddit.Posts.MonitorNew();

public static void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
{
	foreach (var post in e.Added)
	{
		Console.WriteLine("New Post by " + post.Author + ": " + post.Title);
	}
}

// Stop monitoring r/AskReddit for new posts.
askReddit.Posts.MonitorNew();
askReddit.Posts.NewUpdated -= C_NewPostsUpdated;
```

## Code Examples Using Models

The coordinators basically just make calls to the models, which can be accessed directly via the Dispatch controller.  As such, it is possible to bypass the controllers entirely for most things, so long as you don't mind the bulkier code.  This is only recommended for scenarios where the convenience-oriented features of the controllers aren't needed and would just get in the way.  In most cases, it is recommended that you instead use the controllers as demonstrated in the above examples.

Here's how you can do some basic things using the models:

```c#
using Reddit;
using Reddit.Inputs;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.Subreddits;
using Reddit.Inputs.Users;
using Reddit.Things;
using System;
using System.Collections.Generic;

...

// Create a new Reddit.NET instance.
var r = new RedditAPI("MyAppID", "MyRefreshToken");

// Display the name and cake day of the authenticated user.
Console.WriteLine("Username: " + r.Models.Account.Me().Name);
Console.WriteLine("Cake Day: " + r.Models.Account.Me().Created.ToString("D"));

// Retrieve the authenticated user's recent post history.
var postContainer = r.Models.Users.PostHistory(r.Models.Account.Me().Name, "overview", new UsersHistoryInput());
var postHistory = new List<Post>();
foreach (PostChild postChild in postContainer.JSON.Data.Things)
{
    if (postChild.Data != null)
    {
		postHistory.Add(postChild.Data);
    }
}

// Retrieve the authenticated user's recent comment history.
var commentContainer = r.Models.Users.CommentHistory(r.Models.Account.Me().Name, "comments", new UsersHistoryInput());
var commentHistory = new List<Comment>();
foreach (CommentChild commentChild in commentContainer.JSON.Data.Things)
{
    if (commentChild.Data != null)
    {
		commentHistory.Add(commentChild.Data);
    }
}

// Create a new subreddit.
r.Models.Subreddits.SiteAdmin(new SubredditsSiteAdminInput(name: "MyNewSubreddit", title: "My subreddit's title", publicDescription: "Description", description: "Sidebar"));
var mySub = r.Models.Subreddits.About("MyNewSubreddit");

// Get info on another subreddit.
var askReddit = r.Models.Subreddits.About("AskReddit");

// Get the top post from a subreddit.
var topPost = r.Models.Listings.Top(new TimedCatSrListingInput(), "AskReddit").JSON.Data.Things[0].Data;

// Create a new self post.
r.Models.LinksAndComments.Submit(new LinksAndCommentsSubmitInput(title: "Self Post Title", text: "Self post text."));

// Create a new link post.
r.Models.LinksAndComments.Submit(new LinksAndCommentsSubmitInput(title: "Link Post Title", url: "http://www.google.com"));

// Comment on a post.
r.Models.LinksAndComments.Comment(LinksAndCommentsThingInput("This is my comment.", topPost.Name));
```

For more examples, check out the Example and Reddit.NETTests projects.

## How You Can Help

Once I've finished with code reviews/revisions, we'll proceed to beta testing. That will be when I'll be needing people to help by running the tests and posting the results. You can do that now, if you like; they should all pass. Though I'm not seeking beta testers yet, if you do run the tests anyway, please post your results here! So far, I'm the only one who has tested this.

I'm sure there's probably more that I'm forgetting to mention, but I think I've covered all the major points. I'll of course be happy to answer any questions you might have, as well. Thanks for reading!

## Currently Supported Endpoints

GET /api/v1/me  
GET /api/v1/me/karma  
GET /api/v1/me/prefs  
PATCH /api/v1/me/prefs  
GET /api/v1/me/trophies  
GET /prefs/<where>  

POST /api/v1/<subreddit>/emoji_asset_upload_s3.json  
GET /api/v1/<subreddit>/emojis/all  

POST [/r/<subreddit>]/api/clearflairtemplates  
POST [/r/<subreddit>]/api/deleteflair  
POST [/r/<subreddit>]/api/deleteflairtemplate  
POST [/r/<subreddit>]/api/flair  
POST [/r/<subreddit>]/api/flairconfig  
POST [/r/<subreddit>]/api/flaircsv  
GET [/r/<subreddit>]/api/flairlist  
POST [/r/<subreddit>]/api/flairselector  
POST [/r/<subreddit>]/api/flairtemplate  
POST [/r/<subreddit>]/api/flairtemplate_v2  
GET [/r/<subreddit>]/api/link_flair  
GET [/r/<subreddit>]/api/link_flair_v2  
POST [/r/<subreddit>]/api/setflairenabled  
GET [/r/<subreddit>]/api/user_flair  
GET [/r/<subreddit>]/api/user_flair_v2  

POST /api/comment  
POST /api/del  
POST /api/editusertext  
POST /api/hide  
GET [/r/<subreddit>]/api/info  
POST /api/lock  
POST /api/marknsfw  
GET /api/morechildren  
POST /api/report  
POST /api/save  
POST /api/sendreplies  
POST /api/set_contest_mode  
POST /api/set_subreddit_sticky  
POST /api/set_suggested_sort  
POST /api/spoiler  
POST /api/submit  
POST /api/unhide  
POST /api/unlock  
POST /api/unmarknsfw  
POST /api/unsave  
POST /api/unspoiler  
POST /api/vote  

GET /best  
GET /by_id/<names>  
GET [/r/<subreddit>]/comments/<article>  
GET /duplicates/<article>  
GET [/r/<subreddit>]/hot  
GET [/r/<subreddit>]/new  
GET [/r/<subreddit>]/random  
GET [/r/<subreddit>]/rising  
GET [/r/<subreddit>]/<sort>  

POST /api/live/create  
POST /api/live/<thread>/accept_contributor_invite  
POST /api/live/<thread>/close_thread  
POST /api/live/<thread>/delete_update  
POST /api/live/<thread>/edit  
POST /api/live/<thread>/invite_contributor  
POST /api/live/<thread>/leave_contributor  
POST /api/live/<thread>/report  
POST /api/live/<thread>/rm_contributor  
POST /api/live/<thread>/rm_contributor_invite  
POST /api/live/<thread>/set_contributor_permissions  
POST /api/live/<thread>/strike_update  
POST /api/live/<thread>/update  
GET /live/<thread>  
GET /live/<thread>/about  
GET /live/<thread>/contributors  
GET /live/<thread>/updates/<update_id>  

POST /api/block  
POST /api/collapse_message  
POST /api/compose  
POST /api/del_msg  
POST /api/read_all_messages  
POST /api/read_message  
POST /api/uncollapse_message  
POST /api/unread_message  
GET /message/<where>  

GET [/r/<subreddit>]/api/saved_media_text  
GET /api/v1/scopes  

GET [/r/<subreddit>]/about/log  
GET [/r/<subreddit>]/about/<location>  
POST [/r/<subreddit>]/api/accept_moderator_invite  
POST /api/approve  
POST /api/distinguish  
POST /api/ignore_reports  
POST /api/leavecontributor  
POST /api/leavemoderator  
POST /api/remove  
POST /api/unignore_reports  
GET [/r/<subreddit>]/stylesheet  

GET /api/mod/conversations  
POST /api/mod/conversations  
GET /api/mod/conversations/<conversation_id>  
POST /api/mod/conversations/<conversation_id>  
DELETE /api/mod/conversations/<conversation_id>/highlight  
POST /api/mod/conversations/<conversation_id>/highlight  
POST /api/mod/conversations/<conversation_id>/mute  
POST /api/mod/conversations/<conversation_id>/unmute  
GET /api/mod/conversations/<conversation_id>/user  
POST /api/mod/conversations/read  
GET /api/mod/conversations/subreddits  
POST /api/mod/conversations/unread  
GET /api/mod/conversations/unread/count  

POST /api/multi/copy  
GET /api/multi/mine  
POST /api/multi/rename  
GET /api/multi/user/<username>  
DELETE /api/multi/<multipath>  
GET /api/multi/<multipath>  
POST /api/multi/<multipath>  
PUT /api/multi/<multipath>  
GET /api/multi/<multipath>/description  
PUT /api/multi/<multipath>/description  
DELETE /api/multi/<multipath>/r/<srname>  
GET /api/multi/<multipath>/r/<srname>  
PUT /api/multi/<multipath>/r/<srname>  

GET [/r/<subreddit>]/about/<where>  
POST [/r/<subreddit>]/api/delete_sr_banner  
POST [/r/<subreddit>]/api/delete_sr_header  
POST [/r/<subreddit>]/api/delete_sr_icon  
POST [/r/<subreddit>]/api/delete_sr_img  
GET /api/search_reddit_names  
POST /api/search_subreddits  
POST /api/site_admin  
GET [/r/<subreddit>]/api/submit_text  
GET /api/subreddit_autocomplete  
GET /api/subreddit_autocomplete_v2  
POST [/r/<subreddit>]/api/subreddit_stylesheet  
POST /api/subscribe  
POST [/r/<subreddit>]/api/upload_sr_img  
GET /r/<subreddit>/about  
GET /r/<subreddit>/about/edit  
GET /r/<subreddit>/about/rules  
GET /r/<subreddit>/about/traffic  
GET /subreddits/mine/<where>  
GET /subreddits/search  
GET /subreddits/<where>  
GET /users/<where>  

POST /api/block_user  
POST [/r/<subreddit>]/api/friend  
POST /api/report_user  
POST [/r/<subreddit>]/api/setpermissions  
POST [/r/<subreddit>]/api/unfriend  
GET /api/user_data_by_account_ids  
GET /api/username_available  
DELETE /api/v1/me/friends/<username>  
GET /api/v1/me/friends/<username>  
PUT /api/v1/me/friends/<username>  
GET /api/v1/user/<username>/trophies  
GET /user/<username>/about  
GET /user/<username>/<where>  

POST /api/widget  
DELETE /api/widget/<widget_id>  
PUT /api/widget/<widget_id>  
PATCH /api/widget_order/<section>  
GET /api/widgets  

POST [/r/<subreddit>]/api/wiki/alloweditor/<act>  
POST [/r/<subreddit>]/api/wiki/edit  
POST [/r/<subreddit>]/api/wiki/hide  
POST [/r/<subreddit>]/api/wiki/revert  
GET [/r/<subreddit>]/wiki/pages  
GET [/r/<subreddit>]/wiki/revisions  
GET [/r/<subreddit>]/wiki/revisions/<page>  
GET [/r/<subreddit>]/wiki/settings/<page>  
POST [/r/<subreddit>]/wiki/settings/<page>  
GET [/r/<subreddit>]/wiki/<page>  

Total:  169 / 205 (82%)

There are 36 endpoints listed in the API docs that are not currently supported (mostly because I haven't been able to get them to work yet).

Virtually all of the supported endpoints are covered by tests (voting and admin-reporting were manually tested for obvious reasons) and all of those tests are passing.
