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
