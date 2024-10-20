using Microsoft.Playwright;
using NUnit.Framework;
using Serilog;
using TransferMarktTestFramework.Pages;
using TransferMarktTestFramework.Utilities;

namespace TransferMarktTestFramework.Tests;

public class LoginPageTests : BaseTest
{
    private LoginPage _loginPage;
    private Credentials _credentials;

    [SetUp]
    public async Task SetUp()
    {
        _loginPage = new LoginPage(_page);
        _credentials = ConfigLoader.LoadCredentials("credentials.json"); 
        await _loginPage.NavigateToLoginPage(_settings.BaseUrl);
        
    }
    
    [Test]
    public async Task LoginFormFieldsInteractionTest()
    {
        Log.Information("Starting Login Form Fields Interaction Test.");

        await _loginPage.WaitForLoginForm(); 
        Assert.That(await _loginPage.IsLoginFormVisible(), Is.True, "Login form should be visible.");
        Log.Information("Login form is visible.");
        
        await _loginPage.PerformLogin(_credentials.Username, _credentials.Password);

        await _page.WaitForSelectorAsync($"//h2[contains(@class, 'content-box-headline') and contains(., '{_credentials.Username}')]", new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000 
        });

        var confirmationHeadline = await _page.Locator($"//h2[contains(@class, 'content-box-headline') and contains(., '{_credentials.Username}')]").InnerTextAsync();
        Assert.That(confirmationHeadline.Trim(), Is.EqualTo(_credentials.Username).IgnoreCase, "Confirmation message should match the logged-in username.");

        Log.Information("Login process completed successfully for user: {Username}", _credentials.Username);
    }



    [Test]
    public async Task LoginFormCheckWithInvalidCredentials()
    {
        Log.Information("Starting Login Form Check with Invalid Credentials Test.");

        await _loginPage.WaitForLoginForm(); 
        Assert.That(await _loginPage.IsLoginFormVisible(), Is.True, "Login form should be visible.");
        Log.Information("Login form is visible.");

        await _loginPage.PerformLogin("InvalidUser", "InvalidPass");
        Log.Information("Attempted to login with invalid credentials.");
        
        await _page.WaitForSelectorAsync("//div[@id='login-form_es_']", new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000 
        });

        var isErrorVisible = await _loginPage.IsLoginErrorVisible(); // Call method from LoginPage
        Assert.That(isErrorVisible, Is.True, "Error message should be visible for invalid credentials.");
        Log.Information("Error message visibility checked: {IsErrorVisible}", isErrorVisible);
    }
    
    
    [Test]
    public async Task RememberMeCheckboxTest()
    {
        Log.Information("Starting Remember Me Checkbox Test.");

        await _loginPage.WaitForLoginForm();
        Assert.That(await _loginPage.IsLoginFormVisible(), Is.True, "Login form should be visible.");
        Log.Information("Login form is visible.");

        // Check the initial state of the checkbox
        bool initialState = await _loginPage.IsRememberMeChecked();
        Assert.That(initialState, Is.False, "Remember Me checkbox should be initially unchecked.");

        // Toggle the checkbox
        await _loginPage.ToggleRememberMeCheckbox();

        // Check the state after toggling
        bool newState = await _loginPage.IsRememberMeChecked();
        Assert.That(newState, Is.True, "Remember Me checkbox should be checked after toggling.");

        Log.Information("Remember Me Checkbox Test completed successfully.");
    }

}
