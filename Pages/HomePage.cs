using Microsoft.Playwright;

namespace TransferMarktTestFramework.Pages;

public class HomePage
{
    private readonly IPage _page;

    public HomePage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToHomePage()
    {
        await _page.GotoAsync("https://www.transfermarkt.com/");
    }

    public async Task<bool> IsPremierLeagueTableVisible()
    {
        return await _page.Locator("#startseite > div:nth-child(11) > div.large-12.columns.small-12 > div")
            .IsVisibleAsync();
        
    }
}