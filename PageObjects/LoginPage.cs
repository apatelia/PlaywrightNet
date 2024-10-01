namespace PlaywrightNet.PageObjects;

internal class LoginPage(IPage page)
{
  private readonly IPage _page = page;
  public readonly ILocator userName = page.Locator("[data-test='username']");
  public readonly ILocator password = page.Locator("[data-test='password']");
  public readonly ILocator loginButton = page.Locator("[data-test='login-button']");
  public readonly ILocator errorMessage = page.Locator("[data-test='error']");

  public async Task GotoAsync()
  {
    await _page.GotoAsync(Constants.BaseUrl);
  }

  public async Task DoLoginAsync(string userName, string password)
  {
    await this.userName.FillAsync(userName);
    await this.password.FillAsync(password);
    await loginButton.ClickAsync();
  }
}
