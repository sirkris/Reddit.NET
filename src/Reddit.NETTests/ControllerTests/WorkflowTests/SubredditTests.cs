using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class SubredditTests : BaseTests
    {
        private Subreddit Subreddit
        {
            get
            {
                return subreddit ?? GetSubreddit();
            }
            set
            {
                subreddit = value;
            }
        }
        private Subreddit subreddit;

        public SubredditTests() : base() { }

        private Subreddit GetSubreddit()
        {
            Subreddit = reddit.Subreddit(testData["Subreddit"]).About();
            return Subreddit;
        }

        [TestMethod]
        public void SubredditImages()
        {
            // Get the embedded test images.  --Kris
            byte[] imageData = GetResourceFile("birdie.png");
            byte[] imageBannerData = GetResourceFile("banner.jpg");
            byte[] imageIconData = GetResourceFile("birdie256.jpg");

            // Add the images.  --Kris
            Validate(Subreddit.UploadHeader(imageData));
            Validate(Subreddit.UploadImg(imageData, "birdie"));
            Validate(Subreddit.UploadIcon(imageIconData));
            Validate(Subreddit.UploadBanner(imageBannerData));

            // Delete the images.  --Kris
            Subreddit.DeleteHeader();
            Subreddit.DeleteImg("birdie");
            Subreddit.DeleteIcon();
            Subreddit.DeleteBanner();
        }
    }
}
