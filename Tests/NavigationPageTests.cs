using Microsoft.Playwright;
using NUnit.Framework;
using TransferMarktTestFramework.Pages;

namespace TransferMarktTestFramework.Tests;

public class NavigationPageTests : BaseTest
{
    
    private NavigationPage _navigationPage;

    [SetUp]
    public async Task SetUp()
    {
        _navigationPage = new NavigationPage(_page);
    }

    [Test]
    public async Task NavigateToAllSections()
    {
        await _navigationPage.NavigateToSection("Discover");
        Assert.That(await _navigationPage.IsSectionVisible("Discover"), Is.True, "Discover section should be visible.");

        await _navigationPage.NavigateToSection("Transfers & Rumours");
        Assert.That(await _navigationPage.IsSectionVisible("Transfers & Rumours"), Is.True,"Transfers & Rumours section should be visible.");

        await _navigationPage.NavigateToSection("Market Values");
        Assert.That(await _navigationPage.IsSectionVisible("Market Values"), Is.True, "Market Values section should be visible.");

        await _navigationPage.NavigateToSection("Competitions");
        Assert.That(await _navigationPage.IsSectionVisible("Competitions"), Is.True, "Competitions section should be visible.");

        await _navigationPage.NavigateToSection("Statistics");
        Assert.That(await _navigationPage.IsSectionVisible("Statistics"), Is.True, "Statistics section should be visible.");

        await _navigationPage.NavigateToSection("Community");
        Assert.That(await _navigationPage.IsSectionVisible("Community"), Is.True,  "Community section should be visible.");

        await _navigationPage.NavigateToSection("Gaming");
        Assert.That(await _navigationPage.IsSectionVisible("Gaming"), Is.True, "Gaming section should be visible.");
    }
}