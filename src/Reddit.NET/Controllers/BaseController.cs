using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers
{
    public abstract class BaseController
    {
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
