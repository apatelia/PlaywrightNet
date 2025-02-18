# Playwright Test Automation with .Net Core and nUnit

This repo contains a very basic sample test automation framework using [Playwright](https://playwright.dev/ "Playwright") and [.Net Core](https://dotnet.microsoft.com/en-us/ ".Net Core"). [nUnit](https://nunit.org/ "nUnit") is the test runner used for this framework.

You will need to install [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download "Download .Net 8"), if you haven't installed already.

## Usage

1. Clone this repo.

```sh
  git clone https://github.com/apatelia/PlaywrightNet.git
```

2. Change to the project directory.

```sh
  cd PlaywrightNet
```

3. Install the project dependencies.

```sh
  dotnet restore
```

4. Run tests.

```sh
  # This command prints the results to the console.
  # No report document is generated.
  dotnet test

  # If you want a bare-bones HTML report, then use this command.
  # An HTML report will be generated in "TestResults" directory.
  dotnet test --logger html
```
