using System;

namespace Reddit.Inputs.Search
{
    [Serializable]
    public class SearchGetSearchInput : TimedCatSrListingInput
    {
        /// <summary>
        /// a string no longer than 5 characters
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool include_facets { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool restrict_sr { get; set; }

        /// <summary>
        /// one of (relevance, hot, top, new, comments)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// a string no longer than 512 characters
        /// </summary>
        public string q { get; set; }

        /// <summary>
        /// (optional) comma-delimited list of result types (sr, link, user)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Search links page.
        /// </summary>
        /// <param name="q">a string no longer than 512 characters</param>
        /// <param name="restrictSr">boolean value</param>
        /// <param name="sort">one of (relevance, hot, top, new, comments)</param>
        /// <param name="category">a string no longer than 5 characters</param>
        /// <param name="includeFacets">boolean value</param>
        /// <param name="type">(optional) comma-delimited list of result types (sr, link, user)</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">boolean value</param>
        public SearchGetSearchInput(string q = "", bool restrictSr = true, string sort = "new", string category = "", bool includeFacets = false, string type = null,
            string t = "all", string after = null, string before = null, bool includeCategories = false, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
            : base(t, after, before, includeCategories, count, limit, show, srDetail)
        {
            this.category = category;
            include_facets = includeFacets;
            restrict_sr = restrictSr;
            this.sort = sort;
            this.q = q;
            this.type = type;
        }
    }
}
