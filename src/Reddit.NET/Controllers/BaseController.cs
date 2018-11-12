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

        public void Validate(RedditThings.GenericContainer genericContainer)
        {
            if (genericContainer == null)
            {
                throw new RedditControllerException("Reddit API returned null response.");
            }

            Validate(genericContainer.JSON);
        }

        public void Validate(RedditThings.Generic generic)
        {
            if (generic == null)
            {
                throw new RedditControllerException("Reddit API returned empty response container.");
            }
            else if (generic.Errors != null && generic.Errors.Count > 0)
            {
                switch (generic.Errors[0][0])
                {
                    default:
                        throw (RedditControllerException)BuildException(new RedditControllerException("Reddit API returned errors."), generic.Errors);
                    case "RATELIMIT":
                        throw (RedditRateLimitException)BuildException(new RedditRateLimitException("Reddit ratelimit exceeded."), generic.Errors);
                    case "SUBREDDIT_EXISTS":
                        throw (RedditSubredditExistsException)BuildException(new RedditSubredditExistsException("That subreddit already exists."), generic.Errors);
                }
            }
        }
    }
}
