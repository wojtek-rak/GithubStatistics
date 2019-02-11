
using System;
using GithubStatistics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GithubStatistics.Tests
{
    [TestClass]
    public class GithubServiceTests
    {
        [TestMethod]
        public void SearchUser_ValidUsername_GetUsers()
        {
            var githubService = new GithubService();
            var users = githubService.SearchUser("aaab");
            Assert.AreEqual(githubService.Test(), 1);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var githubService = new GithubService();
            Assert.AreEqual(githubService.Test(), 1);
        }
    }
}
