namespace PlaywrightNet.PageObjects;

internal class HeaderFragment(IPage page)
{
  private readonly IPage _page = page;
  public readonly ILocator hamburgerMenuButton = page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" });
  public readonly ILocator logoutMenuEntry = page.GetByRole(AriaRole.Link, new() { Name = "Logout" });
  public readonly ILocator hamburgerMenuCloseButton = page.GetByRole(AriaRole.Button, new() { Name = "Close Menu" });
  public readonly ILocator cartLink = page.Locator("a.shopping_cart_link");
  public readonly ILocator cartItemCount = page.Locator("span.shopping_cart_badge");

  public async Task LogOutAsync()
  {
    await hamburgerMenuButton.ClickAsync();
    await logoutMenuEntry.ClickAsync();
  }

  public async Task ClickOnCartLink()
  {
    await cartLink.ClickAsync();
  }

  public async Task<int> GetCartItemCountAsync()
  {
    var itemCount = 0;

    if (await cartItemCount.IsVisibleAsync())
    {
      var count = $"{await cartItemCount.TextContentAsync()}";
      itemCount = count == "" ? 0 : Int32.Parse(count);
    }

    return itemCount;
  }
}
