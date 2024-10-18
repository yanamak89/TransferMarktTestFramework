using NUnit.Framework;

namespace TransferMarktTestFramework.Tests;

public class LoginPageTests : BaseTest
{
    private LoginPage _loginPage;

    [SetUp]
    public async Task SetUp()
    {
        _loginPage = new LoginPage(_page);
    }

    [Test]
    public async Task LoginFormDisplaysErrorOnInvalidCredentials()
    {
        await _loginPage.PerformLogin("invalid_user", "wrong_password");
        var isErrorVisible = await _loginPage.IsLoginErrorVisible();
        Assert.That(isErrorVisible, Is.True,"Error message should be visible for invalid credentials.");

    }

}