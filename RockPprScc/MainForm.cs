namespace ROCKPAPERSCISSORS;

internal sealed class MainForm : Form
{
    private readonly Random random = new();
    private readonly ToolTip toolTip = new();
    private readonly Label playerScoreValueLabel;
    private readonly Label computerScoreValueLabel;
    private readonly Label playerChoiceLabel;
    private readonly Label roundResultLabel;
    private readonly Label roundFlavorLabel;
    private readonly Label computerChoiceLabel;
    private readonly Label matchStatusLabel;
    private readonly ProgressBar playerProgressBar;
    private readonly ProgressBar computerProgressBar;
    private readonly Button rockButton;
    private readonly Button paperButton;
    private readonly Button scissorsButton;
    private readonly Button restartButton;

    private int playerScore;
    private int computerScore;

    public MainForm()
    {
        Text = "Rock Paper Scissors";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(760, 520);
        BackColor = Color.FromArgb(18, 24, 38);
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        DoubleBuffered = true;

        var titleLabel = new Label
        {
            AutoSize = true,
            Text = "Rock Paper Scissors",
            Font = new Font("Segoe UI Semibold", 26F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.White,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Padding = new Padding(0, 24, 0, 4)
        };

        var subtitleLabel = new Label
        {
            AutoSize = true,
            Text = "First to 3 points wins the match.",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.FromArgb(180, 198, 255),
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Padding = new Padding(0, 0, 0, 16)
        };

        var scorePanel = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 1,
            Dock = DockStyle.Top,
            Height = 110,
            Padding = new Padding(32, 0, 32, 0)
        };
        scorePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        scorePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        var playerCard = CreateScoreCard("Player", out playerScoreValueLabel, Color.FromArgb(90, 197, 255));
        var computerCard = CreateScoreCard("Computer", out computerScoreValueLabel, Color.FromArgb(255, 135, 135));

        playerProgressBar = CreateProgressBar();
        computerProgressBar = CreateProgressBar();

        var progressPanel = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 1,
            Dock = DockStyle.Top,
            Height = 46,
            Padding = new Padding(44, 0, 44, 0)
        };
        progressPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        progressPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        progressPanel.Controls.Add(playerProgressBar, 0, 0);
        progressPanel.Controls.Add(computerProgressBar, 1, 0);

        scorePanel.Controls.Add(playerCard, 0, 0);
        scorePanel.Controls.Add(computerCard, 1, 0);

        roundResultLabel = new Label
        {
            AutoSize = false,
            Height = 64,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.White,
            Text = "Choose a move to start the match."
        };

        roundFlavorLabel = new Label
        {
            AutoSize = false,
            Height = 28,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.FromArgb(191, 203, 232),
            Text = ""
        };

        playerChoiceLabel = new Label
        {
            AutoSize = false,
            Height = 28,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.FromArgb(140, 220, 255),
            Text = ""
        };

        computerChoiceLabel = new Label
        {
            AutoSize = false,
            Height = 32,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.FromArgb(191, 203, 232),
            Text = ""
        };

        var buttonPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 84,
            ColumnCount = 3,
            RowCount = 1,
            Padding = new Padding(24, 12, 24, 12),
            BackColor = Color.Transparent
        };
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));

        rockButton = CreateMoveButton("Rock", Color.FromArgb(72, 136, 255));
        paperButton = CreateMoveButton("Paper", Color.FromArgb(56, 193, 140));
        scissorsButton = CreateMoveButton("Scissors", Color.FromArgb(255, 176, 59));
        restartButton = CreateRestartButton();

        ConfigureMoveButton(rockButton, Color.FromArgb(103, 162, 255), "Smash with rock.");
        ConfigureMoveButton(paperButton, Color.FromArgb(76, 208, 157), "Wrap it up with paper.");
        ConfigureMoveButton(scissorsButton, Color.FromArgb(255, 197, 92), "Slice with scissors.");
        ConfigureRestartButton(restartButton);

        rockButton.Click += (_, _) => PlayRound("ROCK");
        paperButton.Click += (_, _) => PlayRound("PAPER");
        scissorsButton.Click += (_, _) => PlayRound("SCISSORS");
        restartButton.Click += (_, _) => ResetMatch();

        rockButton.Dock = DockStyle.Fill;
        paperButton.Dock = DockStyle.Fill;
        scissorsButton.Dock = DockStyle.Fill;

        buttonPanel.Controls.Add(rockButton, 0, 0);
        buttonPanel.Controls.Add(paperButton, 1, 0);
        buttonPanel.Controls.Add(scissorsButton, 2, 0);

        matchStatusLabel = new Label
        {
            AutoSize = false,
            Height = 36,
            Dock = DockStyle.Bottom,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.FromArgb(140, 152, 180),
            Text = ""
        };

        var centerPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(0, 8, 0, 8)
        };
        centerPanel.Controls.Add(matchStatusLabel);
        centerPanel.Controls.Add(restartButton);
        centerPanel.Controls.Add(buttonPanel);
        centerPanel.Controls.Add(computerChoiceLabel);
        centerPanel.Controls.Add(playerChoiceLabel);
        centerPanel.Controls.Add(roundFlavorLabel);
        centerPanel.Controls.Add(roundResultLabel);
        centerPanel.Controls.Add(progressPanel);
        centerPanel.Controls.Add(scorePanel);

        Controls.Add(centerPanel);
        Controls.Add(subtitleLabel);
        Controls.Add(titleLabel);

        ResetMatch();
    }

    private Panel CreateScoreCard(string title, out Label valueLabel, Color accentColor)
    {
        var card = new Panel
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(12),
            BackColor = Color.FromArgb(29, 38, 58),
            Padding = new Padding(18)
        };

        var titleLabel = new Label
        {
            AutoSize = false,
            Dock = DockStyle.Top,
            Height = 28,
            Text = title,
            ForeColor = accentColor,
            Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point),
            TextAlign = ContentAlignment.MiddleCenter
        };

        valueLabel = new Label
        {
            AutoSize = false,
            Dock = DockStyle.Fill,
            Text = "0",
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 32F, FontStyle.Bold, GraphicsUnit.Point),
            TextAlign = ContentAlignment.MiddleCenter
        };

        card.Controls.Add(valueLabel);
        card.Controls.Add(titleLabel);
        return card;
    }

    private Button CreateMoveButton(string text, Color accentColor)
    {
        return new Button
        {
            Text = text,
            Margin = new Padding(8, 4, 8, 4),
            FlatStyle = FlatStyle.Flat,
            BackColor = accentColor,
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point),
            Cursor = Cursors.Hand,
            MinimumSize = new Size(120, 48)
        };
    }

    private static ProgressBar CreateProgressBar()
    {
        return new ProgressBar
        {
            Dock = DockStyle.Fill,
            Minimum = 0,
            Maximum = 3,
            Value = 0,
            Style = ProgressBarStyle.Continuous,
            ForeColor = Color.FromArgb(96, 214, 255)
        };
    }

    private void ConfigureMoveButton(Button button, Color hoverColor, string tooltipText)
    {
        Color baseColor = button.BackColor;

        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = hoverColor;
        button.FlatAppearance.MouseDownBackColor = hoverColor;

        toolTip.SetToolTip(button, tooltipText);

        button.MouseEnter += (_, _) =>
        {
            button.BackColor = hoverColor;
            button.FlatAppearance.BorderSize = 2;
            button.FlatAppearance.BorderColor = Color.White;
        };

        button.MouseLeave += (_, _) =>
        {
            button.BackColor = baseColor;
            button.FlatAppearance.BorderSize = 0;
        };
    }

    private void ConfigureRestartButton(Button button)
    {
        toolTip.SetToolTip(button, "Start a fresh match and reset both scores.");

        button.FlatAppearance.BorderSize = 0;
        button.MouseEnter += (_, _) => button.BackColor = Color.FromArgb(59, 74, 105);
        button.MouseLeave += (_, _) => button.BackColor = Color.FromArgb(43, 54, 78);
    }

    private Button CreateRestartButton()
    {
        return new Button
        {
            Text = "New Match",
            Margin = new Padding(10, 8, 10, 6),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(43, 54, 78),
            ForeColor = Color.FromArgb(233, 238, 247),
            Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point),
            Cursor = Cursors.Hand,
            Anchor = AnchorStyles.None,
            Dock = DockStyle.Bottom,
            Width = 150,
            Height = 44
        };
    }

    private void PlayRound(string playerChoice)
    {
        if (playerScore >= 3 || computerScore >= 3)
        {
            return;
        }

        string computerChoice = GetComputerChoice();
        int roundResult = GetRoundResult(playerChoice, computerChoice);

        playerChoiceLabel.Text = $"You picked {GetChoiceEmoji(playerChoice)} {playerChoice.ToLowerInvariant()}.";
        computerChoiceLabel.Text = $"Computer chose {GetChoiceEmoji(computerChoice)} {computerChoice.ToLowerInvariant()}.";

        if (roundResult == 0)
        {
            roundResultLabel.Text = "It's a draw!";
            roundResultLabel.ForeColor = Color.FromArgb(255, 223, 120);
            roundFlavorLabel.Text = "Both moves crash into each other.";
        }
        else if (roundResult > 0)
        {
            playerScore++;
            roundResultLabel.Text = "You win the round!";
            roundResultLabel.ForeColor = Color.FromArgb(102, 224, 168);
            roundFlavorLabel.Text = GetRoundFlavor(playerChoice, computerChoice, true);
        }
        else
        {
            computerScore++;
            roundResultLabel.Text = "Computer wins the round!";
            roundResultLabel.ForeColor = Color.FromArgb(255, 136, 136);
            roundFlavorLabel.Text = GetRoundFlavor(playerChoice, computerChoice, false);
        }

        UpdateScoreLabels();
        UpdateProgressBars();
        UpdateWindowTitle();

        if (playerScore >= 3 || computerScore >= 3)
        {
            bool playerWon = playerScore >= 3;
            roundResultLabel.Text = playerWon ? "You won the match!" : "Computer won the match!";
            roundResultLabel.ForeColor = playerWon ? Color.FromArgb(102, 224, 168) : Color.FromArgb(255, 136, 136);
            roundFlavorLabel.Text = playerWon ? "Victory dance unlocked." : "The CPU claimed the crown.";
            matchStatusLabel.Text = "Press New Match to play again.";
            SetButtonsEnabled(false);
        }
        else
        {
            matchStatusLabel.Text = "First to 3 points wins.";
        }
    }

    private void ResetMatch()
    {
        playerScore = 0;
        computerScore = 0;

        UpdateScoreLabels();
        UpdateProgressBars();
        UpdateWindowTitle();
        roundResultLabel.Text = "Choose a move to start the match.";
        roundResultLabel.ForeColor = Color.White;
        roundFlavorLabel.Text = "Every round can flip the match.";
        playerChoiceLabel.Text = string.Empty;
        computerChoiceLabel.Text = string.Empty;
        matchStatusLabel.Text = "First to 3 points wins.";
        SetButtonsEnabled(true);
    }

    private void UpdateScoreLabels()
    {
        playerScoreValueLabel.Text = playerScore.ToString();
        computerScoreValueLabel.Text = computerScore.ToString();
    }

    private void UpdateProgressBars()
    {
        playerProgressBar.Value = playerScore;
        computerProgressBar.Value = computerScore;
    }

    private void UpdateWindowTitle()
    {
        Text = $"Rock Paper Scissors - You {playerScore} / CPU {computerScore}";
    }

    private void SetButtonsEnabled(bool enabled)
    {
        rockButton.Enabled = enabled;
        paperButton.Enabled = enabled;
        scissorsButton.Enabled = enabled;
    }

    private static string GetChoiceEmoji(string choice)
    {
        return choice switch
        {
            "ROCK" => "🪨",
            "PAPER" => "📄",
            _ => "✂️"
        };
    }

    private static string GetRoundFlavor(string playerChoice, string computerChoice, bool playerWon)
    {
        return (playerChoice, computerChoice, playerWon) switch
        {
            ("ROCK", "SCISSORS", true) => "Rock crushes scissors. Clean hit.",
            ("PAPER", "ROCK", true) => "Paper covers rock. Nice wrap.",
            ("SCISSORS", "PAPER", true) => "Scissors slice through paper. Sharp play.",
            ("ROCK", "PAPER", false) => "Paper covers rock. The CPU got you this time.",
            ("PAPER", "SCISSORS", false) => "Scissors cut paper. The CPU struck fast.",
            ("SCISSORS", "ROCK", false) => "Rock smashes scissors. The CPU was ready.",
            _ => ""
        };
    }

    private string GetComputerChoice()
    {
        return random.Next(3) switch
        {
            0 => "ROCK",
            1 => "PAPER",
            _ => "SCISSORS"
        };
    }

    private static int GetRoundResult(string playerChoice, string computerChoice)
    {
        if (playerChoice == computerChoice)
        {
            return 0;
        }

        return (playerChoice, computerChoice) switch
        {
            ("ROCK", "SCISSORS") => 1,
            ("PAPER", "ROCK") => 1,
            ("SCISSORS", "PAPER") => 1,
            _ => -1
        };
    }
}