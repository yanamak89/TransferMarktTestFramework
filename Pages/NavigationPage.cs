using Microsoft.Playwright;
using Serilog;

namespace TransferMarktTestFramework.Pages;

public class NavigationPage
{
    private readonly IPage _page;

    public NavigationPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToSection(string sectionName)
    {
        string href = sectionName switch
        {
            "Discover" => "/",
            "Transfers & Rumours" => "/navigation/transfersundgeruechte",
            "Market Values" => "/navigation/marktwerte",
            "Competitions" => "/navigation/wettbewerbe",
            "Statistics" => "/navigation/statistiken",
            "Community" => "/navigation/community",
            "Gaming" => "/navigation/gaming",
            _ => throw new ArgumentException("Invalid section name")
        };

        var linkLocator = sectionName switch
        {
            "Discover" => _page.GetByRole(AriaRole.Link, new() { Name = "Discover", Exact = true }),
            _ => _page.Locator($"a[href='{href}']")
        };

        await linkLocator.WaitForAsync();
        await linkLocator.ClickAsync();
    }
    
    public async Task<bool> IsSectionVisible(string sectionName)
    {
        string href = sectionName switch
        {
            "Discover" => "/",
            "Transfers & Rumours" => "/navigation/transfersundgeruechte",
            "Market Values" => "/navigation/marktwerte",
            "Competitions" => "/navigation/wettbewerbe",
            "Statistics" => "/navigation/statistiken",
            "Community" => "/navigation/community",
            "Gaming" => "/navigation/gaming",
            _ => throw new ArgumentException("Invalid section name")
        };

        var linkLocator = sectionName switch
        {
            "Discover" => _page.GetByRole(AriaRole.Link, new() { Name = "Discover", Exact = true }),
            _ => _page.Locator($"a[href='{href}']")
        };

        try
        {
            await linkLocator.WaitForAsync();
        }
        catch (Exception ex)
        {
            Log.Error($"Error waiting for element: {ex.Message}");
        }

        bool isVisible = await linkLocator.IsVisibleAsync();
        return isVisible;
    }
}
