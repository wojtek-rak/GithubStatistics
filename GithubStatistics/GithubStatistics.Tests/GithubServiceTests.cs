
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
            Assert.AreEqual(githubService.SearchUser("aaab"), 1);
        }

        [TestMethod]
        public void GeUser_ValidUsername_GetUsers()
        {
            var githubService = new GithubService();
            var users = githubService.SearchUser("aaab");
            Assert.AreEqual(githubService.GetUserDetails("aaab"), 1);
        }
    }
}
