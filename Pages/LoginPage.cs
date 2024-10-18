using Microsoft.Playwright;

namespace TransferMarktTestFramework.Tests;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task ClickLoginButton()
    {
        await _page.ClickAsync("button[title='Mein Profil']");
    }

    public async Task PerformLogin(string username, string password)
    {
        await _page.FillAsync("//input[@id='LoginForm_username']", username);
        await _page.FillAsync("//input[@id='LoginForm_password']", password);
        await _page.ClickAsync("//input[@class='button login-button' and @type='submit' and @value='Login']\n");
    }

    public async Task<bool> IsLoginErrorVisible()
    {
        return await _page.Locator("//div[@id='login-form_es_']").IsVisibleAsync();
    }
}