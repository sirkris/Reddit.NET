using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class LiveThreadsTests : BaseTests
    {
        // All endpoints being tested require an existing live thread, so we'll just stick them into a single method until sorted tests are supported.  --Kris
        [TestMethod]
        public void Workflow()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // Create a new live thread.  --Kris
            LiveThreadCreateResultContainer createRes = reddit.Models.LiveThreads.Create("This is a test.", false, "Resources text.", "Title text.");

            Assert.IsNotNull(createRes);
            Assert.IsNotNull(createRes.JSON);
            Assert.IsNotNull(createRes.JSON.Data);
            Assert.IsNotNull(createRes.JSON.Data.Id);
            Assert.IsTrue(createRes.JSON.Errors == null || createRes.JSON.Errors.Count == 0);

            // Load newly-created thread info.  --Kris
            LiveUpdateEventContainer liveEvent = reddit.Models.LiveThreads.About(createRes.JSON.Data.Id);

            Assert.IsNotNull(liveEvent);
            Assert.IsNotNull(liveEvent.Data);

            // Edit the live thread.  --Kris
            GenericContainer editRes = reddit.Models.LiveThreads.Edit(liveEvent.Data.Id, "This is an ADULTS-ONLY test.", true, "Resources text.", "Title text.");

            Assert.IsNotNull(editRes);
            Assert.IsNotNull(editRes.JSON);
            Assert.IsTrue(editRes.JSON.Errors == null || editRes.JSON.Errors.Count == 0);

            // Invite a contributor.  --Kris
            GenericContainer inviteRes = reddit.Models.LiveThreads.InviteContributor(liveEvent.Data.Id, "RedditDotNetBot", "+update", "liveupdate_contributor_invite");

            Assert.IsNotNull(inviteRes);
            Assert.IsNotNull(inviteRes.JSON);
            Assert.IsTrue(inviteRes.JSON.Errors == null || inviteRes.JSON.Errors.Count == 0);

            // Change contributor permissions.  --Kris
            GenericContainer permsRes = reddit.Models.LiveThreads.SetContributorPermissions(liveEvent.Data.Id, "RedditDotNetBot", "+edit", "liveupdate_contributor_invite");

            Assert.IsNotNull(permsRes);
            Assert.IsNotNull(permsRes.JSON);
            Assert.IsTrue(permsRes.JSON.Errors == null || permsRes.JSON.Errors.Count == 0);

            // Get contributors.  --Kris
            List<UserListContainer> contributors = reddit.Models.LiveThreads.Contributors(liveEvent.Data.Id);

            Assert.IsNotNull(contributors);
            Assert.IsTrue(contributors.Count == 2);
            Assert.IsNotNull(contributors[0].Data);
            Assert.IsNotNull(contributors[0].Data.Children);
            Assert.IsTrue(contributors[0].Data.Children.Count == 1);
            Assert.IsNotNull(contributors[1].Data);
            Assert.IsNotNull(contributors[1].Data.Children);
            Assert.IsTrue(contributors[1].Data.Children.Count == 1);
            Assert.IsTrue(contributors[1].Data.Children[0].Name.Equals("RedditDotNetBot"));

            // Remove contributor invite.  --Kris
            GenericContainer removeInviteRes = reddit.Models.LiveThreads.RemoveContributorInvite(liveEvent.Data.Id, contributors[1].Data.Children[0].Id);

            Assert.IsNotNull(removeInviteRes);
            Assert.IsNotNull(removeInviteRes.JSON);
            Assert.IsTrue(removeInviteRes.JSON.Errors == null || removeInviteRes.JSON.Errors.Count == 0);

            // Post an update.  --Kris
            string update = "Test update.";
            GenericContainer updateRes = reddit.Models.LiveThreads.Update(liveEvent.Data.Id, update);

            Assert.IsNotNull(updateRes);
            Assert.IsNotNull(updateRes.JSON);
            Assert.IsTrue(updateRes.JSON.Errors == null || updateRes.JSON.Errors.Count == 0);

            // Get updates.  --Kris
            LiveUpdateContainer updates = reddit.Models.LiveThreads.GetUpdates(liveEvent.Data.Id, "", "", "");

            Assert.IsNotNull(updates);
            Assert.IsNotNull(updates.Data);
            Assert.IsNotNull(updates.Data.Children);
            Assert.IsTrue(updates.Data.Children.Count == 1);
            Assert.IsNotNull(updates.Data.Children[0].Data);
            Assert.IsTrue(updates.Data.Children[0].Data.Body.Equals(update));

            // Strike update.  --Kris
            GenericContainer strikeRes = reddit.Models.LiveThreads.StrikeUpdate(liveEvent.Data.Id, updates.Data.Children[0].Data.Name);

            Assert.IsNotNull(strikeRes);
            Assert.IsNotNull(strikeRes.JSON);
            Assert.IsTrue(strikeRes.JSON.Errors == null || strikeRes.JSON.Errors.Count == 0);

            // Get update.  --Kris
            LiveUpdateContainer liveUpdate = reddit.Models.LiveThreads.GetUpdate(liveEvent.Data.Id, updates.Data.Children[0].Data.Id);

            Assert.IsNotNull(liveUpdate);
            Assert.IsNotNull(liveUpdate.Data);
            Assert.IsNotNull(liveUpdate.Data.Children);
            Assert.IsTrue(liveUpdate.Data.Children.Count == 1);
            Assert.IsNotNull(liveUpdate.Data.Children[0].Data);
            Assert.IsTrue(liveUpdate.Data.Children[0].Data.Body.Equals(update));
            Assert.IsTrue(liveUpdate.Data.Children[0].Data.Stricken);

            // Delete update.  --Kris
            GenericContainer delUpdateRes = reddit.Models.LiveThreads.DeleteUpdate(liveEvent.Data.Id, liveUpdate.Data.Children[0].Data.Name);

            Assert.IsNotNull(delUpdateRes);
            Assert.IsNotNull(delUpdateRes.JSON);
            Assert.IsTrue(delUpdateRes.JSON.Errors == null || delUpdateRes.JSON.Errors.Count == 0);

            // Close the live thread.  --Kris
            GenericContainer closeRes = reddit.Models.LiveThreads.CloseThread(liveEvent.Data.Id);

            Assert.IsNotNull(closeRes);
            Assert.IsNotNull(closeRes.JSON);
            Assert.IsTrue(closeRes.JSON.Errors == null || closeRes.JSON.Errors.Count == 0);
        }
    }
}
