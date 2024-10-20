using Microsoft.Playwright;
using Serilog;

namespace TransferMarktTestFramework.Pages;

public class LoginPage
{
    private readonly IPage _page;
    
    private const string UsernameFieldLocator = "#LoginForm_username";
    private const string PasswordFieldLocator = "#LoginForm_password";
    private const string RememberMeCheckboxLocator = "#LoginForm_rememberMe";
    private const string LoginButtonLocator = ".button.login-button";
    private const string LoginErrorLocator = "//div[@id='login-form_es_']";
    private const string LoginFormLocator = "#login-form";


    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToLoginPage(string baseUrl)
    {
        string loginUrl = $"{baseUrl}profil/login";
        Log.Information("Navigating to login page: {LoginUrl}", loginUrl);
        await _page.GotoAsync(loginUrl);
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await HandleCookieConsent();
        await _page.PressAsync("body", "Tab");
        await _page.PressAsync("body", "Enter");
        await WaitForLoginForm();
        Log.Information("Login page loaded successfully.");
    }

    public async Task HandleCookieConsent()
    {
        var cookieConsentLocator = _page.Locator("your-cookie-consent-locator"); // Replace with actual locator for cookie consent
        if (await cookieConsentLocator.IsVisibleAsync())
        {
            await cookieConsentLocator.ClickAsync(); // Or whatever action you need to dismiss the modal
            Log.Information("Cookie consent modal dismissed.");
        }
    }

    public async Task ClickLoginButton()
    {
        var loginButton = _page.Locator(LoginButtonLocator);
        if (await loginButton.IsVisibleAsync() && await loginButton.IsEnabledAsync())
        {
            await loginButton.ClickAsync();
            Log.Information("Login button clicked.");
        }
    }

    public async Task PerformLogin(string username, string password)
    {
        Log.Information("Entering username: {Username}", username);
        await _page.FillAsync(UsernameFieldLocator, username);
        
        Log.Information("Entering password.{Password}, password");
        await _page.FillAsync(PasswordFieldLocator, password);
        
        await ClickLoginButton();
    }

    public async Task<bool> IsLoginErrorVisible()
    {
        bool isVisible = await _page.Locator(LoginErrorLocator).IsVisibleAsync();
        Log.Information("Login error visibility checked: {IsVisible}", isVisible);
        return isVisible;
    }

    public async Task<bool> IsLoginFormVisible()
    {
        bool isVisible = await _page.Locator(LoginFormLocator).IsVisibleAsync();
        Log.Information("Login form visibility checked: {IsVisible}", isVisible);
        return isVisible;
    }
    public async Task WaitForLoginForm()
    {
        await _page.WaitForSelectorAsync(UsernameFieldLocator, new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000 // Adjust the timeout as needed
        });
        Log.Information("Waited for login form to be visible.");
    }
}