using Microsoft.Playwright;
using NUnit.Framework;

namespace TransferMarktTestFramework.Tests;

public class BaseTest
{
    protected IPlaywright _playwright;
    protected IBrowser _browser;
    protected IPage _page;

    [SetUp]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        _page = await _browser.NewPageAsync();

        await _page.GotoAsync("https://www.transfermarkt.com/");

        await HandleCookieConsent();
    }

    public async Task HandleCookieConsent()
    {
        var acceptButton = _page.Locator("button[title='Accept & continue']");
        if (await acceptButton.IsVisibleAsync())
        {
            await acceptButton.ClickAsync();
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}