using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System;
using System.Collections.Generic;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class LiveThreadTests : BaseTests
    {
        private LiveThread LiveThread
        {
            get
            {
                return liveThread ?? CreateThread();
            }
            set
            {
                liveThread = value;
            }
        }
        private LiveThread liveThread;

        private bool LiveThreadUpdated = false;
        private bool LiveThreadContributorsUpdated = false;
        private List<Reddit.Things.LiveUpdate> LiveThreadUpdates;

        public LiveThreadTests() : base() { }

        private LiveThread CreateThread()
        {
            LiveThread = reddit.LiveThread("Test Thread", "This is a test live thread created by Reddit.NET.").Create(nsfw: false, resources: "Resources text.");
            return LiveThread;
        }

        [TestMethod]
        public void Create()
        {
            Validate(LiveThread).Close();
        }

        [TestMethod]
        public void SaveChanges()
        {
            Validate(LiveThread);

            // Make some changes.  --Kris
            LiveThread.NSFW = true;
            string updatedResourcesText = "Updated resources text.";
            LiveThread.Resources = updatedResourcesText;

            // Save the changes (makes a call to the Edit model).  --Kris
            LiveThread.SaveChanges();

            // Update the live thread instance to match what the API has (should be the same after SaveChanges was called).  --Kris
            LiveThread = LiveThread.About();

            // If SaveChanges failed for whatever reason, the values will not match.  --Kris
            Validate(LiveThread);
            Assert.AreEqual(true, LiveThread.NSFW);
            Assert.AreEqual(updatedResourcesText, LiveThread.Resources);
        }

        [TestMethod]
        public void Workflow()
        {
            User patsy = GetTargetUser();

            // Create a new live thread.  --Kris
            Validate(LiveThread);

            // Edit the live thread.  --Kris
            LiveThread.Edit(LiveThread.Title, LiveThread.Description, false, LiveThread.Resources);

            // Invite a contributor.  --Kris
            LiveThread.InviteContributor(patsy.Name, "+update", "liveupdate_contributor_invite");

            // Change contributor permissions.  --Kris
            LiveThread.SetContributorPermissions(patsy.Name, "+edit", "liveupdate_contributor_invite");

            // Remove contributor invite.  --Kris
            LiveThread.RemoveContributorInvite(patsy.Fullname);

            // Re-invite contributor so they can accept it.  Note how this can also be called from the User instance.  --Kris
            reddit.User(patsy.Name).InviteToLiveThread(LiveThread.Id, "+update", "liveupdate_contributor_invite");

            // Accept the invitation.  --Kris
            reddit2.Account.AcceptLiveThreadInvite(LiveThread.Id);

            // Target user is doing a lousy job.  --Kris
            reddit.User(patsy.Name).RemoveFromLiveThread(LiveThread.Id);

            // Target user has friends in high places.  --Kris
            reddit.User(patsy.Name).InviteToLiveThread(LiveThread.Id, "+update", "liveupdate_contributor_invite");
            reddit2.LiveThread(LiveThread).AcceptContributorInvite();

            // You can't fire me!  I quit!  --Kris
            reddit2.LiveThread(LiveThread).Abandon();

            // Post an update.  --Kris
            string update = "Test update.";
            LiveThread.Update(update);

            // Get updates.  --Kris
            Validate(LiveThread.Updates);
            Assert.AreEqual(1, LiveThread.Updates.Count);
            Assert.IsNotNull(LiveThread.Updates[0]);
            Assert.AreEqual(update, LiveThread.Updates[0].Body);

            // Strike update.  --Kris
            LiveThread.StrikeUpdate(LiveThread.Updates[0].Name);

            // Get update.  --Kris
            Reddit.Things.LiveUpdate liveUpdate = LiveThread.GetUpdate(LiveThread.Updates[0].Id);

            Validate(liveUpdate);
            Assert.AreEqual(LiveThread.Updates[0].Id, liveUpdate.Id);

            // Delete update.  --Kris
            LiveThread.DeleteUpdate(liveUpdate.Name);

            // Close the live thread.  --Kris
            LiveThread.Close();
        }

        [TestMethod]
        public void Monitoring()
        {
            User patsy = GetTargetUser();

            reddit.User(patsy).InviteToLiveThread(LiveThread.Id, "+update", "liveupdate_contributor_invite");

            // Create a new live thread.  --Kris
            Validate(LiveThread);

            // Monitor the thread for any configuration changes.  --Kris
            LiveThread.MonitorThread();
            LiveThread.ThreadUpdated += C_LiveThreadUpdated;

            // Monitor the thread for new and abdicated/removed contributors.  --Kris
            LiveThread.GetContributors();
            LiveThread.MonitorContributors();
            LiveThread.ContributorsUpdated += C_LiveThreadContributorsUpdated;

            // Monitor the thread for new and deleted updates.  --Kris
            // TODO - Trigger an event if one of the NewUpdates list has a stricken value that differs from its OldUpdates list counterpart.  --Kris
            LiveThread.MonitorUpdates();
            LiveThread.UpdatesUpdated += C_LiveThreadUpdatesUpdated;

            // Make changes to trigger all three monitors and validate the results.  --Kris
            patsy.AcceptLiveThreadInvite(LiveThread.Id);

            // Despite what VS says, we don't want to use await here.  --Kris
            LiveThread.EditAsync(LiveThread.Title, LiveThread.Description, !LiveThread.NSFW, LiveThread.Resources);

            LiveThreadUpdates = new List<Reddit.Things.LiveUpdate>();
            for (int i = 1; i <= 5; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                LiveThread.UpdateAsync("Primary user test update #" + i.ToString());
                patsy.UpdateLiveThreadAsync(LiveThread.Id, "Secondary user test update #" + i.ToString());
            }

            DateTime start = DateTime.Now;
            while ((!LiveThreadUpdated || !LiveThreadContributorsUpdated || LiveThreadUpdates.Count < 10)
                && start.AddMinutes(1) > DateTime.Now) { }

            // Stop monitoring and close the thread.  --Kris
            LiveThread.MonitorUpdates();
            LiveThread.UpdatesUpdated -= C_LiveThreadUpdatesUpdated;

            LiveThread.MonitorContributors();
            LiveThread.ContributorsUpdated -= C_LiveThreadContributorsUpdated;

            LiveThread.MonitorThread();
            LiveThread.ThreadUpdated -= C_LiveThreadUpdated;

            LiveThread.Close();

            Assert.IsTrue(LiveThreadUpdated);
            Assert.IsTrue(LiveThreadContributorsUpdated);
            Assert.AreEqual(10, LiveThreadUpdates.Count);
        }

        private void C_LiveThreadUpdated(object sender, LiveThreadUpdateEventArgs e)
        {
            try
            {
                Assert.AreNotEqual(e.OldThread.NSFW, e.NewThread.NSFW);
                LiveThreadUpdated = true;
            } catch (AssertFailedException) { }
        }

        private void C_LiveThreadContributorsUpdated(object sender, LiveThreadContributorsUpdateEventArgs e)
        {
            Assert.IsTrue(e.Added.Count > 0);
            LiveThreadContributorsUpdated = true;
        }

        private void C_LiveThreadUpdatesUpdated(object sender, LiveThreadUpdatesUpdateEventArgs e)
        {
            LiveThreadUpdates.AddRange(e.Added);
        }
    }
}
