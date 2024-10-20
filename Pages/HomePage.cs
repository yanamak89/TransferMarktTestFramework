using Microsoft.Playwright;
using Serilog;
using TransferMarktTestFramework.Utilities;

namespace TransferMarktTestFramework.Pages;

public class HomePage
{
    private readonly IPage _page;

    private readonly AppSettings _settings;
    
    private const string PremierLeagueTableSelector = "a[href*='premier-league'].direct-headline__link";
    public string PremierLeagueTableSelectorPublic => PremierLeagueTableSelector;

    public HomePage(IPage page, AppSettings settings)
    {
        _page = page;
        _settings = settings;
    }

    public async Task NavigateToHomePage()
    {
        await _page.GotoAsync(_settings.BaseUrl);
    }

    public async Task<bool> IsPremierLeagueTableVisible()
    {
        var premierLeagueTableSelector = "//a[@href='/premier-league/startseite/wettbewerb/GB1' and @class='direct-headline__link']"; 
        try
        {
            await _page.WaitForSelectorAsync(premierLeagueTableSelector, new PageWaitForSelectorOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 60000 
            });

            return await _page.Locator(premierLeagueTableSelector).IsVisibleAsync();
        }
        catch (TimeoutException ex)
        {
            Log.Error($"Failed to find Premier League table: {ex.Message}");
            return false;
        }
    }
}