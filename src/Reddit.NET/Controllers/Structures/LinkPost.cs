using Newtonsoft.Json.Linq;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers.Structures
{
    class LinkPost : Post
    {
        public JObject Preview;
        public string URL;
        public string Thumbnail;
        public int? ThumbnailHeight;
        public int? ThumbnailWidth;

        public LinkPost(Listing listing) : base(listing)
        {
            this.Preview = listing.Preview;
            this.URL = listing.URL;
            this.Thumbnail = listing.Thumbnail;
            this.ThumbnailHeight = listing.ThumbnailHeight;
            this.ThumbnailWidth = listing.ThumbnailWidth;
        }

        public LinkPost() { }
    }
}
