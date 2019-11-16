using Newtonsoft.Json;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reddit.Controllers.Internal
{
    public class Lists
    {
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

        public List<CommentOrPost> GetCommentsAndPosts(OverviewContainer overviewContainer, Dispatch dispatch)
        {
            if (overviewContainer == null || overviewContainer.Data == null)
            {
                return null;
            }

            return overviewContainer.Data.Children;
        }

        public List<Post> GetPosts(PostResultContainer postContainer, Dispatch dispatch)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(PostResultContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts)
        {
            return GetPosts(postContainer, dispatch, out linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(PostResultContainer postContainer, Dispatch dispatch, out List<SelfPost> selfPosts)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out selfPosts);
        }

        public List<Post> GetPosts(PostResultContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts)
        {
            linkPosts = new List<LinkPost>();
            selfPosts = new List<SelfPost>();

            if (postContainer == null || postContainer.JSON == null || postContainer.JSON.Data == null || postContainer.JSON.Data.Things == null)
            {
                return null;
            }

            List<Post> posts = new List<Post>();
            foreach (PostChild postChild in postContainer.JSON.Data.Things)
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

        public List<Post> GetPosts(PostContainer postContainer, Dispatch dispatch)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(PostContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts)
        {
            return GetPosts(postContainer, dispatch, out linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(PostContainer postContainer, Dispatch dispatch, out List<SelfPost> selfPosts)
        {
            return GetPosts(postContainer, dispatch, out List<LinkPost> linkPosts, out selfPosts);
        }

        public List<Post> GetPosts(PostContainer postContainer, Dispatch dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts)
        {
            linkPosts = new List<LinkPost>();
            selfPosts = new List<SelfPost>();

            if (postContainer == null || postContainer.Data == null || postContainer.Data.Children == null)
            {
                return null;
            }

            List<Post> posts = new List<Post>();
            foreach (PostChild postChild in postContainer.Data.Children)
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

        public List<Post> GetPosts(List<PostContainer> postContainers, Dispatch dispatch)
        {
            return GetPosts(postContainers, dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(List<PostContainer> postContainers, Dispatch dispatch, out List<LinkPost> linkPosts)
        {
            return GetPosts(postContainers, dispatch, out linkPosts, out List<SelfPost> selfPosts);
        }

        public List<Post> GetPosts(List<PostContainer> postContainers, Dispatch dispatch, out List<SelfPost> selfPosts)
        {
            return GetPosts(postContainers, dispatch, out List<LinkPost> linkPosts, out selfPosts);
        }

        public List<Post> GetPosts(List<PostContainer> postContainers, Dispatch dispatch, out List<LinkPost> linkPosts, out List<SelfPost> selfPosts)
        {
            linkPosts = new List<LinkPost>();
            selfPosts = new List<SelfPost>();

            if (postContainers == null)
            {
                return null;
            }

            List<Post> posts = new List<Post>();
            foreach (PostContainer postContainer in postContainers)
            {
                posts.AddRange(GetPosts(postContainer, dispatch, out linkPosts, out selfPosts));
            }

            return posts;
        }

        public List<Comment> GetComments(CommentResultContainer commentContainer, Dispatch dispatch)
        {
            if (commentContainer == null || commentContainer.JSON == null || commentContainer.JSON.Data == null || commentContainer.JSON.Data.Things == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            foreach (CommentChild commentChild in commentContainer.JSON.Data.Things)
            {
                if (commentChild.Data != null)
                {
                    comments.Add(new Comment(dispatch, commentChild.Data));
                }
            }

            return comments;
        }

        public List<Comment> GetComments(CommentContainer commentContainer, Dispatch dispatch)
        {
            if (commentContainer == null || commentContainer.Data == null || commentContainer.Data.Children == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            foreach (CommentChild commentChild in commentContainer.Data.Children)
            {
                if (commentChild.Data != null)
                {
                    comments.Add(new Comment(dispatch, commentChild.Data));
                }
            }

            return comments;
        }

        public List<Comment> GetComments(List<Things.Comment> commentsList, Dispatch dispatch)
        {
            if (commentsList == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            foreach (Things.Comment comment in commentsList)
            {
                comments.Add(new Comment(dispatch, comment));
            }

            return comments;
        }

        public List<Subreddit> GetSubreddits(SubredditContainer subredditContainer, Dispatch dispatch)
        {
            List<Subreddit> subreddits = new List<Subreddit>();
            foreach (SubredditChild subredditChild in subredditContainer.Data.Children)
            {
                if (subredditChild.Data != null)
                {
                    subreddits.Add(new Subreddit(dispatch, subredditChild.Data));
                }
            }

            return subreddits;
        }

        public List<User> GetUsers(UserContainer userContainer, Dispatch dispatch)
        {
            List<User> users = new List<User>();
            foreach (UserChild userChild in userContainer.Data.Children)
            {
                if (userChild.Data != null)
                {
                    users.Add(new User(dispatch, userChild.Data));
                }
            }

            return users;
        }

        public List<LiveUpdate> GetLiveUpdates(LiveUpdateContainer liveUpdateContainer)
        {
            List<LiveUpdate> res = new List<LiveUpdate>();
            foreach (LiveUpdateChild liveUpdateChild in liveUpdateContainer.Data.Children)
            {
                res.Add(liveUpdateChild.Data);
            }

            return res;
        }

        public List<T> GetAboutChildren<T>(DynamicShortListingContainer dynamicShortListingContainer)
        {
            List<T> res = new List<T>();
            if (dynamicShortListingContainer.Data.Children != null)
            {
                res = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dynamicShortListingContainer.Data.Children));
            }

            return res;
        }
    }
}
