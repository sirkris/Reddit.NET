using Newtonsoft.Json;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Example.exe <Reddit App ID> <Reddit Refresh Token> [Reddit Access Token]");
            }
            else
            {
                string appId = args[0];
                string refreshToken = args[1];
                string accessToken = (args.Length > 2 ? args[2] : null);

                // Initialize the API library instance.  --Kris
                RedditAPI reddit = new RedditAPI(appId, refreshToken, accessToken);

                // Get info on the Reddit user authenticated by the OAuth credentials.  --Kris
                User me = reddit.Account.Me;

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                // Get post and comment histories (note that pinned profile posts appear at the top even on new sort; use "newForced" sort as a workaround).  --Kris
                List<Post> postHistory = me.PostHistory(sort: "newForced");
                List<Comment> commentHistory = me.CommentHistory(sort: "new");

                Console.WriteLine("Most recent post: " + postHistory[0].Title);
                Console.WriteLine("Most recent comment: " + commentHistory[0].Body);

                // Create a new subreddit.  --Kris
                //Subreddit newSub = reddit.Subreddit("RDNBotSub", "Test Subreddit", "Test sub created by Reddit.NET", "My sidebar.").Create();

                // Get best posts.  --Kris
                List<Post> bestPosts = reddit.Subreddit().Posts.Best;

                Console.WriteLine("Current best post (by " + bestPosts[0].Author + "): [" + bestPosts[0].Subreddit + "] " + bestPosts[0].Title);

                // Get info about a subreddit.  --Kris
                Subreddit sub = reddit.Subreddit("AskReddit").About();

                Console.WriteLine("Subreddit Name: " + sub.Name);
                Console.WriteLine("Subreddit Fullname: " + sub.Fullname);
                Console.WriteLine("Subreddit Title: " + sub.Title);
                Console.WriteLine("Subreddit Description: " + sub.Description);

                // Get submit text.  --Kris
                /*Console.WriteLine("Submit text: " + sub.SubmitText.ToString());

                // Get moderators.  --Kris
                List<Moderator> moderators = sub.GetModerators();

                Console.WriteLine("Moderators:");
                Console.WriteLine("{");
                int i = 0;
                foreach (Moderator moderator in moderators)
                {
                    Console.WriteLine("\t" + moderator.Name);

                    i++;
                    if (i > 10 && moderators.Count > 12)
                    {
                        Console.WriteLine("(and " + (moderators.Count - 10).ToString() + " others)");
                        break;
                    }
                }
                Console.WriteLine("}");*/

                // Get approved submitters (requires mod access).  --Kris
                /*List<SubredditUser> contributors = reddit.Subreddit("StillSandersForPres").GetContributors();

                Console.WriteLine("Approved Submitters:");
                Console.WriteLine("{");
                i = 0;
                foreach (SubredditUser contributor in contributors)
                {
                    Console.WriteLine("\t" + contributor.Name);

                    i++;
                    if (i > 10 && contributors.Count > 12)
                    {
                        Console.WriteLine("(and " + (contributors.Count - 10).ToString() + " others)");
                        break;
                    }
                }
                Console.WriteLine("}");*/

                // Get approved submitters for the wiki (requires mod access).  --Kris
                /*List<SubredditUser> wikiContributors = reddit.Subreddit("StillSandersForPres").GetWikiContributors();

                Console.WriteLine("Approved Wiki Submitters:");
                Console.WriteLine("{");
                i = 0;
                foreach (SubredditUser wikiContributor in wikiContributors)
                {
                    Console.WriteLine("\t" + wikiContributor.Name);

                    i++;
                    if (i > 10 && wikiContributors.Count > 12)
                    {
                        Console.WriteLine("(and " + (wikiContributors.Count - 10).ToString() + " others)");
                        break;
                    }
                }
                Console.WriteLine("}");*/

                // Get muted users (requires mod access).  --Kris
                /*List<SubredditUser> mutedUsers = reddit.Subreddit("StillSandersForPres").GetMutedUsers();

                Console.WriteLine("Muted Users:");
                Console.WriteLine("{");
                i = 0;
                foreach (SubredditUser mutedUser in mutedUsers)
                {
                    Console.WriteLine("\t" + mutedUser.Name);

                    i++;
                    if (i > 10 && mutedUsers.Count > 12)
                    {
                        Console.WriteLine("(and " + (mutedUsers.Count - 10).ToString() + " others)");
                        break;
                    }
                }
                Console.WriteLine("}");*/

                // Get wiki-banned users (requires mod access).  --Kris
                //List<BannedUser> wikiBannedUsers = reddit.Subreddit("StillSandersForPres").GetWikiBannedUsers();

                //Console.WriteLine("Wiki-banned users retrieved: " + wikiBannedUsers.Count);

                // Get banned users (requires mod access).  --Kris
                //List<BannedUser> bannedUsers = reddit.Subreddit("StillSandersForPres").GetBannedUsers();

                //Console.WriteLine("Banned users retrieved: " + bannedUsers.Count);

                // Get new posts from this subreddit.  --Kris
                List<Post> newPosts = sub.Posts.New;

                Console.WriteLine("Retrieved " + newPosts.Count.ToString() + " new posts.");

                // Monitor new posts on this subreddit for a minute.  --Kris
                Console.WriteLine("Monitoring " + sub.Name + " for new posts....");

                sub.Posts.NewUpdated += C_NewPostsUpdated;
                sub.Posts.MonitorNew();  // Toggle on.

                // But wait, there's more!  We can monitor posts on multiple subreddits at once (delay is automatically multiplied to keep us under speed the limit).  --Kris
                Subreddit funny = reddit.Subreddit("funny");
                Subreddit worldnews = reddit.Subreddit("worldnews");

                // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in.  --Kris
                funny.Posts.GetNew();
                worldnews.Posts.GetNew();

                Console.WriteLine("Monitoring funny for new posts....");

                funny.Posts.NewUpdated += C_NewPostsUpdated;
                funny.Posts.MonitorNew();  // Toggle on.

                Console.WriteLine("Monitoring worldnews for new posts....");

                worldnews.Posts.NewUpdated += C_NewPostsUpdated;
                worldnews.Posts.MonitorNew();  // Toggle on.

                DateTime start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                // Stop monitoring new posts.  --Kris
                sub.Posts.MonitorNew();  // Toggle off.
                sub.Posts.NewUpdated -= C_NewPostsUpdated;

                funny.Posts.MonitorNew();  // Toggle off.
                funny.Posts.NewUpdated -= C_NewPostsUpdated;

                worldnews.Posts.MonitorNew();  // Toggle off.
                worldnews.Posts.NewUpdated -= C_NewPostsUpdated;

                Console.WriteLine("Done monitoring!");

                // Grab today's top post in AskReddit and monitor its new comments.  --Kris
                Post post = sub.Posts.GetTop("day")[0];
                post.Comments.GetNew();

                Console.WriteLine("Monitoring today's top post on AskReddit....");

                post.Comments.MonitorNew();  // Toggle on.
                post.Comments.NewUpdated += C_NewCommentsUpdated;

                start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                post.Comments.MonitorNew();  // Toggle off.
                post.Comments.NewUpdated -= C_NewCommentsUpdated;

                Console.WriteLine("Done monitoring!");

                // Now let's monitor r/all for a bit.  --Kris
                Subreddit all = reddit.Subreddit("all");
                all.Posts.GetNew();

                Console.WriteLine("Monitoring r/all for new posts....");

                all.Posts.MonitorNew();  // Toggle on.
                all.Posts.NewUpdated += C_NewPostsUpdated;

                start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                all.Posts.MonitorNew();  // Toggle off.
                all.Posts.NewUpdated -= C_NewPostsUpdated;

                Console.WriteLine("Done monitoring!");

                // Temporary code - Verify I've got all the models right and catalogue their returns.  Will then proceed to writing unit tests.  --Kris
                /*
                File.WriteAllText("Account.Trophies.json", JsonConvert.SerializeObject(reddit.Models.Account.Trophies()));
                File.WriteAllText("Account.PrefsFriends.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("friends")));
                File.WriteAllText("Account.PrefsBlocked.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("blocked")));
                File.WriteAllText("Account.PrefsMessaging.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("messaging")));
                File.WriteAllText("Account.PrefsTrusted.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs("trusted")));

                File.WriteAllText("Emoji.All.json", JsonConvert.SerializeObject(reddit.Models.Emoji.All("WayOfTheBern")));
                
                File.WriteAllText("Flair.UserFlair.json", JsonConvert.SerializeObject(reddit.Models.Flair.UserFlair("SandersForPresident")));
                File.WriteAllText("Flair.UserFlairV2.json", JsonConvert.SerializeObject(reddit.Models.Flair.UserFlairV2("SandersForPresident")));
                
                File.WriteAllText("Listings.Best.json", JsonConvert.SerializeObject(reddit.Models.Listings.Best(null, null, true)));
                File.WriteAllText("Listings.BestNoCats.json", JsonConvert.SerializeObject(reddit.Models.Listings.Best(null, null, false)));
                File.WriteAllText("Listings.BestWithSrDetail.json", JsonConvert.SerializeObject(reddit.Models.Listings.Best(null, null, true, 0, 25, "all", true)));
                File.WriteAllText("Listings.GetByNames.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetByNames("t3_9gaze5,t3_9mfizx")));
                File.WriteAllText("Listings.GetComments.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetComments("9gaze5", 0, false, false, "top", true, 0)));
                File.WriteAllText("Listings.GetCommentsWithEditsAndMoreAndTruncate.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetComments("8gmf99", 0, true, true, "top", true, 50)));
                File.WriteAllText("Listings.GetCommentsWithContext.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetComments("8gmf99", 8, true, true, "top", true, 0, 
                    "FloridaMan", "dyd2vtc")));
                File.WriteAllText("Listings.Hot.json", JsonConvert.SerializeObject(reddit.Models.Listings.Hot("GLOBAL", null, null, true)));
                File.WriteAllText("Listings.New.json", JsonConvert.SerializeObject(reddit.Models.Listings.New(null, null, true, "StillSandersForPres")));
                File.WriteAllText("Listings.Random.json", JsonConvert.SerializeObject(reddit.Models.Listings.Random("Facepalm")));
                File.WriteAllText("Listings.RandomNoSub.json", JsonConvert.SerializeObject(reddit.Models.Listings.Random()));
                File.WriteAllText("Listings.Rising.json", JsonConvert.SerializeObject(reddit.Models.Listings.Rising(null, null, true)));
                File.WriteAllText("Listings.Top.json", JsonConvert.SerializeObject(reddit.Models.Listings.Top("all", null, null, true)));
                File.WriteAllText("Listings.TopDay.json", JsonConvert.SerializeObject(reddit.Models.Listings.Top("day", null, null, true)));
                File.WriteAllText("Listings.Controversial.json", JsonConvert.SerializeObject(reddit.Models.Listings.Controversial("all", null, null, true)));

                File.WriteAllText("Misc.Scopes.json", JsonConvert.SerializeObject(reddit.Models.Misc.Scopes()));
                
                File.WriteAllText("Moderation.GetLog.json", JsonConvert.SerializeObject(reddit.Models.Moderation.GetLog(null, null, "StillSandersForPres")));
                File.WriteAllText("Moderation.ModQueueReports.json", JsonConvert.SerializeObject(reddit.Models.Moderation.ModQueue("reports", null, null, "links", "StillSandersForPres")));
                File.WriteAllText("Moderation.ModQueueSpam.json", JsonConvert.SerializeObject(reddit.Models.Moderation.ModQueue("spam", null, null, "comments", "StillSandersForPres")));
                File.WriteAllText("Moderation.ModQueue.json", JsonConvert.SerializeObject(reddit.Models.Moderation.ModQueue("modqueue", null, null, "links", "StillSandersForPres")));
                File.WriteAllText("Moderation.ModQueueUnmoderated.json", JsonConvert.SerializeObject(reddit.Models.Moderation.ModQueue("unmoderated", null, null, "links", "StillSandersForPres")));
                File.WriteAllText("Moderation.ModQueueEdited.json", JsonConvert.SerializeObject(reddit.Models.Moderation.ModQueue("edited", null, null, "links", "StillSandersForPres")));
                File.WriteAllText("Moderation.Approve.json", JsonConvert.SerializeObject(reddit.Models.Moderation.Approve("t3_9gaze5")));

                File.WriteAllText("Modmail.Subreddits.json", JsonConvert.SerializeObject(reddit.Models.Modmail.Subreddits()));
                File.WriteAllText("Modmail.UnreadCount.json", JsonConvert.SerializeObject(reddit.Models.Modmail.UnreadCount()));

                File.WriteAllText("Multis.Mine.json", JsonConvert.SerializeObject(reddit.Models.Multis.Mine(false)));
                File.WriteAllText("Multis.MineWithExpandSrs.json", JsonConvert.SerializeObject(reddit.Models.Multis.Mine(true)));
                File.WriteAllText("Multis.User.json", JsonConvert.SerializeObject(reddit.Models.Multis.User("KrisCraig", false)));
                File.WriteAllText("Multis.Get.json", JsonConvert.SerializeObject(reddit.Models.Multis.Get("user/KrisCraig/m/unitedprogressives", false)));
                File.WriteAllText("Multis.GetDescription.json", JsonConvert.SerializeObject(reddit.Models.Multis.GetDescription("user/KrisCraig/m/unitedprogressives")));
                File.WriteAllText("Multis.GetMultiSub.json", JsonConvert.SerializeObject(reddit.Models.Multis.GetMultiSub("user/KrisCraig/m/unitedprogressives", "StillSandersForPres")));

                // The Search endpoints work but they never return any results.  According to the API docs, this should work.  --Kris
                //File.WriteAllText("Search.GetSearch.json", JsonConvert.SerializeObject(reddit.Models.Search.GetSearch(null, null, null, false, "Sanders", false,
                //    "new", "all", "SandersForPresident")));
                //File.WriteAllText("Search.GetSearchWithIncludeFacets.json", JsonConvert.SerializeObject(reddit.Models.Search.GetSearch(null, null, null, true, "Bernie Sanders", true,
                //    "new", "all", "WayOfTheBern")));

                File.WriteAllText("Subreddits.About.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.About("WayOfTheMueller")));
                File.WriteAllText("Subreddits.AboutBanned.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.About("banned", null, null, null, false, "StillSandersForPres")));
                File.WriteAllText("Subreddits.AboutMuted.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.About("muted", null, null, null, false, "StillSandersForPres")));
                File.WriteAllText("Subreddits.AboutWikiBanned.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.About("wikibanned", null, null, null, false, "StillSandersForPres")));
                File.WriteAllText("Subreddits.AboutContributors.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.About("contributors", null, null, null, false, "StillSandersForPres")));
                File.WriteAllText("Subreddits.AboutWikiContributors.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.About("wikicontributors", null, null, null, false, "StillSandersForPres")));
                File.WriteAllText("Subreddits.AboutModerators.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.About("moderators", null, null, null, false, "StillSandersForPres")));
                File.WriteAllText("Subreddits.Recommend.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Recommend("t5_3ff8l,t5_3fblp", "t5_2cneq,t5_2zbq7,t5_38unr", false)));
                File.WriteAllText("Subreddits.SearchRedditNames.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SearchRedditNames(false, true, true, "Shitty")));
                File.WriteAllText("Subreddits.SubmitText.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SubmitText("WayOfTheBern")));
                File.WriteAllText("Subreddits.SearchSubreddits.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SearchSubreddits(false, true, true, "Shitty")));
                File.WriteAllText("Subreddits.Rules.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Rules("WayOfTheBern")));
                File.WriteAllText("Subreddits.Traffic.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Traffic("StillSandersForPres")));
                //File.WriteAllText("Subreddits.Sidebar.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Sidebar("WayOfTheMueller")));
                //File.WriteAllText("Subreddits.Sticky1.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Sticky("WayOfTheBern", 1)));
                //File.WriteAllText("Subreddits.Sticky2.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Sticky("WayOfTheBern", 2)));
                File.WriteAllText("Subreddits.MineSubscriber.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Mine("subscriber", null, null, false)));
                File.WriteAllText("Subreddits.MineContributor.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Mine("contributor", null, null, false)));
                File.WriteAllText("Subreddits.MineModerator.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Mine("moderator", null, null, false)));
                File.WriteAllText("Subreddits.Search.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Search(null, null, "Sanders", false, "relevance")));
                File.WriteAllText("Subreddits.SearchWithShowUsers.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Search(null, null, "Sanders", true, "relevance")));
                File.WriteAllText("Subreddits.GetPopular.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Get("popular", null, null, false)));
                File.WriteAllText("Subreddits.GetNew.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Get("new", null, null, false)));
                File.WriteAllText("Subreddits.GetDefault.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Get("default", null, null, false)));
                File.WriteAllText("Subreddits.GetUserSubredditsPopular.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.GetUserSubreddits("popular", null, null, false)));
                File.WriteAllText("Subreddits.GetUserSubredditsNew.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.GetUserSubreddits("new", null, null, false)));

                File.WriteAllText("Users.UserDataByAccountIds.json", JsonConvert.SerializeObject(reddit.Models.Users.UserDataByAccountIds("t2_6vsit,t2_2cclzaxt")));
                //File.WriteAllText("Users.UsernameAvailable.json", JsonConvert.SerializeObject(reddit.Models.Users.UsernameAvailable("KrisCraig")));
                //File.WriteAllText("Users.UsernameAvailable2.json", JsonConvert.SerializeObject(reddit.Models.Users.UsernameAvailable(DateTime.Now.ToString("ddd-dd-MMM-yyyy-ffff"))));
                //File.WriteAllText("Users.UsernameAvailable3.json", JsonConvert.SerializeObject(reddit.Models.Users.UsernameAvailable("ThisUserDoesNotExistAndIfHeDoesThenHeProbablyFucksChickensOrSomething")));
                File.WriteAllText("Users.UsernameAvailable.json", reddit.Models.Users.UsernameAvailable("KrisCraig").ToString());
                File.WriteAllText("Users.UsernameAvailable2.json", reddit.Models.Users.UsernameAvailable(DateTime.Now.ToString("ddd-dd-MMM-yyyy-ffff")).ToString());
                File.WriteAllText("Users.UsernameAvailable3.json", reddit.Models.Users.UsernameAvailable("ThisUserDoesNotExistAndIfHeDoesThenHeProbablyFucksChickensOrSomething").ToString());
                File.WriteAllText("Users.Trophies.json", JsonConvert.SerializeObject(reddit.Models.Users.Trophies("KrisCraig")));
                File.WriteAllText("Users.About.json", JsonConvert.SerializeObject(reddit.Models.Users.About("KrisCraig")));
                //File.WriteAllText("Users.HistoryOverviewLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "overview", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistoryOverviewComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "overview", 10, "given", "top", "all", "comments", null, null, false)));
                //File.WriteAllText("Users.HistorySubmittedLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "submitted", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistorySubmittedComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "submitted", 10, "given", "top", "all", "comments", null, null, false)));
                //File.WriteAllText("Users.HistoryCommentsLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "comments", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistoryCommentsComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "comments", 10, "given", "top", "all", "comments", null, null, false)));
                //File.WriteAllText("Users.HistoryUpvotedLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "upvoted", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistoryUpvotedComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "upvoted", 10, "given", "top", "all", "comments", null, null, false)));
                //File.WriteAllText("Users.HistoryDownvotedLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "downvoted", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistoryDownvotedComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "downvoted", 10, "given", "top", "all", "comments", null, null, false)));
                //File.WriteAllText("Users.HistoryHiddenLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "hidden", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistoryHiddenComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "hidden", 10, "given", "top", "all", "comments", null, null, false)));
                //File.WriteAllText("Users.HistorySavedLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "saved", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistorySavedComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "saved", 10, "given", "top", "all", "comments", null, null, false)));
                //File.WriteAllText("Users.HistoryGildedLinks.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "gilded", 10, "given", "top", "all", "links", null, null, false)));
                //File.WriteAllText("Users.HistoryGildedComments.json", JsonConvert.SerializeObject(reddit.Models.Users.History("KrisCraig", "gilded", 10, "given", "top", "all", "comments", null, null, false)));

                File.WriteAllText("Wiki.Pages.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Pages("ShittyEmails")));
                File.WriteAllText("Wiki.Revisions.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Revisions(null, null, "ShittyEmails")));
                File.WriteAllText("Wiki.PageRevisions.json", JsonConvert.SerializeObject(reddit.Models.Wiki.PageRevisions("index", null, null, "ShittyEmails")));
                File.WriteAllText("Wiki.GetPermissions.json", JsonConvert.SerializeObject(reddit.Models.Wiki.GetPermissions("index", "ShittyEmails")));
                File.WriteAllText("Wiki.Page.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Page("index", null, null, "ShittyEmails")));
                File.WriteAllText("Wiki.PageWithV.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Page("index", "51c412fc-6b26-11e8-a963-0e7fba92da48", null, "ShittyEmails")));
                File.WriteAllText("Wiki.PageWithV2.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Page("index", "51c412fc-6b26-11e8-a963-0e7fba92da48", 
                    "483f05ca-6b26-11e8-b04f-0e02e061d980", "ShittyEmails")));

                File.WriteAllText("Subreddits.SiteAdminCreate.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SiteAdmin(false, true, true, true, true, true, false, 
                    "Proving ground for bots running Reddit.NET.  Please feel free to test your bots here.", false, true, null, "Reddit.NET Bot Testing", false, "#0000FF", "en-US", 
                    "any", "RedditDotNETBot", true, false, "Proving ground for bots running Reddit.NET.  Please feel free to test your bots here.", true, true, "low", "high", "high", true, 
                    null, "New Bot Link!", "Robots and humans are welcome to post here.  Please adhere to Reddit's rules.", "New Bot Post", "new", null, false, "Reddit.NET Bot Testing", 
                    "public", "modonly")));
                File.WriteAllText("Users.FriendInviteMod.json", JsonConvert.SerializeObject(reddit.Models.Users.Friend(null, null, null, null, 999, "RedditDotNetBot", "+all",
                    "moderator_invite", "RedditDotNETBot")));
                File.WriteAllText("LinksAndComments.SubmitLinkPost.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Submit(false, "", "", "", "", "",
                    "link", false, true, null, true, false, "RedditDotNETBot", "", "UPDATE:  As of " + DateTime.Now.ToString("f") + ", she's still looking into it....", "http://iwilllookintoit.com/", null)));
                File.WriteAllText("LinksAndComments.Comment.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Comment(false, "", "This is a test comment.  So there.", "t3_9nhy54")));
                

                File.WriteAllText("Users.PostHistoryOverview.json", JsonConvert.SerializeObject(reddit.Models.Users.PostHistory("KrisCraig", "overview", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.PostHistorySubmitted.json", JsonConvert.SerializeObject(reddit.Models.Users.PostHistory("KrisCraig", "submitted", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.PostHistoryUpvoted.json", JsonConvert.SerializeObject(reddit.Models.Users.PostHistory("KrisCraig", "upvoted", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.PostHistoryDownvoted.json", JsonConvert.SerializeObject(reddit.Models.Users.PostHistory("KrisCraig", "downvoted", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.PostHistoryHidden.json", JsonConvert.SerializeObject(reddit.Models.Users.PostHistory("KrisCraig", "hidden", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.PostHistorySaved.json", JsonConvert.SerializeObject(reddit.Models.Users.PostHistory("KrisCraig", "saved", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.PostHistoryGilded.json", JsonConvert.SerializeObject(reddit.Models.Users.PostHistory("KrisCraig", "gilded", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.CommentHistoryComments.json", JsonConvert.SerializeObject(reddit.Models.Users.CommentHistory("KrisCraig", "comments", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.CommentHistorySaved.json", JsonConvert.SerializeObject(reddit.Models.Users.CommentHistory("KrisCraig", "saved", 10, "given", "top", "all", null, null, false)));
                File.WriteAllText("Users.CommentHistoryGilded.json", JsonConvert.SerializeObject(reddit.Models.Users.CommentHistory("KrisCraig", "gilded", 10, "given", "top", "all", null, null, false)));

                File.WriteAllText("PrivateMessages.GetMessagesInbox.json", JsonConvert.SerializeObject(reddit.Models.PrivateMessages.GetMessages("inbox", false, "", "", "", false)));
                File.WriteAllText("PrivateMessages.GetMessagesUnread.json", JsonConvert.SerializeObject(reddit.Models.PrivateMessages.GetMessages("unread", false, "", "", "", false)));
                File.WriteAllText("PrivateMessages.GetMessagesSent.json", JsonConvert.SerializeObject(reddit.Models.PrivateMessages.GetMessages("sent", false, "", "", "", false)));
                File.WriteAllText("PrivateMessages.Compose.json", JsonConvert.SerializeObject(reddit.Models.PrivateMessages.Compose("", "", "Test Message", "This is a test.  So there.", "RedditDotNetBot")));
                File.WriteAllText("PrivateMessages.ComposeWithSr.json", JsonConvert.SerializeObject(reddit.Models.PrivateMessages.Compose("StillSandersForPres", "", "Test Message", "This is a test.  So there.", 
                    "RedditDotNetBot")));
                
                File.WriteAllText("LinksAndComments.Info.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Info("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.InfoComment.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Info("t1_e7s0vb1")));
                File.WriteAllText("LinksAndComments.InfoLinkCommentSub.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Info("t3_9nhy54,t1_e7s0vb1,t5_2r5rp")));
                File.WriteAllText("LinksAndComments.SubmitSelfPost.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Submit(false, "", "", "", "", "",
                                    "self", false, true, null, true, false, "RedditDotNETBot", "This is a self-post.", "Test post", null, null)));
                File.WriteAllText("LinksAndComments.EditUserText.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.EditUserText(false, null,
                    "(redacted)", "t3_9r7uo1")));
                //File.WriteAllText("LinksAndComments.Hide.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Hide("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.Unhide.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unhide("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.Lock.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Lock("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.Unlock.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unlock("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.Save.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Save("RDNTestCat", "t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.Unsave.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unsave("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.MarkNSFW.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.MarkNSFW("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.UnmarkNSFW.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.UnmarkNSFW("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.Spoiler.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Spoiler("t3_9nhy54")));
                //File.WriteAllText("LinksAndComments.Unspoiler.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unspoiler("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.MoreChildren.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.MoreChildren("dlpnw9j", false,
                    "t3_6tyfna", "new")));
                File.WriteAllText("LinksAndComments.Report.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Report("This is an API test.  Please disregard.",
                    null, "This is a test.", false, "Some other reason.", "Some reason.", "Some rule reason.", "Some site reason.", "RedditDotNETBot",
                    "t3_9ri1cx", "RedditDotNetBot")));
                //File.WriteAllText("LinksAndComments.SavedCategories.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SavedCategories()));
                //File.WriteAllText("LinksAndComments.SendRepliesDisable.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SendReplies("t3_9nhy54", false)));
                //File.WriteAllText("LinksAndComments.SendRepliesEnable.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SendReplies("t3_9nhy54", true)));
                File.WriteAllText("LinksAndComments.SetContestModeEnable.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SetContestMode("t3_9nhy54", true)));
                File.WriteAllText("LinksAndComments.SetContestModeDisable.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SetContestMode("t3_9nhy54", false)));
                File.WriteAllText("LinksAndComments.StickyOn.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SetSubredditSticky("t3_9nhy54", 1, true, false)));
                File.WriteAllText("LinksAndComments.StickyOff.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SetSubredditSticky("t3_9nhy54", 1, false, false)));
                File.WriteAllText("LinksAndComments.SetSuggestedSort.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.SetSuggestedSort("t3_9nhy54", "new")));
                //File.WriteAllText("LinksAndComments.Delete.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Delete("t3_9riiex")));

                //RedditThings.AccountPrefs accountPrefs = reddit.Models.Account.Prefs();
                //accountPrefs.Autoplay = !accountPrefs.Autoplay;
                //File.WriteAllText("Account.UpdatePrefs.json", JsonConvert.SerializeObject(reddit.Models.Account.UpdatePrefs(accountPrefs)));
                File.WriteAllText("Account.Prefs.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs()));

                //File.WriteAllText("Emoji.AcquireLease.json", JsonConvert.SerializeObject(reddit.Models.Emoji.AcquireLease("RedditDotNETBot", "birdie.png", "image/png")));

                // Upload Emoji image to Reddit.  --Kris
                */
                byte[] imageData;
                using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Example.Resources.birdie.png"))
                {
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        imageData = binaryReader.ReadBytes(int.MaxValue / 2);
                    }
                }/*
                RedditThings.S3UploadLeaseContainer s3 = reddit.Models.Emoji.AcquireLease("RedditDotNETBot", "birdie.jpg", "image/jpeg");
                //File.WriteAllText("Emoji.UploadLeaseImage.json", JsonConvert.SerializeObject(reddit.Models.Emoji.UploadLeaseImage(imageData, s3)));
                reddit.Models.Emoji.UploadLeaseImage(imageData, s3);
                File.WriteAllText("Emoji.Add.json", JsonConvert.SerializeObject(reddit.Models.Emoji.Add("RedditDotNETBot", "Birdie", s3.S3UploadLease.Fields.First(
                    item => item.Name.Equals("key")).Value)));
                File.WriteAllText("Emoji.All2.json", JsonConvert.SerializeObject(reddit.Models.Emoji.All("RedditDotNETBot")));

                File.WriteAllText("Flair.CreateLink.json", JsonConvert.SerializeObject(reddit.Models.Flair.Create("", "t3_9rirb3", "", "Test Link Flair", "RedditDotNETBot")));
                File.WriteAllText("Flair.CreateUser.json", JsonConvert.SerializeObject(reddit.Models.Flair.Create("", "", "KrisCraig", "Test User Flair", "RedditDotNETBot")));
                File.WriteAllText("Flair.LinkFlair.json", JsonConvert.SerializeObject(reddit.Models.Flair.LinkFlair("RedditDotNETBot")));
                File.WriteAllText("Flair.LinkFlairV2.json", JsonConvert.SerializeObject(reddit.Models.Flair.LinkFlairV2("RedditDotNETBot")));
                File.WriteAllText("Flair.FlairTemplateLink.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairTemplate("", "", "LINK_FLAIR",
                    DateTime.Now.ToString("fffffff"), false, "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairTemplateUser.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairTemplate("", "", "USER_FLAIR",
                    DateTime.Now.ToString("fffffff"), false, "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairTemplateV2Link.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairTemplateV2("#88AAFF", "", "LINK_FLAIR", false,
                    "V2-" + DateTime.Now.ToString("fffffff"), "dark", false, "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairTemplateV2User.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairTemplateV2("#005588", "", "USER_FLAIR", false,
                    "V2-" + DateTime.Now.ToString("fffffff"), "light", false, "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairSelector.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairSelector(null, "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairSelectorLink.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairSelector(null, "RedditDotNETBot", "t3_9rirb3")));
                File.WriteAllText("Flair.FlairSelectorUser.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairSelector("KrisCraig", "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairList.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairList("", "", "", "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairCSV.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairCSV("KrisCraig,Mod,"
                    + Environment.NewLine + "quietidiot,Human,", "RedditDotNETBot")));
                File.WriteAllText("Flair.SetFlairEnabled.json", JsonConvert.SerializeObject(reddit.Models.Flair.SetFlairEnabled(true, "RedditDotNETBot")));
                //File.WriteAllText("Flair.SelectFlair.json", JsonConvert.SerializeObject(reddit.Models.Flair.SelectFlair("#88BBFF", "c1e232a6-db49-11e8-83f1-0e3c0039d3b4",
                //    "", "", "all", "V2-3628702_EDITED", "dark", "RedditDotNETBot")));
                //System.Collections.Generic.List<RedditThings.Flair> flairs = reddit.Models.Flair.LinkFlair("RedditDotNETBot");
                //flairs.Reverse();
                //File.WriteAllText("Flair.FlairTemplateOrder.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairTemplateOrder("LINK_FLAIR", flairs, "RedditDotNETBot")));
                File.WriteAllText("Flair.FlairConfig.json", JsonConvert.SerializeObject(reddit.Models.Flair.FlairConfig(true, "right", true, "right", true, "RedditDotNETBot")));
                File.WriteAllText("Flair.DeleteFlair.json", JsonConvert.SerializeObject(reddit.Models.Flair.DeleteFlair("quietidiot", "RedditDotNETBot")));
                File.WriteAllText("Flair.DeleteFlairTemplate.json", JsonConvert.SerializeObject(reddit.Models.Flair.DeleteFlairTemplate("0778d5ec-db43-11e8-9258-0e3a02270976",
                    "RedditDotNETBot")));
                File.WriteAllText("Flair.ClearFlairTemplates.json", JsonConvert.SerializeObject(reddit.Models.Flair.ClearFlairTemplates("USER_FLAIR", "RedditDotNETBot")));

                //File.WriteAllText("Listings.TrendingSubreddits.json", JsonConvert.SerializeObject(reddit.Models.Listings.TrendingSubreddits()));
                File.WriteAllText("Listings.GetDuplicates.json", JsonConvert.SerializeObject(reddit.Models.Listings.GetDuplicates("9gaze5", "", "", false, "num_comments", "")));

                File.WriteAllText("Misc.SavedMediaText.json", JsonConvert.SerializeObject(reddit.Models.Misc.SavedMediaText(
                    "https://e.thumbs.redditmedia.com/bOToSJt13ylszjE4.png", "pics")));

                //File.WriteAllText("Moderation.MuteMessageAuthor.json", JsonConvert.SerializeObject(reddit.Models.Moderation.MuteMessageAuthor("t2_2cclzaxt")));
                //File.WriteAllText("Moderation.UnmuteMessageAuthor.json", JsonConvert.SerializeObject(reddit.Models.Moderation.UnmuteMessageAuthor("t2_2cclzaxt")));
                File.WriteAllText("Moderation.Stylesheet.json", reddit.Models.Moderation.Stylesheet("StillSandersForPres"));

                //File.WriteAllText("Modmail.BulkRead.json", JsonConvert.SerializeObject(reddit.Models.Modmail.BulkRead("t5_3fblp", "all")));
                File.WriteAllText("Modmail.GetConversations.json", JsonConvert.SerializeObject(reddit.Models.Modmail.GetConversations("", "t5_3fblp", "unread", "all")));
                string multiName = "RDNTest_" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                RedditThings.LabeledMultiSubmit model = new RedditThings.LabeledMultiSubmit("This is a test multi.",
                    multiName, "None", "#0000FF",
                    new System.Collections.Generic.List<string> { "StillSandersForPres", "RedditDotNETBot" }, "public", "classic");

                //File.WriteAllText("Multis.Create.json", JsonConvert.SerializeObject(reddit.Models.Multis.Create("user/KrisCraig/m/" + multiName, model, true)));

                //File.WriteAllText("PrivateMessages.UnblockSubreddit.json", JsonConvert.SerializeObject(reddit.Models.PrivateMessages.UnblockSubreddit("t5_3fblp")));

                //File.WriteAllText("Search.GetSearch.json", JsonConvert.SerializeObject(reddit.Models.Search.GetSearch("", "", "", false, "Florida", true, "new", "all",
                //    "FloridaMan")));

                File.WriteAllText("Subreddits.SubredditAutocomplete.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SubredditAutocomplete(false, true, "Shitty")));
                File.WriteAllText("Subreddits.SubredditAutocompleteV2.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SubredditAutocompleteV2(true, false,
                    true, "Shitty")));
                File.WriteAllText("Subreddits.SubredditStylesheetPreview.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SubredditStylesheet("preview",
                    "This is a test.", ".whatever{}", "RedditDotNETBot")));
                File.WriteAllText("Subreddits.SubredditStylesheetSave.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SubredditStylesheet("save",
                    "This is a test.", ".whatever{}", "RedditDotNETBot")));
                File.WriteAllText("Subreddits.UploadSrImgIcon.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie",
                    "icon", "RedditDotNETBot", "png")));
                File.WriteAllText("Subreddits.UploadSrImgBanner.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie",
                    "banner", "RedditDotNETBot", "png")));
                File.WriteAllText("Subreddits.UploadSrImgHeader.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.UploadSrImg(imageData, 1, "birdie",
                    "header", "RedditDotNETBot", "png")));
                File.WriteAllText("Subreddits.UploadSrImgImg.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie",
                    "img", "RedditDotNETBot", "png")));
                File.WriteAllText("Subreddits.DeleteSrHeader.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.DeleteSrHeader("RedditDotNETBot")));
                File.WriteAllText("Subreddits.DeleteSrImg.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.DeleteSrImg("birdie", "RedditDotNETBot")));
                File.WriteAllText("Subreddits.DeleteSrBanner.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.DeleteSrBanner("RedditDotNETBot")));
                File.WriteAllText("Subreddits.DeleteSrIcon.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.DeleteSrIcon("RedditDotNETBot")));
                //File.WriteAllText("Subreddits.SubscribeByFullname.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SubscribeByFullname("sub", false,
                //    "t5_3fblp")));
                //File.WriteAllText("Subreddits.Subscribe.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Subscribe("sub", false,
                //    "RedditDotNETBot")));
                //File.WriteAllText("Subreddits.SearchProfiles.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.SearchProfiles("", "", "t2_6vsit", "relevance")));
                File.WriteAllText("Subreddits.Edit.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Edit("RedditDotNETBot", false, "")));
                File.WriteAllText("Subreddits.EditWithCreated.json", JsonConvert.SerializeObject(reddit.Models.Subreddits.Edit("RedditDotNETBot", true, "")));

                //File.WriteAllText("Account.UpdatePrefs.json", JsonConvert.SerializeObject(reddit.Models.Account.UpdatePrefs(
                //    new RedditThings.AccountPrefsSubmit(reddit.Models.Account.Prefs(), "US", false, ""))));

                File.WriteAllText("Users.BlockUser.json", JsonConvert.SerializeObject(reddit.Models.Users.BlockUser("", "RedditDotNetBot")));
                File.WriteAllText("Users.UnfriendUnblockUser.json", JsonConvert.SerializeObject(reddit.Models.Users.Unfriend("t2_6vsit", "", "RedditDotNetBot", "enemy")));
                //File.WriteAllText("Users.ReportUser.json", JsonConvert.SerializeObject(reddit.Models.Users.ReportUser("", "This is an API unit test.  Please disregard.", 
                //    "RedditDotNetBot")));
                ///string json = JsonConvert.SerializeObject(new System.Collections.Generic.Dictionary<string, string> { { "name", "RedditDotNetBot" }, { "note", "This is a test." } });
                File.WriteAllText("Users.UpdateFriend.json", JsonConvert.SerializeObject(reddit.Models.Users.UpdateFriend("RedditDotNetBot", "{}")));
                File.WriteAllText("Users.GetFriend.json", JsonConvert.SerializeObject(reddit.Models.Users.GetFriend("RedditDotNetBot")));
                //File.WriteAllText("Users.DeleteFriend.json", JsonConvert.SerializeObject(reddit.Models.Users.DeleteFriend("RedditDotNetBot")));
                File.WriteAllText("Users.SetPermissions.json", JsonConvert.SerializeObject(reddit.Models.Users.SetPermissions("RedditDotNetBot", "+wiki",
                    "moderator_invite", "RedditDotNETBot")));

                //File.WriteAllText("Wiki.AllowEditor.json", JsonConvert.SerializeObject(reddit.Models.Wiki.AllowEditor("index", "RedditDotNetBot", "ShittyEmails")));
                //File.WriteAllText("Wiki.DenyEditor.json", JsonConvert.SerializeObject(reddit.Models.Wiki.DenyEditor("index", "RedditDotNetBot", "ShittyEmails")));
                //File.WriteAllText("Wiki.Edit.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Edit("Lorem ipsum dolor sit amet, motherfucker.", "index", "",
                //    "Because I can.", "RedditDotNETBot")));
                File.WriteAllText("Wiki.Hide.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Hide("index", "8566d93a-e008-11e8-bfae-0e3db72e7e22", "RedditDotNETBot")));
                //File.WriteAllText("Wiki.Revert.json", JsonConvert.SerializeObject(reddit.Models.Wiki.Revert("index", "2c57192e-e006-11e8-9659-0e20fd6a62a2", "RedditDotNETBot")));
                File.WriteAllText("Wiki.UpdatePermissions.json", JsonConvert.SerializeObject(reddit.Models.Wiki.UpdatePermissions("index", true, 0, "RedditDotNETBot")));

                File.WriteAllText("LiveThreads.Create.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.Create("This is a test.", false, "Resources text.", "Title text.")));
                //File.WriteAllText("LiveThreads.GetById.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.GetById("11wa6l86jy5th")));
                //File.WriteAllText("LiveThreads.GetByIdMultiple.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.GetById("11wa6l86jy5th,11wa45xqbkw3m")));
                File.WriteAllText("LiveThreads.About.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.About("11wa6l86jy5th")));
                File.WriteAllText("LiveThreads.HappeningNow.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.HappeningNow()));
                File.WriteAllText("LiveThreads.Edit.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.Edit("11wa6l86jy5th", "This is an ADULTS-ONLY test.", 
                    true, "Resources text.", "Title text.")));
                File.WriteAllText("LiveThreads.InviteContributor.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.InviteContributor("11wa6l86jy5th",
                    "RedditDotNetBot", "+update", "liveupdate_contributor_invite")));
                File.WriteAllText("LiveThreads.Report.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.Report("11wa6l86jy5th", "spam")));
                File.WriteAllText("LiveThreads.SetContributorPermissions.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.SetContributorPermissions("11wa6l86jy5th",
                    "RedditDotNetBot", "+edit", "liveupdate_contributor_invite")));
                File.WriteAllText("LiveThreads.Contributors.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.Contributors("11wa6l86jy5th")));
                File.WriteAllText("LiveThreads.RemoveContributorInvite.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.RemoveContributorInvite("11wa6l86jy5th",
                    "t2_2cclzaxt")));
                File.WriteAllText("LiveThreads.Update.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.Update("11wa6l86jy5th", "Test update!")));
                File.WriteAllText("LiveThreads.GetUpdates.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.GetUpdates("11wa6l86jy5th", "", "", "")));
                File.WriteAllText("LiveThreads.StrikeUpdate.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.StrikeUpdate("11wa6l86jy5th",
                    "LiveUpdate_7e623536-e0d4-11e8-a8f9-0e205404551e")));
                File.WriteAllText("LiveThreads.GetUpdate.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.GetUpdate("11wa6l86jy5th",
                    "7e623536-e0d4-11e8-a8f9-0e205404551e")));
                File.WriteAllText("LiveThreads.DeleteUpdate.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.DeleteUpdate("11wa6l86jy5th",
                    "LiveUpdate_7e623536-e0d4-11e8-a8f9-0e205404551e")));
                //File.WriteAllText("LiveThreads.Discussions.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.Discussions("11wa6l86jy5th", "", "")));
                File.WriteAllText("LiveThreads.LeaveContributor.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.LeaveContributor("11waah0gnqebm")));
                File.WriteAllText("LiveThreads.RemoveContributor.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.RemoveContributor("11wa45xqbkw3m", "t2_6vsit")));
                File.WriteAllText("LiveThreads.CloseThread.json", JsonConvert.SerializeObject(reddit.Models.LiveThreads.CloseThread("11wa6l86jy5th")));

                File.WriteAllText("Widgets.AddTextArea.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Add(
                    new RedditThings.WidgetTextArea("TestWidget", "This is a test."), "RedditDotNETBot")));
                File.WriteAllText("Widgets.UpdateTextArea.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Update("widget_11wo976t5rax7",
                    new RedditThings.WidgetTextArea("TestWidgetMod", "This is a MODIFIED test."), "RedditDotNETBot")));
                File.WriteAllText("Widgets.GetWithProgressiveImages.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Get(true, "RedditDotNETBot")));
                RedditThings.WidgetResults widgetResults = reddit.Models.Widgets.Get(false, "RedditDotNETBot");
                System.Collections.Generic.List<string> order = widgetResults.Layout.Sidebar.Order;
                order.Reverse();
                //File.WriteAllText("Widgets.UpdateOrder.json", JsonConvert.SerializeObject(reddit.Models.Widgets.UpdateOrder("sidebar", order, "RedditDotNETBot")));
                //File.WriteAllText("Widgets.Delete.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Delete(
                    new System.Collections.Generic.List<string>(widgetResults.Items.Keys)[widgetResults.Items.Keys.Count - 1], "RedditDotNETBot")));
                File.WriteAllText("Widgets.AddCalendar.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Add(
                    new RedditThings.WidgetCalendar(new RedditThings.WidgetCalendarConfiguration(0, true, true, true, true, true), "kris.craig@gmail.com",
                    false, "Test Calendar Widget 2", new RedditThings.WidgetStyles()), "RedditDotNETBot")));
                File.WriteAllText("Widgets.UpdateCalendar.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Update("widget_11wuxmi1wr2pg",
                    new RedditThings.WidgetCalendar(new RedditThings.WidgetCalendarConfiguration(20, true, true, true, true, true), "kris.craig@gmail.com",
                    false, "Test Calendar Widget 2b", new RedditThings.WidgetStyles()), "RedditDotNETBot")));
                File.WriteAllText("Widgets.AddCommunityList.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Add(
                    new RedditThings.WidgetCommunityList(new System.Collections.Generic.List<string> { "StillSandersForPres", "RedditDotNETBot" },
                    "Test CommunityList Widget", new RedditThings.WidgetStyles()), "RedditDotNETBot")));
                File.WriteAllText("Widgets.UpdateCommunityList.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Update("widget_11wv1rdhjxydr",
                    new RedditThings.WidgetCommunityList(new System.Collections.Generic.List<string> { "StillSandersForPres", "RedditDotNETBot" },
                    "Test CommunityList Widget B", new RedditThings.WidgetStyles()), "RedditDotNETBot")));
                File.WriteAllText("Widgets.Get.json", JsonConvert.SerializeObject(reddit.Models.Widgets.Get(false, "RedditDotNETBot")));*/
            }
        }

        public static void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
        {
            foreach (Post post in e.Added)
            {
                Console.WriteLine("[" + post.Subreddit + "] New Post by " + post.Author + ": " + post.Title);
            }
        }

        public static void C_NewCommentsUpdated(object sender, CommentsUpdateEventArgs e)
        {
            foreach (Comment comment in e.Added)
            {
                Console.WriteLine("[" + comment.Subreddit + "/" + comment.Root.Title + "] New Comment by " + comment.Author + ": " + comment.Body);
            }
        }
    }
}
