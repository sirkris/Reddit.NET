using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.Subreddits;
using Reddit.Things;

namespace RedditTests.ModelTests.WorkflowTests
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
            ImageUploadResult resHeader = reddit.Models.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imageData, 1, "birdie", "header", "png"), testData["Subreddit"]);
            ImageUploadResult resImg = reddit.Models.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imageData, 0, "birdie", "img", "png"), testData["Subreddit"]);
            ImageUploadResult resIcon = reddit.Models.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imageData, 0, "birdie", "icon", "png"), testData["Subreddit"]);
            ImageUploadResult resBanner = reddit.Models.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imageData, 0, "birdie", "banner", "png"), testData["Subreddit"]);

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
            resIcon = reddit.Models.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imageIconData, 0, "birdieIcon", "icon", "jpg"), testData["Subreddit"]);
            resBanner = reddit.Models.Subreddits.UploadSrImg(new SubredditsUploadSrImgInput(imageBannerData, 0, "birdieBanner", "banner", "jpg"), testData["Subreddit"]);

            Validate(resIcon);
            Validate(resBanner);

            // Delete the images.  --Kris
            GenericContainer resDelHeader = reddit.Models.Subreddits.DeleteSrHeader(testData["Subreddit"]);
            GenericContainer resDelImg = reddit.Models.Subreddits.DeleteSrImg(new SubredditsDeleteSrImgInput("birdie"), testData["Subreddit"]);
            GenericContainer resDelBanner = reddit.Models.Subreddits.DeleteSrBanner(testData["Subreddit"]);
            GenericContainer resDelIcon = reddit.Models.Subreddits.DeleteSrIcon(testData["Subreddit"]);

            Validate(resDelHeader);
            Validate(resDelImg);
            Validate(resDelBanner);
            Validate(resDelIcon);
        }
    }
}
