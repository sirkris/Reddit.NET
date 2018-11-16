using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    public class Account : BaseController
    {
        public User Me
        {
            get
            {
                return (MeLastUpdated.HasValue
                    && MeLastUpdated.Value.AddMinutes(1) > DateTime.Now ? me : GetMe());
            }
            set
            {
                me = value;
                MeLastUpdated = DateTime.Now;
            }
        }
        private User me;
        private DateTime? MeLastUpdated;

        public Dispatch Dispatch;

        public Account(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        /// <summary>
        /// Returns a User instance with the data returned from a call to the "me" endpoint.
        /// </summary>
        public User GetMe()
        {
            Me = new User(Dispatch, Dispatch.Account.Me());
            return Me;
        }

        /// <summary>
        /// Return a breakdown of subreddit karma.
        /// </summary>
        /// <returns>A breakdown of subreddit karma.</returns>
        public List<RedditThings.UserKarma> Karma()
        {
            return ((RedditThings.UserKarmaContainer)Validate(Dispatch.Account.Karma())).Data;
        }

        /// <summary>
        /// Return the preference settings of the logged in user.
        /// </summary>
        /// <returns>The preference settings of the logged in user.</returns>
        public RedditThings.AccountPrefs Prefs()
        {
            return Validate(Dispatch.Account.Prefs());
        }

        /// <summary>
        /// Update preferences.
        /// </summary>
        /// <param name="accountPrefs">A valid AccountPrefs instance.</param>
        /// <returns>The updated preference settings of the logged in user.</returns>
        public RedditThings.AccountPrefs UpdatePrefs(RedditThings.AccountPrefsSubmit accountPrefs)
        {
            return Validate(Dispatch.Account.UpdatePrefs(accountPrefs));
        }

        /// <summary>
        /// Update preferences asynchronously.
        /// </summary>
        /// <param name="accountPrefs">A valid AccountPrefs instance.</param>
        public async void UpdatePrefsAsync(RedditThings.AccountPrefsSubmit accountPrefs)
        {
            await Task.Run(() =>
            {
                Validate(UpdatePrefs(accountPrefs));
            });
        }

        /// <summary>
        /// Return a list of trophies for the current user.
        /// </summary>
        /// <returns>A list of trophies for the current user.</returns>
        public List<RedditThings.Award> Trophies()
        {
            RedditThings.TrophyList trophyList = Dispatch.Account.Trophies();
            if (trophyList == null || trophyList.Data == null || trophyList.Data.Trophies == null)
            {
                return null;
            }

            List<RedditThings.Award> res = new List<RedditThings.Award>();
            foreach (RedditThings.AwardContainer awardContainer in trophyList.Data.Trophies)
            {
                res.Add(awardContainer.Data);
            }

            return res;
        }

        /// <summary>
        /// Get users whom the current user has friended.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<RedditThings.UserPrefs> Friends(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            List<RedditThings.UserPrefs> res = new List<RedditThings.UserPrefs>();
            foreach (RedditThings.UserPrefsContainer userPrefsContainer in Validate(Dispatch.Account.PrefsList("friends", after, before, count, limit,
                show, srDetail, includeCategories)))
            {
                res.AddRange(userPrefsContainer.Data.Children);
            }

            return res;
        }

        /// <summary>
        /// Get users with whom the current user is messaging.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<RedditThings.UserPrefs> Messaging(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            List<RedditThings.UserPrefs> res = new List<RedditThings.UserPrefs>();
            foreach (RedditThings.UserPrefsContainer userPrefsContainer in Validate(Dispatch.Account.PrefsList("messaging", after, before, count, limit,
                show, srDetail, includeCategories)))
            {
                res.AddRange(userPrefsContainer.Data.Children);
            }

            return res;
        }

        /// <summary>
        /// Get users whom the current user has blocked.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<RedditThings.UserPrefs> Blocked(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            return Validate(Dispatch.Account.PrefsSingle("blocked", after, before, count, limit, show, srDetail, includeCategories)).Data.Children;
        }

        /// <summary>
        /// Get users whom the current user has trusted.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<RedditThings.UserPrefs> Trusted(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            return Validate(Dispatch.Account.PrefsSingle("trusted", after, before, count, limit, show, srDetail, includeCategories)).Data.Children;
        }
    }
}
