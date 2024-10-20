using Microsoft.Playwright;

namespace TransferMarktTestFramework.Tests;

public class SearchPage
{
    private readonly IPage _page;

    public SearchPage(IPage page)
    {
        _page = page;
    }

    public async Task PerformSearch(string query)
    {
        await _page.FillAsync("//*[@id=\"schnellsuche\"]/input", query);
        await _page.Keyboard.PressAsync("Enter");
    }
    
    public async Task<bool> IsPlayerNameVisibleInSearchResult(string playerName)
    {
        var playerLocator = _page.Locator($"text='{playerName}'"); // Adjust selector as needed
        return await playerLocator.IsVisibleAsync();
    }
}