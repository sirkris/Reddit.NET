using Newtonsoft.Json;
using Reddit.NET.Exceptions;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Reddit.NET.Controllers
{
    public abstract class BaseController
    {
        internal Models.Internal.Monitor MonitorNull = null;
        internal MonitoringSnapshot MonitoringSnapshotNull = null;
        
        internal abstract ref Models.Internal.Monitor MonitorModel { get; }
        internal abstract ref MonitoringSnapshot Monitoring { get; }

        public int MonitoringWaitDelayMS = 1500;

        internal Dictionary<string, Thread> Threads;

        protected volatile bool Terminate = false;

        public BaseController()
        {
            Threads = new Dictionary<string, Thread>();
        }

        internal void TerminateThread()
        {
            Terminate = true;
        }

        public void WaitOrDie(Thread thread, int timeout = 60)
        {
            DateTime start = DateTime.Now;
            while (thread.IsAlive)
            {
                if (start.AddSeconds(timeout) <= DateTime.Now)
                {
                    // Thread.Abort was removed from .NET Core.  --Kris
                    throw new RedditControllerException("Unable to terminate monitoring thread (thread not responding).");
                }
            }
        }

        private string GetFullnameFromObj(dynamic obj)
        {
            return (obj is string ? obj : (string)obj.Fullname);
        }

        /// <summary>
        /// Scan two lists for any differences.  Sequence is ignored.
        /// T must be a string or an object with a string Fullname.
        /// </summary>
        /// <param name="oldList">The original list being compared against</param>
        /// <param name="newList">The new list</param>
        /// <param name="added">Any entries that are present in the new list but not the old</param>
        /// <param name="removed">Any entries that are present in the old list but not the new</param>
        /// <returns>True if the lists differ, otherwise false.</returns>
        public bool ListDiff<T>(List<T> oldList, List<T> newList, out List<T> added, out List<T> removed)
        {
            added = new List<T>();
            removed = new List<T>();

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

            // Index by Reddit fullname.  --Kris
            Dictionary<string, T> oldByFullname = new Dictionary<string, T>();
            Dictionary<string, T> newByFullname = new Dictionary<string, T>();
            for (int i = 0; i < Math.Max(oldList.Count, newList.Count); i++)
            {
                if (i < oldList.Count)
                {
                    oldByFullname.Add(GetFullnameFromObj(oldList[i]), oldList[i]);
                }

                if (i < newList.Count)
                {
                    newByFullname.Add(GetFullnameFromObj(newList[i]), newList[i]);
                }
            }

            // Scan for any new objects.  --Kris
            foreach (KeyValuePair<string, T> pair in newByFullname)
            {
                if (!oldByFullname.ContainsKey(pair.Key))
                {
                    added.Add(pair.Value);
                }
                else
                {
                    // So we don't have to check the same element twice.  --Kris
                    oldByFullname.Remove(pair.Key);
                }
            }

            // Scan for any objects no longer appearing in the list.  --Kris
            foreach (KeyValuePair<string, T> pair in oldByFullname)
            {
                // All the matching elements are gone, leaving only the removed ones.  --Kris
                removed.Add(pair.Value);
            }

            return !(added.Count == 0 && removed.Count == 0);
        }

        /// <summary>
        /// The Reddit API doesn't always return new-sorted posts in the correct chronological order (pinned posts are always on top, for example).
        /// Use this method to give the list a proper sort.
        /// </summary>
        /// <param name="posts">A list of posts</param>
        /// <param name="descending">If true, sort by descending order (newest first); otherwise, sort by ascending order (oldest first)</param>
        /// <returns>A chronologically sorted list of posts.</returns>
        public List<Post> ForceNewSort(List<Post> posts, bool descending = true)
        {
            if (descending)
            {
                return posts.OrderByDescending(p => p.Created).ToList();
            }
            else
            {
                return posts.OrderBy(p => p.Created).ToList();
            }
        }

        /// <summary>
        /// The Reddit API doesn't always return new-sorted comments in the correct chronological order (pinned comments are always on top, for example).
        /// Use this method to give the list a proper sort.
        /// </summary>
        /// <param name="comments">A list of comments</param>
        /// <param name="descending">If true, sort by descending order (newest first); otherwise, sort by ascending order (oldest first)</param>
        /// <returns>A chronologically sorted list of comments.</returns>
        public List<Comment> ForceNewSort(List<Comment> comments, bool descending = true)
        {
            if (descending)
            {
                return comments.OrderByDescending(p => p.Created).ToList();
            }
            else
            {
                return comments.OrderBy(p => p.Created).ToList();
            }
        }

        public List<Post> GetPosts(RedditThings.PostResultContainer postContainer, Dispatch dispatch)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(RedditThings.PostResultContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts)
        {
            return GetPosts(postContainer, dispatch, out linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(RedditThings.PostResultContainer postContainer, Dispatch dispatch, out List<SelfPost> selfPosts)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out selfPosts);
        }

        public List<Post> GetPosts(RedditThings.PostResultContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts)
        {
            linkPosts = new List<LinkPost>();
            selfPosts = new List<SelfPost>();

            if (postContainer == null || postContainer.JSON == null || postContainer.JSON.Data == null || postContainer.JSON.Data.Things == null)
            {
                return null;
            }

            List<Post> posts = new List<Post>();
            foreach (RedditThings.PostChild postChild in postContainer.JSON.Data.Things)
            {
                if (postChild.Data != null)
                {
                    if (postChild.Data.IsSelf)
                    {
                        SelfPost selfPost = new SelfPost(dispatch, postChild.Data);
                        posts.Add(selfPost);
                        selfPosts.Add(selfPost);
                    }
                    else
                    {
                        LinkPost linkPost = new LinkPost(dispatch, postChild.Data);
                        posts.Add(linkPost);
                        linkPosts.Add(linkPost);
                    }
                }
            }

            return posts;
        }

        public List<Post> GetPosts(RedditThings.PostContainer postContainer, Dispatch dispatch)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(RedditThings.PostContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts)
        {
            return GetPosts(postContainer, dispatch, out linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(RedditThings.PostContainer postContainer, Dispatch dispatch, out List<SelfPost> selfPosts)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out selfPosts);
        }

        public List<Post> GetPosts(RedditThings.PostContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts)
        {
            linkPosts = new List<LinkPost>();
            selfPosts = new List<SelfPost>();

            if (postContainer == null || postContainer.Data == null || postContainer.Data.Children == null)
            {
                return null;
            }

            List<Post> posts = new List<Post>();
            foreach (RedditThings.PostChild postChild in postContainer.Data.Children)
            {
                if (postChild.Data != null)
                {
                    if (postChild.Data.IsSelf)
                    {
                        SelfPost selfPost = new SelfPost(dispatch, postChild.Data);
                        posts.Add(selfPost);
                        selfPosts.Add(selfPost);
                    }
                    else
                    {
                        LinkPost linkPost = new LinkPost(dispatch, postChild.Data);
                        posts.Add(linkPost);
                        linkPosts.Add(linkPost);
                    }
                }
            }

            return posts;
        }

        public List<Post> GetPosts(List<RedditThings.PostContainer> postContainers, Dispatch dispatch)
        {
            return GetPosts(postContainers, dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(List<RedditThings.PostContainer> postContainers, Dispatch dispatch, out List<LinkPost> linkPosts)
        {
            return GetPosts(postContainers, dispatch, out linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(List<RedditThings.PostContainer> postContainers, Dispatch dispatch, out List<SelfPost> selfPosts)
        {
            return GetPosts(postContainers, dispatch, out List<LinkPost> linkPosts, out selfPosts);
        }

        public List<Post> GetPosts(List<RedditThings.PostContainer> postContainers, Dispatch dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts)
        {
            linkPosts = new List<LinkPost>();
            selfPosts = new List<SelfPost>();

            if (postContainers == null)
            {
                return null;
            }

            List<Post> posts = new List<Post>();
            foreach (RedditThings.PostContainer postContainer in postContainers)
            {
                posts.AddRange(GetPosts(postContainer, dispatch, out linkPosts, out selfPosts));
            }

            return posts;
        }

        public List<Comment> GetComments(RedditThings.CommentResultContainer commentContainer, Dispatch dispatch)
        {
            if (commentContainer == null || commentContainer.JSON == null || commentContainer.JSON.Data == null || commentContainer.JSON.Data.Things == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            foreach (RedditThings.CommentChild commentChild in commentContainer.JSON.Data.Things)
            {
                if (commentChild.Data != null)
                {
                    comments.Add(new Comment(dispatch, commentChild.Data));
                }
            }

            return comments;
        }

        public List<Comment> GetComments(RedditThings.CommentContainer commentContainer, Dispatch dispatch)
        {
            if (commentContainer == null || commentContainer.Data == null || commentContainer.Data.Children == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            foreach (RedditThings.CommentChild commentChild in commentContainer.Data.Children)
            {
                if (commentChild.Data != null)
                {
                    comments.Add(new Comment(dispatch, commentChild.Data));
                }
            }

            return comments;
        }

        public List<Comment> GetComments(List<(RedditThings.PostContainer, RedditThings.CommentContainer)> ps, Dispatch dispatch)
        {
            return GetComments(ps[0].Item2, dispatch);
        }

        public List<Subreddit> GetSubreddits(RedditThings.SubredditContainer subredditContainer, Dispatch dispatch)
        {
            List<Subreddit> subreddits = new List<Subreddit>();
            foreach (RedditThings.SubredditChild subredditChild in subredditContainer.Data.Children)
            {
                if (subredditChild.Data != null)
                {
                    subreddits.Add(new Subreddit(dispatch, subredditChild.Data));
                }
            }

            return subreddits;
        }

        public List<RedditThings.LiveUpdate> GetLiveUpdates(RedditThings.LiveUpdateContainer liveUpdateContainer)
        {
            List<RedditThings.LiveUpdate> res = new List<RedditThings.LiveUpdate>();
            foreach (RedditThings.LiveUpdateChild liveUpdateChild in liveUpdateContainer.Data.Children)
            {
                res.Add(liveUpdateChild.Data);
            }

            return res;
        }

        protected void LaunchThreadIfNotNull(string key, Thread thread)
        {
            if (thread != null)
            {
                Threads.Add(key, thread);
                Threads[key].Start();
                while (!Threads[key].IsAlive) { }
            }
        }

        internal bool Monitor(string key, Thread thread, string subKey, out Thread newThread)
        {
            newThread = null;
            if (Monitoring.Get(key).Contains(subKey))
            {
                // Stop monitoring.  --Kris
                MonitorModel.RemoveMonitoringKey(key, subKey, ref Monitoring);
                WaitOrDie(Threads[key]);

                Threads.Remove(key);

                return false;
            }
            else
            {
                // Start monitoring.  --Kris
                MonitorModel.AddMonitoringKey(key, subKey, ref Monitoring);

                newThread = thread;

                return true;
            }
        }

        protected void KillThreads(Dictionary<string, Thread> oldThreads)
        {
            TerminateThread();

            foreach (KeyValuePair<string, Thread> pair in oldThreads)
            {
                pair.Value.Join();
                Threads.Remove(pair.Key);
            }

            Terminate = false;
        }

        protected List<T> GetAboutChildren<T>(RedditThings.DynamicShortListingContainer dynamicShortListingContainer)
        {
            List<T> res = new List<T>();
            if (dynamicShortListingContainer.Data.Children != null)
            {
                res = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dynamicShortListingContainer.Data.Children));
            }

            return res;
        }

        public Exception BuildException(Exception ex, List<List<string>> errors)
        {
            ex.Data.Add("errors", errors);

            return ex;
        }

        private void CheckErrors(List<List<string>> errors)
        {
            if (errors != null)
            {
                foreach (List<string> errorList in errors)
                {
                    CheckErrors(errorList);
                }
            }
        }

        // Exception thrown will be on the first error in the list.  --Kris
        private void CheckErrors(List<string> errors)
        {
            if (errors != null)
            {
                foreach (string error in errors)
                {
                    switch (error)
                    {
                        default:
                            throw (RedditControllerException)BuildException(new RedditControllerException("Reddit API returned errors."), new List<List<string>> { errors });
                        case "RATELIMIT":
                            throw (RedditRateLimitException)BuildException(new RedditRateLimitException("Reddit ratelimit exceeded."), new List<List<string>> { errors });
                        case "SUBREDDIT_EXISTS":
                            throw (RedditSubredditExistsException)BuildException(new RedditSubredditExistsException("That subreddit already exists."),
                                new List<List<string>> { errors });
                        case "INVALID_PERMISSION_TYPE":
                            throw (RedditInvalidPermissionTypeException)BuildException(new RedditInvalidPermissionTypeException(errors[1]),
                                new List<List<string>> { errors });
                    }
                }
            }
        }

        private void CheckNull(object res, string msg = "Reddit API returned null response.")
        {
            if (res == null)
            {
                throw new RedditControllerException(msg);
            }
        }

        public dynamic Validate(dynamic obj)
        {
            CheckNull(obj);

            return obj;
        }
        
        public RedditThings.GenericContainer Validate(RedditThings.GenericContainer genericContainer)
        {
            CheckNull(genericContainer);

            Validate(genericContainer.JSON);

            return genericContainer;
        }

        public RedditThings.Generic Validate(RedditThings.Generic generic)
        {
            CheckNull(generic, "Reddit API returned empty response container.");

            CheckErrors(generic.Errors);

            return generic;
        }

        public RedditThings.DynamicShortListingContainer Validate(RedditThings.DynamicShortListingContainer dynamicShortListingContainer)
        {
            CheckNull(dynamicShortListingContainer);

            Validate(dynamicShortListingContainer.Data);

            return dynamicShortListingContainer;
        }

        public RedditThings.DynamicShortListingData Validate(RedditThings.DynamicShortListingData dynamicShortListingData)
        {
            CheckNull(dynamicShortListingData, "Reddit API returned empty response container.");

            return dynamicShortListingData;
        }

        public RedditThings.ImageUploadResult Validate(RedditThings.ImageUploadResult imageUploadResult)
        {
            CheckNull(imageUploadResult);

            CheckErrors(imageUploadResult.Errors);

            return imageUploadResult;
        }

        public RedditThings.LiveUpdateEventContainer Validate(RedditThings.LiveUpdateEventContainer liveUpdateEventContainer)
        {
            CheckNull(liveUpdateEventContainer);
            CheckNull(liveUpdateEventContainer.Data);

            return liveUpdateEventContainer;
        }

        public RedditThings.LiveThreadCreateResultContainer Validate(RedditThings.LiveThreadCreateResultContainer liveThreadCreateResultContainer)
        {
            CheckNull(liveThreadCreateResultContainer);
            CheckNull(liveThreadCreateResultContainer.JSON);
            CheckErrors(liveThreadCreateResultContainer.JSON.Errors);
            CheckNull(liveThreadCreateResultContainer.JSON.Data);
            CheckNull(liveThreadCreateResultContainer.JSON.Data.Id);

            return liveThreadCreateResultContainer;
        }

        public RedditThings.LiveUpdateContainer Validate(RedditThings.LiveUpdateContainer liveUpdateContainer, int? minChildren = null)
        {
            CheckNull(liveUpdateContainer);
            CheckNull(liveUpdateContainer.Data);
            if (minChildren.HasValue)
            {
                CheckNull(liveUpdateContainer.Data.Children);
                if (liveUpdateContainer.Data.Children.Count < minChildren.Value)
                {
                    throw new RedditControllerException("Expected number of results not returned.");
                }
            }

            return liveUpdateContainer;
        }

        public RedditThings.SubredditSettingsContainer Validate(RedditThings.SubredditSettingsContainer subredditSettingsContainer)
        {
            CheckNull(subredditSettingsContainer);

            Validate(subredditSettingsContainer.Data);

            return subredditSettingsContainer;
        }

        public RedditThings.SubredditSettings Validate(RedditThings.SubredditSettings subredditSettings)
        {
            CheckNull(subredditSettings, "Reddit API returned empty response container.");

            return subredditSettings;
        }

        public List<RedditThings.ActionResult> Validate(List<RedditThings.ActionResult> actionResults)
        {
            CheckNull(actionResults);

            foreach (RedditThings.ActionResult actionResult in actionResults)
            {
                Validate(actionResult);
            }

            return actionResults;
        }

        public RedditThings.ActionResult Validate(RedditThings.ActionResult actionResult)
        {
            CheckNull(actionResult);

            if (!actionResult.Ok)
            {
                RedditControllerException ex = new RedditControllerException("Reddit API returned non-Ok response.");

                ex.Data.Add("actionResult", actionResult);

                throw ex;
            }

            return actionResult;
        }

        public RedditThings.FlairListResultContainer Validate(RedditThings.FlairListResultContainer flairListResultContainer)
        {
            CheckNull(flairListResultContainer);
            CheckNull(flairListResultContainer.Users, "Reddit API returned empty response container.");

            foreach (RedditThings.FlairListResult flairListResult in flairListResultContainer.Users)
            {
                Validate(flairListResult);
            }

            return flairListResultContainer;
        }

        public RedditThings.Flair Validate(RedditThings.Flair flair)
        {
            CheckNull(flair);
            CheckNull(flair.Id, "Reddit API returned flair object with no Id.");

            return flair;
        }

        public RedditThings.FlairV2 Validate(RedditThings.FlairV2 flairV2)
        {
            CheckNull(flairV2);
            CheckNull(flairV2.Id, "Reddit API returned flair object with no Id.");

            return flairV2;
        }

        public RedditThings.ModActionContainer Validate(RedditThings.ModActionContainer modActionContainer)
        {
            CheckNull(modActionContainer);

            Validate(modActionContainer.Data);

            return modActionContainer;
        }

        public RedditThings.ModActionData Validate(RedditThings.ModActionData modActionData)
        {
            CheckNull(modActionData, "Reddit API returned empty response object.");
            CheckNull(modActionData.Children, "Reddit API returned response with null children.");

            return modActionData;
        }

        public RedditThings.WikiPageRevisionContainer Validate(RedditThings.WikiPageRevisionContainer wikiPageRevisionContainer)
        {
            CheckNull(wikiPageRevisionContainer);
            CheckNull(wikiPageRevisionContainer.Data, "Reddit API returned empty response object.");

            return wikiPageRevisionContainer;
        }

        public RedditThings.WikiPageRevisionData Validate(RedditThings.WikiPageRevisionData wikiPageRevisionData)
        {
            CheckNull(wikiPageRevisionData);

            return wikiPageRevisionData;
        }

        public RedditThings.WikiPageSettingsContainer Validate(RedditThings.WikiPageSettingsContainer wikiPageSettingsContainer)
        {
            CheckNull(wikiPageSettingsContainer);
            CheckNull(wikiPageSettingsContainer.Data, "Reddit API returned empty response object.");

            return wikiPageSettingsContainer;
        }

        public RedditThings.WikiPageSettings Validate(RedditThings.WikiPageSettings wikiPageSettings)
        {
            CheckNull(wikiPageSettings);

            return wikiPageSettings;
        }

        public List<RedditThings.UserPrefsContainer> Validate(List<RedditThings.UserPrefsContainer> userPrefsContainers)
        {
            CheckNull(userPrefsContainers);

            foreach (RedditThings.UserPrefsContainer userPrefsContainer in userPrefsContainers)
            {
                CheckNull(userPrefsContainer, "Reddit API returned a list with at least one null entry.");
                CheckNull(userPrefsContainer.Data, "Reddit API returned a list with at least one entry that contains null data.");
            }

            return userPrefsContainers;
        }

        public RedditThings.UserPrefsContainer Validate(RedditThings.UserPrefsContainer userPrefsContainer)
        {
            CheckNull(userPrefsContainer);
            CheckNull(userPrefsContainer.Data, "Reddit API returned empty response object.");

            return userPrefsContainer;
        }

        public RedditThings.UserPrefsData Validate(RedditThings.UserPrefsData userPrefsData)
        {
            CheckNull(userPrefsData);

            return userPrefsData;
        }

        public RedditThings.PostResultShortContainer Validate(RedditThings.PostResultShortContainer postResultShortContainer)
        {
            CheckNull(postResultShortContainer);
            CheckNull(postResultShortContainer.JSON, "Reddit API returned an empty response object.");
            CheckErrors(postResultShortContainer.JSON.Errors);
            CheckNull(postResultShortContainer.JSON.Data, "Reddit API returned a response object with null data.");

            return postResultShortContainer;
        }

        public RedditThings.PostResultShort Validate(RedditThings.PostResultShort postResultShort)
        {
            CheckNull(postResultShort);
            CheckErrors(postResultShort.Errors);
            CheckNull(postResultShort.Data, "Reddit API returned an empty response object.");

            return postResultShort;
        }

        public RedditThings.PostResultContainer Validate(RedditThings.PostResultContainer postResultContainer)
        {
            CheckNull(postResultContainer);
            CheckNull(postResultContainer.JSON, "Reddit API returned an empty response object.");
            CheckErrors(postResultContainer.JSON.Errors);
            CheckNull(postResultContainer.JSON.Data, "Reddit API returned a response object with null data.");
            CheckNull(postResultContainer.JSON.Data.Things, "Reddit API returned a response object with empty data.");

            if (postResultContainer.JSON.Data.Things.Count == 0)
            {
                throw new RedditControllerException("Reddit API returned a PostResultContainer with an empty result list.");
            }

            return postResultContainer;
        }

        public RedditThings.PostResult Validate(RedditThings.PostResult postResult)
        {
            CheckNull(postResult);
            CheckErrors(postResult.Errors);
            CheckNull(postResult.Data, "Reddit API returned an empty response object.");
            CheckNull(postResult.Data.Things, "Reddit API returned a response object with empty data.");

            if (postResult.Data.Things.Count == 0)
            {
                throw new RedditControllerException("Reddit API returned a PostResult with an empty result list.");
            }

            return postResult;
        }

        public RedditThings.JQueryReturn Validate(RedditThings.JQueryReturn jQueryReturn)
        {
            CheckNull(jQueryReturn);

            if (!jQueryReturn.Success)
            {
                throw new RedditControllerException("Reddit API returned a non-success response.");
            }

            return jQueryReturn;
        }

        public List<(RedditThings.PostContainer, RedditThings.CommentContainer)> Validate(List<(RedditThings.PostContainer, RedditThings.CommentContainer)> ps)
        {
            CheckNull(ps);

            if (ps.Count == 0)
            {
                throw new RedditControllerException("Empty list returned.");
            }

            CheckNull(ps[0].Item1);
            CheckNull(ps[0].Item2);

            return ps;
        }

        public RedditThings.CommentResultContainer Validate(RedditThings.CommentResultContainer commentResultContainer)
        {
            CheckNull(commentResultContainer);
            CheckNull(commentResultContainer.JSON, "Reddit API returned empty response object.");

            CheckErrors(commentResultContainer.JSON.Errors);

            CheckNull(commentResultContainer.JSON.Data, "Reddit API returned response object with empty JSON.");
            CheckNull(commentResultContainer.JSON.Data.Things, "Reddit API returned response object with empty data.");

            if (commentResultContainer.JSON.Data.Things.Count == 0)
            {
                throw new RedditControllerException("JSON data contains empty comments list.");
            }

            CheckNull(commentResultContainer.JSON.Data.Things[0].Data, "Reddit API returned response object with null comment data.");

            return commentResultContainer;
        }

        public RedditThings.SubredditContainer Validate(RedditThings.SubredditContainer subredditContainer)
        {
            CheckNull(subredditContainer);
            CheckNull(subredditContainer.Data, "Reddit API returned empty response object.");
            CheckNull(subredditContainer.Data.Children, "Reddit API returned a response object with null children.");

            return subredditContainer;
        }
    }
}
