using Microsoft.Playwright;
using NUnit.Framework;
using Serilog;
using TransferMarktTestFramework.Pages;

namespace TransferMarktTestFramework.Tests;

public class SearchPageTests : BaseTest
{
    private SearchPage _searchPage;

    [SetUp]
    public async Task TestSetup()
    {
        _searchPage = new SearchPage(_page);
    }

    /*[Test]
    public async Task SearchForPlayer()
    {
        string playerNameMessi = "Messi";
        
        Log.Information($"Searching for player: {playerNameMessi}");
        await _searchPage.PerformSearch(playerNameMessi);
        Log.Information("Starting search for player: {playerNameMessi}", playerNameMessi);

        await _page.WaitForSelectorAsync(".responsive-table"); 
        Log.Information("Performed search for player: {playerNameMessi}", playerNameMessi);

        var isMessiVisible = await _searchPage.IsPlayerNameVisibleInSearchResult("Lionel Messi");
        Assert.That(isMessiVisible, Is.True, "Lionel Messi should be visible in the search results.");
        Log.Information($"Player {playerNameMessi} search completed. Visible: {isMessiVisible}");

        
        string playerNameHenry = "Henry";
        Log.Information($"Searching for player: {playerNameHenry}");
        await _searchPage.PerformSearch(playerNameHenry);
        Log.Information("Starting search for player: {playerNameHenry}", playerNameHenry);
        await _page.WaitForSelectorAsync(".responsive-table"); 
        Log.Information("Performed search for player: {playerNameHenry}", playerNameHenry);

        var isHenryVisible = await _searchPage.IsPlayerNameVisibleInSearchResult("Thierry Henry");
        Assert.That(isMessiVisible, Is.True, "Thierry Henry should be visible in the search results.");

        Log.Information($"Player {playerNameHenry} search completed. Visible: {isHenryVisible}"); 
    }*/

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