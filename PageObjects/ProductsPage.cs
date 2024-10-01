namespace PlaywrightNet.PageObjects;

internal class ProductsPage(IPage page)
{
  private readonly IPage _page = page;
  public readonly ILocator productHeading = page.GetByText("Products");
  public readonly ILocator allProducts = page.Locator("div.inventory_item");
  public readonly ILocator productSortOptions = page.Locator("select.product_sort_container");

  public async Task GotoAsync()
  {
    await _page.GotoAsync($"{Constants.BaseUrl}/inventory.html");
  }

  public async Task<bool> IsProductHeadingVisible()
  {
    return await productHeading.IsVisibleAsync();
  }

  public async Task AddProductToCartAsync(string productName)
  {
    var product = allProducts.Filter(new() { HasText = productName });

    var addToCartButton = product.Locator("button");
    await addToCartButton.ClickAsync();
  }

  public async Task RemoveProductFromCartAsync(string productName)
  {
    var product = allProducts.Filter(new() { HasText = productName });

    var removeButton = product.Locator("button");
    await removeButton.ClickAsync();
  }
}
