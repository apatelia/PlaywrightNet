using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;
using PlaywrightNet.PageObjects;

namespace PlaywrightNet.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[Category("Login")]
public partial class LoginTests : PageTest
{
  [GeneratedRegex(".*inventory.html$")]
  private static partial Regex ProductsPageUrlRegex();

  [Test]
  public async Task LoginWorksForValidCredentials()
  {
    var loginPage = new LoginPage(Page);
    await loginPage.GotoAsync();
    await loginPage.DoLoginAsync("standard_user", "secret_sauce");

    await Expect(Page).ToHaveURLAsync(ProductsPageUrlRegex());

    var productsPage = new ProductsPage(Page);
    Assert.That(await productsPage.IsProductHeadingVisible(), Is.True);
  }

  [Test]
  public async Task LoginDoesNotWorkForInvalidCredentials()
  {
    var loginPage = new LoginPage(Page);
    await loginPage.GotoAsync();
    await loginPage.DoLoginAsync("standard_user", "incorrect_password");

    await Expect(loginPage.errorMessage).ToBeVisibleAsync();
    await Expect(loginPage.errorMessage).ToContainTextAsync("Epic sadface: Username and password do not match any user in this service");
  }

  [Test]
  public async Task LockedOutUserCanNotLogin()
  {
    var loginPage = new LoginPage(Page);
    await loginPage.GotoAsync();
    await loginPage.DoLoginAsync("locked_out_user", "secret_sauce");

    await Expect(loginPage.errorMessage).ToBeVisibleAsync();
    await Expect(loginPage.errorMessage).ToContainTextAsync("Epic sadface: Sorry, this user has been locked out.");
  }
}
