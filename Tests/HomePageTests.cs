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
            try
            {
                Log.Information("Starting test: PremierLeagueTableIsVisible");

                await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                Log.Information("Page has fully loaded.");

                var premierLeagueTableSelector = "a[href*='premier-league'].direct-headline__link";

                await _page.WaitForSelectorAsync(premierLeagueTableSelector, new PageWaitForSelectorOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000 
                });

                await _page.EvaluateAsync(@"(selector) => {
                    const element = document.querySelector(selector);
                    if (element) {
                        element.scrollIntoView();
                    }
                }", premierLeagueTableSelector);

                var isVisible = await _homePage.IsPremierLeagueTableVisible();

                Log.Information($"Premier League table visibility: {isVisible}");

                Assert.That(isVisible, Is.True, "Premier League table should be visible on the home page.");
                Log.Information("Test completed successfully: Premier League table is visible.");
            }
            catch (TimeoutException ex)
            {
                Log.Error($"Timeout exception: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred during the test: {ex.Message}");
                throw; 
            }
        }
    }
}