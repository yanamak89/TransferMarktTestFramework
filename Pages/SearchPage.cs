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
        await _page.PressAsync("//*[@id=\"schnellsuche\"]/button/svg", "Enter");
    }

    public async Task AcceptCookies()
    {
        var acceptButton = await _page.WaitForSelectorAsync("//button[@aria-label='Accept & continue']", new PageWaitForSelectorOptions
        {
            Timeout = 10000,  
            State = WaitForSelectorState.Visible  
        });

        // If the button is found and visible, click it
        if (acceptButton != null)
        {
            await acceptButton.ClickAsync();
        }
    }

    
    public async Task<bool> IsPlayerNameVisibleInSearchResult(string playerName)
    {
        string xpath = $"//table[@class='items']//a[contains(text(),'{playerName}')]";
        return await _page.Locator(xpath).IsVisibleAsync();

    }
}