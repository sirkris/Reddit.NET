using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class LiveThreadsTests : BaseTests
    {
        public LiveThreadsTests() : base() { }

        // All endpoints being tested require an existing live thread, so we'll just stick them into a single method until sorted tests are supported.  --Kris
        [TestMethod]
        public void Workflow()
        {
            // Create a new live thread.  --Kris
            LiveThreadCreateResultContainer createRes = reddit.Models.LiveThreads.Create("This is a test.", false, "Resources text.", "Title text.");

            Validate(createRes);

            // Load newly-created thread info.  --Kris
            LiveUpdateEventContainer liveEvent = reddit.Models.LiveThreads.About(createRes.JSON.Data.Id);

            Validate(liveEvent);

            // Edit the live thread.  --Kris
            GenericContainer editRes = reddit.Models.LiveThreads.Edit(liveEvent.Data.Id, "This is an ADULTS-ONLY test.", true, "Resources text.", "Title text.");

            Validate(editRes);

            // Invite a contributor.  --Kris
            GenericContainer inviteRes = reddit.Models.LiveThreads.InviteContributor(liveEvent.Data.Id, "RedditDotNetBot", "+update", "liveupdate_contributor_invite");

            Validate(inviteRes);

            // Change contributor permissions.  --Kris
            GenericContainer permsRes = reddit.Models.LiveThreads.SetContributorPermissions(liveEvent.Data.Id, "RedditDotNetBot", "+edit", "liveupdate_contributor_invite");

            Validate(permsRes);

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

            Validate(removeInviteRes);

            // Post an update.  --Kris
            string update = "Test update.";
            GenericContainer updateRes = reddit.Models.LiveThreads.Update(liveEvent.Data.Id, update);

            Validate(updateRes);

            // Get updates.  --Kris
            LiveUpdateContainer updates = reddit.Models.LiveThreads.GetUpdates(liveEvent.Data.Id, "", "", "");

            Validate(updates);
            Assert.IsTrue(updates.Data.Children.Count == 1);
            Assert.IsNotNull(updates.Data.Children[0].Data);
            Assert.IsTrue(updates.Data.Children[0].Data.Body.Equals(update));

            // Strike update.  --Kris
            GenericContainer strikeRes = reddit.Models.LiveThreads.StrikeUpdate(liveEvent.Data.Id, updates.Data.Children[0].Data.Name);

            Validate(strikeRes);

            // Get update.  --Kris
            LiveUpdateContainer liveUpdate = reddit.Models.LiveThreads.GetUpdate(liveEvent.Data.Id, updates.Data.Children[0].Data.Id);

            Validate(liveUpdate);
            Assert.IsTrue(liveUpdate.Data.Children.Count == 1);
            Assert.IsNotNull(liveUpdate.Data.Children[0].Data);
            Assert.IsTrue(liveUpdate.Data.Children[0].Data.Body.Equals(update));
            Assert.IsTrue(liveUpdate.Data.Children[0].Data.Stricken);

            // Delete update.  --Kris
            GenericContainer delUpdateRes = reddit.Models.LiveThreads.DeleteUpdate(liveEvent.Data.Id, liveUpdate.Data.Children[0].Data.Name);

            Validate(delUpdateRes);

            // Close the live thread.  --Kris
            GenericContainer closeRes = reddit.Models.LiveThreads.CloseThread(liveEvent.Data.Id);

            Validate(closeRes);
        }
    }
}
