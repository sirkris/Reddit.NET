using Reddit.NET.Exceptions;
using Reddit.NET.Controllers.EventArgs;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Reddit.NET.Controllers
{
    public abstract class BaseController
    {
        // Value is always null.  --Kris
        public Dictionary<string, List<string>> Monitoring;

        public event EventHandler<MonitoringUpdateEventArgs> MonitoringUpdated;

        public int MonitoringWaitDelayMS = 1500;

        public BaseController()
        {
            Monitoring = new Dictionary<string, List<string>>();
            MonitoringUpdated += C_MonitoringUpdated;
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

        public int MonitoringCount()
        {
            return Monitoring.Sum(x => x.Value.Count);
        }

        protected virtual void OnMonitoringUpdated(MonitoringUpdateEventArgs e)
        {
            MonitoringUpdated?.Invoke(this, e);
        }

        public virtual void UpdateMonitoring(Dictionary<string, List<string>> monitoring)
        {
            Monitoring = monitoring;
        }

        public void C_MonitoringUpdated(object sender, MonitoringUpdateEventArgs e)
        {
            UpdateMonitoring(e.Monitoring);
        }

        /// <summary>
        /// Scan two lists for any differences.  Sequence is ignored.
        /// </summary>
        /// <param name="oldList">The original list being compared against</param>
        /// <param name="newList">The new list</param>
        /// <param name="added">Any entries that are present in the new list but not the old</param>
        /// <param name="removed">Any entries that are present in the old list but not the new</param>
        /// <returns>True if the lists differ, otherwise false.</returns>
        public bool ListDiff(List<Post> oldList, List<Post> newList, out List<Post> added, out List<Post> removed)
        {
            added = new List<Post>();
            removed = new List<Post>();

            // Index by Reddit fullname.  --Kris
            Dictionary<string, Post> oldByFullname = new Dictionary<string, Post>();
            Dictionary<string, Post> newByFullname = new Dictionary<string, Post>();
            for (int i = 0; i < Math.Max(oldList.Count, newList.Count); i++)
            {
                if (i < oldList.Count)
                {
                    oldByFullname.Add(oldList[i].Fullname, oldList[i]);
                }

                if (i < newList.Count)
                {
                    newByFullname.Add(newList[i].Fullname, newList[i]);
                }
            }

            // Scan for any new posts.  --Kris
            foreach (KeyValuePair<string, Post> pair in newByFullname)
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

            // Scan for any posts no longer appearing in the list.  --Kris
            foreach (KeyValuePair<string, Post> pair in oldByFullname)
            {
                // All the matching elements are gone, leaving only the removed ones.  --Kris
                removed.Add(pair.Value);
            }

            return !(added.Count == 0 && removed.Count == 0);
        }

        public List<Post> GetPosts(RedditThings.PostContainer postContainer, Dispatch dispatch)
        {
            List<Post> posts = new List<Post>();
            foreach (RedditThings.PostChild postChild in postContainer.Data.Children)
            {
                if (postChild.Data != null)
                {
                    if (postChild.Data.IsSelf)
                    {
                        posts.Add(new SelfPost(dispatch, postChild.Data));
                    }
                    else
                    {
                        posts.Add(new LinkPost(dispatch, postChild.Data));
                    }
                }
            }

            return posts;
        }

        public List<Comment> GetComments(RedditThings.CommentContainer commentContainer, Dispatch dispatch)
        {
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
    }
}
