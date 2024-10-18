using Microsoft.Playwright;
using NUnit.Framework;
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

    [Test]
    public async Task SearchForPlayer()
    {
        // Проводимо пошук за "Messi"
        await _searchPage.PerformSearch("Messi");

        // Перевіряємо, чи Мессі є в результатах пошуку
        var isMessiVisible = await _searchPage.IsPlayerNameVisibleInSearchResult("Lionel Messi");
        Assert.That(isMessiVisible, Is.True, "Lionel Messi should be visible in the search results.");
    }
}