using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Structures;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    public class LiveThread : BaseController
    {
        public event EventHandler<LiveThreadUpdateEventArgs> ThreadUpdated;
        public event EventHandler<LiveThreadContributorsUpdateEventArgs> ContributorsUpdated;
        public event EventHandler<LiveThreadUpdatesUpdateEventArgs> UpdatesUpdated;

        internal override ref Models.Internal.Monitor MonitorModel => ref Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

        public string Id;
        public string Fullname;
        public string Description;
        public bool NSFW;
        public string Resources;
        public string Title;

        public int? TotalViews;
        public DateTime Created;
        public string WebsocketURL;  // TODO - Support for Websockets.  --Kris
        public bool IsAnnouncement;
        public string AnnouncementURL;
        public string State;
        public int ViewerCount;
        public string Icon;

        public RedditThings.LiveUpdateEvent EventData;

        public List<RedditThings.LiveUpdate> Updates
        {
            get
            {
                return (UpdatesLastUpdated.HasValue
                    && UpdatesLastUpdated.Value.AddSeconds(5) > DateTime.Now ? updates : GetUpdates());
            }
            private set
            {
                updates = value;
            }
        }
        private List<RedditThings.LiveUpdate> updates;
        private DateTime? UpdatesLastUpdated;

        public List<RedditThings.UserListContainer> Contributors
        {
            get
            {
                return (ContributorsLastUpdated.HasValue
                    && ContributorsLastUpdated.Value.AddSeconds(15) > DateTime.Now ? contributors : GetContributors());
            }
            private set
            {
                contributors = value;
            }
        }
        private List<RedditThings.UserListContainer> contributors;
        private DateTime? ContributorsLastUpdated;

        private readonly Dispatch Dispatch;

        public LiveThread(Dispatch dispatch, LiveThread liveThread)
        {
            Dispatch = dispatch;

            Import(liveThread.Id, liveThread.Description, liveThread.NSFW, liveThread.Resources, liveThread.Title,
                liveThread.TotalViews, liveThread.Created, liveThread.Fullname, liveThread.WebsocketURL, liveThread.AnnouncementURL,
                liveThread.State, liveThread.ViewerCount, liveThread.Icon);

            EventData = liveThread.EventData;
        }

        public LiveThread(Dispatch dispatch, RedditThings.LiveUpdateEvent liveUpdateEvent)
        {
            Dispatch = dispatch;

            Import(liveUpdateEvent.Id, liveUpdateEvent.Description, liveUpdateEvent.NSFW, liveUpdateEvent.Resources, liveUpdateEvent.Title,
                liveUpdateEvent.TotalViews, liveUpdateEvent.Created, liveUpdateEvent.Name, liveUpdateEvent.WebsocketURL, liveUpdateEvent.AnnouncementURL,
                liveUpdateEvent.State, liveUpdateEvent.ViewerCount, liveUpdateEvent.Icon);

            EventData = liveUpdateEvent;
        }

        public LiveThread(Dispatch dispatch, string title = null, string description = null, bool nsfw = false, string resources = null,
            string id = null, string name = null, string websocketUrl = null, string announcementUrl = null, string state = null,
            string icon = null, int? totalViews = null, int viewerCount = 0, DateTime created = default(DateTime))
        {
            Dispatch = dispatch;

            Import(id, description, nsfw, resources, title, totalViews, created, name, websocketUrl, announcementUrl, state, viewerCount, icon);

            EventData = new RedditThings.LiveUpdateEvent(this);
        }

        public LiveThread(Dispatch dispatch, string id)
        {
            Dispatch = dispatch;
            Id = id;
        }

        private void Import(string id, string description, bool nsfw, string resources, string title,
            int? totalViews, DateTime created, string fullname, string websocketUrl, string announcementUrl,
            string state, int viewerCount, string icon)
        {
            Id = id;
            Description = description;
            NSFW = nsfw;
            Resources = resources;
            Title = title;
            TotalViews = totalViews;
            Created = created;
            Fullname = fullname;
            WebsocketURL = websocketUrl;
            AnnouncementURL = announcementUrl;
            State = state;
            ViewerCount = viewerCount;
            Icon = icon;
        }

        /// <summary>
        /// Get some basic information about the live thread.
        /// </summary>
        /// <returns>An instance of this class populated with the returned data.</returns>
        public LiveThread About()
        {
            return new LiveThread(Dispatch, Validate(Dispatch.LiveThreads.About(Id)).Data);
        }

        /// <summary>
        /// Get a list of updates posted in this thread.
        /// </summary>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="styleSr">subreddit name</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <returns>The requested live updates.</returns>
        public List<RedditThings.LiveUpdate> GetUpdates(string after = "", string before = "", string styleSr = "", int count = 0, int limit = 25)
        {
            Updates = GetLiveUpdates(Validate(Dispatch.LiveThreads.GetUpdates(Id, after, before, styleSr, count, limit)));
            UpdatesLastUpdated = DateTime.Now;

            return Updates;
        }

        /// <summary>
        /// Create a new live thread.
        /// </summary>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        /// <returns>An instance of this class populated with data from the new live thread.</returns>
        public LiveThread Create(string title = null, string description = null, bool? nsfw = null, string resources = null, bool retry = true)
        {
            try
            {
                return new LiveThread(Dispatch, Validate(Dispatch.LiveThreads.Create(description ?? Description, nsfw ?? NSFW, resources ?? Resources, title ?? Title)).JSON.Data.Id).About();
            }
            catch (RedditRateLimitException ex)
            {
                List<string> errors = ((List<List<string>>)ex.Data["errors"])[0];

                // TODO - Move this to where it'll work for all endpoints.  --Kris
                // If the wait time is in seconds (i.e. less than a minute), just go ahead and wait then retry.  --Kris
                int waitSeconds = 0;
                if (errors[1].StartsWith("you are doing that too much. try again in ")
                    && errors[1].EndsWith("seconds."))
                {
                    waitSeconds = Convert.ToInt32(Regex.Match(errors[1], @"\d+").Value);
                }

                if (retry
                    && waitSeconds > 0
                    && waitSeconds < 60)
                {
                    Thread.Sleep(waitSeconds * 1000);
                    return Create(title, description, nsfw, resources, false);
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Create a new live thread asynchronously.
        /// </summary>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        /// <returns>An instance of this class populated with data from the new live thread.</returns>
        public async Task CreateAsync(string title = null, string description = null, bool? nsfw = null, string resources = null)
        {
            await Task.Run(() =>
            {
                Create(title, description, nsfw, resources);
            });
        }

        /// <summary>
        /// Accept a pending invitation to contribute to the thread.
        /// </summary>
        public void AcceptContributorInvite()
        {
            Validate(Dispatch.LiveThreads.AcceptContributorInvite(Id));
        }

        /// <summary>
        /// Asynchronously accept a pending invitation to contribute to the thread.
        /// </summary>
        public async Task AcceptContributorInviteAsync()
        {
            await Task.Run(() =>
            {
                AcceptContributorInvite();
            });
        }

        /// <summary>
        /// Permanently close the thread, disallowing future updates.
        /// Requires the close permission for this thread.
        /// Returns forbidden response if the thread has already been closed.
        /// </summary>
        public void Close()
        {
            Validate(Dispatch.LiveThreads.CloseThread(Id));
        }

        /// <summary>
        /// Permanently close the thread asynchronously, disallowing future updates.
        /// Requires the close permission for this thread.
        /// Returns forbidden response if the thread has already been closed.
        /// </summary>
        public async Task CloseAsync()
        {
            await Task.Run(() =>
            {
                Close();
            });
        }

        /// <summary>
        /// Delete an update from the thread.
        /// Requires that specified update must have been authored by the user or that you have the edit permission for this thread.
        /// </summary>
        /// <param name="updateName">the Name of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        public void DeleteUpdate(string updateId)
        {
            Validate(Dispatch.LiveThreads.DeleteUpdate(Id, updateId));
        }

        /// <summary>
        /// Delete an update from the thread asynchronously.
        /// Requires that specified update must have been authored by the user or that you have the edit permission for this thread.
        /// </summary>
        /// <param name="updateName">the Name of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        public async Task DeleteUpdateAsync(string updateId)
        {
            await Task.Run(() =>
            {
                DeleteUpdate(updateId);
            });
        }

        /// <summary>
        /// Configure the thread.
        /// Requires the settings permission for this thread.
        /// </summary>
        public void SaveChanges()
        {
            Edit(Title, Description, NSFW, Resources);
        }

        /// <summary>
        /// Configure the thread asynchronously.
        /// Requires the settings permission for this thread.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await Task.Run(() =>
            {
                SaveChanges();
            });
        }

        /// <summary>
        /// Configure the thread.
        /// Requires the settings permission for this thread.
        /// </summary>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        public void Edit(string title, string description, bool nsfw, string resources)
        {
            Validate(Dispatch.LiveThreads.Edit(Id, description, nsfw, resources, title));
        }

        /// <summary>
        /// Configure the thread asynchronously.
        /// Requires the settings permission for this thread.
        /// </summary>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        public async Task EditAsync(string title, string description, bool nsfw, string resources)
        {
            await Task.Run(() =>
            {
                Edit(title, description, nsfw, resources);
            });
        }

        /// <summary>
        /// Invite another user to contribute to the thread.
        /// Requires the manage permission for this thread. If the recipient accepts the invite, they will be granted the permissions specified.
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public void InviteContributor(string name, string permissions, string type)
        {
            Validate(Dispatch.LiveThreads.InviteContributor(Id, name, permissions, type));
        }

        /// <summary>
        /// Asynchronously invite another user to contribute to the thread.
        /// Requires the manage permission for this thread. If the recipient accepts the invite, they will be granted the permissions specified.
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public async Task InviteContributorAsync(string name, string permissions, string type)
        {
            await Task.Run(() =>
            {
                InviteContributor(name, permissions, type);
            });
        }

        /// <summary>
        /// Abdicate contributorship of the thread.
        /// </summary>
        public void Abandon()
        {
            Validate(Dispatch.LiveThreads.LeaveContributor(Id));
        }

        /// <summary>
        /// Abdicate contributorship of the thread asynchronously.
        /// </summary>
        public async Task AbandonAsync()
        {
            await Task.Run(() =>
            {
                Abandon();
            });
        }

        /// <summary>
        /// Report the thread for violating the rules of reddit.
        /// </summary>
        /// <param name="type">one of (spam, vote-manipulation, personal-information, sexualizing-minors, site-breaking)</param>
        public void Report(string type)
        {
            Validate(Dispatch.LiveThreads.Report(Id, type));
        }

        /// <summary>
        /// Asynchronously report the thread for violating the rules of reddit.
        /// </summary>
        /// <param name="type">one of (spam, vote-manipulation, personal-information, sexualizing-minors, site-breaking)</param>
        public async Task ReportAsync(string type)
        {
            await Task.Run(() =>
            {
                Report(type);
            });
        }

        /// <summary>
        /// Revoke another user's contributorship.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="user">fullname of an account</param>
        public void RemoveContributor(string user)
        {
            Validate(Dispatch.LiveThreads.RemoveContributor(Id, user));
        }

        /// <summary>
        /// Revoke another user's contributorship asynchronously.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="user">fullname of an account</param>
        public async Task RemoveContributorAsync(string user)
        {
            await Task.Run(() =>
            {
                RemoveContributor(user);
            });
        }

        /// <summary>
        /// Revoke an outstanding contributor invite.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="user">fullname of an account</param>
        public void RemoveContributorInvite(string user)
        {
            Validate(Dispatch.LiveThreads.RemoveContributorInvite(Id, user));
        }

        /// <summary>
        /// Revoke an outstanding contributor invite asynchronously.
        /// Requires the manage permission for this thread.
        /// </summary>
        /// <param name="user">fullname of an account</param>
        public async Task RemoveContributorInviteAsync(string user)
        {
            await Task.Run(() =>
            {
                RemoveContributorInvite(user);
            });
        }

        /// <summary>
        /// Change a contributor or contributor invite's permissions.
        /// Requires the manage permission for this thread.
        /// Note that permissions overrides the previous value completely.
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public void SetContributorPermissions(string name, string permissions, string type)
        {
            Validate(Dispatch.LiveThreads.SetContributorPermissions(Id, name, permissions, type));
        }

        /// <summary>
        /// Change a contributor or contributor invite's permissions asynchronously.
        /// Requires the manage permission for this thread.
        /// Note that permissions overrides the previous value completely.
        /// </summary>
        /// <param name="name">the name of an existing user</param>
        /// <param name="permissions">permission description e.g. +update,+edit,-manage</param>
        /// <param name="type">one of (liveupdate_contributor_invite, liveupdate_contributor)</param>
        public async Task SetContributorPermissionsAsync(string name, string permissions, string type)
        {
            await Task.Run(() =>
            {
                SetContributorPermissions(name, permissions, type);
            });
        }

        /// <summary>
        /// Strike (mark incorrect and cross out) the content of an update.
        /// Requires that specified update must have been authored by the user or that you have the edit permission for this thread.
        /// </summary>
        /// <param name="updateId">the ID (Name) of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        public void StrikeUpdate(string updateId)
        {
            Validate(Dispatch.LiveThreads.StrikeUpdate(Id, updateId));
        }

        /// <summary>
        /// Strike (mark incorrect and cross out) the content of an update asynchronously.
        /// Requires that specified update must have been authored by the user or that you have the edit permission for this thread.
        /// </summary>
        /// <param name="updateId">the ID (Name) of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        public async Task StrikeUpdateAsync(string updateId)
        {
            await Task.Run(() =>
            {
                StrikeUpdate(updateId);
            });
        }

        /// <summary>
        /// Post an update to the thread.
        /// Requires the update permission for this thread.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        public void Update(string body)
        {
            Validate(Dispatch.LiveThreads.Update(Id, body));
        }

        /// <summary>
        /// Post an update to the thread asynchronously.
        /// Requires the update permission for this thread.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        public async Task UpdateAsync(string body)
        {
            await Task.Run(() =>
            {
                Update(body);
            });
        }

        /// <summary>
        /// Get a list of users that contribute to this thread.
        /// Note that this includes users who were invited but have not yet accepted.
        /// </summary>
        /// <returns>A list of users (0 => Active contributors, 1 => Invited/pending contributors).</returns>
        public List<RedditThings.UserListContainer> GetContributors()
        {
            Contributors = Validate(Dispatch.LiveThreads.Contributors(Id));
            ContributorsLastUpdated = DateTime.Now;

            return Contributors;
        }

        /// <summary>
        /// Get details about a specific update in a live thread.
        /// </summary>
        /// <param name="updateId">Update Id (not the Name; i.e. without the "LiveUpdate_" prefix)</param>
        /// <returns>The requested update.</returns>
        public RedditThings.LiveUpdate GetUpdate(string updateId)
        {
            return Validate(Dispatch.LiveThreads.GetUpdate(Id, updateId), 1).Data.Children[0].Data;
        }

        protected virtual void OnThreadUpdated(LiveThreadUpdateEventArgs e)
        {
            ThreadUpdated?.Invoke(this, e);
        }

        protected virtual void OnContributorsUpdated(LiveThreadContributorsUpdateEventArgs e)
        {
            ContributorsUpdated?.Invoke(this, e);
        }

        protected virtual void OnUpdatesUpdated(LiveThreadUpdatesUpdateEventArgs e)
        {
            UpdatesUpdated?.Invoke(this, e);
        }

        public bool MonitorThread()
        {
            string key = "LiveThread";
            return Monitor(key, new Thread(() => MonitorThreadThread(key)), Id);
        }

        public bool MonitorContributors()
        {
            string key = "LiveThreadContributors";
            return Monitor(key, new Thread(() => MonitorContributorsThread(key)), Id);
        }

        public bool MonitorUpdates()
        {
            string key = "LiveThreadUpdates";
            return Monitor(key, new Thread(() => MonitorUpdatesThread(key)), Id);
        }

        private bool Monitor(string key, Thread thread, string subKey)
        {
            bool res = Monitor(key, thread, subKey, out Thread newThread);

            RebuildThreads();
            LaunchThreadIfNotNull(key, newThread);

            return res;
        }

        private Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "LiveThread":
                    return new Thread(() => MonitorLiveThread(key, "thread", subKey, startDelayMs));
                case "LiveThreadContributors":
                    return new Thread(() => MonitorLiveThread(key, "contributors", subKey, startDelayMs));
                case "LiveThreadUpdates":
                    return new Thread(() => MonitorLiveThread(key, "updates", subKey, startDelayMs));
            }
        }

        private void RebuildThreads()
        {
            List<string> oldThreads = new List<string>(Threads.Keys);
            KillThreads(oldThreads);

            int i = 0;
            foreach (string key in oldThreads)
            {
                Threads.Add(key, CreateMonitoringThread(key, Id, (i * MonitoringWaitDelayMS)));
                Threads[key].Start();
                i++;
            }
        }

        private void MonitorThreadThread(string key)
        {
            MonitorLiveThread(key, "thread", Id);
        }

        private void MonitorContributorsThread(string key)
        {
            MonitorLiveThread(key, "contributors", Id);
        }

        private void MonitorUpdatesThread(string key)
        {
            MonitorLiveThread(key, "updates", Id);
        }

        private void MonitorLiveThread(string key, string type, string subKey, int startDelayMs = 0)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            while (!Terminate
                && Monitoring.Get(key).Contains(subKey))
            {
                switch (type)
                {
                    default:
                        throw new RedditControllerException("Unrecognized type '" + type + "'.");
                    case "thread":
                        CheckLiveThread();
                        break;
                    case "contributors":
                        CheckContributors();
                        break;
                    case "updates":
                        CheckUpdates();
                        break;
                }

                Thread.Sleep(Monitoring.Count() * MonitoringWaitDelayMS);
            }
        }

        private bool Diff(LiveThread compare)
        {
            return !(Id.Equals(compare.Id)
                && Description.Equals(compare.Description)
                && NSFW.Equals(compare.NSFW)
                && Resources.Equals(compare.Resources)
                && TotalViews.Equals(compare.TotalViews)
                && Created.Equals(compare.Created)
                && Fullname.Equals(compare.Fullname)
                && IsAnnouncement.Equals(compare.IsAnnouncement)
                && AnnouncementURL.Equals(compare.AnnouncementURL)
                && State.Equals(compare.State)
                && ViewerCount.Equals(compare.ViewerCount)
                && Icon.Equals(compare.Icon));
        }

        private void CheckLiveThread()
        {
            LiveThread newThread = About();

            if (Diff(newThread))
            {
                // Event handler to alert the calling app that the object has changed.  --Kris
                LiveThreadUpdateEventArgs args = new LiveThreadUpdateEventArgs
                {
                    OldThread = this, 
                    NewThread = newThread
                };
                OnThreadUpdated(args);
            }
        }

        /// <summary>
        /// If automatic monitoring of the thread (not updates or contributors) is enabled, this callback will apply any changes to this instance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_ApplyThreadUpdates(object sender, LiveThreadUpdateEventArgs e)
        {
            Import(e.NewThread.Id, e.NewThread.Description, e.NewThread.NSFW, e.NewThread.Resources, e.NewThread.Title, e.NewThread.TotalViews,
                e.NewThread.Created, e.NewThread.Fullname, e.NewThread.WebsocketURL, e.NewThread.AnnouncementURL, e.NewThread.State, e.NewThread.ViewerCount,
                e.NewThread.Icon);
        }

        private void CheckContributors()
        {
            List<RedditThings.UserListContainer> oldList = contributors;
            List<RedditThings.UserListContainer> newList = GetContributors();

            if (UserListDiff(oldList, newList, out List<RedditThings.UserListContainer> added, out List<RedditThings.UserListContainer> removed))
            {
                // Event handler to alert the calling app that the list has changed.  --Kris
                LiveThreadContributorsUpdateEventArgs args = new LiveThreadContributorsUpdateEventArgs
                {
                    OldContributors = oldList, 
                    NewContributors = newList, 
                    Added = added, 
                    Removed = removed
                };
                OnContributorsUpdated(args);
            }
        }

        private bool UserListDiff(List<RedditThings.UserListContainer> oldList, List<RedditThings.UserListContainer> newList, out List<RedditThings.UserListContainer> added,
            out List<RedditThings.UserListContainer> removed)
        {
            added = new List<RedditThings.UserListContainer>();
            removed = new List<RedditThings.UserListContainer>();

            if (oldList == null && newList == null)
            {
                return false;
            }
            else if (oldList == null)
            {
                added = newList;
                return true;
            }
            else if (newList == null)
            {
                removed = oldList;
                return true;
            }

            for (int i = 0; i <= 1; i++)
            {
                added.Add(new RedditThings.UserListContainer { Data = new RedditThings.UserListData { Children = new List<RedditThings.UserListChild>() } });
                removed.Add(new RedditThings.UserListContainer { Data = new RedditThings.UserListData { Children = new List<RedditThings.UserListChild>() } });

                if (ListDiff(oldList[i].Data.Children, newList[i].Data.Children, out List<RedditThings.UserListChild> childrenAdded, out List<RedditThings.UserListChild> childrenRemoved))
                {
                    added[i].Data.Children = childrenAdded;
                    removed[i].Data.Children = childrenRemoved;
                }
            }

            return !(added[0].Data.Children.Count == 0 && removed[0].Data.Children.Count == 0 
                && added[1].Data.Children.Count == 0 && removed[1].Data.Children.Count == 0);
        }

        private void CheckUpdates()
        {
            List<RedditThings.LiveUpdate> oldList = updates;
            List<RedditThings.LiveUpdate> newList = GetUpdates();

            if (ListDiff(oldList, newList, out List<RedditThings.LiveUpdate> added, out List<RedditThings.LiveUpdate> removed))
            {
                // Event handler to alert the calling app that the list has changed.  --Kris
                LiveThreadUpdatesUpdateEventArgs args = new LiveThreadUpdatesUpdateEventArgs
                {
                    OldUpdates = oldList,
                    NewUpdates = newList,
                    Added = added,
                    Removed = removed
                };
                OnUpdatesUpdated(args);
            }
        }
    }
}
