using NUnit.Framework;
using Serilog;

namespace TransferMarktTestFramework.Tests;

public class SearchPageTests : BaseTest
{
    private SearchPage _searchPage;

    [SetUp]
    public async Task TestSetup()
    {
        _searchPage = new SearchPage(_page);
    }

    [Test]
    public async Task SearchForPlayer()
    {
        await SearchAndVerifyPlayer("Messi", "Lionel Messi");
        await SearchAndVerifyPlayer("Henry", "Thierry Henry");
    }

    private async Task SearchAndVerifyPlayer(string playerName, string expectedName)
    {
        Log.Information($"Searching for player: {playerName}");
        await _searchPage.PerformSearch(playerName);
        Log.Information("Starting search for player: {playerName}", playerName);

        await _page.WaitForSelectorAsync(".responsive-table");
        Log.Information("Performed search for player: {playerName}", playerName);

        bool isVisible = await _searchPage.IsPlayerNameVisibleInSearchResult(expectedName);
        Assert.That(isVisible, Is.True, $"{expectedName} should be visible in the search results.");
        
        Log.Information($"Player {playerName} search completed. Visible: {isVisible}.");
    }
}