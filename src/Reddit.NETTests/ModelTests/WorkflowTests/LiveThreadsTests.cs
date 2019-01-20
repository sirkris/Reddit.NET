using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.LiveThreads;
using Reddit.Things;
using System.Collections.Generic;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class LiveThreadsTests : BaseTests
    {
        public LiveThreadsTests() : base() { }

        [TestMethod]
        public void Workflow()
        {
            User patsy = GetTargetUserModel();

            // Create a new live thread.  --Kris
            LiveThreadCreateResultContainer createRes = reddit.Models.LiveThreads.Create(new LiveThreadsConfigInput("Title text.", "This is a test.", false, "Resources text."));

            Validate(createRes);

            // Load newly-created thread info.  --Kris
            LiveUpdateEventContainer liveEvent = reddit.Models.LiveThreads.About(createRes.JSON.Data.Id);

            Validate(liveEvent);

            // Edit the live thread.  --Kris
            GenericContainer editRes = reddit.Models.LiveThreads.Edit(liveEvent.Data.Id, new LiveThreadsConfigInput("Title text.", "This is an ADULTS-ONLY test.", true, "Resources text."));

            Validate(editRes);

            // Invite a contributor.  --Kris
            GenericContainer inviteRes = reddit.Models.LiveThreads.InviteContributor(liveEvent.Data.Id, new LiveThreadsContributorInput(patsy.Name, "+update", "liveupdate_contributor_invite"));

            Validate(inviteRes);

            // Change contributor permissions.  --Kris
            GenericContainer permsRes = reddit.Models.LiveThreads.SetContributorPermissions(liveEvent.Data.Id, new LiveThreadsContributorInput(patsy.Name, "+edit", "liveupdate_contributor_invite"));
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
            Assert.IsTrue(contributors[1].Data.Children[0].Name.Equals(patsy.Name));

            // Remove contributor invite.  --Kris
            GenericContainer removeInviteRes = reddit.Models.LiveThreads.RemoveContributorInvite(liveEvent.Data.Id, contributors[1].Data.Children[0].Id);

            Validate(removeInviteRes);

            // Re-invite contributor so they can accept it.  --Kris
            inviteRes = reddit.Models.LiveThreads.InviteContributor(liveEvent.Data.Id, new LiveThreadsContributorInput(patsy.Name, "+update", "liveupdate_contributor_invite"));

            Validate(inviteRes);

            // Accept the invitation.  --Kris
            GenericContainer acceptRes = reddit2.Models.LiveThreads.AcceptContributorInvite(liveEvent.Data.Id);

            Validate(acceptRes);

            // Target user is doing a lousy job.  --Kris
            GenericContainer removeRes = reddit.Models.LiveThreads.RemoveContributor(liveEvent.Data.Id, "t2_" + patsy.Id);

            Validate(removeRes);

            // Target user has friends in high places.  --Kris
            inviteRes = reddit.Models.LiveThreads.InviteContributor(liveEvent.Data.Id, new LiveThreadsContributorInput(patsy.Name, "+update", "liveupdate_contributor_invite"));
            acceptRes = reddit2.Models.LiveThreads.AcceptContributorInvite(liveEvent.Data.Id);

            Validate(inviteRes);
            Validate(acceptRes);

            // You can't fire me!  I quit!  --Kris
            GenericContainer leaveRes = reddit2.Models.LiveThreads.LeaveContributor(liveEvent.Data.Id);

            Validate(leaveRes);

            // Post an update.  --Kris
            string update = "Test update.";
            GenericContainer updateRes = reddit.Models.LiveThreads.Update(liveEvent.Data.Id, update);

            Validate(updateRes);

            // Get updates.  --Kris
            LiveUpdateContainer updates = reddit.Models.LiveThreads.GetUpdates(liveEvent.Data.Id, new LiveThreadsGetUpdatesInput());

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
