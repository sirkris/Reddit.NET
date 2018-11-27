using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests
{
    [TestClass]
    public class LinkPostTests : PostTests
    {
        private LinkPost Post
        {
            get
            {
                return post ?? GetPost();
            }
            set
            {
                post = value;
            }
        }
        private LinkPost post;

        public LinkPostTests() : base() { }

        private LinkPost GetPost()
        {
            Post = reddit.LinkPost(PostFullname).About();
            return Post;
        }

        [TestMethod]
        public void GetDuplicates()
        {
            Validate(Post.GetDuplicates());
        }

        [TestMethod]
        public void GetCrossPosts()
        {
            Validate(Post.GetCrossPosts());
        }
    }
}
