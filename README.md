# Rock Paper Scissors Game

A small Windows desktop game where you play Rock, Paper, Scissors against the computer. The first to reach 3 points wins the match, then you can start a new match from the UI.

## Requirements

- .NET SDK 8.0 or later

## Run It

From the project folder:

```bash
dotnet build
dotnet run --project RockPprScc/RockPprScc.csproj
```

## Deploy It

This is a Windows desktop app, so it deploys as a published `.exe` rather than a web app.

To create a self-contained build that can run on a Windows machine without the .NET SDK installed:

```bash
dotnet publish RockPprScc/RockPprScc.csproj -c Release -r win-x64 --self-contained true
```

The published files will be placed under `RockPprScc/bin/Release/net8.0-windows/publish/`.

You can also publish from the included Visual Studio publish profile:

```bash
dotnet publish RockPprScc/RockPprScc.csproj -p:PublishProfile=FolderProfile
```

## Release Workflow

The repository now includes a GitHub Actions workflow that creates versioned releases from tags.

To publish a release, create and push a tag that starts with `v`, for example:

```bash
git tag v1.0.0
git push origin v1.0.0
```

GitHub Actions will build the app, package the Windows publish output into a zip file, and attach it to a GitHub Release named after the tag.

You can also run the workflow manually and provide a tag such as `v1.0.0`.

## CI Pipeline

The repository also includes a continuous integration workflow that runs on every push and pull request to `main`.

It restores, builds, and publishes a Windows package, then uploads the published files as a GitHub Actions artifact.

That gives you two separate pipelines:

- CI for verifying changes on pull requests and mainline pushes.
- Release for tagged versioned builds.

## How It Works

- Click the Rock, Paper, or Scissors buttons.
- The computer makes a random choice each round.
- Scores are tracked until either side reaches 3.
- After the match ends, use the New Match button to reset the score.

## Project Files

- [RockPprScc/Program.cs](RockPprScc/Program.cs) contains the game loop and round logic.
- [RockPprScc/RockPprScc.csproj](RockPprScc/RockPprScc.csproj) defines the .NET project.

## Possible Next Improvements

- Add round history and win/loss statistics.
- Add a best-of-N mode.
- Add a simple GUI version.
