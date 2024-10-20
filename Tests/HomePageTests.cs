using Microsoft.Playwright;
using Serilog;
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
            _homePage = new HomePage(_page, _settings);
        }
        
        [Test]
        public async Task PremierLeagueTableIsVisible()
        {
            Log.Information("Starting test: PremierLeagueTableIsVisible");

            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            Log.Information("Page has fully loaded.");


            await _page.WaitForSelectorAsync(_homePage.PremierLeagueTableSelectorPublic, new PageWaitForSelectorOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 60000 
            });           

            await _page.EvaluateAsync(@"(selector) => {
                    const element = document.querySelector(selector);
                    if (element) {
                        element.scrollIntoView();
                    }
                }", _homePage.PremierLeagueTableSelectorPublic);

            var isVisible = await _homePage.IsPremierLeagueTableVisible();

            Log.Information($"Premier League table visibility: {isVisible}");

            Assert.That(isVisible, Is.True, "Premier League table should be visible on the home page.");
            Log.Information("Test completed successfully: Premier League table is visible.");
        }
    }
}