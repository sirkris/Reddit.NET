using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Controllers.Structures
{
    public class SubredditPosts
    {
        public List<Post> Best
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? best : GetBest());
            }
            private set
            {
                best = value;
            }
        }
        private List<Post> best;

        public List<Post> Hot
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? hot : GetHot());
            }
            private set
            {
                hot = value;
            }
        }
        private List<Post> hot;

        public List<Post> New
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? newPosts : GetNew());
            }
            private set
            {
                newPosts = value;
            }
        }
        private List<Post> newPosts;

        public List<Post> Rising
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? rising : GetRising());
            }
            private set
            {
                rising = value;
            }
        }
        private List<Post> rising;

        public List<Post> Top
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? top : GetTop());
            }
            private set
            {
                top = value;
            }
        }
        private List<Post> top;

        public List<Post> Controversial
        {
            get
            {
                return (BestLastUpdated.HasValue
                    && BestLastUpdated.Value.AddSeconds(15) > DateTime.Now ? controversial : GetControversial());
            }
            private set
            {
                controversial = value;
            }
        }
        private List<Post> controversial;

        private DateTime? BestLastUpdated;
        private DateTime? HotLastUpdated;
        private DateTime? NewLastUpdated;
        private DateTime? RisingLastUpdated;
        private DateTime? TopLastUpdated;
        private DateTime? ControversialLastUpdated;

        private readonly string Subreddit;
        
        private readonly Dispatch Dispatch;

        public SubredditPosts(Dispatch dispatch, string subreddit, List<Post> best = null, List<Post> hot = null, List<Post> newPosts = null,
            List<Post> rising = null, List<Post> top = null, List<Post> controversial = null)
        {
            Best = best ?? new List<Post>();
            Hot = hot ?? new List<Post>();
            New = newPosts ?? new List<Post>();
            Rising = rising ?? new List<Post>();
            Top = top ?? new List<Post>();
            Controversial = controversial ?? new List<Post>();

            BestLastUpdated = null;
            HotLastUpdated = null;
            NewLastUpdated = null;
            RisingLastUpdated = null;
            TopLastUpdated = null;
            ControversialLastUpdated = null;

            Subreddit = subreddit;

            Dispatch = dispatch;
        }

        // Let's just pretend this one belongs to the "all" subreddit so we can put it here with the others.  --Kris
        public List<Post> GetBest(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Best(after, before, false, limit: limit));

            BestLastUpdated = DateTime.Now;

            Best = posts;
            return posts;
        }

        public List<Post> GetHot(string g = "", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Hot(g, after, before, false, limit: limit, subreddit: Subreddit));

            HotLastUpdated = DateTime.Now;

            Hot = posts;
            return posts;
        }

        public List<Post> GetNew(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.New(after, before, false, limit: limit, subreddit: Subreddit));

            NewLastUpdated = DateTime.Now;

            New = posts;
            return posts;
        }

        public List<Post> GetRising(string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Rising(after, before, false, limit: limit, subreddit: Subreddit));

            RisingLastUpdated = DateTime.Now;

            Rising = posts;
            return posts;
        }

        public List<Post> GetTop(string t = "all", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Top(t, after, before, false, limit: limit, subreddit: Subreddit));

            TopLastUpdated = DateTime.Now;

            Top = posts;
            return posts;
        }

        public List<Post> GetControversial(string t = "all", string after = "", string before = "", int limit = 100)
        {
            List<Post> posts = GetPosts(Dispatch.Listings.Controversial(t, after, before, false, limit: limit, subreddit: Subreddit));

            ControversialLastUpdated = DateTime.Now;

            Controversial = posts;
            return posts;
        }

        private List<Post> GetPosts(RedditThings.PostContainer postContainer)
        {
            List<Post> posts = new List<Post>();
            foreach (RedditThings.PostChild postChild in postContainer.Data.Children)
            {
                if (postChild.Data != null)
                {
                    if (postChild.Data.IsSelf)
                    {
                        posts.Add(new SelfPost(Dispatch, postChild.Data));
                    }
                    else
                    {
                        posts.Add(new LinkPost(Dispatch, postChild.Data));
                    }
                }
            }

            return posts;
        }
    }
}
