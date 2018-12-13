using Reddit.NET.Controllers.Structures;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    public class LiveThread : BaseController
    {
        internal override ref Models.Internal.Monitor MonitorModel => ref MonitorNull;
        internal override ref MonitoringSnapshot Monitoring => ref MonitoringSnapshotNull;

        public string Id;
        public string Description;
        public bool NSFW;
        public string Resources;
        public string Title;

        public int? TotalViews;
        public DateTime Created;
        public string Name;
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

        private readonly Dispatch Dispatch;

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
            int? totalViews, DateTime created, string name, string websocketUrl, string announcementUrl,
            string state, int viewerCount, string icon)
        {
            Id = id;
            Description = description;
            NSFW = nsfw;
            Resources = resources;
            Title = title;
            TotalViews = totalViews;
            Created = created;
            Name = name;
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
        public LiveThread Create(string title, string description, bool nsfw, string resources)
        {
            return new LiveThread(Dispatch, Validate(Dispatch.LiveThreads.Create(description, nsfw, resources, title)).JSON.Data.Id).About();
        }

        /// <summary>
        /// Create a new live thread asynchronously.
        /// </summary>
        /// <param name="title">a string no longer than 120 characters</param>
        /// <param name="description">raw markdown text</param>
        /// <param name="nsfw">boolean value</param>
        /// <param name="resources">raw markdown text</param>
        /// <returns>An instance of this class populated with data from the new live thread.</returns>
        public async Task CreateAsync(string title, string description, bool nsfw, string resources)
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
        /// <param name="updateId">the ID of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
        public void DeleteUpdate(string updateId)
        {
            Validate(Dispatch.LiveThreads.DeleteUpdate(Id, updateId));
        }

        /// <summary>
        /// Delete an update from the thread asynchronously.
        /// Requires that specified update must have been authored by the user or that you have the edit permission for this thread.
        /// </summary>
        /// <param name="updateId">the ID of a single update. e.g. LiveUpdate_ff87068e-a126-11e3-9f93-12313b0b3603</param>
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
        public List<RedditThings.UserListContainer> Contributors()
        {
            return Validate(Dispatch.LiveThreads.Contributors(Id));
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
    }
}
