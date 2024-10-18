// Tests/HomePageTests.cs
using Microsoft.Playwright;
using NUnit.Framework;
using TransferMarktTestFramework.Pages;

namespace TransferMarktTestFramework.Tests
{
    public class HomePageTests : BaseTest
    {
        private HomePage _homePage;

        [SetUp]
        public async Task Setup()
        {
            _homePage = new HomePage(_page);
            await _homePage.NavigateToHomePage();
        }

        [Test]
        public async Task PremierLeagueTableIsVisible()
        {
            var isVisible = await _homePage.IsPremierLeagueTableVisible();
            Assert.That(isVisible, Is.True, "Premier League table should be visible on the home page.");
        }
    }
}