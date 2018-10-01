using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers.Structures
{
    class SelfPost : Post
    {
        public string SelfText;
        public string SelfTextHTML;

        public SelfPost(Listing listing) : base(listing)
        {
            this.SelfText = listing.SelfText;
            this.SelfTextHTML = listing.SelftextHTML;
        }

        public SelfPost() { }
    }
}
