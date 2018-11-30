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
    public class PrivateMessagesTests : BaseTests
    {
        public PrivateMessagesTests() : base() { }

        [TestMethod]
        public void Inbox()
        {
            Validate(reddit.Account.Messages.Inbox);
        }

        [TestMethod]
        public void Unread()
        {
            Validate(reddit.Account.Messages.Unread);
        }

        [TestMethod]
        public void Sent()
        {
            Validate(reddit.Account.Messages.Sent);
        }
    }
}
