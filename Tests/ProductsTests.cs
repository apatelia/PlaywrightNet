using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;
using PlaywrightNet.PageObjects;

namespace PlaywrightNet.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[Category("Products")]
public partial class ProductsTests : PageTest
{
  [GeneratedRegex(".*inventory.html$")]
  private static partial Regex ProductsPageUrlRegex();

  [SetUp]
  public async Task SetUp()
  {
    var loginPage = new LoginPage(Page);
    await loginPage.GotoAsync();
    await loginPage.DoLoginAsync("standard_user", "secret_sauce");

    await Expect(Page).ToHaveURLAsync(ProductsPageUrlRegex());
  }

  [Test]
  public async Task AddProductToCart()
  {
    const string productName = Constants.Backpack;

    var productsPage = new ProductsPage(Page);
    await productsPage.AddProductToCartAsync(productName);

    var header = new HeaderFragment(Page);
    int cartItemCount = await header.GetCartItemCountAsync();
    Assert.That(cartItemCount, Is.EqualTo(1));
  }

  [Test]
  public async Task RemoveProductFromCart()
  {
    const string productName = Constants.BikeLight;

    var productsPage = new ProductsPage(Page);
    await productsPage.AddProductToCartAsync(productName);

    var header = new HeaderFragment(Page);
    int cartItemCount = await header.GetCartItemCountAsync();
    Assert.That(cartItemCount, Is.EqualTo(1));

    await productsPage.RemoveProductFromCartAsync(productName);
    await Expect(header.cartItemCount).ToHaveCountAsync(0);
  }

  [Test]
  public async Task LogOutShouldWork()
  {
    var header = new HeaderFragment(Page);
    await header.LogOutAsync();

    var loginPage = new LoginPage(Page);
    await Expect(loginPage.loginButton).ToBeVisibleAsync();
  }
}
