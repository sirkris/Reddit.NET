using Newtonsoft.Json;
using Reddit.NET;
using Reddit.NET.Controllers;
using System;
using System.IO;

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

                RedditAPI reddit = new RedditAPI(appId, refreshToken, accessToken);

                User me = reddit.User().Me();

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                // Temporary code - Verify I've got all the models right and catalogue their returns.  Will then proceed to writing unit tests.  --Kris
                /*
                File.WriteAllText("Account.Prefs.json", JsonConvert.SerializeObject(reddit.Models.Account.Prefs()));
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
                //File.WriteAllText("Moderation.Stylesheet.json", JsonConvert.SerializeObject(reddit.Models.Moderation.Stylesheet("StillSandersForPres")));

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
                File.WriteAllText("LinksAndComments.Hide.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Hide("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.Unhide.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unhide("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.Lock.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Lock("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.Unlock.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unlock("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.Save.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Save("RDNTestCat", "t3_9nhy54")));
                File.WriteAllText("LinksAndComments.Unsave.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unsave("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.MarkNSFW.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.MarkNSFW("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.UnmarkNSFW.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.UnmarkNSFW("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.Spoiler.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Spoiler("t3_9nhy54")));
                File.WriteAllText("LinksAndComments.Unspoiler.json", JsonConvert.SerializeObject(reddit.Models.LinksAndComments.Unspoiler("t3_9nhy54")));*/

            }
        }
    }
}
