using System;

namespace Reddit.Inputs.Listings
{
    [Serializable]
    public class ListingsGetCommentsInput : BaseInput
    {
        /// <summary>
        /// an integer between 0 and 8
        /// </summary>
        public int context { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool showedits { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool showmore { get; set; }

        /// <summary>
        /// one of (confidence, top, new, controversial, old, random, qa, live)
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// boolean value
        /// </summary>
        public bool threaded { get; set; }

        /// <summary>
        /// an integer between 0 and 50
        /// </summary>
        public int truncate { get; set; }

        /// <summary>
        /// (optional) ID36 of a comment
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// (optional) an integer
        /// </summary>
        public int? depth { get; set; }

        /// <summary>
        /// (optional) an integer
        /// </summary>
        public int? limit { get; set; }

        /// <summary>
        /// (optional) expand subreddits
        /// </summary>
        public bool sr_detail { get; set; }

        public ListingsGetCommentsInput(int context = 0, bool showEdits = false, bool showMore = true, string sort = "new", bool threaded = true, int truncate = 0,
            string comment = null, int? depth = null, int? limit = null, bool srDetail = false)
        {
            this.context = context;
            showedits = showEdits;
            showmore = showMore;
            this.sort = sort;
            this.threaded = threaded;
            this.truncate = truncate;
            this.comment = comment;
            this.depth = depth;
            this.limit = limit;
            sr_detail = srDetail;
        }
    }
}
