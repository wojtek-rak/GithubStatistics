
using System;
using GithubStatistics.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GithubStatistics.Tests
{
    [TestClass]
    public class GithubServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var githubService = new GithubService();
            Assert.AreEqual(githubService.Test(), 1);
        }
    }
}
