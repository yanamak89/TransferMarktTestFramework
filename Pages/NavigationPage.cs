using Microsoft.Playwright;

namespace TransferMarktTestFramework.Tests;

public class NavigationPage
{
    private readonly IPage _page;

    public NavigationPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToSection(string sectionName)
    {
        var selector = $"//a[contains(text(),'{sectionName}')]";
        await _page.ClickAsync(selector);
    }

    public async Task<bool> IsSectionVisible(string sectionName)
    {
        var selector = $"//a[contains(text(), '{sectionName}')]";
        return await _page.Locator(selector).IsVisibleAsync();
    }
}