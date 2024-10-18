using Microsoft.Playwright;
using NUnit.Framework;
using System.IO;
using Newtonsoft.Json;
using TransferMarktTestFramework.Utilities;

namespace TransferMarktTestFramework.Tests;

public class BaseTest
{
    protected IPlaywright _playwright;
    protected IBrowser _browser;
    protected IPage _page;
    protected AppSettings _settings;

    [SetUp]
    public async Task Setup()
    {
        LoadConfiguration();
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        _page = await _browser.NewPageAsync();

        await _page.GotoAsync(_settings.BaseUrl);

        await HandleCookieConsent();
    }

    private void LoadConfiguration()
    {
        var json = File.ReadAllText(
            "/Users/yanamakogon/Documents/net developer/Automation /TransferMarktTestFramework/Utilities/appsettings.json");
        _settings = JsonConvert.DeserializeObject<AppSettings>(json);
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