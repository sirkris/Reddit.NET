using Reddit.NET.Exceptions;
using Reddit.NET.Controllers.EventArgs;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.NET.Controllers
{
    public abstract class BaseController
    {
        // Value is always null.  --Kris
        public Dictionary<string, string> Monitoring;

        public event EventHandler<MonitoringUpdateEventArgs> MonitoringUpdated;

        public int MonitoringWaitDelayMS = 1500;

        public BaseController()
        {
            Monitoring = new Dictionary<string, string>();
            MonitoringUpdated += C_MonitoringUpdated;
        }

        public void WaitOrDie(Thread thread, int timeout = 3)
        {
            DateTime start = DateTime.Now;
            while (thread.IsAlive)
            {
                if (start.AddSeconds(timeout) <= DateTime.Now)
                {
                    thread.Abort();
                    break;
                }
            }
        }

        protected virtual void OnMonitoringUpdated(MonitoringUpdateEventArgs e)
        {
            MonitoringUpdated?.Invoke(this, e);
        }

        public virtual void UpdateMonitoring(Dictionary<string, string> monitoring)
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

        public RedditThings.RulesContainer Validate(RedditThings.RulesContainer rulesContainer)
        {
            CheckNull(rulesContainer);

            return rulesContainer;
        }

        public RedditThings.Traffic Validate(RedditThings.Traffic traffic)
        {
            CheckNull(traffic);

            return traffic;
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

        public RedditThings.FlairListResult Validate(RedditThings.FlairListResult flairListResult)
        {
            CheckNull(flairListResult);

            return flairListResult;
        }

        public RedditThings.FlairSelectorResultContainer Validate(RedditThings.FlairSelectorResultContainer flairSelectorResultContainer)
        {
            CheckNull(flairSelectorResultContainer);

            return flairSelectorResultContainer;
        }

        public RedditThings.FlairSelectorResult Validate(RedditThings.FlairSelectorResult flairSelectorResult)
        {
            CheckNull(flairSelectorResult);

            return flairSelectorResult;
        }

        public List<RedditThings.Flair> Validate(List<RedditThings.Flair> flairs)
        {
            CheckNull(flairs);

            return flairs;
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
    }
}
