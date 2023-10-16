using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

        // Expect an attribute "to be strictly equal" to the value.
        await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));

        await Expect(Page
            .GetByRole(AriaRole.Heading, new() { Name = "Installation"}))
            .ToBeVisibleAsync();
    }

    [Test]
    public async Task MyTest()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");

        await Page.Locator("[data-test=\"username\"]").ClickAsync();

        await Page.Locator("[data-test=\"username\"]").FillAsync("standard_user");

        await Page.Locator("[data-test=\"username\"]").PressAsync("Tab");

        await Page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");

        await Page.Locator("[data-test=\"login-button\"]").ClickAsync();

        await Expect(Page.GetByText("Products")).ToBeVisibleAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "Logout" }).ClickAsync();

        await Expect(Page.Locator("[data-test=\"login-button\"]")).ToBeEnabledAsync();
    }
}
