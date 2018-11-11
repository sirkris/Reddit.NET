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
