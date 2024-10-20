using Microsoft.Playwright;
using NUnit.Framework;
using Newtonsoft.Json;
using Serilog;
using TransferMarktTestFramework.Utilities;

namespace TransferMarktTestFramework.Tests;

public class BaseTest
{
    protected IPlaywright _playwright;
    protected IBrowser _browser;
    protected IPage _page;
    protected AppSettings _settings;

    static BaseTest()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }

    [SetUp]
    public async Task Setup()
    {
        Log.Information("Setting up the test environment.");
        LoadConfiguration();
        _playwright = await Playwright.CreateAsync();

        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions 
        { 
            Headless = _settings.HeadlessMode, 
            Args = new[] { "--start-fullscreen" }, 
        });
    
        _page = await _browser.NewPageAsync();
        await _page.GotoAsync(_settings.BaseUrl);
    
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    
        await _page.PressAsync("body", "Tab");
        await _page.PressAsync("body", "Enter");
    
        await HandleDialog();
        Log.Information("Test environment setup complete.");
    }


    private void LoadConfiguration()
    {
        Log.Information("Loading configuration from appsettings.json.");
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Utilities", "appsettings.json");

        if (!File.Exists(path))
        {
            Log.Error("File appsettings.json not found.");
            throw new FileNotFoundException("File appsettings.json not exists.");
        }

        var json = File.ReadAllText(path);
        _settings = JsonConvert.DeserializeObject<AppSettings>(json);
        Log.Information("Configuration loaded successfully.");
    }

    public async Task HandleDialog()
    {
        _page.Dialog += async (sender, e) =>
        {
            Log.Information($"Dialog appeared with message: {e.Message}");
            await e.AcceptAsync();
            // Alternatively, you can dismiss the dialog
            // await e.DismissAsync();
        };
    }

    public async Task HandleCookieConsent()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var acceptButton = _page.Locator("button[title='Accept & continue']");
        if (await acceptButton.IsVisibleAsync())
        {
            await acceptButton.ClickAsync();
            Log.Information("Cookie consent accepted.");
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        Log.Information("Tearing down the test environment.");
        await _browser.CloseAsync();
        Log.Information("Test environment torn down.");
        _playwright.Dispose();
    }
}