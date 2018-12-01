using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class SubredditsTests : BaseTests
    {
        public SubredditsTests() : base() { }

        [TestMethod]
        public void SubredditImages()
        {
            // Get the embedded test images.  --Kris
            byte[] imageData = GetResourceFile("birdie.png");
            byte[] imageBannerData = GetResourceFile("banner.jpg");
            byte[] imageIconData = GetResourceFile("birdie256.jpg");

            // Add the images (two succeed, two fail due to size constraints).  --Kris
            ImageUploadResult resHeader = reddit.Models.Subreddits.UploadSrImg(imageData, 1, "birdie", "header", testData["Subreddit"], "png");
            ImageUploadResult resImg = reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie", "img", testData["Subreddit"], "png");
            ImageUploadResult resIcon = reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie", "icon", testData["Subreddit"], "png");
            ImageUploadResult resBanner = reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie", "banner", testData["Subreddit"], "png");

            Validate(resHeader);
            Validate(resImg);

            Assert.IsNotNull(resIcon);
            Assert.IsTrue(resIcon.Errors != null && resIcon.Errors.Count == 1);
            Assert.IsTrue(resIcon.Errors[0].Equals("IMAGE_ERROR"));
            Assert.IsTrue(resIcon.ErrorsValues != null && resIcon.ErrorsValues.Count == 1);
            Assert.IsTrue(resIcon.ErrorsValues[0].Equals("must be 256x256 pixels"));
            Assert.IsTrue(string.IsNullOrWhiteSpace(resIcon.ImgSrc));

            Assert.IsNotNull(resBanner);
            Assert.IsTrue(resBanner.Errors != null && resBanner.Errors.Count == 1);
            Assert.IsTrue(resBanner.Errors[0].Equals("IMAGE_ERROR"));
            Assert.IsTrue(resBanner.ErrorsValues != null && resBanner.ErrorsValues.Count == 1);
            Assert.IsTrue(resBanner.ErrorsValues[0].Equals("10:3 aspect ratio required"));
            Assert.IsTrue(string.IsNullOrWhiteSpace(resBanner.ImgSrc));

            // Add the remaining two images (both succeed).  --Kris
            resIcon = reddit.Models.Subreddits.UploadSrImg(imageIconData, 0, "birdieIcon", "icon", testData["Subreddit"], "jpg");
            resBanner = reddit.Models.Subreddits.UploadSrImg(imageBannerData, 0, "birdieBanner", "banner", testData["Subreddit"], "png");

            Validate(resIcon);
            Validate(resBanner);

            // Delete the images.  --Kris
            GenericContainer resDelHeader = reddit.Models.Subreddits.DeleteSrHeader(testData["Subreddit"]);
            GenericContainer resDelImg = reddit.Models.Subreddits.DeleteSrImg("birdie", testData["Subreddit"]);
            GenericContainer resDelBanner = reddit.Models.Subreddits.DeleteSrBanner(testData["Subreddit"]);
            GenericContainer resDelIcon = reddit.Models.Subreddits.DeleteSrIcon(testData["Subreddit"]);

            Validate(resDelHeader);
            Validate(resDelImg);
            Validate(resDelBanner);
            Validate(resDelIcon);
        }
    }
}
